using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.ValueObjects;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Formularios.ServiceContract.TMP.Data;
using SMC.Framework.Caching;
using SMC.Framework.Ioc;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Helpers
{
    public static class SGFHelper
    {
        /// <summary>
        /// Busca as etapas de um template de processo no SGF, considerando cache
        /// </summary>
        /// <param name="seqTemplateProcesso"></param>
        /// <returns></returns>
        public static EtapaSimplificadaData[] BuscarEtapasSGFCache(long seqTemplateProcesso)
        {
            // Resolve o serviço
            return SMCIocContainer.Resolve<IEtapaService, EtapaSimplificadaData[]>((etapaService) =>
            {
                var keyCacheTemplate = "__SGA_SGF_ETAPAS_TEMPLATE_" + seqTemplateProcesso;

                // Propositalmente, este método retorna o data do SGF
                // A implementação é apenas para buscar os dados e colocar em cache para não precisar ficar chamando do sgf todas as vezes

                var etapasSGF = SMCCacheManager.Get(keyCacheTemplate) as EtapaSimplificadaData[];
                if (etapasSGF == null)
                {
                    etapasSGF = etapaService.BuscarEtapasDoTemplate(seqTemplateProcesso);
                    SMCCacheManager.Add(keyCacheTemplate, etapasSGF);
                }

                return etapasSGF;
            });
        }

        public static EtapaData BuscarEtapaSGFPorSeqEtapaCache(long seqEtapa)
        {            
            // Resolve o serviço
            return SMCIocContainer.Resolve<IEtapaService, EtapaData>((etapaService) =>
            {
                var keyCacheTemplate = "__SGA_SGF_ETAPA_" + seqEtapa;             

                var etapaSGF = SMCCacheManager.Get(keyCacheTemplate) as EtapaData;
                if (etapaSGF == null)
                {
                    etapaSGF = etapaService.BuscarEtapa(seqEtapa, Formularios.Common.Areas.TMP.Includes.IncludesEtapa.Paginas_Pagina);
                    SMCCacheManager.Add(keyCacheTemplate, etapaSGF);
                }

                return etapaSGF;
            });
        }

        public static List<EtapaListaVO> BuscarEtapas(long seqSolicitacaoServico)
        {
            // Resolve o serviço
            return SMCIocContainer.Resolve<IAplicacaoService, List<EtapaListaVO>>((aplicacaoService) =>
            {
                return SMCIocContainer.Resolve<SolicitacaoServicoDomainService, List<EtapaListaVO>>((solicitacaoServicoDomainService) =>
                {
                    return SMCIocContainer.Resolve<PessoaAtuacaoBloqueioDomainService, List<EtapaListaVO>>((pessoaAtuacaoBloqueioDomainService) =>
                    {
                        var aplicacaoSGAAluno = aplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ALUNO);
                        var possuiEscalonamento = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento != null);

                        // Busca as etapas da solicitação
                        IEnumerable<EtapaProjecaoVO> etapas = null;

                        // Caso possua escalonamento, busca as configurações da etapa do próprio escalonamento. Caso contrário, das configurações de etapa direto
                        if (possuiEscalonamento)
                        {
                            etapas = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento.Itens.Select(y =>
                                new EtapaProjecaoVO
                                {
                                    SeqSolicitacaoServico = x.Seq,
                                    DescricaoEtapa = y.Escalonamento.ProcessoEtapa.DescricaoEtapa,
                                    DataInicio = y.Escalonamento.DataInicio,
                                    DataFim = y.Escalonamento.DataFim,
                                    SeqEtapaSGF = y.Escalonamento.ProcessoEtapa.SeqEtapaSgf,
                                    Ativo = y.Escalonamento.DataInicio <= DateTime.Now && y.Escalonamento.DataFim >= DateTime.Now,
                                    ExibeItemAposTerminoEtapa = y.Escalonamento.ProcessoEtapa.ExibeItemAposTerminoEtapa,
                                    ExibeItemMatriculaSolicitante = y.Escalonamento.ProcessoEtapa.ExibeItemMatriculaSolicitante,
                                    SeqProcessoEtapa = y.Escalonamento.SeqProcessoEtapa,
                                    SeqConfiguracaoProcesso = x.SeqConfiguracaoProcesso,
                                    SeqGrupoEscalonamento = x.SeqGrupoEscalonamento,
                                    SeqEscalonamento = y.SeqEscalonamento,
                                    // ? Não sei se está certo abaixo
                                    Instrucoes = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).OrientacaoEtapa,
                                    // Segundo a Jéssica, o ingressante possui apenas uma solicitação de serviço.
                                    UltimaSituacaoEtapaSGF = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq).SituacaoAtual,
                                    SeqTemplateProcessoSgf = y.Escalonamento.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                                    SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq,
                                    SeqsMotivosBloqueios = y.Parcelas.Select(p => p.SeqMotivoBloqueio).ToList(),
                                })
                            ).ToList();
                        }
                        else
                        {
                            etapas = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(y =>
                                new EtapaProjecaoVO
                                {
                                    SeqSolicitacaoServico = x.Seq,
                                    DescricaoEtapa = y.ProcessoEtapa.DescricaoEtapa,
                                    DataInicio = y.ProcessoEtapa.DataInicio ?? default(DateTime),
                                    DataFim = y.ProcessoEtapa.DataFim,
                                    SeqEtapaSGF = y.ProcessoEtapa.SeqEtapaSgf,
                                    Ativo = (!y.ProcessoEtapa.DataInicio.HasValue || y.ProcessoEtapa.DataInicio <= DateTime.Now) && (!y.ProcessoEtapa.DataFim.HasValue || y.ProcessoEtapa.DataFim >= DateTime.Now),
                                    ExibeItemAposTerminoEtapa = y.ProcessoEtapa.ExibeItemAposTerminoEtapa,
                                    ExibeItemMatriculaSolicitante = y.ProcessoEtapa.ExibeItemMatriculaSolicitante,
                                    SeqProcessoEtapa = y.SeqProcessoEtapa,
                                    SeqConfiguracaoProcesso = x.SeqConfiguracaoProcesso,
                                    SeqGrupoEscalonamento = x.SeqGrupoEscalonamento,
                                    SeqEscalonamento = 0,
                                    // ? Não sei se está certo abaixo
                                    Instrucoes = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).OrientacaoEtapa,
                                    // Segundo a Jéssica, o ingressante possui apenas uma solicitação de serviço.
                                    UltimaSituacaoEtapaSGF = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq).SituacaoAtual,
                                    SeqTemplateProcessoSgf = y.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                                    SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq,
                                    SeqsMotivosBloqueios = y.ConfiguracoesBloqueio.Select(p => p.SeqMotivoBloqueio).ToList(),
                                })
                            ).ToList();
                        }

                        var seqTemplateProcessoSGF = (long?)etapas.FirstOrDefault()?.SeqTemplateProcessoSgf;
                        EtapaSimplificadaData[] etapasSGF = null;
                        if (seqTemplateProcessoSGF.HasValue)
                            etapasSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSGF.Value);

                        List<EtapaListaVO> ret = new List<EtapaListaVO>();
                        foreach (var etapa in etapas)
                        {
                            var etapaSGF = etapasSGF.FirstOrDefault(e => e.Seq == etapa.SeqEtapaSGF);

                            EtapaListaVO vo = new EtapaListaVO
                            {
                                DescricaoEtapa = etapa.DescricaoEtapa,
                                DataInicio = etapa.DataInicio,
                                DataFim = etapa.DataFim,
                                SeqEtapaSGF = etapa.SeqEtapaSGF,
                                Ativo = etapa.Ativo,
                                SeqProcessoEtapa = etapa.SeqProcessoEtapa,
                                SeqConfiguracaoProcesso = etapa.SeqConfiguracaoProcesso,
                                SeqGrupoEscalonamento = etapa.SeqGrupoEscalonamento,
                                SeqEscalonamento = etapa.SeqEscalonamento,
                                Instrucoes = etapa.Instrucoes,
                                SeqSolicitacaoServico = etapa.SeqSolicitacaoServico,
                                // Conforme informado pela Carol, devemos considerar neste ponto apenas o sequencial da aplicação em questão
                                // Pois podem existir páginas sem fluxo associadas diretamente à etapa.
                                PossuiFluxoNaAplicacaoSGAAluno = etapaSGF?.SeqAplicacaoSAS == aplicacaoSGAAluno.Seq,
                                SeqConfiguracaoEtapa = etapa.SeqConfiguracaoEtapa,
                                SeqTemplateProcessoSGF = etapa.SeqTemplateProcessoSgf,
                                ExibirVisualizarPlanoEstudos = etapa.ExibeItemMatriculaSolicitante,
                                Situacoes = etapaSGF?.Situacoes?.Select(x => new EtapaSituacaoVO { SeqSituacao = x.SeqSituacao, Descricao = x.Descricao, ClassificacaoSituacaoFinal = x.ClassificacaoSituacaoFinal, SituacaoFinalEtapa = x.SituacaoFinalEtapa, SituacaoFinalProcesso = x.SituacaoFinalProcesso, SituacaoInicialEtapa = x.SituacaoInicialEtapa, Seq = x.Seq, SituacaoSolicitante = x.SituacaoSolicitante, CategoriaSituacao = x.CategoriaSituacao }).ToList(),
                                Paginas = etapaSGF?.Paginas?.Select(x => new EtapaPaginaVO { Seq = x.Seq, SeqSituacaoEtapaFinal = x.SeqSituacaoEtapaFinal, SeqSituacaoEtapaInicial = x.SeqSituacaoEtapaInicial }).ToList(),
                                OrdemEtapaSGF = etapaSGF?.Ordem ?? 0
                            };

                            if (etapasSGF != null)
                            {
                                var situacaoAtual = etapasSGF.SelectMany(e => e.Situacoes).FirstOrDefault(s => s.Seq == etapa?.UltimaSituacaoEtapaSGF?.SeqSituacaoEtapaSgf);

                                if (situacaoAtual == null || situacaoAtual.SituacaoInicialEtapa)
                                    vo.SituacaoEtapaIngressante = SituacaoEtapaSolicitacaoMatricula.NaoIniciada;
                                else if (!situacaoAtual.SituacaoInicialEtapa && !situacaoAtual.SituacaoFinalEtapa)
                                    vo.SituacaoEtapaIngressante = SituacaoEtapaSolicitacaoMatricula.EmAndamento;
                                else if (situacaoAtual.SituacaoFinalEtapa && situacaoAtual.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                {
                                    vo.SituacaoEtapaIngressante = SituacaoEtapaSolicitacaoMatricula.Finalizada;

                                    if (etapa.SeqsMotivosBloqueios.Count > 0)
                                    {
                                        // Busca os bloqueios para saber se é ou não pendente de pagamento de matrícula
                                        var bloqueios = pessoaAtuacaoBloqueioDomainService.SearchProjectionBySpecification(new PessoaAtuacaoBloqueioFilterSpecification { SeqSolicitacaoServico = seqSolicitacaoServico, SeqMotivoBloqueio = etapa.SeqsMotivosBloqueios, SituacaoBloqueio = SituacaoBloqueio.Bloqueado }, x => new
                                        {
                                            TokenMotivo = x.MotivoBloqueio.Token
                                        }).ToList();

                                        if (bloqueios != null && bloqueios.Any())
                                            vo.SituacaoEtapaIngressante = vo.SituacaoEtapaIngressante | SituacaoEtapaSolicitacaoMatricula.AguardandoPagamento;
                                    }
                                }
                                else if (situacaoAtual.SituacaoFinalEtapa && situacaoAtual.ClassificacaoSituacaoFinal != Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso)
                                    vo.SituacaoEtapaIngressante = SituacaoEtapaSolicitacaoMatricula.NaoFinalizada;
                            }

                            /*  Comando deverá ser exibido somente para a etapa do processo que foi configurada para exibir itens para o · solicitante.
                                Para ESSA etapa do processo, verificar se foi configurada também para "exibir após término da etapa".·
                                - Caso ocorra,  o comando será exibido somente após o término do escalonamento.
                                - Caso NÃO ocorra, exibir o comando apenas após a finalização da etapa, de acordo com a regra: Comando será exibido se a última situação do histórico de situações da solicitação, de acordo com a etapa em questão, estiver com a situação configurada para ser final da etapa e com o valor da "Classificação Situação" igual a "Finalizada com sucesso".
                            */
                            if (vo.ExibirVisualizarPlanoEstudos)
                            {
                                if (etapa.ExibeItemAposTerminoEtapa)
                                    vo.ExibirVisualizarPlanoEstudos = etapa.DataFim < DateTime.Now;
                                else
                                    vo.ExibirVisualizarPlanoEstudos = vo.SituacaoEtapaIngressante.HasFlag(SituacaoEtapaSolicitacaoMatricula.Finalizada);
                            }

                            ret.Add(vo);
                        }
                        return ret?.OrderBy(r => r.OrdemEtapaSGF).ToList();
                    });
                });
            });
        }

        /// <summary>
        /// Busca a etapa de acordo com ingressante, solicitação de serviço e configuração da etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração etapa</param>
        /// <returns>Objeto da etapa atual</returns>
        public static EtapaListaVO BuscarEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa)
        {
            // Resolve o serviço
            return SMCIocContainer.Resolve<SolicitacaoServicoDomainService, EtapaListaVO>((solicitacaoServicoDomainService) =>
            {
                var possuiEscalonamento = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento != null);

                // Busca as etapas da solicitação
                IEnumerable<EtapaListaVO> etapas = null;

                if (possuiEscalonamento)
                {
                    // Busca as etapas da solicitação de matricula
                    etapas = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento.Itens.Select(y =>
                    new EtapaListaVO()
                    {
                        SeqSolicitacaoServico = x.Seq,
                        DescricaoEtapa = y.Escalonamento.ProcessoEtapa.DescricaoEtapa,
                        DataInicio = y.Escalonamento.DataInicio,
                        DataFim = y.Escalonamento.DataFim,
                        SeqEtapaSGF = y.Escalonamento.ProcessoEtapa.SeqEtapaSgf,
                        Ativo = y.Escalonamento.DataInicio <= DateTime.Now && y.Escalonamento.DataFim >= DateTime.Now,
                        SeqProcessoEtapa = y.Escalonamento.SeqProcessoEtapa,
                        SeqConfiguracaoProcesso = x.SeqConfiguracaoProcesso,
                        SeqGrupoEscalonamento = x.SeqGrupoEscalonamento,
                        SeqEscalonamento = y.SeqEscalonamento,
                        SeqTemplateProcessoSGF = y.Escalonamento.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq,
                        Situacoes = y.Escalonamento.ProcessoEtapa.SituacoesItemMatricula.Select(s => new EtapaSituacaoVO { SeqSituacao = s.Seq }).ToList()
                    })
                    );
                }
                else
                {
                    etapas = solicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(y =>
                        new EtapaListaVO
                        {
                            SeqSolicitacaoServico = x.Seq,
                            DescricaoEtapa = y.ProcessoEtapa.DescricaoEtapa,
                            DataInicio = y.ProcessoEtapa.DataInicio ?? default(DateTime),
                            DataFim = y.ProcessoEtapa.DataFim,
                            SeqEtapaSGF = y.ProcessoEtapa.SeqEtapaSgf,
                            Ativo = (!y.ProcessoEtapa.DataInicio.HasValue || y.ProcessoEtapa.DataInicio <= DateTime.Now) && (!y.ProcessoEtapa.DataFim.HasValue || y.ProcessoEtapa.DataFim >= DateTime.Now),
                            SeqProcessoEtapa = y.SeqProcessoEtapa,
                            SeqConfiguracaoProcesso = x.SeqConfiguracaoProcesso,
                            SeqGrupoEscalonamento = x.SeqGrupoEscalonamento,
                            SeqEscalonamento = 0,
                            SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                            SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq,
                            Situacoes = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).ProcessoEtapa.SituacoesItemMatricula.Select(s => new EtapaSituacaoVO { SeqSituacao = s.Seq }).ToList()
                        })
                    );
                }

                var etapaAtual = etapas.Where(w => w.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).FirstOrDefault();
                return etapaAtual;
            });
        }

        public static List<SMCDatasourceItem> BuscarTemplatesSGFPorSeqClasseCache(long seqClasseTemplateProcesso)
        {
            return SMCIocContainer.Resolve<ITemplateProcessoService, List<SMCDatasourceItem>>((templateProcessoService) =>
            {
                var keyCacheTemplate = "__SGA_SGF_TEMPLATE_" + seqClasseTemplateProcesso;

                var templatesSGF = SMCCacheManager.Get(keyCacheTemplate) as List<SMCDatasourceItem>;
                if (templatesSGF == null)
                {
                    templatesSGF = templateProcessoService.BuscarTemplatesProcessos(seqClasseTemplateProcesso);
                    SMCCacheManager.Add(keyCacheTemplate, templatesSGF);
                }

                return templatesSGF;
            });
        }

        public static List<SMCDatasourceItem> BuscarTemplatesSGFPorSeqClasse(long seqClasseTemplateProcesso)
        {
            return SMCIocContainer.Resolve<ITemplateProcessoService, List<SMCDatasourceItem>>((templateProcessoService) =>
            {
                var templatesSGF = templateProcessoService.BuscarTemplatesProcessos(seqClasseTemplateProcesso);
                return templatesSGF;
            });
        }

        public static bool ValidarSituacaoMotivoTemplateCancelarSolicitacoes(long seqTemplateProcesso)
        {
            return SMCIocContainer.Resolve<ITemplateProcessoService, bool>((templateProcessoService) =>
            {
                var retorno = templateProcessoService.ValidarSituacaoMotivoTemplateCancelarSolicitacoes(seqTemplateProcesso);
                return retorno;
            });
        }
    }
}