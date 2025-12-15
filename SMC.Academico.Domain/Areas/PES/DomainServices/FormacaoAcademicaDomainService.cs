using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class FormacaoAcademicaDomainService : AcademicoContextDomain<FormacaoAcademica>
    {
        #region Domain Services

        private HierarquiaClassificacaoDomainService HierarquiaClassificacaoDomainService => this.Create<HierarquiaClassificacaoDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();
        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService => this.Create<ColaboradorVinculoDomainService>();

        #endregion Domain Services
        /// <summary>
        /// Busca os dados de cabeçalho de uma pessoa atuação
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public FormacaoAcademicaCabecalhoVO BuscarFormacaoAcademicaCabecalho(long seq)
        {
            var spec = new SMCSeqSpecification<PessoaAtuacao>(seq);

            var cabecalho = PessoaAtuacaoDomainService.SearchProjectionByKey(spec, i => new FormacaoAcademicaCabecalhoVO()
            {
                Nome = i.DadosPessoais.Nome,
                NomeSocial = i.DadosPessoais.NomeSocial,
                Cpf = i.Pessoa.Cpf,
                NumeroPassaporte = i.Pessoa.NumeroPassaporte
            });

            /// Nome segundo a regra RN_PES_023 - Nome e Nome Social - Visão Administrativo          
            cabecalho.Nome = string.IsNullOrEmpty(cabecalho.NomeSocial) ? cabecalho.Nome : $"{cabecalho.NomeSocial} ({cabecalho.Nome})";

            return cabecalho;
        }

        public FormacaoAcademicaVO BuscarFormacaoAcademicaInserted(FormacaoAcademicaVO model)
        {
            model.SeqHierarquiaClassificacao = HierarquiaClassificacaoDomainService.BuscarSeqHierarquiaClassificacao("Área de Conhecimento CNPq");
            model.Sexo = PessoaAtuacaoDomainService.SearchProjectionByKey(model.SeqPessoaAtuacao, p => p.DadosPessoais.Sexo);
            return model;
        }

        public FormacaoAcademicaVO BuscarFormacaoAcademica(long seq)
        {
            var model = this.SearchByKey(seq, x => x.DocumentosApresentados);
            var modelVo = model.Transform<FormacaoAcademicaVO>();
            modelVo.SeqsDocumentosApresentados = model.DocumentosApresentados.SMCAny() ?
                    model.DocumentosApresentados.Select(x => x.SeqTitulacaoDocumentoComprobatorio).ToList() : new List<long>();
            modelVo.SeqHierarquiaClassificacao = HierarquiaClassificacaoDomainService.BuscarSeqHierarquiaClassificacao("Área de Conhecimento CNPq");
            modelVo.Sexo = PessoaAtuacaoDomainService.SearchProjectionByKey(model.SeqPessoaAtuacao, p => p.DadosPessoais.Sexo);
            return modelVo;
        }

        public long SalvarFormacacaoAcademica(FormacaoAcademicaVO model)
        {
            /*A pessoa x atuação poderá ter o mesmo tipo de titulação cadastrada 
             * mais de uma vez  desde que o ano de conclusão do curso sejam diferentes.
             * Desta forma ele poderá ter mais de um mestrado, mais de um doutorado 
             * etc. Caso isto ocorra  o ano de conclusão  destas titulações deverão 
             * ser informados e deverão ser diferentes*/

            //Duas formações com anoObtencaoTitulo idênticos
            var specFormacaoAnoObtencao = new FormacaoAcademicaFilterSpecification() { SeqTitulacao = model.SeqTitulacao, SeqPessoaAtuacao = model.SeqPessoaAtuacao, AnoObtencaoTitulo = model.AnoObtencaoTitulo };

            var titulacoesIdenticas = this.SearchProjectionBySpecification(specFormacaoAnoObtencao,
                    x => new
                    {
                        x.Seq,
                        x.AnoObtencaoTitulo
                    }
                ).Where(f => f.Seq != model.Seq).ToList();

            if (titulacoesIdenticas.SMCCount() > 0 && titulacoesIdenticas.Count(x => x.AnoObtencaoTitulo == model.AnoObtencaoTitulo) > 0)
            {
                throw new FormacaoAcademicaTitulacaoDuplicadaException();
            }

            //TitulacaoMaxima 
            var specTitulacaoMaximaPorPessoa = new FormacaoAcademicaFilterSpecification() { SeqPessoaAtuacao = model.SeqPessoaAtuacao, TitulacaoMaxima = model.TitulacaoMaxima };
            var formacaoContadorTitulacaoMaxima = this.Count(specTitulacaoMaximaPorPessoa);


            if (formacaoContadorTitulacaoMaxima > 0 && model.TitulacaoMaxima.HasValue && model.TitulacaoMaxima.Value == true)
            {
                var entidadeRemoveTitulacaoMaxima = this.SearchBySpecification(specTitulacaoMaximaPorPessoa).FirstOrDefault();
                entidadeRemoveTitulacaoMaxima.TitulacaoMaxima = false;
                SaveEntity(entidadeRemoveTitulacaoMaxima);
            }

            var formacaoAcademica = model.Transform<FormacaoAcademica>();
            formacaoAcademica.DocumentosApresentados = null;
            SaveEntity(formacaoAcademica);

            var documentos = new List<DocumentoApresentadoFormacao>();

            if (model.SeqsDocumentosApresentados != null)
            {
                foreach (var seqTituloDocumentoComprobatorio in model.SeqsDocumentosApresentados)
                {
                    documentos.Add(new DocumentoApresentadoFormacao()
                    {
                        SeqFormacaoAcademica = formacaoAcademica.Seq,
                        SeqTitulacaoDocumentoComprobatorio = seqTituloDocumentoComprobatorio
                    });
                }
            }

            formacaoAcademica.DocumentosApresentados = documentos.ToList();
            SaveEntity(formacaoAcademica);
            return formacaoAcademica.Seq;
        }

        public void ExcluirFormacaoAcademica(FormacaoAcademicaVO model)
        {

            var entidadeExclusao = this.SearchByKey(model.Seq);

            if (entidadeExclusao.TitulacaoMaxima)
            {
                throw new FormacaoAcademicaExcluirTitulacaoMaximaException();
            }

            var specVinculo = new ColaboradorVinculoFilterSpecification() { VinculoAtivo = true, ExigeFormacao = true, SeqColaborador = model.SeqPessoaAtuacao };

            var vinculo = ColaboradorVinculoDomainService.Count(specVinculo);
            var modelPraDeletar = this.SearchByKey(model.Seq);
            if (vinculo > 0)
            {
                var specFormacao = new FormacaoAcademicaFilterSpecification() { SeqPessoaAtuacao = model.SeqPessoaAtuacao };
                var formacao = this.Count(specFormacao);
                if (formacao == 1)
                {
                    throw new FormacaoAcademicaVinculoExigeFormacaoException();
                }
                else if (formacao > 1)
                {
                    DeleteEntity(modelPraDeletar);
                }
            }
            else
            {
                DeleteEntity(modelPraDeletar);
            }
        }

        public bool ValidarTitulacaoMaxima(long seqPessoaAtuacao, bool? titulacaoMaxima, long seq)
        {

            if (titulacaoMaxima.HasValue && titulacaoMaxima.Value == false)
                return false;

            var specValidaQtdTitulacao = new FormacaoAcademicaFilterSpecification() { SeqPessoaAtuacao = seqPessoaAtuacao, TitulacaoMaxima = titulacaoMaxima };
            var contador = this.SearchProjectionBySpecification(specValidaQtdTitulacao, x => x.Seq).ToList();

            return contador.Count(x => x != seq) > 0;
        }


    }
}