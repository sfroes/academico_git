using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Helpers;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Inscricoes.Common;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ProcessoSeletivoDomainService : AcademicoContextDomain<ProcessoSeletivo>
    {
        #region DomainServices

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        private InstituicaoNivelTipoProcessoSeletivoDomainService InstituicaoNivelTipoProcessoSeletivoDomainService => Create<InstituicaoNivelTipoProcessoSeletivoDomainService>();

        private TipoProcessoSeletivoDomainService TipoProcessoSeletivoDomainService => Create<TipoProcessoSeletivoDomainService>();

        private NivelEnsinoDomainService NivelEnsinoDomainService => Create<NivelEnsinoDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService => Create<CursoOfertaLocalidadeTurnoDomainService>();

        private ProcessoSeletivoProcessoMatriculaDomainService ProcessoSeletivoProcessoMatriculaDomainService => Create<ProcessoSeletivoProcessoMatriculaDomainService>();

        private CampanhaOfertaItemDomainService CampanhaOfertaItemDomainService => Create<CampanhaOfertaItemDomainService>();

        #endregion DomainServices

        #region Services

        public IProcessoService ProcessoService => Create<IProcessoService>();

        public IEtapaProcessoService EtapaProcessoService => Create<IEtapaProcessoService>();

        #endregion Services

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos cilos letivos</returns>
        public List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaSelect(long seqCampanha)
        {
            var specCampanha = new ProcessoSeletivoFilterSpecification() { SeqCampanha = seqCampanha };
            specCampanha.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(specCampanha, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        public SMCPagerData<CampanhaCopiaProcessoSeletivoListaVO> BuscarProcessosSeletivos(CampanhaCopiaProcessoSeletivoFiltroVO filtro)
        {
            var spec = filtro.Transform<CampanhaCopiaProcessoSeletivoFilterSpecification>();

            spec.SetOrderBy(x => x.Descricao);

            var lista = SearchProjectionBySpecification(spec, x => new CampanhaCopiaProcessoSeletivoListaVO
            {
                Seq = x.Seq,
                SeqProcessoGpi = x.SeqProcessoGpi,
                Descricao = x.Descricao,
                TipoProcessoSeletivo = x.TipoProcessoSeletivo.Descricao,
            }, out int total);

            return new SMCPagerData<CampanhaCopiaProcessoSeletivoListaVO>(lista, total);
        }

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos processos seletivos</returns>
        public List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect(long seqCampanha)
        {
            var specCampanha = new ProcessoSeletivoFilterSpecification() { SeqCampanha = seqCampanha };
            var specIngressoDireto = new ProcessoSeletivoFilterSpecification() { IngressoDireto = true };
            var specToken = new ProcessoSeletivoFilterSpecification() { TokenTipoProcessoSeletivo = TOKEN_TIPO_PROCESSO_SELETIVO.DISCIPLINA_ISOLADA };
            var specOr = new SMCOrSpecification<ProcessoSeletivo>(specIngressoDireto, specToken);
            var spec = new SMCAndSpecification<ProcessoSeletivo>(specCampanha, specOr);
            spec.SetOrderBy(o => o.Descricao);
            return SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();
        }

        public List<SMCDatasourceItem> BuscarProcessosSeletivosSelect()
        {
            return SearchAll().Select(p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).OrderBy(a => a.Descricao).ToList();
        }

        public ProcessoSeletivoVO BuscarProcessosSeletivo(long seq)
        {
            var spec = new SMCSeqSpecification<ProcessoSeletivo>(seq);
            var data = SearchProjectionByKey(spec, x => new ProcessoSeletivoVO
            {
                Seq = x.Seq,
                SeqCampanha = x.SeqCampanha,
                Descricao = x.Descricao,
                SeqFormaIngresso = x.SeqFormaIngresso,
                SeqsNivelEnsino = x.NiveisEnsino.Select(f => f.SeqNivelEnsino).ToList(),
                SeqProcessoGpi = x.SeqProcessoGpi,
                SeqTipoProcessoSeletivo = x.SeqTipoProcessoSeletivo,
                SeqTipoVinculoAluno = x.SeqTipoVinculoAluno,
                ReservaVaga = x.ReservaVaga,
                ProcessosMatricula = x.ProcessosMatricula.Select(f => new ProcessoSeletivoProcessoMatriculaVO
                {
                    Seq = f.Seq,
                    SeqCicloLetivo = f.Processo.SeqCicloLetivo,
                    SeqProcesso = f.SeqProcesso
                }).ToList()
            });

            if (data.ProcessosMatricula != null)
            {
                foreach (var matricula in data.ProcessosMatricula)
                {
                    if (matricula.SeqCicloLetivo.HasValue)
                        matricula.Processos = ProcessoDomainService.BuscarProcessosMatriculaPorCicloLetivoSelect(matricula.SeqCicloLetivo.Value, data.SeqCampanha);
                }
            }
            return data;
        }

        public List<ProcessoSeletivoIntegracaoGpiVO> BuscarProcessosSeletivosIntegracaoGpi(long[] seqsProcessosSeletivos, long[] seqsCampanhasOfertas)
        {
            var specProcessosSeletivos = new ProcessoSeletivoFilterSpecification() { SeqsProcessosSeletivos = seqsProcessosSeletivos.ToArray() };

            var processosSeletivos = SearchProjectionBySpecification(specProcessosSeletivos, p => new ProcessoSeletivoIntegracaoGpiVO()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,
                SeqTipoProcessoSeletivo = p.SeqTipoProcessoSeletivo,
                SeqTipoVinculoAluno = p.SeqTipoVinculoAluno,
                SeqFormaIngresso = p.SeqFormaIngresso,
                ReservaVaga = p.ReservaVaga,
                SeqProcessoGpi = p.SeqProcessoGpi,
                SeqsNiveisEnsino = p.NiveisEnsino.Select(n => n.SeqNivelEnsino).ToList(),
                Ofertas = p.Ofertas.Where(op => seqsCampanhasOfertas.Contains(op.SeqCampanhaOferta)).Select(op => new ProcessoSeletivoOfertaIntegracaoGpiVO()
                {
                    SeqProcessoSeletivoOferta = op.Seq,
                    SeqCampanhaOferta = op.SeqCampanhaOferta,
                    TipoOferta = new TipoOfertaIntegracaoGpiVO() { Seq = op.CampanhaOferta.TipoOferta.Seq, Token = op.CampanhaOferta.TipoOferta.Token, ExigeCursoOfertaLocalidadeTurno = op.CampanhaOferta.TipoOferta.ExigeCursoOfertaLocalidadeTurno },
                    SeqHierarquiaOfertaGpi = op.SeqHierarquiaOfertaGpi,
                    QuantidadeVagas = op.QuantidadeVagas,
                    Itens = op.CampanhaOferta.Itens.Select(i => new CampanhaOfertaItemIntegracaoGpiVO()
                    {
                        Seq = i.Seq,
                        TokenTipoOferta = i.CampanhaOferta.TipoOferta.Token,
                        Curso = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.Nome,
                        CursoOferta = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                        Localidade = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                        Turno = i.CursoOfertaLocalidadeTurno.Turno.Descricao,
                        AreaTematica = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_TEMATICA).FormacaoEspecifica.Descricao,
                        //AreaConcentracao = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.FormacaoEspecificaSuperior.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao,
                        //LinhaPesquisa = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA).FormacaoEspecifica.Descricao,
                        EixoTematico = i.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.EIXO_TEMATICO).FormacaoEspecifica.Descricao,
                        Orientador = i.Colaborador.DadosPessoais.Nome,
                    }).ToList()
                }).ToList(),
            }).ToList();

            foreach (var campanhaOfertaItem in processosSeletivos.SelectMany(p=>p.Ofertas.SelectMany(o=>o.Itens)))
            {
                switch (campanhaOfertaItem.TokenTipoOferta)
                {
                    case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.Descricao);

                        break;

                    case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao);
                        campanhaOfertaItem.LinhaPesquisa = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.FormacaoEspecifica.Descricao);

                        break;

                    default:

                        campanhaOfertaItem.AreaConcentracao = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.FormacaoEspecificaSuperior.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.AREA_CONCENTRACAO).FormacaoEspecifica.FormacaoEspecificaSuperior.Descricao);
                        campanhaOfertaItem.LinhaPesquisa = CampanhaOfertaItemDomainService.SearchProjectionByKey(campanhaOfertaItem.Seq, c => c.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.FormacoesEspecificas.FirstOrDefault(f => f.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA).FormacaoEspecifica.Descricao);

                        break;
                }
            }

            return processosSeletivos;

        }

        public long SalvarProcessoSeletivo(ProcessoSeletivoVO processoSeletivoVO)
        {
            var processoSeletivo = processoSeletivoVO.Transform<ProcessoSeletivo>();
            // Mapeia os niveis de ensino.
            processoSeletivo.NiveisEnsino = new List<ProcessoSeletivoNivelEnsino>();
            foreach (var nivelEnsino in processoSeletivoVO.SeqsNivelEnsino)
            {
                processoSeletivo.NiveisEnsino.Add(new ProcessoSeletivoNivelEnsino() { SeqNivelEnsino = nivelEnsino });
            }

            ProcessoSeletivo processoSeletivoBanco = null;

            VerificarEtapaInscricaoProcessoGPI(processoSeletivoVO.SeqProcessoGpi);

            // RN_CAM_032
            VerificarDadosInclusao(processoSeletivoVO, processoSeletivo);

            if (processoSeletivoVO.Seq != 0)
            {
                processoSeletivoBanco = SearchByKey(new SMCSeqSpecification<ProcessoSeletivo>(processoSeletivoVO.Seq),
                                                        x => x.TipoProcessoSeletivo, x => x.NiveisEnsino, x => x.ProcessosMatricula, x => x.Ofertas);
                VerificarDadosAlteracao(processoSeletivoVO, processoSeletivo, processoSeletivoBanco);
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                if (processoSeletivo.Seq == 0)
                {
                    if (processoSeletivo.SeqProcessoGpi.HasValue)
                        ProcessoService.IntegracaoProcesso(processoSeletivo.SeqProcessoGpi.Value, true);
                }
                else if (!processoSeletivoBanco.Ofertas.Any())
                {
                    if (!processoSeletivoBanco.SeqProcessoGpi.HasValue && processoSeletivo.SeqProcessoGpi.HasValue)
                    {
                        ProcessoService.IntegracaoProcesso(processoSeletivo.SeqProcessoGpi.Value, true);
                        // RN_CAM_075
                    }
                    else if (processoSeletivoBanco.SeqProcessoGpi.HasValue && !processoSeletivo.SeqProcessoGpi.HasValue)
                    {
                        ProcessoService.IntegracaoProcesso(processoSeletivoBanco.SeqProcessoGpi.Value, false);
                        // RN_CAM_076
                    }
                }

                SaveEntity(processoSeletivo);

                unitOfWork.Commit();
            }

            return processoSeletivo.Seq;
        }

        public void VerificarEtapaInscricaoProcessoGPI(long? seqProcessoGpi)
        {
            //Verifica se possui processo GPI e se este possui estapa de inscrição.
            //Se houver processo do GPI associado ao processo seletivo, verificar se ele possui etapa de inscrição 
            //cadastrada. Se não possuir, abortar a operação e emitir a mensagem de erro:
            //"Associação não permitida. O processo do GPI associado ao processo seletivo desta oferta não possui etapa de inscrição cadastrada."
            if (seqProcessoGpi.HasValue)
            {
                if (!EtapaProcessoService.ExisteEtapa(seqProcessoGpi.Value, TOKENS.ETAPA_INSCRICAO))
                {
                    throw new ProcessoGPISemEtapaInscricaoException();
                }
            }
        }

        private void VerificarDadosInclusao(ProcessoSeletivoVO processoSeletivoVO, ProcessoSeletivo processoSeletivo)
        {
            // Inclusão / Alteração
            // 1. Não permitir associar mais de um processo de matricula por ciclo letivo.
            if (processoSeletivoVO.ProcessosMatricula != null)
            {
                VerificaIntegridadeProcessosMatricula(processoSeletivoVO);
            }

            // 2. Verifica se para o processo de matricula informado, a configuração está por nível de ensino ou por curso.
            foreach (var processoMatricula in processoSeletivoVO.ProcessosMatricula)
            {
                var processoMatriculaBanco = ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(processoMatricula.SeqProcesso.Value),
                                                                    IncludesProcesso.Configuracoes_NiveisEnsino |
                                                                    IncludesProcesso.Configuracoes_Cursos);

                if (TipoProcessoSeletivoDomainService.SearchProjectionByKey(
                            new SMCSeqSpecification<TipoProcessoSeletivo>(processoSeletivoVO.SeqTipoProcessoSeletivo.GetValueOrDefault()), x => x.Token) != TOKEN_TIPO_PROCESSO_SELETIVO.DISCIPLINA_ISOLADA)
                {
                    VerificaProcessoSeletivoPorNivelEnsino(processoSeletivoVO, processoMatricula, processoMatriculaBanco);
                }
            }
        }

        private void VerificarDadosAlteracao(ProcessoSeletivoVO processoSeletivoVO, ProcessoSeletivo processoSeletivo, ProcessoSeletivo processoSeletivoBanco)
        {
            // 5. Não permite trocar reserva de vagas se houver ofertas associadas.
            if (processoSeletivo.ReservaVaga != processoSeletivoBanco.ReservaVaga && processoSeletivoBanco.Ofertas.Any())
            {
                throw new ProcessoSeletivoComOfertasException();
            }

            // 4. Verifica troca do tipo de processo seletivo
            if (processoSeletivo.SeqTipoProcessoSeletivo != processoSeletivoBanco.SeqTipoProcessoSeletivo)
            {
                var seqTiposOfertaNovoTipoProcesso = TipoProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<TipoProcessoSeletivo>(processoSeletivo.SeqTipoProcessoSeletivo),
                                                                            x => x.TiposOferta.Select(f => f.SeqTipoOferta));
                var seqsTiposOfertaAtuais = SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(processoSeletivoVO.Seq),
                                                                    x => x.Ofertas.Select(f => f.CampanhaOferta.SeqTipoOferta)).Distinct();

                if (!seqsTiposOfertaAtuais.All(f => seqTiposOfertaNovoTipoProcesso.Any(g => g == f)))
                {
                    throw new ProcessoSeletivoComTipoOfertasDiferentesException();
                }
            }

            // 2. Remover nível de ensino. Verifica se a lista de itens nova contem todos os itens que existiam no banco.
            VerificaAlteracaoNivelEnsino(processoSeletivoVO, processoSeletivo, processoSeletivoBanco);

            // 3. Remover processo matrícula.
            VerificaAlteracaoDosProcessosMatricula(processoSeletivoVO, processoSeletivo, processoSeletivoBanco);
        }

        private static void VerificaIntegridadeProcessosMatricula(ProcessoSeletivoVO processoSeletivoVO)
        {
            for (int i = 0; i < processoSeletivoVO.ProcessosMatricula.Count; i++)
            {
                for (int x = i + 1; x < processoSeletivoVO.ProcessosMatricula.Count; x++)
                {
                    if (processoSeletivoVO.ProcessosMatricula[i].SeqCicloLetivo == processoSeletivoVO.ProcessosMatricula[x].SeqCicloLetivo)
                    {
                        throw new ProcessoSeletivoCicloLetivoDuplicadoException();
                    }
                }
            }
        }

        private void VerificaProcessoSeletivoPorNivelEnsino(ProcessoSeletivoVO processoSeletivoVO, ProcessoSeletivoProcessoMatriculaVO processoMatricula, Processo processo)
        {
            if (ProcessoDomainService.Count(new ProcessoMatriculaPorNivelEnsinoSpecification(
                                                            processoMatricula.SeqProcesso.Value,
                                                            processoSeletivoVO.SeqsNivelEnsino,
                                                            processoSeletivoVO.SeqTipoVinculoAluno == null ? new List<long>() : new List<long>() { processoSeletivoVO.SeqTipoVinculoAluno.Value })) == 0)
            {
                throw new ProcessoSeletivoNaoConfiguradoNivelEnsinoComVinculoException(processo.Descricao);
            }
        }

        private void VerificaAlteracaoNivelEnsino(ProcessoSeletivoVO processoSeletivoVO, ProcessoSeletivo processoSeletivo, ProcessoSeletivo processoSeletivoBanco)
        {
            if (!processoSeletivo.NiveisEnsino.SMCContainsList(processoSeletivoBanco.NiveisEnsino, m => m.SeqNivelEnsino, out IEnumerable<long> missingNiveis))
            {
                var niveisEnsino = SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(processoSeletivoVO.Seq),
                                                        x => x.Ofertas.SelectMany(f => f.CampanhaOferta.Itens.Select(g =>
                                                                g.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)));
                // Verifica se existe alguma oferta associada ao processo seletivo para os níveis de ensino removidos.
                if (niveisEnsino.Any(f => missingNiveis.Any(g => g == f)))
                {
                    var nivelEnsino = NivelEnsinoDomainService.SearchProjectionByKey(new SMCSeqSpecification<NivelEnsino>(missingNiveis.First()), x => x.Descricao);
                    throw new ProcessoSeletivoComNivelEnsinoAssociadoException(nivelEnsino);
                }
            }
        }

        private void VerificaAlteracaoDosProcessosMatricula(ProcessoSeletivoVO processoSeletivoVO, ProcessoSeletivo processoSeletivo, ProcessoSeletivo processoSeletivoBanco)
        {
            // Verifica por processos removidos.
            processoSeletivo.ProcessosMatricula.SMCContainsList(processoSeletivoBanco.ProcessosMatricula, m => m.SeqProcesso, out IEnumerable<long> missingProcessos);
            HashSet<long> processosModificados = new HashSet<long>(missingProcessos);

            // Verifica por processos modificados.
            foreach (var item in processoSeletivo.ProcessosMatricula)
            {
                if (processoSeletivoBanco.ProcessosMatricula.Any(f => f.Seq == item.Seq && f.SeqProcesso != item.SeqProcesso))
                {
                    processosModificados.Add(item.SeqProcesso);
                }
            }

            // 5. Verifica existencia de ingressantes.
            foreach (var processoModificado in processosModificados)
            {
                var dadosProcesso = ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(processoModificado), x => new
                {
                    x.SeqCicloLetivo,
                    x.Descricao,
                    DescricaoCicloLetivo = x.CicloLetivo.Descricao
                });

                var spec = new IngressanteFilterSpecification() { SeqProcessoSeletivo = processoSeletivoVO.Seq, SeqCicloLetivo = dadosProcesso.SeqCicloLetivo };
                if (IngressanteDomainService.Count(spec) > 0)
                {
                    throw new ProcessoSeletivoComIngressanteException(dadosProcesso.Descricao, dadosProcesso.DescricaoCicloLetivo);
                }
            }

            // 6.
            foreach (var oferta in processoSeletivoBanco.Ofertas)
            {
                var dadosProcessoSeletivo = ProcessoSeletivoOfertaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivoOferta>(oferta.Seq),
                                                        x => new
                                                        {
                                                            x.CampanhaOferta.TipoOferta.ExigeCursoOfertaLocalidadeTurno,
                                                            x.CampanhaOferta.Itens.FirstOrDefault().SeqCursoOfertaLocalidadeTurno,
                                                            NiveisEnsino = x.CampanhaOferta.Itens.Select(f => f.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.SeqNivelEnsino)
                                                        });
                // Verifica se o tipo da oferta exige curso oferta localidade turno.
                if (dadosProcessoSeletivo.ExigeCursoOfertaLocalidadeTurno)
                {
                    foreach (var processoModificado in processosModificados)
                    {
                        // Verifica se o usuário informou um tipo de vinculo na tela.
                        if (processoSeletivoVO.SeqTipoVinculoAluno.HasValue)
                        {
                            if (ProcessoDomainService.Count(new ProcessoMatriculaPorCursoSpecification(
                                                                    processoModificado,
                                                                    dadosProcessoSeletivo.SeqCursoOfertaLocalidadeTurno.Value,
                                                                    new List<long>() { processoSeletivoVO.SeqTipoVinculoAluno.Value })) == 0)
                            {
                                if (ProcessoDomainService.Count(new ProcessoMatriculaPorNivelEnsinoSpecification(
                                                           processoModificado,
                                                           dadosProcessoSeletivo.NiveisEnsino,
                                                           new List<long>() { processoSeletivoVO.SeqTipoVinculoAluno.Value })) == 0)
                                {
                                    var descricaoProcesso = ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(processoModificado), x => x.Descricao);
                                    throw new ProcessoMatriculaNaoConfiguradoCursoNivelEnsinoException(descricaoProcesso);
                                }
                            }
                        }
                        else
                        {
                            // O tipo de vinculo não for informado.
                            var tiposVinculos = InstituicaoNivelTipoProcessoSeletivoDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoProcessoSeletivoFilterSpecification(processoSeletivoVO.SeqTipoProcessoSeletivo.GetValueOrDefault()),
                                                                        x => x.InstituicaoNivelFormaIngresso.FormaIngresso.SeqTipoVinculoAluno);
                            if (ProcessoDomainService.Count(new ProcessoMatriculaPorCursoSpecification(
                                                                processoModificado,
                                                                dadosProcessoSeletivo.SeqCursoOfertaLocalidadeTurno.Value,
                                                                tiposVinculos)) == 0)
                            {
                                if (ProcessoDomainService.Count(new ProcessoMatriculaPorNivelEnsinoSpecification(
                                                           processoModificado,
                                                           dadosProcessoSeletivo.NiveisEnsino,
                                                           tiposVinculos)) == 0)
                                {
                                    var descricaoProcesso = ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(processoModificado), x => x.Descricao);
                                    throw new ProcessoMatriculaNaoConfiguradoCursoNivelSemVinculoEnsinoException(descricaoProcesso);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que lista os processos seletivos e suas respectivas convocações, da campanha
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns></returns>
        public List<ProcessoSeletivoVO> BuscarProcessosSeletivosConvocacao(long seqCampanha)
        {
            var spec = new ProcessoSeletivoFilterSpecification() { SeqCampanha = seqCampanha };

            var data = SearchProjectionBySpecification(spec, x => new ProcessoSeletivoVO
            {
                Seq = x.Seq,
                SeqCampanha = x.SeqCampanha,
                Descricao = x.Descricao,
                Convocacoes = x.Convocacoes.Select(f => new ConvocacaoVO
                {
                    Seq = f.Seq,
                    SeqProcessoSeletivo = f.SeqProcessoSeletivo,
                    Descricao = f.Descricao
                }).ToList()
            }).ToList();

            return data;
        }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoVO> BuscarConvocacoesProcessosSeletivos(long[] seqsProcessosSeletivos)
        {
            var spec = new CampanhaCopiaProcessoSeletivoFilterSpecification() { SeqsProcessosSeletivos = seqsProcessosSeletivos };

            var data = SearchProjectionBySpecification(spec, x => new CampanhaCopiaConvocacaoProcessoSeletivoVO
            {
                SeqProcessoSeletivo = x.Seq,
                Convocacoes = x.Convocacoes.Select(f => new CampanhaCopiaConvocacaoProcessoSeletivoItemVO
                {
                    Checked = true,
                    Seq = f.Seq,
                    SeqProcessoSeletivo = f.SeqProcessoSeletivo,
                    Descricao = f.Descricao
                }).ToList()
            }).ToList();

            return data;
        }

        public List<CampanhaCopiaEtapaProcessoGPIVO> BuscarEtapasProcessosGPI(long[] seqsProcessosSeletivos)
        {
            var result = new List<CampanhaCopiaEtapaProcessoGPIVO>();

            var spec = new CampanhaCopiaProcessoSeletivoFilterSpecification() { SeqsProcessosSeletivos = seqsProcessosSeletivos };

            var dadosProcessos = SearchProjectionBySpecification(spec, x => new
            {
                x.Seq,
                x.SeqProcessoGpi
            }).ToList();

            var templatesProcessos = ProcessoService.BuscarSeqsTemplatesProcessosSGF(dadosProcessos.Select(d => d.SeqProcessoGpi.GetValueOrDefault()).ToArray());

            foreach (var dadoProcesso in dadosProcessos)
            {
                if (dadoProcesso.SeqProcessoGpi.HasValue)
                {
                    result.Add(new CampanhaCopiaEtapaProcessoGPIVO()
                    {
                        SeqProcessoSeletivo = dadoProcesso.Seq,
                        EtapasGPI = SGFHelper.BuscarEtapasSGFCache(templatesProcessos.FirstOrDefault(t => t.Seq == dadoProcesso.SeqProcessoGpi).SeqTemplateProcessoSGF)
                                             .Select(e => new CampanhaCopiaEtapaProcessoGPIItemVO()
                                             {
                                                 Checked = true,
                                                 Seq = e.Seq,
                                                 Descricao = e.Descricao,
                                                 SeqProcessoSeletivo = dadoProcesso.Seq,
                                                 CopiarConfiguracaoEtapa = true,
                                                 Token = e.Token
                                             }).ToList()
                    });
                }
                else
                {
                    result.Add(new CampanhaCopiaEtapaProcessoGPIVO() { SeqProcessoSeletivo = dadoProcesso.Seq, EtapasGPI = new List<CampanhaCopiaEtapaProcessoGPIItemVO>() });
                }
            }

            return result;
        }
    }
}