using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CNC.Validators;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;
using SMC.Framework.Specification;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Framework;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class TitulacaoDomainService : AcademicoContextDomain<Titulacao>
    {

        private ITipoDocumentoService TipoDocumentoService => Create<ITipoDocumentoService>();

        private CursoOfertaDomainService CursoOfertaDomainService => Create<CursoOfertaDomainService>();

        /// <summary>
        /// Busca uma titulação
        /// </summary>
        /// <param name="seq">Sequencia da titulação a ser recuperada</param>
        /// <returns>Dados da titulação</returns>
        public TitulacaoVO BuscarTitulacao(long seq)
        {
            return this.SearchProjectionByKey(seq, x => new TitulacaoVO
            {
                Ativo = x.Ativo,
                DescricaoAbreviada = x.DescricaoAbreviada,
                DescricaoCurso = x.Curso.Nome,
                DescricaoFeminino = x.DescricaoFeminino,
                DescricaoGrauAcademico = x.GrauAcademico.Descricao,
                DescricaoMasculino = x.DescricaoMasculino,
                DescricaoXSD = x.DescricaoXSD,
                DocumentosComprobatorios = x.DocumentosComprobatorios.Select(d => new TitulacaoDocumentoComprobatorioVO
                {
                    Seq = d.Seq,
                    SeqTipoDocumento = d.SeqTipoDocumento
                }).ToList(),
                Seq = x.Seq,
                SeqCurso = x.SeqCurso,
                SeqGrauAcademico = x.SeqGrauAcademico
            });
        }

        /// <summary>
        /// Busca as titulações que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Filtros da listagem de titulação</param>
        /// <returns>Lista de titulações</returns>
        public SMCPagerData<TitulacaoVO> BuscarTitulacoes(TitulacaoFiltroVO filtros)
        {
            /*A ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE PARA ORDENAR
            PELO CAMPO DESCRIÇÃO, QUE É O CAMPO PADRÃO DE ORDENAÇÃO E NÃO ESTÁ NO BANCO*/

            var spec = filtros.Transform<TitulacaoFilterSpecification>();

            //LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            int qtdeRegistros = this.Count();
            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var lista = this.SearchProjectionBySpecification(spec, x => new TitulacaoVO
            {
                Ativo = x.Ativo,
                DescricaoAbreviada = x.DescricaoAbreviada,
                DescricaoCurso = x.Curso.Nome,
                DescricaoFeminino = x.DescricaoFeminino,
                DescricaoGrauAcademico = x.GrauAcademico.Descricao,
                DescricaoMasculino = x.DescricaoMasculino,
                DocumentosComprobatorios = x.DocumentosComprobatorios.Select(d => new TitulacaoDocumentoComprobatorioVO
                {
                    Seq = d.Seq,
                    SeqTipoDocumento = d.SeqTipoDocumento

                }).ToList(),
                Seq = x.Seq,
                SeqCurso = x.SeqCurso,
                SeqGrauAcademico = x.SeqGrauAcademico
            }).ToList();

            int total = lista.Count();

            if (filtros.PageSettings != null)
            {
                //ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
                List<SMCSortInfo> listaOrdenacao = filtros.PageSettings.SortInfo;

                if (!listaOrdenacao.Any())
                {
                    //ORDENAÇÃO DEFAULT PELA DESCRIÇÃO. COLOCANDO A ANOTAÇÃO SMCSORTABLE SEMPRE APARECE NA CAIXA DE ORDENAÇÃO
                    //MESMO QUE O CAMPO SEJA HIDDEN E ALLOW FALSE
                    lista = lista.OrderBy(o => o.Descricao).ToList();
                }
                else
                {
                    foreach (var sort in listaOrdenacao)
                    {
                        if (sort.FieldName == nameof(TitulacaoVO.DescricaoAbreviada))
                        {
                            if (sort.Direction == SMCSortDirection.Ascending)
                                lista = lista.OrderBy(o => o.DescricaoAbreviada).ToList();
                            else
                                lista = lista.OrderByDescending(o => o.DescricaoAbreviada).ToList();
                        }
                        if (sort.FieldName == nameof(TitulacaoVO.DescricaoGrauAcademico))
                        {
                            if (sort.Direction == SMCSortDirection.Ascending)
                                lista = lista.OrderBy(o => o.DescricaoGrauAcademico).ToList();
                            else
                                lista = lista.OrderByDescending(o => o.DescricaoGrauAcademico).ToList();
                        }
                        if (sort.FieldName == nameof(TitulacaoVO.DescricaoCurso))
                        {
                            if (sort.Direction == SMCSortDirection.Ascending)
                                lista = lista.OrderBy(o => o.DescricaoCurso).ToList();
                            else
                                lista = lista.OrderByDescending(o => o.DescricaoCurso).ToList();
                        }
                        if (sort.FieldName == nameof(TitulacaoVO.Ativo))
                        {
                            if (sort.Direction == SMCSortDirection.Ascending)
                                lista = lista.OrderBy(o => o.Ativo).ToList();
                            else
                                lista = lista.OrderByDescending(o => o.Ativo).ToList();
                        }
                    }
                }
            }

            //CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            lista = lista.Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize).Take(filtros.PageSettings.PageSize).ToList();

            return new SMCPagerData<TitulacaoVO>(lista, total);
        }

        /// <summary>
        /// Busca as titulações que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>Lista de titulações para select.
        /// A descrição será apresentada conforme o flag DescricaoAbreviada, sendo por padrão completa.
        /// Caso o campo sexo seja informado, será retornada apenas a descrição do genero informado.</returns>
        public List<SMCDatasourceItem> BuscarTitulacoesSelect(TitulacaoFiltroVO filtros)
        {
            SMCSpecification<Titulacao> spec = filtros.Transform<TitulacaoFilterSpecification>();

            //FIX: Ajuste para a Task 42592:TSK - Corrigir Bugs CAN - UC_CSO_001_01 - Curso
            //(Listagem não é atualizada quando selecionamos o combo de grau academico na Formação Específica)
            if (filtros.SeqCursoFormacaoEspecifica == 0)
                return new List<SMCDatasourceItem>();

            if (filtros.SeqCursoOuGrauAcademicoCurso.GetValueOrDefault())
            {
                var specCurso = new TitulacaoFilterSpecification() { SeqCurso = filtros.SeqCurso };
                var specGrau = new TitulacaoFilterSpecification();
                // NV01 UC_CSO_001_01_06 - Manter Titulação
                // Opção1 - Cadastro simpes.
                // Deverá ser considero o campo grau acadêmico selecinado no cadastro da respectivia formação.
                if (filtros.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Simples)
                {
                    if (filtros.SeqGrauAcademico.HasValue)
                        specGrau.SeqGrauAcademico = filtros.SeqGrauAcademico;
                    else
                        specGrau.SeqCurso = filtros.SeqCurso;
                }
                // Opção 2 - Seleção de Formação Específica
                // Deverá ser considerado os graus associados ao nível de ensino do respectivo curso.
                else if (filtros.CursoTipoFormacao == CursoTipoFormacao.Selecao_Formacao)
                {
                    specGrau.SeqCurso = filtros.SeqCurso;
                    specGrau.GrauAcademicoCurso = true;
                }
                // Opção 3 - Cadastro com Oferta de Curso
                // Deverá ser considerado o campo grau acadêmico no cadastro da oferta de curso selecionada na respectiva formação.
                else if (filtros.CursoTipoFormacao == CursoTipoFormacao.Cadastro_Oferta)
                {
                    specGrau.SeqGrauAcademico = CursoOfertaDomainService.SearchProjectionByKey(filtros.SeqCursoOferta.GetValueOrDefault(), p => p.SeqGrauAcademico);
                }
                else if (filtros.ListaGrauAcademico != null)
                {
                    specGrau.SeqsGrauAcademico = filtros.ListaGrauAcademico;
                }
                var specCursoOuGrau = new SMCOrSpecification<Titulacao>(specCurso, specGrau);
                (spec as TitulacaoFilterSpecification).SeqCurso = null;
                (spec as TitulacaoFilterSpecification).SeqGrauAcademico = null;
                spec = new SMCAndSpecification<Titulacao>(spec, specCursoOuGrau);
            }

            var titulacoes = SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                p.DescricaoFeminino,
                p.DescricaoMasculino,
                p.DescricaoAbreviada
            }).ToList();
            var retorno = new List<SMCDatasourceItem>();
            foreach (var titulacao in titulacoes)
            {
                string descricao;

                if (filtros.DescricaoAbrevida.GetValueOrDefault())
                {
                    descricao = titulacao.DescricaoAbreviada;
                }
                else
                {
                    if (filtros.Sexo == Sexo.Feminino)
                    {
                        descricao = titulacao.DescricaoFeminino;
                    }
                    else if (filtros.Sexo == Sexo.Masculino || titulacao.DescricaoMasculino == titulacao.DescricaoFeminino)
                    {
                        descricao = titulacao.DescricaoMasculino;
                    }
                    else
                    {
                        descricao = $"{titulacao.DescricaoMasculino} / {titulacao.DescricaoFeminino}";
                    }
                }

                retorno.Add(new SMCDatasourceItem(titulacao.Seq, descricao));
            }
            return retorno.OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Grava uma titulação
        /// </summary>
        /// <param name="titulacaoVO">Titulação a ser gravado</param>
        /// <returns>Sequencia da titulação gravada</returns>
        public long SalvarTitulacao(TitulacaoVO titulacaoVO)
        {
            ValidarModelo(titulacaoVO);

            var titulacao = titulacaoVO.Transform<Titulacao>();
            return this.SaveEntity<Titulacao>(titulacao, new TitulacaoValidator());
        }

        private void ValidarModelo(TitulacaoVO titulacaoVO)
        {
            if (titulacaoVO.DocumentosComprobatorios.Any())
            {
                var documentoComprobatorio = titulacaoVO.DocumentosComprobatorios.Where(w => w.SeqTipoDocumento == null).FirstOrDefault();
                if (documentoComprobatorio != null && documentoComprobatorio.SeqTipoDocumento == null)
                    throw new TitulacaoSemDocumentoComprobatorioException();
            }
        }
    }
}