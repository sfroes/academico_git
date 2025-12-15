using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data.SolicitacaoReabertura;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Seguranca.ServiceContract.Areas.USU.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoServicoService : SMCServiceBase, ISolicitacaoServicoService
    {
        #region Serviços

        private IUsuarioService UsuarioService
        {
            get { return this.Create<IUsuarioService>(); }
        }

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        #endregion Serviços

        #region [ DomainService ]

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private SolicitacaoReaberturaMatriculaDomainService SolicitacaoReaberturaMatriculaDomainService => Create<SolicitacaoReaberturaMatriculaDomainService>();

        private SolicitacaoIntercambioDomainService SolicitacaoIntercambioDomainService => Create<SolicitacaoIntercambioDomainService>();

        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService => Create<ConfiguracaoEtapaBloqueioDomainService>();

        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private SolicitacaoHistoricoUsuarioResponsavelDomainService SolicitacaoHistoricoUsuarioResponsavelDomainService => Create<SolicitacaoHistoricoUsuarioResponsavelDomainService>();

        private SolicitacaoAtividadeComplementarDomainService SolicitacaoAtividadeComplementarDomainService => Create<SolicitacaoAtividadeComplementarDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        #endregion [ DomainService ]

        public SMCPagerData<SolicitacaoServicoListarData> ListarSolicitacoesServico(SolicitacaoServicoFiltroData filtro)
        {
            var result = this.SolicitacaoServicoDomainService.ListarSolicitacoesServico(filtro.Transform<SolicitacaoServicoFilterSpecification>());

            return new SMCPagerData<SolicitacaoServicoListarData>(result.TransformList<SolicitacaoServicoListarData>(), result.Total);
        }

        public List<SMCDatasourceItem> BuscarBloqueiosDoProcessoSelect(List<long> seqsProcessos)
        {
            var retorno = new List<SMCDatasourceItem>();

            if (seqsProcessos.Count > 0)
            {
                var specProcessoEtapa = new ProcessoEtapaFilterSpecification() { SeqsProcessos = seqsProcessos };

                var configuracoesEtapas = ProcessoEtapaDomainService.SearchProjectionBySpecification(specProcessoEtapa, p => new { SeqsConfiguracoesEtapas = p.ConfiguracoesEtapa.Select(c => c.Seq).Distinct() });

                var specConfiguracaoEtapa = new ConfiguracaoEtapaBloqueioFilterSpecification() { SeqsConfiguracoesEtapas = configuracoesEtapas.SelectMany(c => c.SeqsConfiguracoesEtapas).ToList() };

                var configuracoesEtapasBloqueios = ConfiguracaoEtapaBloqueioDomainService.SearchProjectionBySpecification(specConfiguracaoEtapa,
                     a => new
                     {
                         Seq = a.SeqMotivoBloqueio,
                         DescricaoTipoBloqueio = a.MotivoBloqueio.TipoBloqueio.Descricao,
                         DescricaoMotivoBloqueio = a.MotivoBloqueio.Descricao,
                     }, isDistinct: true).OrderBy(o => o.DescricaoTipoBloqueio).ThenBy(t => t.DescricaoMotivoBloqueio).ToList();

                retorno.AddRange(configuracoesEtapasBloqueios.Select(c => new SMCDatasourceItem()
                {
                    Seq = c.Seq,
                    Descricao = $"{c.DescricaoTipoBloqueio} - {c.DescricaoMotivoBloqueio}"
                }).ToList());
            }
            else
            {
                var motivosBloqueios = MotivoBloqueioDomainService.SearchProjectionAll(a =>
                new
                {
                    Seq = a.Seq,
                    DescricaoTipoBloqueio = a.TipoBloqueio.Descricao,
                    DescricaoMotivoBloqueio = a.Descricao
                }).OrderBy(o => o.DescricaoTipoBloqueio).ThenBy(t => t.DescricaoMotivoBloqueio).ToList();

                retorno.AddRange(motivosBloqueios.Select(c => new SMCDatasourceItem()
                {
                    Seq = c.Seq,
                    Descricao = $"{c.DescricaoTipoBloqueio} - {c.DescricaoMotivoBloqueio}"
                }).ToList());
            }

            return retorno;
        }

        /// <summary>
        /// Realizar o processamento de plano de estudo das solicitações de serviços listada
        /// </summary>
        /// <param name="processamento">Objeto de processamento utilizado via WebApi</param>
        public void ProcessamentoPlanoEstudoServicoMatricula(ProcessamentoPlanoEstudoSATData processamento)
        {
            this.SolicitacaoMatriculaItemDomainService.ProcessamentoPlanoEstudoServicoMatricula(processamento.Transform<ProcessamentoPlanoEstudoSATVO>());
        }

        /// <summary>
        /// Buscar os usuários que possuem solicitações de serviços atribuidas
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public List<SMCDatasourceItem> BuscarUsuariosSolicitacoesAtribuidasSelect()
        {
            var lista = new List<SMCDatasourceItem>();

            //Recupera os sequenciais dos usuários que possuem solicitações atribuidas
            var seqsUsuariosSolicitacoesAtribuidas = this.SolicitacaoHistoricoUsuarioResponsavelDomainService.SearchProjectionAll(w => new
            {
                SeqUsuarioResponsavel = w.SeqUsuarioResponsavel,
                SeqEntidadeResponsavel = w.SolicitacaoServico.SeqEntidadeResponsavel
            }, isDistinct: true).ToArray();

            //Aciona o serviço do SAS para buscar os usuários pra seleção, pelos seqs recuperados
            var result = this.UsuarioService.BuscarUsuariosSelect(seqsUsuariosSolicitacoesAtribuidas.Select(x => x.SeqUsuarioResponsavel).ToArray());

            result.SMCForEach(u =>
            {
                lista.Add(new SMCDatasourceItem(u.Seq, u.CodigoPessoa.HasValue ? $"{u.CodigoPessoa} - {u.Nome}" : u.Nome));
            });

            return lista;
        }

        /// <summary>
        /// Busca as situações de documentação pelo sequencial do processo
        /// </summary>
        /// <param name="seqsProcessos">Sequenciais dos processos</param>
        /// <returns>Lista com as situações de documentação</returns>
        public List<SMCDatasourceItem> BuscarSituacoesDocumentacaoDoProcessoSelect(List<long> seqsProcessos)
        {
            return this.SolicitacaoServicoDomainService.BuscarSituacoesDocumentacaoDoProcessoSelect(seqsProcessos);
        }

        /// <summary>
        /// Busca os dados se identificação do solicitante de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados de identificação do solicitante</returns>
        public DadosSolicitacaoData BuscarDadosIdentificacaoSolicitante(long seqSolicitacaoServico)
        {
            var result = this.SolicitacaoServicoDomainService.BuscarDadosIdentificacaoSolicitante(seqSolicitacaoServico);

            return result.Transform<DadosSolicitacaoData>();
        }

        /// <summary>
        /// Busca os historicos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Históricos de uma solicitação de serviços</returns>
        public DadosSolicitacaoData BuscarHistoricosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            var result = this.SolicitacaoServicoDomainService.BuscarHistoricosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);

            return result.Transform<DadosSolicitacaoData>();
        }

        /// <summary>
        /// Busca as notificações de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Notificações de uma solicitação de serviços</returns>
        public DadosSolicitacaoData BuscarNotificacoesSolicitacao(NotificacaoSolicitacaoFiltroData filtro)
        {
            var result = this.SolicitacaoServicoDomainService.BuscarNotificacoesSolicitacao(filtro.Transform<NotificacaoSolicitacaoFiltroVO>());

            return result.Transform<DadosSolicitacaoData>();
        }

        /// <summary>
        /// Busca os documentos de uma solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Documentos de uma solicitação de serviços</returns>
        public DadosSolicitacaoData BuscarDocumentosSolicitacao(long seqSolicitacaoServico, long seqPessoaAtuacao)
        {
            var result = this.SolicitacaoServicoDomainService.BuscarDocumentosSolicitacao(seqSolicitacaoServico, seqPessoaAtuacao);

            return result.Transform<DadosSolicitacaoData>();
        }

        public SolicitacaoServicoData BuscarSolicitacaoServico(long seq)
        {
            var result = SolicitacaoServicoDomainService.SearchProjectionByKey(seq, x => new SolicitacaoServicoData
            {
                DataPrevistaSolucao = x.DataPrevistaSolucao,
                DataSolicitacao = x.DataSolicitacao,
                DataSolucao = x.DataSolucao,
                DescricaoAtualizada = x.DescricaoAtualizada,
                DescricaoOriginal = x.DescricaoOriginal,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                DescricaoServico = x.ConfiguracaoProcesso.Processo.Servico.Descricao,
                Etapas = x.Etapas.Select(e => new SolicitacaoServicoEtapaData
                {
                    HistoricosNavegacao = e.HistoricosNavegacao.Select(h => new SolicitacaoHistoricoNavegacaoData
                    {
                        DataEntrada = h.DataEntrada,
                        DataSaida = h.DataSaida,
                        Seq = h.Seq,
                        SeqConfiguracaoEtapaPagina = h.SeqConfiguracaoEtapaPagina,
                        SeqSolicitacaoServicoEtapa = h.SeqSolicitacaoServicoEtapa
                    }).ToList(),
                    HistoricosSituacao = e.HistoricosSituacao.Select(h => new SolicitacaoHistoricoSituacaoData
                    {
                        CategoriaSituacao = h.CategoriaSituacao,
                        DataExclusao = h.DataExclusao,
                        Seq = h.Seq,
                        SeqSituacaoEtapaSgf = h.SeqSituacaoEtapaSgf,
                        SeqSolicitacaoServicoEtapa = h.SeqSolicitacaoServicoEtapa,
                    }).ToList(),
                    Seq = e.Seq,
                    SeqConfiguracaoEtapa = e.SeqConfiguracaoEtapa,
                    SeqSolicitacaoServico = e.SeqSolicitacaoServico,
                }).ToList(),
                JustificativaComplementar = x.JustificativaComplementar,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                Seq = x.Seq,
                NumeroProtocolo = x.NumeroProtocolo,
                RASolicitante = (x.PessoaAtuacao as Aluno).NumeroRegistroAcademico,
                SeqAlunoHistorico = x.SeqAlunoHistorico,
                SeqConfiguracaoProcesso = x.SeqConfiguracaoProcesso,
                SeqEntidadeCompartilhada = x.SeqEntidadeCompartilhada,
                SeqEntidadeResponsavel = x.SeqEntidadeResponsavel,
                SeqGrupoEscalonamento = x.SeqGrupoEscalonamento,
                SeqJustificativaSolicitacaoServico = x.SeqJustificativaSolicitacaoServico,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqProcesso = x.ConfiguracaoProcesso.SeqProcesso,
                SeqServico = x.ConfiguracaoProcesso.Processo.SeqServico,
                SituacaoDocumentacao = x.SituacaoDocumentacao,
                TokenServico = x.ConfiguracaoProcesso.Processo.Servico.Token,
                TokenTipoServico = x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token
            });

            return result;
        }

        /// <summary>
        /// Busca o totalizador de solicitações de serviço de um determinado pessoa atuação (aluno)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação relacionada</param>
        /// <returns>Totalizador de solicitações da pessoa atuação</returns>
        public TotalizadorSolicitacaoServicoData BuscarTotalizadorSolicitacoesServico(long seqPessoaAtuacao)
        {
            return SolicitacaoServicoDomainService.BuscarTotalizadorSolicitacoesServico(seqPessoaAtuacao).Transform<TotalizadorSolicitacaoServicoData>();
        }

        public SMCPagerData<SolicitacaoServicoPessoaAtuacaoListaData> BuscarSolicitacoesPessoaAtuacao(SolicitacaoServicoPessoaAtuacaoFiltroData filtro)
        {
            return SolicitacaoServicoDomainService.BuscarSolicitacoesPessoaAtuacao(filtro.Transform<SolicitacaoServicoFilterSpecification>()).Transform<SMCPagerData<SolicitacaoServicoPessoaAtuacaoListaData>>();
        }

        /// <summary>
        /// Cria as solicitações de prorrogação manualmente
        /// </summary>
        /// <param name="codigosMigracao">Codigos de migração dos alunos
        /// </param>
        public List<long> CriarSolicitacoesProrrogacaoManual(List<long> codigosMigracao)
        {
            return SolicitacaoServicoDomainService.CriarSolicitacoesProrrogacaoManual(codigosMigracao);
        }

        /// <summary>
        /// Cria uma nova solicitação para a pessoa atuação em questão, referente ao serviço informado
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Dados da solicitação criada</returns>
        public DadosNovaSolicitacaoServicoData CriarSolicitacaoServico(CriarSolicitacaoData model)
        {
            (long SeqSolicitacaoServico, long SeqConfiguracaoEtapa, long SeqSolicitacaoServicoEtapa) dados = SolicitacaoServicoDomainService.CriarSolicitacaoServico(model.Transform<CriarSolicitacaoVO>());
            return new DadosNovaSolicitacaoServicoData
            {
                SeqConfiguracaoEtapa = dados.SeqConfiguracaoEtapa,
                SeqSolicitacaoServico = dados.SeqSolicitacaoServico
            };
        }

        public DadosSolicitanteData BuscarDadosSolicitante(long seqPessoaAtuacao)
        {
            return this.SolicitacaoServicoDomainService.BuscarDadosSolicitante(seqPessoaAtuacao, true).Transform<DadosSolicitanteData>();
        }

        /// <summary>
        /// Busca os historicos de navegação de uma solicitacao serviço / etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao de serviço</param>
        /// <param name="seqSolicitacaoServicoEtapa">Sequencial da etapa da solicitacao de serviço</param>
        /// <returns>Historicos de navegação da solicitacao de servico / etapa informada</returns>
        public HistoricoNavegacaoData BuscarHistoricosNavegacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa)
        {
            return this.SolicitacaoServicoDomainService.BuscarHistoricosNavegacao(seqSolicitacaoServico, seqSolicitacaoServicoEtapa).Transform<HistoricoNavegacaoData>();
        }

        public DadosSolicitacaoPadraoData BuscarDadosSolicitacaoPadrao(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.BuscarDadosSolicitacaoPadrao(seqSolicitacaoServico).Transform<DadosSolicitacaoPadraoData>();
        }

        public void SalvarJustificativaSolicitacao(DadosSolicitacaoPadraoData dadosSolicitacaoPadraoData)
        {
            SolicitacaoServico solicitacao = new SolicitacaoServico { Seq = dadosSolicitacaoPadraoData.SeqSolicitacaoServico, SeqJustificativaSolicitacaoServico = dadosSolicitacaoPadraoData.SeqJustificativa, JustificativaComplementar = dadosSolicitacaoPadraoData.ObservacoesJustificativa };
            SolicitacaoServicoDomainService.UpdateFields(solicitacao, x => x.JustificativaComplementar, x => x.SeqJustificativaSolicitacaoServico);
        }

        /// <summary>
        /// Busca os bloqueios de uma solicitacao serviço / etapa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao de serviço</param>
        /// <param name="seqSolicitacaoServicoEtapa">Sequencial da solicitacao de serviço / etapa</param>
        /// <returns>Bloqueios da solicitacao de servico / etapa informada</returns>
        public BloqueioEtapaSolicitacaoData BuscarBloqueiosEtapaSolicitacao(long seqSolicitacaoServico, long seqSolicitacaoServicoEtapa)
        {
            return this.SolicitacaoServicoDomainService.BuscarBloqueiosEtapaSolicitacao(seqSolicitacaoServico, seqSolicitacaoServicoEtapa).Transform<BloqueioEtapaSolicitacaoData>();
        }

        /// <summary>
        /// Busca os detalhes de uma notificação
        /// </summary>
        /// <param name="seqNotificacaoEmail">Sequencial da notificação</param>
        /// <param name="seqTipoNotificacao">Sequencial do tipo de notificação</param>
        /// <returns>Detalhes da notificação</returns>
        public DetalheNotificacaoSolicitacaoData BuscarDetalheNotificacaoSolicitacao(long seqNotificacaoEmail, long? seqTipoNotificacao = null, long? seqSolicitacaoServico = null)
        {
            return this.SolicitacaoServicoDomainService.BuscarDetalheNotificacaoSolicitacao(seqNotificacaoEmail, seqTipoNotificacao, seqSolicitacaoServico).Transform<DetalheNotificacaoSolicitacaoData>();
        }

        /// <summary>
        /// Envia notificação de um solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação serviço</param>
        /// <param name="tokenTipoNotificacao">Token do tipo de notificação</param>
        /// <returns></returns>
        public void EnviarNotificacaoSolicitacao(long seqSolicitacaoServico, string tokenTipoNotificacao = null)
        {
            SolicitacaoServicoDomainService.EnviarNotificacaoSolicitacao(seqSolicitacaoServico, tokenTipoNotificacao);
        }

        /// <summary>
        /// Envia notificação de um solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação serviço</param>
        /// <param name="seqNotificacaoEmailDestinatario">Sequencial da notificação email destinatário</param>
        /// <returns></returns>
        public void ReenviarNotificacaoSolicitacao(long seqSolicitacaoServico, long seqNotificacaoEmailDestinatario, PermiteReenvio permiteReenvio)
        {
            SolicitacaoServicoDomainService.ReenviarNotificacaoSolicitacao(seqSolicitacaoServico, seqNotificacaoEmailDestinatario, permiteReenvio);
        }

        /// <summary>
        /// Prepara o modelo para as operações com a solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviços</param>
        /// <returns>Modelo com as validações realizadas para realização das operações com a solicitação de servciço</returns>
        public DadosSolicitacaoData PrepararModeloSolicitacaoServico(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.PrepararModeloSolicitacaoServico(seqSolicitacaoServico).Transform<DadosSolicitacaoData>();
        }

        public DadosFormularioSolicitacaoPadraoData BuscarDadosFormularioSolicitacaoPadrao(long seqSolicitacaoServico, long seqConfiguracaoEtapaPagina)
        {
            return SolicitacaoServicoDomainService.BuscarDadosFormularioSolicitacaoPadrao(seqSolicitacaoServico, seqConfiguracaoEtapaPagina).Transform<DadosFormularioSolicitacaoPadraoData>();
        }

        public void SalvarDadosFormularioSolicitacaoPadrao(DadosFormularioSolicitacaoPadraoData dados)
        {
            SolicitacaoServicoDomainService.SalvarDadosFormularioSolicitacaoPadrao(dados.Transform<DadosFormularioSolicitacaoVO>());
        }

        /// <summary>
        /// Busca os dados de cabecalho para cancelamento da solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados do cabeçalho para cancelamento da solicitação de serviços</returns>
        public CabecalhoCancelamentoSolicitacaoData BuscarDadosCabecalhoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.BuscarDadosCabecalhoCancelamentoSolicitacao(seqSolicitacaoServico).Transform<CabecalhoCancelamentoSolicitacaoData>();
        }

        /// <summary>
        /// Busca os dados de cabecalho para reabertura da solicitação de serviços
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados do cabeçalho para reabertura da solicitação de serviços</returns>
        public CabecalhoReaberturaSolicitacaoData BuscarDadosCabecalhoReaberturaSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.BuscarDadosCabecalhoReaberturaSolicitacao(seqSolicitacaoServico).Transform<CabecalhoReaberturaSolicitacaoData>();
        }

        /// <summary>
        /// Buscar as situações de cancelamento de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Lista com as situações de cancelamento</returns>
        public List<SMCDatasourceItem> BuscarSituacoesCancelamentoSolicitacaoSelect(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.BuscarSituacoesCancelamentoSolicitacaoSelect(seqSolicitacaoServico);
        }

        /// <summary>
        /// Salva o cancelamento de uma solicitação de serviços
        /// </summary>
        /// <param name="modelo">Modelo com os dados do cancelamento</param>
        public void SalvarCancelamentoSolicitacao(CancelamentoSolicitacaoData modelo)
        {
            this.SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(modelo.Transform<CancelamentoSolicitacaoVO>());
        }

        /// <summary>
        /// Realiza a reabertura de uma solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser reaberta</param>
        /// <param name="observacao">Observação da reabertura</param>
        public void ReabrirSolicitacao(long seqSolicitacaoServico, string observacao)
        {
            this.SolicitacaoServicoDomainService.ReabrirSolicitacao(seqSolicitacaoServico, observacao);
        }

        public void SalvarConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico, long seqPessoaAtuacao, long seqConfiguracaoEtapa)
        {
            SolicitacaoServicoDomainService.SalvarConfirmacaoSolicitacaoPadrao(seqSolicitacaoServico, seqPessoaAtuacao, seqConfiguracaoEtapa);
        }

        public DadosFinaisSolicitacaoPadraoData BuscarDadosFinaisSolicitacaoPadrao(long seqSolicitacaoServico, long? seqConfiguracaoEtapa = null)
        {
            return SolicitacaoServicoDomainService.BuscarDadosFinaisSolicitacaoPadrao(seqSolicitacaoServico, seqConfiguracaoEtapa).Transform<DadosFinaisSolicitacaoPadraoData>();
        }

        public CabecalhoAtendimentoPadraoData BuscarDadosCabecalhoAtendimentoPadrao(long seqSolicitacaoServico)
        {
            var dadosSolicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                CriadoPor = x.UsuarioInclusao,
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                NomeSocialSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.Nome,
                Protocolo = x.NumeroProtocolo,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                DadosVinculo = x.PessoaAtuacao.Descricao,
                DescricaoVinculo = (x.PessoaAtuacao as Aluno).TipoVinculoAluno.Descricao ?? (x.PessoaAtuacao as Ingressante).TipoVinculoAluno.Descricao,
                TokenNivelEnsino = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Curso.NivelEnsino.Token
            });

            var formacoesEspecificas = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(dadosSolicitacao.SeqPessoaAtuacao, tokenNivelEnsino: dadosSolicitacao.TokenNivelEnsino).Select(x => x.DescricaoFormacaoEspecifica).ToList();
            var ret = SMCMapperHelper.Create<CabecalhoAtendimentoPadraoData>(dadosSolicitacao);
            ret.FormacoesEspecificas = formacoesEspecificas;
            return ret;
        }

        public void AtualizarUsuarioResponsavelAtendimento(long seqSolicitacaoServico)
        {
            SolicitacaoServicoDomainService.AtualizarUsuarioResponsavelAtendimento(seqSolicitacaoServico);
        }

        public List<SMCDatasourceItem> BuscarSolicitacoesPessoaAtuacaoTipoDocumentoSelect(long seqPessoaAtuacao, long seqTipoDocumento)
        {
            return this.SolicitacaoServicoDomainService.BuscarSolicitacoesPessoaAtuacaoTipoDocumentoSelect(seqPessoaAtuacao, seqTipoDocumento);
        }

        public DadosParecerData BuscarDadosParecer(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new DadosParecerData
            {
                DescricaoProcesso = x.ConfiguracaoProcesso.Processo.Descricao,
                Nome = x.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocial = x.PessoaAtuacao.DadosPessoais.NomeSocial,
                OrientacoesDeferimento = x.ConfiguracaoProcesso.Processo.Servico.OrientacaoDeferimento,
                Protocolo = x.NumeroProtocolo,
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ValidarSituacaoFutura = x.ConfiguracaoProcesso.Processo.Servico.ValidarSituacaoFutura,
                TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                DataSolicitacao = x.DataSolicitacao
            });
        }

        public void RealizarAtendimento(long seqSolicitacaoServico, bool? situacao, string parecer)
        {
            SolicitacaoServicoDomainService.RealizarAtendimento(seqSolicitacaoServico, situacao, parecer);
        }

        /// <summary>
        /// Verifica se solicitante possui alguma pendencia financeira
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser verificada</param>
        /// <returns>TRUE caso exista pendencia financeira e FALSE caso contrário</returns>
        public bool ValidarNadaConstaFinanceiro(long seqSolicitacaoServico)
        {
            var dados = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                x.DataSolicitacao
            });
            if (dados.CodigoAlunoMigracao.HasValue)
                return AlunoDomainService.ValidarNadaConstaFinanceiro(dados.CodigoAlunoMigracao.Value, dados.DataSolicitacao);
            else
                return true; // Se não encontrou o codigo aluno migração, retorna TRUE, pois não é possível avaliar as pendencias.
        }

        public void SalvarDocumentosSolicitacao(long seqSolicitacaoServico, long seqConfiguracaoEtapa)
        {
            SolicitacaoServicoDomainService.VerificaDocumentosSolicitacao(seqSolicitacaoServico, seqConfiguracaoEtapa);
        }

        public SolicitacaoAtividadeComplementarPaginaData BuscarSolicitacaoAtividadeComplementar(long seqSolicitacao)
        {
            return SolicitacaoAtividadeComplementarDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoAtividadeComplementar>(seqSolicitacao),
                                        x => new SolicitacaoAtividadeComplementarPaginaData()
                                        {
                                            SeqSolicitacaoServico = x.Seq,
                                            SeqTipoDivisaoComponente = x.DivisaoComponente.SeqTipoDivisaoComponente,
                                            SeqDivisaoComponente = x.SeqDivisaoComponente,
                                            Descricao = x.Descricao,
                                            SeqCicloLetivo = x.SeqCicloLetivo,
                                            CargaHoraria = x.CargaHoraria,
                                            DataInicio = x.DataInicio,
                                            DataFim = x.DataFim,
                                            TipoPublicacao = x.SolicitacaoArtigo.FirstOrDefault().TipoPublicacao,
                                            DescricaoEvento = x.SolicitacaoArtigo.FirstOrDefault().DescricaoEvento,
                                            AnoRealizacaoEvento = x.SolicitacaoArtigo.FirstOrDefault().AnoRealizacaoEvento,
                                            NaturezaArtigo = x.SolicitacaoArtigo.FirstOrDefault().NaturezaArtigo,
                                            TipoEvento = x.SolicitacaoArtigo.FirstOrDefault().TipoEvento,
                                            UfEvento = x.SolicitacaoArtigo.FirstOrDefault().UfEvento,
                                            CodCidadeEvento = x.SolicitacaoArtigo.FirstOrDefault().CodCidadeEvento,
                                            SeqPeriodico = x.SolicitacaoArtigo.FirstOrDefault().SeqQualisPeriodico,
                                            NumeroVolumePeriodico = x.SolicitacaoArtigo.FirstOrDefault().NumeroVolumePeriodico,
                                            NumeroFasciculoPeriodico = x.SolicitacaoArtigo.FirstOrDefault().NumeroFasciculoPeriodico,
                                            NumeroPaginaInicialPeriodico = x.SolicitacaoArtigo.FirstOrDefault().NumeroPaginaInicialPeriodico,
                                            NumeroPaginaFinalPeriodico = x.SolicitacaoArtigo.FirstOrDefault().NumeroPaginaFinalPeriodico,
                                            Faltas = x.Faltas,
                                            Nota = x.Nota,
                                            SeqEscalaApuracaoItem = x.SeqEscalaApuracaoItem,
                                            SituacaoFinal = x.SituacaoHistoricoEscolar
                                        });
        }

        public void SalvarSolicitacaoAtividadeComplementar(SolicitacaoAtividadeComplementarPaginaData model)
        {
            SolicitacaoServicoDomainService.SalvarSolicitacaoAtividadeComplementar(model.Transform<SolicitacaoAtividadeComplementarPaginaVO>());
        }

        /// <summary>
        /// Atualiza o campo entidade compartilhada da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqEntidadeCompartilhada">Sequencial da entidade complartilhada (grupo de programa)</param>
        public void AtualizarSolicitacaoServicoEntidadeCompartilhada(long seqSolicitacaoServico, long seqEntidadeCompartilhada)
        {
            SolicitacaoServicoDomainService.AtualizarSolicitacaoServicoEntidadeCompartilhada(seqSolicitacaoServico, seqEntidadeCompartilhada);
        }

        /// <summary>
        /// Verifica se existe algum serviço em aberto que conflita com o serviço passado como parâmetro
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço a ser validado</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista com as restrições encontradas</returns>
        public List<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaData> BuscarRestricoesSolicitacaoSimultanea(long seqServico, long seqPessoaAtuacao)
        {
            return this.SolicitacaoServicoDomainService.BuscarRestricoesSolicitacaoSimultanea(seqServico, seqPessoaAtuacao).TransformList<SolicitacaoDispensaRestricaoSolicitacaoSimultaneaData>();
        }

        /// <summary>
        /// Busca as situações futuras do aluno, considerando uma data de referencia.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dataReferencia">Data de referencia para verificar situações futuras</param>
        /// <returns>Lista com as situações futuras encontradas</returns>
        public List<SolicitacaoDispensaSituacaoFuturaAlunoData> BuscarSituacoesFuturasAluno(long seqPessoaAtuacao, DateTime dataReferencia)
        {
            return AlunoHistoricoSituacaoDomainService.BuscarSituacoesFuturasAluno(seqPessoaAtuacao, dataReferencia).TransformList<SolicitacaoDispensaSituacaoFuturaAlunoData>();
        }

        public List<SMCDatasourceItem> BuscarMotivosCancelamentoSolicitacaoSelect(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.BuscarMotivosCancelamentoSolicitacaoSelect(seqSolicitacaoServico);
        }

        public bool ValidarMotivoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.ValidarMotivoCancelamentoSolicitacao(seqSolicitacaoServico);
        }

        public bool ValidarTipoCancelamentoSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.ValidarTipoCancelamentoSolicitacao(seqSolicitacaoServico);
        }

        public long RetornarSolicitacaoParaEtapaAnterior(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.RetornarSolicitacaoParaEtapaAnterior(seqSolicitacaoServico);
        }

        /// <summary>
        /// Busca os dados da solicitação de serviço para detalhes da modal de acordo com sequencial ou protocolo
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="protocolo">Número do protocolo</param>
        /// <returns>Objeto de solicitacao de serviço simplificado para modal</returns>
        public DadosModalSolicitacaoServicoData BuscarDadosModalSolicitacaoServico(long? seqSolicitacaoServico, string protocolo)
        {
            return this.SolicitacaoServicoDomainService.BuscarDadosModalSolicitacaoServico(seqSolicitacaoServico, protocolo).Transform<DadosModalSolicitacaoServicoData>();
        }

        /// <summary>
        /// Busca os dados para realizar o atendimento de reabertura de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Dados complementares</returns>
        public AtendimentoReaberturaData BuscarDadosAtendimentoReabertura(long seqSolicitacaoServico)
        {
            return SolicitacaoReaberturaMatriculaDomainService.BuscarDadosAtendimentoReabertura(seqSolicitacaoServico).Transform<AtendimentoReaberturaData>();
        }

        /// <summary>
        /// Salva os dados informados no atendimento de uma solicitação de reabertura
        /// </summary>
        /// <param name="atendimentoReaberturaData">Dados</param>
        public void SalvarDadosAtendimentoReabertura(AtendimentoReaberturaData atendimentoReaberturaData)
        {
            if (atendimentoReaberturaData.SeqGrupoEscalonamentoMatricula == 0)
                atendimentoReaberturaData.SeqGrupoEscalonamentoMatricula = null;

            var solicitacao = new SolicitacaoReaberturaMatricula
            {
                SeqGrupoEscalonamentoMatricula = atendimentoReaberturaData.SeqGrupoEscalonamentoMatricula,
                Seq = atendimentoReaberturaData.SeqSolicitacaoServico
            };

            SolicitacaoReaberturaMatriculaDomainService.UpdateFields(solicitacao, x => x.SeqGrupoEscalonamentoMatricula);
        }

        public DadosConfirmacaoSolicitacaoPadraoData BuscarDadosConfirmacaoSolicitacaoPadrao(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.BuscarDadosConfirmacaoSolicitacaoPadrao(seqSolicitacaoServico).Transform<DadosConfirmacaoSolicitacaoPadraoData>();
        }

        public List<long> BuscarSeqsConfiguracoesEtapaSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoDomainService.SearchProjectionByKey(seqSolicitacaoServico, s => s.ConfiguracaoProcesso.ConfiguracoesEtapa.OrderBy(c => c.ProcessoEtapa.Ordem).Select(a => a.Seq)).ToList();
        }

        public void SalvarDadosAtendimentoIntercambio(AtendimentoIntercambioData atendimentoIntercambioData)
        {
            this.SolicitacaoIntercambioDomainService.SalvarDadosAtendimentoIntercambio(atendimentoIntercambioData.Transform<AtendimentoIntercambioVO>());
        }

        public AtendimentoIntercambioData BuscarDadosIniciaisAtendimentoIntercambio(long seqSolicitacaoServico)
        {
            return SolicitacaoIntercambioDomainService.BuscarDadosIniciaisAtendimentoIntercambio(seqSolicitacaoServico).Transform<AtendimentoIntercambioData>();
        }

        public AtendimentoIntercambioData BuscarDadosTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoTermoIntercambio)
        {
            return SolicitacaoIntercambioDomainService.BuscarDadosTermoAtendimentoIntercambio(seqSolicitacaoServico, seqTipoTermoIntercambio).Transform<AtendimentoIntercambioData>();
        }

        public AtendimentoIntercambioData BuscarDadosOrientacaoTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoOrientacao, long seqTipoTermoIntercambio, long seqTermoIntercambio)
        {
            return SolicitacaoIntercambioDomainService.BuscarDadosOrientacaoTermoAtendimentoIntercambio(seqSolicitacaoServico, seqTipoOrientacao, seqTipoTermoIntercambio, seqTermoIntercambio).Transform<AtendimentoIntercambioData>();
        }

        /// <summary>
        /// Busca o campo justificativa complementar da solicitação de serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Justificativa complementar</returns>
        public string BuscarSolicitacaoServicoJustificativaComplementar(long seq)
        {
            return SolicitacaoServicoDomainService.BuscarSolicitacaoServicoJustificativaComplementar(seq);
        }

        /// <summary>
        /// Atualiza o campo justificativa complementar da solicitação de serviço, utilizado para disciplina eletiva com o valor do grupo de programa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="justificativa">Justificativa complementar da solicitação de serviço</param>
        public void AtualizarSolicitacaoServicoJustificativaComplementar(long seqSolicitacaoServico, string justificativa)
        {
            SolicitacaoServicoDomainService.AtualizarSolicitacaoServicoJustificativaComplementar(seqSolicitacaoServico, justificativa);
        }

        /// <summary>
        /// Busca as situações de matricula de acordo com o tipo de atuação informado
        /// </summary>
        /// <param name="tipoAtuacao">Tipo de atuação informado</param>
        /// <returns>Lista de situações de matrícula encontradas</returns>
        public List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipoAtuacaoSelect(TipoAtuacao tipoAtuacao)
        {
            return SolicitacaoServicoDomainService.BuscarSituacoesMatriculaPorTipoAtuacaoSelect(tipoAtuacao);
        }

        public BotoesSolicitacaoData GerarBotoesSolicitacao(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.GerarBotoesSolicitacao(seqSolicitacaoServico).Transform<BotoesSolicitacaoData>();
        }

        public bool VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(long seqConfiguracaoProcesso)
        {
            return SolicitacaoServicoDomainService.VerificarConfiguracaoPossuiSolicitacaoServicoEmAberto(seqConfiguracaoProcesso);
        }

        public SolicitacaoCobrancaTaxaData PrepararModeloSolicitacaoCobrancaTaxa(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.PrepararModeloSolicitacaoCobrancaTaxa(seqSolicitacaoServico).Transform<SolicitacaoCobrancaTaxaData>();
        }

        public void AtualizarSolicitacaoServicoTipoEmissaoTaxa(long seqSolicitacaoServico, TipoEmissaoTaxa tipoEmissaoTaxa)
        {
            SolicitacaoServicoDomainService.AtualizarSolicitacaoServicoTipoEmissaoTaxa(seqSolicitacaoServico, tipoEmissaoTaxa);
        }

        public long ProcedimentosReemissaoTitulo(long seqTitulo, long seqTaxa, long seqServico, long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.ProcedimentosReemissaoTitulo(seqTitulo, seqTaxa, seqServico, seqSolicitacaoServico);
        }

        public bool VerificarConfiguracaoPossuiSolicitacaoServico(long seqConfiguracaoProcesso)
        {
            return SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = seqConfiguracaoProcesso }) > 0;
        }

        public bool VerificarProcessoPossuiSolicitacaoServico(long seqProcesso)
        {
            return SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqProcesso = seqProcesso }) > 0;
        }

        public List<DadosRelatorioSolicitacoesBloqueioData> BuscarDadosRelatorioSolicitacoesBloqueio(RelatorioSolicitacoesBloqueioFiltroData filtro)
        {
            return SolicitacaoServicoDomainService.BuscarDadosRelatorioSolicitacoesBloqueio(filtro.Transform<RelatorioSolicitacoesBloqueioFiltroVO>())
                                       .TransformList<DadosRelatorioSolicitacoesBloqueioData>();
        }

        public string BuscarTokenEtapaAtualSolicitacao(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.BuscarTokenEtapaAtualSolicitacao(seqSolicitacaoServico);
        }

        /// <summary>
        /// Buscar o usuário SAS da pessoa atuação da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacao">Sequencial da solicitação</param>
        /// <returns>Sequencial do usuário sAS da pessoa atuação da solicitaçãod e serviço</returns>
        public long BuscarSeqUsuarioSASSolicitacaoServico(long seqSolicitacao)
        {
            var spec = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacao);
            return SolicitacaoServicoDomainService.SearchProjectionByKey(spec, x => x.PessoaAtuacao.Pessoa.SeqUsuarioSAS).GetValueOrDefault();
        }

        /// <summary>
        /// Atualizar o termo de entrega da documentação
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequecial da solicitação de serviços</param>
        /// <param name="compromissoEntregaDocumentacao">Falg de compormisso de entrega da documentação</param>
        public void AtualizarTermoEntregaDocumentacao(long seqSolicitacaoServico, bool? compromissoEntregaDocumentacao)
        {
            SolicitacaoServicoDomainService.AtualizarTermoEntregaDocumentacao(seqSolicitacaoServico, compromissoEntregaDocumentacao);
        }

        /// <summary>
        /// Buscar se o termo de entraga de documentação foi aceito
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao serviço</param>
        /// <returns></returns>
        public bool BuscarTermoEntregaDocumentacaoFoiAceito(long seqSolicitacaoServico)
        {
            return SolicitacaoServicoDomainService.BuscarTermoEntregaDocumentacaoFoiAceito(seqSolicitacaoServico);
        }

        public string BuscarDescricaoTipoDocumento(long seqTipoDocumento)
        {
            return SolicitacaoServicoDomainService.BuscarDescricaoTipoDocumento(seqTipoDocumento);
        }

        public void SalvarSelecaoComponenteCurricular(long seqSolicitacaoServico, long seqDivisaoComponente)
        {
            SolicitacaoServicoDomainService.SalvarSelecaoComponenteCurricular(seqSolicitacaoServico, seqDivisaoComponente);
        }
    }
}