using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.FIN.Includes;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ServicoDomainService : AcademicoContextDomain<Servico>
    {
        #region DomainServices

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private ContratoDomainService ContratoDomainService => Create<ContratoDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService => Create<InstituicaoNivelServicoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private TipoServicoDomainService TipoServicoDomainService => Create<TipoServicoDomainService>();

        private TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService => Create<TipoHierarquiaEntidadeDomainService>();

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService => Create<HierarquiaEntidadeDomainService>();

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService => Create<HierarquiaEntidadeItemDomainService>();

        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        #endregion DomainServices

        #region Serviços

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return Create<IIntegracaoFinanceiroService>(); }
        }

        #endregion Serviços

        public ServicoVO BuscarServico(long seqServico)
        {
            var spec = new SMCSeqSpecification<Servico>(seqServico);

            var servico = this.SearchByKey(spec,
                                           IncludesServico.Justificativas |
                                           IncludesServico.SituacoesAluno_SituacaoMatricula |
                                           IncludesServico.SituacoesIngressante |
                                           IncludesServico.InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno_TipoVinculoAluno |
                                           IncludesServico.InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno_InstituicaoNivel_NivelEnsino |
                                           IncludesServico.RestricoesSolicitacaoSimultanea_ServicoRestricao |
                                           IncludesServico.MotivosBloqueioParcela_MotivoBloqueio |
                                           IncludesServico.TiposNotificacao |
                                           IncludesServico.Taxas |
                                           IncludesServico.ParametrosEmissaoTaxa |
                                           IncludesServico.TiposDocumento);

            var retorno = servico.Transform<ServicoVO>();

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS NÃO CHEGAREM COMO LISTAS VAZIAS E SEREM SALVAS COMO OBJETOS VAZIOS
            retorno.Justificativas = retorno.Justificativas.Any() ? retorno.Justificativas : null;
            retorno.RestricoesSolicitacaoSimultanea = retorno.RestricoesSolicitacaoSimultanea.Any() ? retorno.RestricoesSolicitacaoSimultanea : null;
            retorno.MotivosBloqueioParcela = retorno.MotivosBloqueioParcela.Any() ? retorno.MotivosBloqueioParcela : null;
            retorno.TiposNotificacao = retorno.TiposNotificacao.Any() ? retorno.TiposNotificacao : null;
            retorno.Taxas = retorno.Taxas.Any() ? retorno.Taxas : null;
            retorno.ParametrosEmissaoTaxa = retorno.ParametrosEmissaoTaxa.Any() ? retorno.ParametrosEmissaoTaxa : null;
            retorno.TiposDocumento = retorno.TiposDocumento.Any() ? retorno.TiposDocumento : null;

            

            switch (servico.TipoAtuacao)
            {
                case TipoAtuacao.Aluno:

                    if (servico.SituacoesAluno.Any(a => a.PermissaoServico == PermissaoServico.ReabrirSolicitacao))
                    {
                        retorno.SituacoesReabrir = servico.SituacoesAluno.Where(a => a.PermissaoServico == PermissaoServico.ReabrirSolicitacao).Select(a => new ServicoSituacaoReabrirVO()
                        {
                            Seq = a.Seq,
                            SeqServico = a.SeqServico,
                            SeqSituacao = a.SeqSituacaoMatricula,
                            PermissaoServico = a.PermissaoServico

                        }).ToList();
                    }
                    else
                    {
                        retorno.SituacoesReabrir = null;
                    }

                    retorno.SituacoesSolicitar = servico.SituacoesAluno.Where(a => a.PermissaoServico == PermissaoServico.CriarSolicitacao).Select(a => new ServicoSituacaoSolicitarVO()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacao = a.SeqSituacaoMatricula,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    retorno.SituacoesAtender = servico.SituacoesAluno.Where(a => a.PermissaoServico == PermissaoServico.AtenderSolicitacao).Select(a => new ServicoSituacaoAtenderVO()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacao = a.SeqSituacaoMatricula,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    break;

                case TipoAtuacao.Ingressante:

                    if (servico.SituacoesIngressante.Any(a => a.PermissaoServico == PermissaoServico.ReabrirSolicitacao))
                    {
                        retorno.SituacoesReabrir = servico.SituacoesIngressante.Where(a => a.PermissaoServico == PermissaoServico.ReabrirSolicitacao).Select(a => new ServicoSituacaoReabrirVO()
                        {
                            Seq = a.Seq,
                            SeqServico = a.SeqServico,
                            SeqSituacao = (long)a.SituacaoIngressante,
                            PermissaoServico = a.PermissaoServico

                        }).ToList();
                    }
                    else
                    {
                        retorno.SituacoesReabrir = null;
                    }

                    retorno.SituacoesSolicitar = servico.SituacoesIngressante.Where(a => a.PermissaoServico == PermissaoServico.CriarSolicitacao).Select(a => new ServicoSituacaoSolicitarVO()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacao = (long)a.SituacaoIngressante,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    retorno.SituacoesAtender = servico.SituacoesIngressante.Where(a => a.PermissaoServico == PermissaoServico.AtenderSolicitacao).Select(a => new ServicoSituacaoAtenderVO()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacao = (long)a.SituacaoIngressante,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    break;
            }

            return retorno;
        }

        public SMCPagerData<ServicoListarVO> BuscarServicos(ServicoFiltroVO filtro)
        {
            var spec = filtro.Transform<ServicoFilterSpecification>();

            var lista = this.SearchProjectionBySpecification(spec, a => new ServicoListarVO()
            {
                Seq = a.Seq,
                SeqTipoServico = a.SeqTipoServico,
                DescTipoServico = a.TipoServico.Descricao,
                TipoAtuacao = a.TipoAtuacao,
                Descricao = a.Descricao,
                EsconderBotaoConsultarTaxas = !a.Taxas.Any()

            }, out int total).ToList();

            return new SMCPagerData<ServicoListarVO>(lista, total);
        }

        public List<SMCDatasourceItem> BuscarTiposTransacao()
        {
            return IntegracaoFinanceiroService.BuscarTiposTransacao();
        }

        public List<SMCDatasourceItem> BuscarTaxasAcademicas()
        {
            return IntegracaoFinanceiroService.BuscarTaxasAcademicas();
        }

        public List<SMCDatasourceItem> BuscarBancosAgencias()
        {
            return IntegracaoFinanceiroService.BuscarBancosAgencias();
        }

        public List<ValoresTaxaData> ConsultarValoresTaxas(List<int> seqsTaxas)
        {
            return IntegracaoFinanceiroService.ConsultarValoresTaxas(seqsTaxas);
        }

        public List<ConsultarTaxasPorNucleoListarVO> ConsultarTaxasPorNucleo(long seqServico)
        {
            Dictionary<int, string> origensParaValidar = new Dictionary<int, string>
            {
                { 1, "PUC Minas" },
                { 28, "Acadêmico Strictu Sensu" }
            };

            var servico = this.SearchByKey(new SMCSeqSpecification<Servico>(seqServico), IncludesServico.Taxas);
            var seqsTaxas = servico.Taxas.Select(a => a.SeqTaxaGra).ToList();

            var listaCompletaTaxas = IntegracaoFinanceiroService.ConsultarValoresTaxas(seqsTaxas).Where(a => origensParaValidar.Select(b => b.Key).Contains(a.SeqOrigem)).ToList();
            var listaSeqTaxas = listaCompletaTaxas.OrderBy(o => o.DescricaoTaxa).Select(a => a.SeqTaxa).Distinct().ToList();
            var listaRetorno = new List<ConsultarTaxasPorNucleoListarVO>();

            foreach (var seqTaxa in listaSeqTaxas)
            {
                var listaTaxasOrigem = listaCompletaTaxas.Where(a => a.SeqTaxa == seqTaxa).OrderBy(a => a.SeqOrigem).Select(b => new ConsultarTaxasPorNucleoListarVO()
                {
                    SeqTaxaGra = b.SeqTaxa,
                    DescricaoTaxa = b.DescricaoTaxa,
                    SeqOrigem = b.SeqOrigem,
                    DescricaoOrigem = origensParaValidar.FirstOrDefault(a => a.Key == b.SeqOrigem).Value

                }).SMCDistinct(a => a.SeqOrigem).ToList();

                foreach (var taxaOrigem in listaTaxasOrigem)
                {
                    var listaConsultaValores = listaCompletaTaxas.Where(a => a.SeqTaxa == taxaOrigem.SeqTaxaGra && a.SeqOrigem == taxaOrigem.SeqOrigem).OrderBy(o => o.NomeNucleo).ToList();
                    var listaValoresNucleo = new List<ConsultarTaxasPorNucleoListarItemVO>();

                    foreach (var itemNucleo in listaConsultaValores)
                    {
                        var valorNucleo = new ConsultarTaxasPorNucleoListarItemVO()
                        {
                            CodigoNucleo = itemNucleo.CodigoNucleo,
                            NomeNucleo = itemNucleo.NomeNucleo,
                            ValorTaxa = itemNucleo.ValorTaxa,
                            DataInicioValidade = itemNucleo.DataInicioValidade,
                            DataFimValidade = itemNucleo.DataFimValidade
                        };

                        listaValoresNucleo.Add(valorNucleo);
                    }

                    taxaOrigem.TaxasPorNucleo = listaValoresNucleo;
                }

                listaRetorno.AddRange(listaTaxasOrigem);
            }

            return listaRetorno;
        }

        public List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelEnsinoTipoServicoSelect(long seqTipoServico)
        {
            //Busca todos os serviços respeitando o filtro global
            var specInstituicaoNivelServico = new InstituicaoNivelServicoFilterSpecification()
            {
                SeqTipoServico = seqTipoServico
            };

            var seqsServicos = this.InstituicaoNivelServicoDomainService
                                   .SearchProjectionBySpecification(specInstituicaoNivelServico, x => x.Servico.Seq)
                                   .ToArray();

            var spec = new SMCContainsSpecification<Servico, long>(t => t.Seq, seqsServicos);

            spec.SetOrderBy(s => s.Descricao);

            var result = this.SearchProjectionBySpecification(spec, t => new SMCDatasourceItem()
            {
                Seq = t.Seq,
                Descricao = t.Descricao
            });

            return result.ToList();
        }

        public List<SMCDatasourceItem> BuscarServicosSelect(ServicoFiltroVO filtros)
        {
            var spec = filtros.Transform<ServicoFilterSpecification>();

            var result = this.SearchBySpecification(spec);

            return result.OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarServicosGeraSolicitacaoTipoDocumentoSelect(long seqInstituicaoEnsino)
        {
            var spec = new ServicoFilterSpecification()
            {
                TipoAtuacao = TipoAtuacao.Aluno,
                SeqInstituicaoEnsino = seqInstituicaoEnsino
            };

            var result = this.SearchBySpecification(spec).ToList();

            return result.OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarServicosPorInstituicaoNivelDoContratoSelect(long seqContrato)
        {
            var includesContrato = IncludesContrato.Cursos | IncludesContrato.NiveisEnsino | IncludesContrato.Cursos_Curso | IncludesContrato.NiveisEnsino_NivelEnsino;
            var contrato = ContratoDomainService.SearchByKey(new SMCSeqSpecification<Contrato>(seqContrato), includesContrato);
            long seqInstituicaoNivel;
            InstituicaoNivelTipoVinculoAlunoFilterSpecification spec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification();
            var includesVinculoAluno = IncludesInstituicaoNivelTipoVinculoAluno.Servicos | IncludesInstituicaoNivelTipoVinculoAluno.Servicos_Servico;
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (contrato.NiveisEnsino.Count > 0)
            {
                foreach (var itemNiveisEnsino in contrato.NiveisEnsino)
                {
                    seqInstituicaoNivel = InstituicaoNivelDomainService.BuscarSequencialInstituicaoNivelEnsino(itemNiveisEnsino.SeqNivelEnsino, contrato.SeqInstituicaoEnsino);
                    spec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };
                    var vinculos = InstituicaoNivelTipoVinculoAlunoDomainService.SearchBySpecification(spec, includesVinculoAluno);

                    foreach (var itemVinculos in vinculos)
                    {
                        foreach (var itemServicos in itemVinculos.Servicos)
                        {
                            if (retorno.Count == 0)
                            {
                                retorno.Add(new SMCDatasourceItem(itemServicos.Servico.Seq, itemServicos.Servico.Descricao));
                            }
                            else
                            {
                                if (!retorno.Any(x => x.Seq == itemServicos.Servico.Seq))
                                {
                                    retorno.Add(new SMCDatasourceItem(itemServicos.Servico.Seq, itemServicos.Servico.Descricao));
                                }
                            }
                        }
                    }
                }
            }

            if (contrato.Cursos.Count > 0)
            {
                foreach (var itemCursos in contrato.Cursos)
                {
                    seqInstituicaoNivel = InstituicaoNivelDomainService.BuscarSequencialInstituicaoNivelEnsino(itemCursos.Curso.SeqNivelEnsino, contrato.SeqInstituicaoEnsino);
                    spec = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqInstituicaoNivel = seqInstituicaoNivel };
                    var vinculos = InstituicaoNivelTipoVinculoAlunoDomainService.SearchBySpecification(spec, includesVinculoAluno);

                    foreach (var itemVinculos in vinculos)
                    {
                        foreach (var itemServicos in itemVinculos.Servicos)
                        {
                            if (retorno.Count == 0)
                            {
                                retorno.Add(new SMCDatasourceItem(itemServicos.Servico.Seq, itemServicos.Servico.Descricao));
                            }
                            else
                            {
                                if (!retorno.Any(x => x.Seq == itemServicos.Servico.Seq))
                                {
                                    retorno.Add(new SMCDatasourceItem(itemServicos.Servico.Seq, itemServicos.Servico.Descricao));
                                }
                            }
                        }
                    }
                }
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarEtapasDoServicoSelect(long seqServico)
        {
            var spec = new SMCSeqSpecification<Servico>(seqServico);

            var seqTemplateProcesso = this.SearchProjectionByKey(spec, s => s.SeqTemplateProcessoSgf);

            var etapas = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcesso).Select(e => new SMCDatasourceItem()
            {
                Seq = e.Seq,
                Descricao = e.Descricao
            }).ToList();

            return etapas;
        }

        public List<SMCDatasourceItem> BuscarServicosPorAlunoSelect(ServicoPorAlunoFiltroVO filtro)
        {
            var situacaoMatricula = new SituacaoAlunoVO();

            if (filtro.ConsiderarSituacaoAluno)
            {
                // Busca a situação atual do aluno
                situacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(filtro.SeqAluno);
            }

            // Recupera qual a instituição nível tipo vínculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqAluno);

            // Recupera a entidade responsável do aluno
            var seqEntidadeResponsavelAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(filtro.SeqAluno), x => x.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo);

            // Filtra os serviços
            var spec = new ServicoFilterSpecification
            {
                OrigemSolicitacaoServico = filtro.OrigemSolicitacaoServico,
                TipoAtuacao = filtro.TipoAtuacao,
                SeqSituacaoAluno = situacaoMatricula.SeqSituacao,
                PermissaoServico = filtro.PermissaoServico,
                SeqTipoServico = filtro.SeqTipoServico,
                SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoVinculoAluno.Seq,
                Com1EtapaAtiva = filtro.Com1EtapaAtiva,
                SeqEntidadeResponsavelProcesso = seqEntidadeResponsavelAluno
            };
            spec.SetOrderBy(x => x.Descricao);
            return SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
            }).ToList();
        }

        /// <summary>
        /// Busca os serviços conforme o Ciclo Letivo
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Lista de serviços</returns>
        public List<SMCDatasourceItem> BuscarServicosPorCicloLetivoSelect(long seqCicloLetivo)
        {
            var spec = new ServicoFilterSpecification() { SeqCicloLetivo = seqCicloLetivo };

            var result = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Descricao

            }, isDistinct: true).OrderBy(o => o.Descricao).ToList();

            return result;
        }

        public List<SMCDatasourceItem> BuscarTemplatesSGFPorTipoServicoSelect(long seqTipoServico)
        {
            TipoServico tipoServico = TipoServicoDomainService.SearchByKey(new SMCSeqSpecification<TipoServico>(seqTipoServico));
            List<SMCDatasourceItem> templatesSGF = SGFHelper.BuscarTemplatesSGFPorSeqClasse(tipoServico.SeqClasseTemplateProcessoSgf);

            return templatesSGF;
        }

        public List<SMCDatasourceItem> BuscarTiposEmissaoTaxa(OrigemSolicitacaoServico origemSolicitacaoServico)
        {
            var retorno = new List<SMCDatasourceItem>();

            if (origemSolicitacaoServico == OrigemSolicitacaoServico.Presencial)
                retorno.Add(new SMCDatasourceItem() { Seq = (long)TipoEmissaoTaxa.Presencial, Descricao = SMCEnumHelper.GetDescription(TipoEmissaoTaxa.Presencial) });
            else
            {
                foreach (var item in Enum.GetValues(typeof(TipoEmissaoTaxa)).Cast<TipoEmissaoTaxa>())
                {
                    if (item != TipoEmissaoTaxa.Presencial && (long)item != 0)
                    {
                        retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                    }
                }
            }

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarTiposEmissaoCobrancaTaxa()
        {
            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in Enum.GetValues(typeof(TipoEmissaoTaxa)).Cast<TipoEmissaoTaxa>())
            {
                if (item == TipoEmissaoTaxa.EmissaoBoleto)
                {
                    retorno.Add(new SMCDatasourceItem() { Seq = (long)item, Descricao = SMCEnumHelper.GetDescription(item) });
                }
            }

            return retorno;
        }

        public long SalvarServico(ServicoVO modelo)
        {
            modelo.InstituicaoNivelServicos.ForEach(a => a.SeqInstituicaoNivelTipoVinculoAluno = this.InstituicaoNivelTipoVinculoAlunoDomainService.BuscarConfiguracaoVinculo(a.SeqNivelEnsino, a.SeqTipoVinculoAluno).Seq);            

            ValidarModelo(modelo);

            var dominio = modelo.Transform<Servico>();

            //VALIDAÇÃO PARA AS SEÇÕES NÃO OBRIGATÓRIAS QUE NÃO ESTIVEREM PREENCHIDAS FICAREM COMO LISTAS VAZIAS PARA OS RELACIONAMENTOS (FILHOS) SEREM REMOVIDOS
            dominio.Justificativas = dominio.Justificativas != null ? dominio.Justificativas : new List<JustificativaSolicitacaoServico>();
            dominio.RestricoesSolicitacaoSimultanea = dominio.RestricoesSolicitacaoSimultanea != null ? dominio.RestricoesSolicitacaoSimultanea : new List<RestricaoSolicitacaoSimultanea>();
            dominio.MotivosBloqueioParcela = dominio.MotivosBloqueioParcela != null ? dominio.MotivosBloqueioParcela : new List<ServicoMotivoBloqueioParcela>();
            dominio.TiposNotificacao = dominio.TiposNotificacao != null ? dominio.TiposNotificacao : new List<ServicoTipoNotificacao>();
            dominio.Taxas = dominio.Taxas != null ? dominio.Taxas : new List<ServicoTaxa>();
            dominio.ParametrosEmissaoTaxa = dominio.ParametrosEmissaoTaxa != null ? dominio.ParametrosEmissaoTaxa : new List<ServicoParametroEmissaoTaxa>();
            dominio.TiposDocumento = dominio.TiposDocumento != null ? dominio.TiposDocumento : new List<ServicoTipoDocumento>();

            switch (modelo.TipoAtuacao)
            {
                case TipoAtuacao.Aluno:

                    List<ServicoSituacaoAluno> listaSituacoesAlunoReabrir = new List<ServicoSituacaoAluno>();

                    if (modelo.SituacoesReabrir != null)
                    {
                        listaSituacoesAlunoReabrir = modelo.SituacoesReabrir.Select(a => new ServicoSituacaoAluno()
                        {
                            Seq = a.Seq,
                            SeqServico = a.SeqServico,
                            SeqSituacaoMatricula = a.SeqSituacao,
                            PermissaoServico = a.PermissaoServico

                        }).ToList();
                    }

                    List<ServicoSituacaoAluno> listaSituacoesAlunoSolicitar = modelo.SituacoesSolicitar.Select(a => new ServicoSituacaoAluno()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacaoMatricula = a.SeqSituacao,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    List<ServicoSituacaoAluno> listaSituacoesAlunoAtender = modelo.SituacoesAtender.Select(a => new ServicoSituacaoAluno()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SeqSituacaoMatricula = a.SeqSituacao,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    dominio.SituacoesAluno = listaSituacoesAlunoReabrir.Concat(listaSituacoesAlunoSolicitar).Concat(listaSituacoesAlunoAtender).ToList();

                    break;

                case TipoAtuacao.Ingressante:

                    List<ServicoSituacaoIngressante> listaSituacoesIngressanteReabrir = new List<ServicoSituacaoIngressante>();

                    if (modelo.SituacoesReabrir != null)
                    {
                        listaSituacoesIngressanteReabrir = modelo.SituacoesReabrir.Select(a => new ServicoSituacaoIngressante()
                        {
                            Seq = a.Seq,
                            SeqServico = a.SeqServico,
                            SituacaoIngressante = (SituacaoIngressante)a.SeqSituacao,
                            PermissaoServico = a.PermissaoServico

                        }).ToList();
                    }

                    List<ServicoSituacaoIngressante> listaSituacoesIngressanteSolicitar = modelo.SituacoesSolicitar.Select(a => new ServicoSituacaoIngressante()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SituacaoIngressante = (SituacaoIngressante)a.SeqSituacao,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    List<ServicoSituacaoIngressante> listaSituacoesIngressanteAtender = modelo.SituacoesAtender.Select(a => new ServicoSituacaoIngressante()
                    {
                        Seq = a.Seq,
                        SeqServico = a.SeqServico,
                        SituacaoIngressante = (SituacaoIngressante)a.SeqSituacao,
                        PermissaoServico = a.PermissaoServico

                    }).ToList();

                    dominio.SituacoesIngressante = listaSituacoesIngressanteReabrir.Concat(listaSituacoesIngressanteSolicitar).Concat(listaSituacoesIngressanteAtender).ToList();

                    break;
            }

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void ValidarModelo(ServicoVO modelo)
        {
            if (modelo.Taxas.Any() && !modelo.ParametrosEmissaoTaxa.Any())
                throw new ServicoSemParametroEmissaoTaxaException();
        }

        public void ValidarCampoLiberarTrabalhoAcademico(ServicoVO modelo)
        {
            /*Se o parâmetro "Ao liberar o Trabalho Acadêmico para a biblioteca" estiver configurado para "Cancelar solicitações 
            não finalizadas": consistir se existe o motivo no SGF "Aluno Formado" (token ALUNO_FORMADO) para a situação 
            "Solicitação cancelada" associada ao template SGF do serviço. 
            Em caso negativo exibir a mensagem de erro abaixo e abortar operação:*/
            if (modelo.AcaoLiberacaoTrabalho.HasValue && modelo.AcaoLiberacaoTrabalho == AcaoLiberacaoTrabalho.CancelarSolicitacoesNaoFinalizadas)
            {
                var templateValidado = SGFHelper.ValidarSituacaoMotivoTemplateCancelarSolicitacoes(modelo.SeqTemplateProcessoSgf);

                if (!templateValidado)
                    throw new ServicoSemMotivoSGFConfiguradoException();
            }
        }

        public (bool ExibirAssert, string MensagemAssertTaxasNaoParametrizadas) ValidarAssertProximo(ServicoVO modelo)
        {
            /*Ao clicar em "Próximo", avaliar se para cada taxa associada ao serviço, consta devidamente no sistema financeiro a
            parametrização de todas as [taxas por núcleo]*, conforme [correspondência no sistema acadêmico]*.

            Em caso negativo, exibir a seguinte mensagem informativa:
            "Atenção. Foi identificado que não há parametrização de valor por núcleo no sistema financeiro para as seguintes
            entidades x taxas:
            · [Descrição da taxa]
                · [Código Núcleo SEO] - [Nome da entidade]
            Confirma a associação?"

            Se usuário confirmar, prosseguir com o cadastro.
            Senão, retornar para a página em questão.

            Em caso positivo, prosseguir com o cadastro.

            [Taxas por núcleo]* = no sistema financeiro o valor da taxa acadêmica é parametrizado por núcleo e, a identificação
            do núcleo é pelo Código SEO.

            [Correspondência no sistema acadêmico]* = O núcleo utilizado na parametrização das taxas no sistema financeiro,
            corresponde no sistema acadêmico, as entidades que:
            · São entidades responsáveis (pai) pelas entidades do tipo igual a localidade, da hierarquia de localidade vigente e
            da instituição logada e,
            · Cuja entidade do tipo Localidade, possui ofertas de curso por localidade, que o nível de ensino do curso seja igual
            aos níveis de ensino parametrizados para o serviço em questão e,
            · O Código SEO dessas entidades responsáveis (pai) é o mesmo Código SEO (código núcleo) parametrizado no
            sistema financeiro.*/

            var seqsNiveisEnsinoServico = modelo.InstituicaoNivelServicos.Select(a => a.SeqNivelEnsino).Distinct().ToList();

            //Buscando a HE do tipo Localidade (vigente)
            var hierarquiaEntidadeLocalidade = this.HierarquiaEntidadeDomainService.BuscarHierarquiaVigente(TipoVisao.VisaoLocalidades);

            //Buscando todas as entidades dessa HE do tipo de entidade igual a curso-oferta-localidade 
            HierarquiaEntidadeItemFilterSpecification specHierarquia = new HierarquiaEntidadeItemFilterSpecification() { SeqHierarquiaEntidade = hierarquiaEntidadeLocalidade.Seq, TokenTipoEntidade = TOKEN_TIPO_ENTIDADE_EXTERNADA.CURSO_OFERTA_LOCALIDADE };
            specHierarquia.SetOrderBy(o => o.Entidade.Nome);

            var includesHierarquiaEntidadeItem = IncludesHierarquiaEntidadeItem.Entidade_TipoEntidade                                // Utilizado para identificar entidades externadas
                                               | IncludesHierarquiaEntidadeItem.Entidade_HistoricoSituacoes_SituacaoEntidade         // Utilizado para identificar entidades ativas
                                               | IncludesHierarquiaEntidadeItem.ItemSuperior                                         // Utilizado para recuperar o tipo de hierarquia do item superior
                                               | IncludesHierarquiaEntidadeItem.TipoHierarquiaEntidadeItem_ItensFilhos_TipoEntidade; // Utilizado para identificar itens folha segundo a hierarquia de tipo de entidade
            var hierarquiaEntidadeItensCursoOfertaLocalidade = this.HierarquiaEntidadeItemDomainService.SearchBySpecification(specHierarquia, includesHierarquiaEntidadeItem).ToList();
            var listaHierarquiaItens = hierarquiaEntidadeItensCursoOfertaLocalidade.TransformList<HierarquiaEntidadeItemNodeVO>();
            var listaHierarquiaItensAtivos = listaHierarquiaItens.Where(a => a.Ativa).ToList();

            var listaSeqsEntidadeCursoOfertaLocalidade = listaHierarquiaItensAtivos.Select(a => a.SeqEntidade);

            /*Buscando as entidades na curso-ferta-localidade, recuperando os níveis de ensino, as localidades pais da curso-oferta-localidade,
            as entidades pais das localidades, e o código SEO dessas entidades*/
            var specCursoOfertaLocalidade = new SMCContainsSpecification<CursoOfertaLocalidade, long>(t => t.Seq, listaSeqsEntidadeCursoOfertaLocalidade.ToArray());

            var listaCursoOfertaLocalidadeNivelEnsino = this.CursoOfertaLocalidadeDomainService.SearchProjectionBySpecification(specCursoOfertaLocalidade, t => new
            {
                SeqCursoOfertaLocalidade = t.Seq,
                NomeCursoOfertaLocalidade = t.Nome,
                SeqNivelEnsinoCurso = t.CursoOferta.Curso.SeqNivelEnsino,
                DescricaoNivelEnsinoCurso = t.CursoOferta.Curso.NivelEnsino.Descricao,
                SeqLocalidade = t.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade,
                NomeLocalidade = t.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                SeqPaiLocalidade = t.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.SeqEntidade,
                NomePaiLocalidade = t.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.Nome,
                CodigoSEOPaiLocalidade = t.HierarquiasEntidades.FirstOrDefault().ItemSuperior.ItemSuperior.Entidade.CodigoUnidadeSeo

            }).ToList();

            //Validando o nível de ensino do curso associado ao curso-oferta-localidade com nível de ensino do serviço
            var listaCursoOfertaLocalidadeNivelEnsinoValidada = listaCursoOfertaLocalidadeNivelEnsino.Where(a => seqsNiveisEnsinoServico.Contains(a.SeqNivelEnsinoCurso)).ToList();

            var entidadesPaiLocalidades = listaCursoOfertaLocalidadeNivelEnsinoValidada.Select(a => new
            {
                a.SeqPaiLocalidade,
                a.NomePaiLocalidade,
                a.CodigoSEOPaiLocalidade

            }).Where(a => a.CodigoSEOPaiLocalidade.HasValue).Distinct().ToList();

            StringBuilder mensagemAssertTaxasNaoParametrizadas = new StringBuilder();
            bool exibirAssert = false;
            Dictionary<int, string> origensParaValidar = new Dictionary<int, string>
            {
                { 1, "PUC Minas" },
                { 28, "Acadêmico Strictu Sensu" }
            };

            foreach (var item in modelo.Taxas)
            {
                foreach (var origem in origensParaValidar)
                {
                    /*Recupera todos os núcleos que a taxa foi configurada no financeiro, e verifica se está parametrizado
                    para cada taxa todos os núcleos do acadêmico para cada origem*/
                    var taxa = this.IntegracaoFinanceiroService.BuscarTaxa(item.SeqTaxaGra);
                    var valoresPorTaxa = this.IntegracaoFinanceiroService.ConsultarValoresTaxaPorOrigem(item.SeqTaxaGra, origem.Key);
                    var nucleosTaxaFinanceiro = valoresPorTaxa.Select(a => a.CodigoNucleo).Distinct().ToList();

                    StringBuilder nucleosNaoParametrizadosPorTaxa = new StringBuilder();
                    bool taxaNaoParametrizada = false;

                    foreach (var entidadePaiLocalidade in entidadesPaiLocalidades)
                    {
                        if (!nucleosTaxaFinanceiro.Contains(entidadePaiLocalidade.CodigoSEOPaiLocalidade.Value))
                        {
                            taxaNaoParametrizada = exibirAssert = true;
                            nucleosNaoParametrizadosPorTaxa.Append($"<br/> {entidadePaiLocalidade.CodigoSEOPaiLocalidade.Value} - {entidadePaiLocalidade.NomePaiLocalidade}");
                        }
                    }

                    if (taxaNaoParametrizada)
                    {
                        var descricaoOrigem = $"Origem: {origem.Key.ToString().PadLeft(2, '0')} - {origem.Value}";
                        var descricaoTaxa = $"Taxa: { taxa.DescricaoTaxa}";
                        var descricaoOrigemTaxa = $"{descricaoOrigem} / {descricaoTaxa}";
                        mensagemAssertTaxasNaoParametrizadas.Append($"<br/><br/> - {descricaoOrigemTaxa} {nucleosNaoParametrizadosPorTaxa}");
                    }
                }
            }

            return (exibirAssert, mensagemAssertTaxasNaoParametrizadas.ToString());
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<Servico>(seq));
                    this.DeleteEntity(configToDelete);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        #region [ Dados Relatório Posição Consolidada Serviço Por Ciclo Letivo ]

        public List<DadosRelatorioServicoCicloLetivoVO> BuscarDadosRelatorioServicoCicloLetivo(RelatorioServicoCicloLetivoFiltroVO filtro)
        {
            const string PROCEDURE_RELATORIO_SERVICO_CICLO_LETIVO = "ACADEMICO.SRC.st_rel_posicao_consolidada_servico_ciclo_letivo";

            string jSonSituacoes = BuscarJSonSituacoes(filtro.SeqServico.Value);

            var listaEntidades = filtro.SeqsEntidadeResponsavel != null && filtro.SeqsEntidadeResponsavel.Any() ? $"'{string.Join(",", filtro.SeqsEntidadeResponsavel)}'" : "null";

            string chamadaProcedure = $"exec {PROCEDURE_RELATORIO_SERVICO_CICLO_LETIVO} {filtro.SeqCicloLetivo}, {filtro.SeqServico}, {listaEntidades}, '{jSonSituacoes}'";

            var result = RawQuery<DadosRelatorioServicoCicloLetivoVO>(chamadaProcedure);

            return result;
        }

        #region [ Métodos privados ]

        private string BuscarJSonSituacoes(long seqServico)
        {
            var situacoes = BuscarSituacoesEtapaSGF(seqServico);

            return ObjectConvertToJSon(situacoes);
        }

        private List<SituacaoEtapaJSonVO> BuscarSituacoesEtapaSGF(long seqServico)
        {
            var SeqTemplateProcessoSgf = SearchProjectionByKey(new SMCSeqSpecification<Servico>(seqServico), x => x.SeqTemplateProcessoSgf);

            var etapasSGF = SGFHelper.BuscarEtapasSGFCache(SeqTemplateProcessoSgf);

            var situacoes = new List<SituacaoEtapaJSonVO>();

            foreach (var etapa in etapasSGF)
            {
                foreach (var etapaSituacao in etapa.Situacoes)
                {
                    var situacao = new SituacaoEtapaJSonVO();
                    situacao.num_ordem_etapa = etapa.Ordem;
                    situacao.seq_situacao_etapa = etapaSituacao.Seq;
                    situacao.ind_situacao_inicial_etapa = etapaSituacao.SituacaoInicialEtapa;
                    situacao.ind_situacao_final_etapa = etapaSituacao.SituacaoFinalEtapa;
                    situacao.idt_dom_classificacao_situacao_final = (short?)etapaSituacao.ClassificacaoSituacaoFinal;

                    situacoes.Add(situacao);
                }
            }
            return situacoes;
        }

        public string ObjectConvertToJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }

        #endregion [ Métodos privados ]

        #endregion [ Dados Relatório Posição Consolidada Serviço Por Ciclo Letivo ]
    }
}