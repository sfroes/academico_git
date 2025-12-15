using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.Validators;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Repository.Exceptions;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Validation;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class TermoIntercambioDomainService : AcademicoContextDomain<TermoIntercambio>
    {
        #region DomainService

        private ParceriaIntercambioDomainService ParceriaIntercambioDomainService => Create<ParceriaIntercambioDomainService>();

        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private ParceriaIntercambioTipoTermoDomainService ParceriaIntercambioTipoTermoDomainService => Create<ParceriaIntercambioTipoTermoDomainService>();

        private TermoIntercambioVigenciaDomainService TermoIntercambioVigenciaDomainService => Create<TermoIntercambioVigenciaDomainService>();

        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => Create<PessoaAtuacaoTermoIntercambioDomainService>();

        #endregion DomainService

        #region Services

        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        #endregion Services

        public long SalvarTermoIntercambio(TermoIntercambioVO termoIntercambioVO)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var termoIntercambio = SMCMapperHelper.Create<TermoIntercambio>(termoIntercambioVO);

                    foreach (var tiposMobilidade in termoIntercambio.TiposMobilidade)
                    {
                        if (tiposMobilidade.Pessoas != null && tiposMobilidade.Pessoas.Count > 0)
                        {
                            foreach (var pessoa in tiposMobilidade.Pessoas)
                                pessoa.Cpf = pessoa.Cpf.SMCRemoveNonDigits();
                        }
                    }

                    //se o tipo de termo está parametrizado por instituição-nível-vínculo para conceder formação
                    //Refletir alteração no período de intercâmbio de todas as pessoas-atuação que o termo está associado. Na "Data de admissão" e "Data término prevista" de todas as pessoas-atuação que o termo está associado. "Data de admissão" = data início do termo e "Data término prevista = data fim do termo.
                    if (termoIntercambioVO.Seq > 0 && termoIntercambioVO.ConcedeFormacao)
                    {
                        var termo = this.SearchByKey(new SMCSeqSpecification<TermoIntercambio>(termoIntercambioVO.Seq), IncludesTermoIntercambio.PessoasAtuacao);
                        if (termo.PessoasAtuacao?.Count > 0)
                        {
                            foreach (var pessoaAtuacao in termo.PessoasAtuacao)
                            {
                                if ((pessoaAtuacao.PessoaAtuacao as Ingressante) != null)
                                {
                                    var ingressante = IngressanteDomainService.SearchByKey(new SMCSeqSpecification<Ingressante>(pessoaAtuacao.Seq));
                                    ingressante.DataAdmissao = termo.Vigencias.OrderByDescending(w => w.DataInclusao).FirstOrDefault().DataInicio;
                                    ingressante.DataPrevisaoConclusao = termo.Vigencias?.OrderByDescending(w => w.DataInclusao).FirstOrDefault().DataFim;
                                    IngressanteDomainService.UpdateEntity(ingressante);
                                }
                            }
                        }
                    }

                    if (termoIntercambio.Seq > default(long))
                    {
                        var old = SearchByKey(new SMCSeqSpecification<TermoIntercambio>(termoIntercambio.Seq), a => a.Vigencias, a => a.TiposMobilidade);
                        Validar(termoIntercambio, old);
                    }
                    else
                    {
                        Validar(termoIntercambio);
                    }

                    ///Verifica se exige vigência caso 'não', verifica se tem vigência caso tenha as exclui as vigencias
                    var seqParceriaIntercambio = this.ParceriaIntercambioTipoTermoDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambioTipoTermo>(termoIntercambio.SeqParceriaIntercambioTipoTermo)).SeqParceriaIntercambio;
                    var seqInstituicaoEnsino = this.ParceriaIntercambioDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(seqParceriaIntercambio)).SeqInstituicaoEnsino;
                    if (!this.InstituicaoNivelTipoTermoIntercambioDomainService.ExigirVigenciaTermoIntercambio(termoIntercambio.SeqParceriaIntercambioTipoTermo, seqInstituicaoEnsino, termoIntercambio.SeqNivelEnsino))
                    {
                        if (termoIntercambio.Vigencias.SMCAny())
                        {
                            foreach (var item in termoIntercambio.Vigencias)
                            {
                                var vigencia = this.TermoIntercambioVigenciaDomainService.SearchByKey(new SMCSeqSpecification<TermoIntercambioVigencia>(item.Seq));
                                this.TermoIntercambioVigenciaDomainService.DeleteEntity(vigencia);
                            }

                            termoIntercambio.Vigencias = null;
                        }
                    }

                    // Se o arquivo do termo de intercâmbio não foi alterado, atualiza com conteúdo com o que está no banco
                    if (termoIntercambio.Arquivos != null)
                    {
                        foreach (var arquivo in termoIntercambio.Arquivos)
                        {
                            this.EnsureFileIntegrity(arquivo, x => x.SeqArquivoAnexado, x => x.ArquivoAnexado);
                        }
                    }

                    SaveEntity(termoIntercambio);
                    unitOfWork.Commit();
                    return termoIntercambio.Seq;
                }
                catch (Exception e)
                {
                    unitOfWork.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Ao excluir um tipo de termo associado a uma parceria, verificar se este tipo de termo está em uso em algum termo.
        /// Se sim, não permitir a exclusão e exibir a mensagem de erro:
        /// "Exclusão não permitida. Já existem termos de intercâmbio cadastrado para esta parceria de intercâmbio x instituição externa."
        /// </summary>
        /// <param name="seq">Sequencial do termo de intercambio.</param>
        public void ExcluirTermoIntercambio(long seq)
        {
            try
            {
                if (TermoIntercambioPossuiPessoaAtuacao(seq))
                {
                    throw new TermoIntercambioPossuiPessoaAtuacaoException();
                }

                DeleteEntity(SearchByKey(new SMCSeqSpecification<TermoIntercambio>(seq)));
            }
            catch (SMCForeignKeyViolationException fkex)
            {
                if (fkex.ConstraintName.Equals("FK_pessoa_atuacao_termo_intercambio_02"))
                {
                    throw new Exception("Exclusão não permitida. Já existem termos de intercâmbio cadastrado para esta parceria de intercâmbio x instituição externa.");
                }
                else
                {
                    throw fkex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TermoIntercambioVO PreencherModeloTermoIntercambio(long seq)
        {
            var termo = SearchByKey(new SMCSeqSpecification<TermoIntercambio>(seq),
                               IncludesTermoIntercambio.Arquivos
                               | IncludesTermoIntercambio.Arquivos_ArquivoAnexado
                               | IncludesTermoIntercambio.Vigencias
                               | IncludesTermoIntercambio.ParceriaIntercambioInstituicaoExterna_ParceriaIntercambio
                               | IncludesTermoIntercambio.TiposMobilidade
                               | IncludesTermoIntercambio.TiposMobilidade_Pessoas
                               | IncludesTermoIntercambio.PessoasAtuacao
                               | IncludesTermoIntercambio.ParceriaIntercambioTipoTermo);

            var result = termo.Transform<TermoIntercambioVO>();

            if (termo.Arquivos.Count() > 0)
            {
                result.Arquivos.ForEach(f => f.ArquivoAnexado.GuidFile = termo.Arquivos
                                                                              .Where(w => w.SeqArquivoAnexado == f.SeqArquivoAnexado)
                                                                              .Select(s => s.ArquivoAnexado.UidArquivo).First().ToString());
            }

            if (termo.PessoasAtuacao?.Count > 0)
            {
                result.PossuiPessoaAtuacao = true;
                // Verificar se o tipo de termo está parametrizado por instituição-nível - vínculo para não conceder formação.
                var spec = new InstituicaoNivelTipoTermoIntercambioFilterSpecification() { SeqTipoTermoIntercambio = termo.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio };
                var instituicoesNivelTipoTermoIntercambio = InstituicaoNivelTipoTermoIntercambioDomainService.SearchBySpecification(spec);
                var instituicaoNivelTipoTermoIntercambio = instituicoesNivelTipoTermoIntercambio?.OrderByDescending(w => w.DataInclusao).FirstOrDefault();
                if (instituicaoNivelTipoTermoIntercambio?.ConcedeFormacao == true)
                    result.ConcedeFormacao = true;
            }

            result.NameDescriptionParceriraIntercabioTipoTermo = this.ParceriaIntercambioTipoTermoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ParceriaIntercambioTipoTermo>(result.SeqParceriaIntercambioTipoTermo), p => p.TipoTermoIntercambio.Descricao);

            // Informa se deve mostrar as vigências na tela.
            result.ExibeVigencias = InstituicaoNivelTipoTermoIntercambioDomainService.ExigirVigenciaTermoIntercambio(termo.SeqParceriaIntercambioTipoTermo, termo.ParceriaIntercambioInstituicaoExterna.ParceriaIntercambio.SeqInstituicaoEnsino, result.SeqNivelEnsino);

            return result;
        }

        public SMCPagerData<TermoIntercambioListarVO> ListarTermoIntercambio(TermoIntercambioFiltroVO filtro)
        {
            var lista = new List<TermoIntercambioListarVO>();

            var spec = SMCMapperHelper.Create<TermoIntercambioFilterSpecification>(filtro);
            spec.SetOrderBy(a => a.Descricao);

            if (filtro.SeqNivelEnsino.HasValue && filtro.SeqTipoVinculoAluno.HasValue)
            {
                var specVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqNivelEnsino = filtro.SeqNivelEnsino,
                    SeqTipoVinculoAluno = filtro.SeqTipoVinculoAluno
                };
                spec.SeqsTiposTermoIntercambio = InstituicaoNivelTipoVinculoAlunoDomainService
                    .SearchProjectionByKey(specVinculo, p => p.TiposTermoIntercambio.Select(s => s.SeqTipoTermoIntercambio))
                    ?.ToArray();
            }

            var termos = SearchBySpecification(spec, out int total,
                         IncludesTermoIntercambio.Arquivos
                       | IncludesTermoIntercambio.Vigencias
                       | IncludesTermoIntercambio.Arquivos_ArquivoAnexado
                       | IncludesTermoIntercambio.ParceriaIntercambioTipoTermo_TipoTermoIntercambio
                       | IncludesTermoIntercambio.NivelEnsino
                       | IncludesTermoIntercambio.ParceriaIntercambioInstituicaoExterna_InstituicaoExterna
                       | IncludesTermoIntercambio.TiposMobilidade_Pessoas
                       | IncludesTermoIntercambio.PessoasAtuacao_Orientacao_OrientacoesColaborador_Colaborador_DadosPessoais).ToList();

            foreach (var item in termos)
            {
                var model = SMCMapperHelper.Create<TermoIntercambioListarVO>(item);
                //PreencheVigencia(item, model);

                ///Exibir o ÚLITMO período de vigência cadastrado para a parceria.
                if (item.Vigencias.SMCAny())
                {
                    model.DataInicio = item.Vigencias.OrderByDescending(o => o.Seq).Select(s => s.DataInicio).FirstOrDefault();
                    model.DataFim = item.Vigencias.OrderByDescending(o => o.Seq).Select(s => s.DataFim).FirstOrDefault();
                }

                model.NivelEnsino = item.NivelEnsino.Descricao;
                model.TipoTermoIntercambio = item.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao;
                model.InstituicaoEnsinoExterna = item.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome;
                model.CodigoPaisInstituicaoEnsinoExterna = item.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.CodigoPais;
                model.SeqInstituicaoEnsinoExterna = item.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna;
                model.Orientadores = new List<string>();
                model.TiposParticipacaoOrientacao = new List<string>();
                foreach (var pessoaAtuacaoTermoIntercambio in item.PessoasAtuacao.Where(p => p.SeqPessoaAtuacao == filtro.SeqPessoaAtuacao.GetValueOrDefault()))
                {
                    if (pessoaAtuacaoTermoIntercambio.Orientacao != null)
                    {
                        foreach (var colaboradorOrientacao in pessoaAtuacaoTermoIntercambio.Orientacao.OrientacoesColaborador)
                        {
                            model.Orientadores.Add($"{colaboradorOrientacao.TipoParticipacaoOrientacao.SMCGetDescription()} - {(colaboradorOrientacao.Colaborador.DadosPessoais.Sexo == DadosMestres.Common.Areas.PES.Enums.Sexo.Feminino ? "PROFa." : "PROF.")} {colaboradorOrientacao.Colaborador.DadosPessoais.Nome}");
                            model.TiposParticipacaoOrientacao.Add(colaboradorOrientacao.TipoParticipacaoOrientacao.SMCGetDescription());
                        }
                    }
                }
                if (filtro.Ativo.HasValue)
                {
                    if (filtro.Ativo.Value &&
                        ((model.DataInicio <= DateTime.Now.Date && model.DataFim >= DateTime.Now.Date)
                        || (model.DataInicio == null && model.DataFim == null)))
                    {
                        lista.Add(model);
                    }
                    else if (!filtro.Ativo.Value && (model.DataInicio > DateTime.Now.Date || model.DataFim < DateTime.Now.Date))
                    {
                        lista.Add(model);
                    }
                    continue;
                }
                lista.Add(model);
            }

            return new SMCPagerData<TermoIntercambioListarVO>(lista, total);
        }

        /// <summary>
        /// Recupera as descrições do Tipo de Termo de Intercâmbio e da Instituição Externa de um termo de intercâmbio
        /// </summary>
        /// <param name="seqTermoIntercambio">Sequencial do termo de intercâmbio</param>
        /// <returns>Descrições</returns>
        public DadosSimplificadoTermoIntercambioVO BuscarDadosTermoIntercambio(long seqTermoIntercambio)
        {
            var dados = this.SearchProjectionByKey(seqTermoIntercambio, x => new DadosSimplificadoTermoIntercambioVO
            {
                DescricaoTipoTermo = x.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                DescricaoInstituicaoExterna = x.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                DataInicioTipoTermo = (DateTime?)x.Vigencias.OrderByDescending(m => m.Seq).FirstOrDefault().DataInicio,
                DataFimTipoTermo = (DateTime?)x.Vigencias.OrderByDescending(m => m.Seq).FirstOrDefault().DataFim,
                SeqTipoTermoIntercambio = x.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio
            });

            return dados;
        }

        public TermoIntercambioCabecalhoVO BuscarCabecalhoTermoIntercambio(long seqParceriaIntercambio)
        {
            var result = ParceriaIntercambioDomainService.SearchByKey(new SMCSeqSpecification<ParceriaIntercambio>(seqParceriaIntercambio),
                                 IncludesParceriaIntercambio.TiposTermo
                                | IncludesParceriaIntercambio.Vigencias
                                | IncludesParceriaIntercambio.InstituicoesExternas
                                | IncludesParceriaIntercambio.InstituicoesExternas_InstituicaoExterna
                                | IncludesParceriaIntercambio.InstituicaoEnsino);

            var vo = SMCMapperHelper.Create<TermoIntercambioCabecalhoVO>(result);
            vo.Parceria = result.Descricao;

            var vigenciaAtual = result.Vigencias.OrderByDescending(o => o.Seq).FirstOrDefault();

            vo.DataInicio = vigenciaAtual.DataInicio;
            vo.DataFim = vigenciaAtual.DataFim;

            vo.Ativo = vigenciaAtual != null && (vigenciaAtual.DataInicio <= DateTime.Today && (!vigenciaAtual.DataFim.HasValue || vigenciaAtual.DataFim.Value >= DateTime.Today)) ? true : false;

            return vo;
        }

        private static void PreencheVigencia(TermoIntercambio item, TermoIntercambioListarVO model)
        {
            double menor = 0;
            foreach (var vigencia in item.Vigencias)
            {
                if (vigencia.DataInicio <= DateTime.Now && vigencia.DataFim >= DateTime.Now)
                {
                    model.DataInicio = vigencia.DataInicio;
                    model.DataFim = vigencia.DataFim;
                    break;
                }
                else
                {
                    double diferencaDias = 0;
                    var totalDiasAproximados = (vigencia.DataInicio - DateTime.Today).TotalDays;
                    diferencaDias = (totalDiasAproximados < 0) ? totalDiasAproximados * (-1) : totalDiasAproximados;
                    if (menor == 0 || diferencaDias < menor)
                    {
                        menor = diferencaDias;
                        model.DataInicio = vigencia.DataInicio;
                        model.DataFim = vigencia.DataFim;
                        continue;
                    }
                }
            }
        }

        public bool TermoIntercambioPossuiPessoaAtuacao(long seqTermoIntercambio)
        {
            var termo = this.SearchByKey(new SMCSeqSpecification<TermoIntercambio>(seqTermoIntercambio), IncludesTermoIntercambio.PessoasAtuacao);

            return (termo.PessoasAtuacao?.Count > 0) ? true : false;
        }

        public SMCPagerData<TermoIntercambioListarVO> BuscarTermosIntercambiosLookup(TermoIntercambioLookupFiltroVO filtro)
        {
            var lista = new List<TermoIntercambioListarVO>();

            var spec = SMCMapperHelper.Create<TermoIntercambioFilterSpecification>(filtro);

            var termos = this.SearchBySpecification(spec, out int total,
                     IncludesTermoIntercambio.Arquivos
                    | IncludesTermoIntercambio.Vigencias
                    | IncludesTermoIntercambio.Arquivos_ArquivoAnexado
                    | IncludesTermoIntercambio.ParceriaIntercambioTipoTermo
                    | IncludesTermoIntercambio.ParceriaIntercambioTipoTermo_TipoTermoIntercambio
                    | IncludesTermoIntercambio.ParceriaIntercambioInstituicaoExterna
                    | IncludesTermoIntercambio.ParceriaIntercambioInstituicaoExterna_InstituicaoExterna
                    | IncludesTermoIntercambio.TiposMobilidade);

            foreach (var item in termos)
            {
                var model = SMCMapperHelper.Create<TermoIntercambioListarVO>(item);
                PreencheVigencia(item, model);
                model.TipoTermoIntercambio = item.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao;
                model.InstituicaoEnsinoExterna = item.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome;
                lista.Add(model);
            }

            return new SMCPagerData<TermoIntercambioListarVO>(lista, total);
        }

        private void Validar(TermoIntercambio obj, TermoIntercambio old = null)
        {
            //Vai receber o valor da vigência atual, que será usado na regra para validar os termos associados.
            TermoIntercambioVigencia vigencia = (obj.Vigencias != null) ? obj.Vigencias.LastOrDefault() : new TermoIntercambioVigencia();

            #region [ Validando o período de vigência ]

            //NV02 - Só é possível alterar o registro de vigência mais atual, exibir os demais registros desabilitados e sem possibilidade de exclusão.
            //Apenas o registro atual pode ser alterado e excluído.
            if (old != null && obj.Vigencias != null)
            {
                long seqVigente = 0;

                //Verificando se foi adicionada mais de uma vigência
                if (obj.Vigencias.Count(a => a.Seq == default(long)) > 1)
                {
                    throw new UmPeriodoVigenciaException();
                }

                if (old.Vigencias.SMCAny())
                {
                    var ultimaVigencia = old.Vigencias.OrderByDescending(a => a.DataInclusao).FirstOrDefault();
                    seqVigente = ultimaVigencia.Seq;
                }

                //Verificando se a vigência ativa foi apagada
                if (!obj.Vigencias.Any(a => a.Seq == seqVigente))
                {
                    //Verificando se alguma vigência antiga foi apagada
                    if (old.Vigencias.Count(a => a.Seq != seqVigente) > obj.Vigencias.Count(a => a.Seq > default(long)))
                    {
                        throw new AlterarVigenciaAntigaException();
                    }

                    //var vigenciaAtual = obj.Vigencias.Where(a => a.Seq == default(long)).FirstOrDefault();
                    //vigencia = vigenciaAtual ?? throw new SemVigenciaComDataValidaException();

                    //if (vigenciaAtual.DataFim < DateTime.Now)
                    //{
                    //    throw new VigenciaDataFimInvalidaException();
                    //}

                    //Certificando que não haverá alteração de vigências antigas...
                    foreach (var item in obj.Vigencias)
                    {
                        if (item.Seq > default(long))
                        {
                            var vigenciaAntiga = old.Vigencias.Where(a => a.Seq == item.Seq).FirstOrDefault();
                            if ((item.DataFim != vigenciaAntiga.DataFim) || (item.DataInicio != vigenciaAntiga.DataInicio))
                            {
                                string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                                throw new Exception($"{s} não permitida. Apenas o registro atual pode ser alterado ou excluído.");
                            }
                        }
                    }
                }
                else
                {
                    //Verificando se alguma vigência antiga foi apagada
                    if (old.Vigencias.Count(a => a.Seq != seqVigente) > obj.Vigencias.Count(a => a.Seq > default(long) && a.Seq != seqVigente))
                    {
                        throw new AlterarVigenciaAntigaException();
                    }

                    //Verificando se tem vigência nova além da atual já cadastrada.
                    //TermoIntercambioVigencia vigenciaAtual;
                    //if (obj.Vigencias.Count(a => a.Seq == default(long)) == 1)
                    //{
                    //    vigenciaAtual = obj.Vigencias.Where(a => a.Seq == default(long)).FirstOrDefault();
                    //    if (vigenciaAtual.DataFim < DateTime.Now)
                    //    {
                    //        throw new VigenciaDataFimInvalidaException();
                    //    }

                    //    var vigenciaAnterior = obj.Vigencias.Where(a => a.Seq == seqVigente).FirstOrDefault();
                    //    if (vigenciaAnterior.DataFim > DateTime.Now)
                    //    {
                    //        throw new DataFimVigenciaAnteriorInvalidaException();
                    //    }
                    //}
                    //else
                    //{
                    //    vigenciaAtual = obj.Vigencias.Where(a => a.Seq == seqVigente).FirstOrDefault();
                    //    if (vigenciaAtual.DataFim < DateTime.Now)
                    //    {
                    //        throw new VigenciaDataFimInvalidaException();
                    //    }
                    //}
                    //vigencia = vigenciaAtual;

                    //Certificando que não haverá alteração de vigências antigas...
                    foreach (var item in obj.Vigencias.Where(a => a.Seq != seqVigente))
                    {
                        if (item.Seq > default(long))
                        {
                            var vigenciaAntiga = old.Vigencias.Where(a => a.Seq == item.Seq).FirstOrDefault();
                            if ((item.DataFim != vigenciaAntiga.DataFim) || (item.DataInicio != vigenciaAntiga.DataInicio))
                            {
                                string s = obj.Seq > 0 ? "Alteração" : "Inclusão";
                                throw new Exception($"{s} não permitida. Apenas o registro atual pode ser alterado ou excluído.");
                            }
                        }
                    }
                }

                //Verificando se tem APENAS UMA vigência com data de término superior à data atual
                //if (obj.Vigencias.Count(a => a.DataFim > DateTime.Now) != 1)
                //{
                //    throw new SemVigenciaComDataValidaException();
                //}
            }

            if (obj.Vigencias.SMCAny())
            {
                if (obj.Seq == default(long))
                {
                    ///NV14 - Na interface de inclusão não é possível inserir mais de um período de vigência.Caso ocorra, abortar a operação e
                    ///exibir a seguinte mensagem de erro:
                    ///"Inclusão não permitida. Não é possível informar mais de um periodo de vigência para o termo de intercâmbio".
                    if (obj.Vigencias.Count > 1)
                    {
                        throw new TermoIntercambioInclusaoMaisPeriodoVigenciaException();
                    }
                }
                else
                {
                    ///NV13 - Verificar se existe mais de um período de vigência exatamente igual a outro.Caso isto ocorra, abortar operação e
                    ///exibir a seguinte mensagem:
                    ///"Alteração não permitida. Não é possível salvar um termo com períodos de vigência iguais."
                    if (obj.Vigencias.Count() > 1)
                    {
                        var ultimaVigencia = obj.Vigencias.LastOrDefault();
                        var penultimaVigencia = obj.Vigencias[obj.Vigencias.Count() - 2];

                        if (ultimaVigencia.DataInicio == penultimaVigencia.DataInicio && ultimaVigencia.DataFim == penultimaVigencia.DataFim)
                        {
                            throw new TermoIntercambioPeriodoVigenciaExatamenteIguaisException();
                        }
                    }
                }
            }

            //NV07 - O período informado deverá estar entre o período de vigência da parceria.
            //Caso isto não ocorra, abortar a operação e enviar a seguinte mensagem:
            //"<Inclusão/ Alteração> não permitida. O período do termo deve estar dentro do período de vigência da parceria de intercâmbio".
            ParceriaIntercambioFilterSpecification spec = new ParceriaIntercambioFilterSpecification();
            spec.SeqParceriaIntercambioInstituicaoExterna = obj.SeqParceriaIntercambioInstituicaoExterna;
            var parceria = ParceriaIntercambioDomainService.SearchBySpecification(spec, a => a.Vigencias);
            if (parceria != null && parceria.FirstOrDefault().Vigencias != null)
            {
                var vigenciaParceria = parceria.FirstOrDefault().Vigencias.LastOrDefault(); //Supostamente é o período vigente.

                if (obj.Vigencias != null)
                {
                    foreach (var vigenciaTI in obj.Vigencias)
                    {
                        if (vigenciaParceria.DataInicio > vigencia.DataInicio || (vigenciaParceria.DataFim.HasValue && vigenciaParceria.DataFim < vigencia.DataFim))
                        {
                            throw new VigenciaInvalidaException(obj.Seq == default(long) ? Common.Areas.ALN.Resources.ExceptionsResource.Inclusao : Common.Areas.ALN.Resources.ExceptionsResource.Alteracao);
                        }
                    }
                }
            }

            #endregion [ Validando o período de vigência ]

            #region [ Consistência do(s) arquivo(s) ]

            //RN_PES_011 Consistência arquivos
            //Ao carregar um arquivo, as seguintes verificações devem ser feitas:
            //  1.Não deverá ser permitido carregar arquivos maiores que 25 mega.
            //  Em caso de violação, abortar a operação e exibir a mensagem a seguir:
            //  "Tamanho máximo de arquivo é 25 MB".
            //
            //  2.Permitir carregar somente arquivos com extensão doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps.
            //  Em caso de violação, abortar a operação e exibir a mensagem a seguir:
            //  "O arquivo <descrição documento> não é permitido. Favor enviar o arquivo em uma das seguintes extensões: doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps"
            if (obj.Arquivos != null && obj.Arquivos.Count() > 0)
            {
                foreach (var arquivo in obj.Arquivos)
                {
                    string extensao = Path.GetExtension(arquivo.ArquivoAnexado.Nome);
                    if (arquivo.ArquivoAnexado != null && arquivo.ArquivoAnexado.Tamanho > VALIDACAO_ARQUIVO_ANEXADO.TAMANHO_MAXIMO_ARQUIVO_ANEXADO)
                    {
                        throw new Exception("Tamanho máximo de arquivo é 25 MB");
                    }

                    if (string.IsNullOrEmpty(extensao) || !VALIDACAO_ARQUIVO_ANEXADO.EXTENSOES_PERMITIDAS_PARCERIA_TERMO_INTERCAMBIO.Contains(extensao))
                    {
                        throw new Exception(string.Format("O arquivo {0} não é permitido. Favor enviar o arquivo em uma das seguintes extensões: doc, docx, xls, xlsx, jpg, jpeg, png, pdf, rar, zip, ps", arquivo.ArquivoAnexado.Nome));
                    }
                }
            }

            #endregion [ Consistência do(s) arquivo(s) ]

            if (obj.Seq > default(long))
            {
                ///NV17 - Na alteração, verificar se a quantidade de vagas informada é maior ou igual a quantidade de pessoas-atuação
                ///ATIVAS associadas ao termo de intercâmbio em questão. Caso não seja, abortar a operação e exibir a seguinte
                ///mensagem de erro:
                ///"Alteração não permitida. A quantidade de vagas informada tem que ser maior ou igual a quantidade de pessoas
                ///associadas ao termo de intercâmbio."
                var pessoaAtuacaoTermoIntercambio = this.PessoaAtuacaoTermoIntercambioDomainService.SearchBySpecification(new PessoaAtuacaoTermoIntercambioFilterSpecification() { Ativo = true, SeqTermoIntercambio = obj.Seq }).ToList();

                var quantidadesPorTipoMobilidade = pessoaAtuacaoTermoIntercambio.GroupBy(g => g.TipoMobilidade).ToList();

                foreach (var tipoMobilidade in obj.TiposMobilidade)
                {
                    var quantidadePessoas = quantidadesPorTipoMobilidade.SelectMany(sm => sm).Where(w => w.TipoMobilidade == tipoMobilidade.TipoMobilidade).ToList();

                    if (quantidadePessoas.Count() > tipoMobilidade.QuantidadeVagas)
                    {
                        throw new TermoIntercambioPeriodoNumeroVagasMenorPessoaAtivaException();
                    }
                }

                // Validação da exclusão de mobilidades segundo a task 30754 para não excluir mobilidades com pessoas associadas
                var tiposMobilidadeComPessoas = quantidadesPorTipoMobilidade.Select(s => s.Key).ToList();
                var tiposMobilidadeRemovidosComPessoas = old
                    .TiposMobilidade?.Select(s => s.TipoMobilidade) // tipos de mobilidade antes da modificação
                    .Except(obj.TiposMobilidade?.Select(s => s.TipoMobilidade)) // filtra os tipos removidos
                    .Where(w => tiposMobilidadeComPessoas.Contains(w)) // filtra apenas os tipos com pessoas
                    .OrderBy(o => o)
                    .ToList();

                if (tiposMobilidadeRemovidosComPessoas.SMCAny())
                {
                    var tiposMobilidade = string.Join(" e ",
                        tiposMobilidadeRemovidosComPessoas.Select(s => s.SMCGetDescription()));
                    throw new TermoIntercambioExclusaoTipoMobilidadeException(tiposMobilidade);
                }
            }

            var validator = new TermoIntercambioValidator();
            var results = validator.Validate(obj);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }
        }

        public bool ExisteTermoIntercambioPorTipoTermoIntercambio(long seqTipoTermoIntercambio, long seqParceriaIntercambio)
        {
            //return SearchProjectionBySpecification(new TermoIntercambioFilterSpecification() { SeqTipoTermoIntercambio = seqTipoTermoIntercambio, SeqParceriaIntercambio = seqParceriaIntercambio }, a => a.Seq) != null ? true : false;
            return SearchProjectionBySpecification(new TermoIntercambioFilterSpecification() { SeqTipoTermoIntercambio = seqTipoTermoIntercambio, SeqParceriaIntercambio = seqParceriaIntercambio }, a => a.Seq).SMCAny();
        }

        /// <summary>
        /// Busca as informações de termo de intercâmbio para o Historico Escolar
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação do relatório</param>
        /// <returns>Lista de informações de mobilidade para o histórico escolar</returns>
        public List<TipoMobilidadeVO> BuscarTipoMobilidadeHistoricoEscolar(long seqPessoaAtuacao)
        {
            // Filtra os termos de intercâmbio da pessoa-atuação
            var spec = new PessoaAtuacaoTermoIntercambioFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao
            };
            var termos = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(spec, x => new
            {
                Seq = x.Seq,
                seqPessoaAtuacao = x.SeqPessoaAtuacao,
                DescricaoTipoTermoIntercambio = x.TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                Periodo = x.Periodos.Select(p => new
                {
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim
                }),
                InstituicaoExterna = x.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna,
                OrientacoesColaborador = x.Orientacao.OrientacoesColaborador.Select(o => new
                {
                    NomeOrientador = o.Colaborador.DadosPessoais.Nome,
                    TipoParticipacao = o.TipoParticipacaoOrientacao
                })
            });

            // Monta o retorno
            var lista = new List<TipoMobilidadeVO>();
            foreach (var termo in termos)
            {
                TipoMobilidadeVO tipoMobilidade = new TipoMobilidadeVO()
                {   
                    Seq = termo.seqPessoaAtuacao,
                    Titulo = termo.DescricaoTipoTermoIntercambio,
                    Instituicao = termo.InstituicaoExterna.Nome,
                    PaisInstituicao = LocalidadeService.BuscarPais(termo.InstituicaoExterna.CodigoPais)?.Nome ?? string.Empty,
                    OrientadorInstituicao = string.Empty
                };

                if (termo.Periodo != null && termo.Periodo.Count() > 0)
                {
                    foreach (var periodo in termo.Periodo.OrderBy(x => x.DataInicio))
                    {
                        tipoMobilidade.DatasInicioPeriodos += string.Join(", ", periodo.DataInicio.ToShortDateString() + ", ");
                        tipoMobilidade.DatasFimPeriodos += string.Join(", ", periodo.DataFim.ToShortDateString() + ", ");
                    }
                }
               

                tipoMobilidade.OrientadorInstituicao = string.Join(", ", termo.OrientacoesColaborador.Select(o => $"Prof(a). {o.NomeOrientador} ({o.TipoParticipacao.SMCGetDescription()})").ToList());

                //foreach (var orientador in termo.OrientacoesColaborador)
                //{
                //    tipoMobilidade.OrientadorInstituicao += string.Format("Prof(a). {0} ({1})", orientador.NomeOrientador, orientador.TipoParticipacao.SMCGetDescription());
                //}
                lista.Add(tipoMobilidade);
            }
            return lista;
        }
    }
}