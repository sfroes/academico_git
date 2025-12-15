using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Models;
using SMC.Financeiro.Service.FIN;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.Common.Areas.TMP.Includes;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class GrupoEscalonamentoDomainService : AcademicoContextDomain<GrupoEscalonamento>
    {
        #region [ DomainServices ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService => Create<ConfiguracaoEtapaPaginaDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private EscalonamentoDomainService EscalonamentoDomainService => Create<EscalonamentoDomainService>();

        private GrupoEscalonamentoItemDomainService GrupoEscalonamentoItemDomainService => Create<GrupoEscalonamentoItemDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService => Create<IngressanteHistoricoSituacaoDomainService>();

        private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService => Create<InstituicaoNivelServicoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private ParametroEnvioNotificacaoDomainService ParametroEnvioNotificacaoDomainService => Create<ParametroEnvioNotificacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService => Create<ProcessoSeletivoDomainService>();

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService => Create<SolicitacaoHistoricoNavegacaoDomainService>();

        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService => Create<SolicitacaoHistoricoSituacaoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService => Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService { get => Create<CursoOfertaLocalidadeDomainService>(); }

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService { get => Create<CursoOfertaLocalidadeTurnoDomainService>(); }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => Create<ConfiguracaoEventoLetivoDomainService>(); }

        private ContratoDomainService ContratoDomainService { get => Create<ContratoDomainService>(); }

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService { get => Create<ArquivoAnexadoDomainService>(); }

        private TipoNotificacaoDomainService TipoNotificacaoDomainService => Create<TipoNotificacaoDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();
        private TrabalhoAcademicoDivisaoComponenteDomainService TrabalhoAcademicoDivisaoComponenteDomainService => Create<TrabalhoAcademicoDivisaoComponenteDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService => Create<AplicacaoAvaliacaoDomainService>();
        private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService => Create<InstituicaoNivelTipoTrabalhoDomainService>();
        private RequisitoDomainService RequisitoDomainService => Create<RequisitoDomainService>();
        private DivisaoComponenteDomainService DivisaoComponenteDomainService => Create<DivisaoComponenteDomainService>();
        private TrabalhoAcademicoAutoriaDomainService TrabalhoAcademicoAutoriaDomainService => Create<TrabalhoAcademicoAutoriaDomainService>();
        private GrupoEscalonamentoItemParcelaDomainService GrupoEscalonamentoItemParcelaDomainService => Create<GrupoEscalonamentoItemParcelaDomainService>();
        private ServicoDomainService ServicoDomainService => Create<ServicoDomainService>();
        private TipoServicoDomainService TipoServicoDomainService => Create<TipoServicoDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IEtapaService EtapaService
        {
            get { return this.Create<IEtapaService>(); }
        }

        private IAplicacaoService AplicacaoService { get => Create<IAplicacaoService>(); }

        private IIntegracaoService IntegracaoService { get => Create<IIntegracaoService>(); }

        private IInscricaoService InscricaoService { get => Create<IInscricaoService>(); }

        private IInscricaoOfertaHistoricoSituacaoService InscricaoOferta { get => Create<IInscricaoOfertaHistoricoSituacaoService>(); }

        private ISituacaoService SituacaoService { get => Create<ISituacaoService>(); }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService { get => Create<IIntegracaoFinanceiroService>(); }

        private IFinanceiroService FinanceiroService => Create<IFinanceiroService>();

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion [ Services ]

        #region [Constantes]

        private const string INCLUSAO = "Inclusão";
        private const string ALTERACAO = "Alteração";

        #endregion [Constantes]

        public List<GrupoEscalonamentoListaVO> BuscarGruposEscalonamento(GrupoEscalonamentoFiltroVO filtro)
        {
            var listaGrupoEscalonamento = new List<GrupoEscalonamentoListaVO>();

            var spec = filtro.Transform<GrupoEscalonamentoFilterSpecification>(filtro);

            spec.SetOrderBy(f => f.Descricao);

            //Cria um opbjeto anonimo com as informações necessárias
            var gruposEscalonamento = SearchProjectionBySpecification(spec, g => new
            {
                g.Seq,
                g.Descricao,
                g.Ativo,
                g.SeqProcesso,
                DescricaoProcesso = g.Processo.Descricao,
                g.NumeroDivisaoParcelas,
                ProcessoNoPeriodoVigencia = DateTime.Now >= g.Processo.DataInicio && (!g.Processo.DataFim.HasValue || DateTime.Now <= g.Processo.DataFim.Value),
                ProcessoExpirado = (g.Processo.DataFim.HasValue && DateTime.Now > g.Processo.DataFim.Value),
                ProcessoFuturo = g.Processo.DataInicio > DateTime.Now,
                ProcessoEncerrado = g.Processo.DataEncerramento.HasValue && g.Processo.DataEncerramento.Value < DateTime.Now,
                PossuiParcelas = g.Itens.Any(a => a.Parcelas.Any()),
                PossuiIntegracaoFinanceira = g.Processo.Servico.IntegracaoFinanceira == IntegracaoFinanceira.NaoSeAplica ? false : true,
                g.Itens.FirstOrDefault().Escalonamento.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                g.Processo.Servico.PermiteReabrirSolicitacao,
                g.Processo.Servico.TokenPermissaoManutencaoProcesso,
                PermitirNotificacao = g.Processo.Etapas.FirstOrDefault(f => f.Ordem == 1).ConfiguracoesNotificacao.Any(a => a.TipoNotificacao.Token.Equals(TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PERIODO_VIGENCIA)),
                Itens = g.Itens.Select(i => new
                {
                    SeqEtapaSGF = i.Escalonamento.ProcessoEtapa.SeqEtapaSgf,
                    i.Seq,
                    i.SeqGrupoEscalonamento,
                    i.SeqEscalonamento,
                    i.GrupoEscalonamento.Processo.SeqServico,
                    i.GrupoEscalonamento.Processo.Servico.Token,
                    i.GrupoEscalonamento.SeqProcesso,
                    i.Escalonamento.DataInicio,
                    i.Escalonamento.DataFim,
                    i.Escalonamento.DataEncerramento,
                    i.Escalonamento.SeqProcessoEtapa,
                    i.Escalonamento.ProcessoEtapa.DescricaoEtapa,
                    i.Escalonamento.ProcessoEtapa.SituacaoEtapa,
                    QuantidadeParcelas = i.Parcelas.Any() && i.Parcelas.Count() > 0 ? i.Parcelas.Count() : 0,
                    i.Escalonamento.ProcessoEtapa.Ordem,
                    UltimosHistoricosPorEtapa = i.GrupoEscalonamento.SolicitacoesServico.Select(s => s.Etapas.FirstOrDefault(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == i.Escalonamento.ProcessoEtapa.SeqEtapaSgf).SituacaoAtual).Where(a => a != null),
                }).OrderBy(x => x.Ordem)
            }).ToList();

            ///Buscar quantidade de solicitações associadas a um grupo de escalonamento
            var listaQuantidadeSolicitacoes = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = filtro.SeqProcesso }).Select(s =>
            new
            {
                s.SeqGrupoEscalonamento,
                s.QuantidadeSolicitacoes
            }).ToList();

            //Para cada grupo de escalonamento retornado, seta as propriedades dos objetos que serão retornados
            foreach (var grupoEscalonamento in gruposEscalonamento)
            {
                //Busca as etapas no SGF pelo sequencial do template do grupo de escalonamento
                var etapas = SGFHelper.BuscarEtapasSGFCache(grupoEscalonamento.SeqTemplateProcessoSgf);

                var novoGrupoEscalonamento = new GrupoEscalonamentoListaVO()
                {
                    Seq = grupoEscalonamento.Seq,
                    Descricao = grupoEscalonamento.Descricao,
                    Ativo = grupoEscalonamento.Ativo,
                    DescricaoProcesso = grupoEscalonamento.DescricaoProcesso,
                    NumeroDivisaoParcelas = grupoEscalonamento.NumeroDivisaoParcelas,
                    ProcessoNoPeriodoVigencia = grupoEscalonamento.ProcessoNoPeriodoVigencia,
                    ProcessoExpirado = grupoEscalonamento.ProcessoExpirado,
                    ProcessoFuturo = grupoEscalonamento.ProcessoFuturo,
                    ProcessoEncerrado = grupoEscalonamento.ProcessoEncerrado,
                    PossuiParcelas = grupoEscalonamento.PossuiParcelas,
                    PossuiIntegracaoFinanceira = grupoEscalonamento.PossuiIntegracaoFinanceira,
                    SeqProcesso = grupoEscalonamento.SeqProcesso,
                    Itens = new List<GrupoEscalonamentoListaItemVO>(),
                    TodasEtapasEncerradas = grupoEscalonamento.Itens.Any(a => (a.SituacaoEtapa != SituacaoEtapa.Liberada && a.SituacaoEtapa != SituacaoEtapa.EmManutencao && a.SituacaoEtapa != SituacaoEtapa.AguardandoLiberacao)),
                    PermiteReabrirSolicitacao = grupoEscalonamento.PermiteReabrirSolicitacao,
                    PermitirNotificacao = grupoEscalonamento.PermitirNotificacao,
                };

                novoGrupoEscalonamento.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(grupoEscalonamento.TokenPermissaoManutencaoProcesso);

                ///Senão, se todas as etapas do grupo em questão estiverem expirado, o botão deverá ser desabilitado
                novoGrupoEscalonamento.NaoPermiteAssociarSolicitacaoEtapasExpiradas = !grupoEscalonamento.Itens.Any(a => a.DataFim >= DateTime.Now);

                ///Associar quantidade de solicitações do grupo de escalonamento
                if (listaQuantidadeSolicitacoes.Any(a => a.SeqGrupoEscalonamento == grupoEscalonamento.Seq))
                {
                    novoGrupoEscalonamento.QuantidadeSolicitacoes = listaQuantidadeSolicitacoes.FirstOrDefault(f => f.SeqGrupoEscalonamento == grupoEscalonamento.Seq).QuantidadeSolicitacoes;
                }

                foreach (var grupoEscalonamentoItem in grupoEscalonamento.Itens)
                {
                    //recupera a etapa atual da lista de etapas do SGF
                    var etapaAtualSGF = etapas.FirstOrDefault(e => e.Seq == grupoEscalonamentoItem.SeqEtapaSGF);

                    //Recupera a situação final da etapa atual que esteja parametrizada como "Finalizada com Sucesso"
                    var situacaoFinalSucesso = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.SituacaoFinalEtapa && s.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso);

                    //Verifica se algum histórico possui etapa na situação "Finalizada com Sucesso"
                    var algumItemNaSituacaoFinalSucesso = grupoEscalonamentoItem.UltimosHistoricosPorEtapa.Any(a => a.SeqSituacaoEtapaSgf == situacaoFinalSucesso.Seq);

                    novoGrupoEscalonamento.Itens.Add(new GrupoEscalonamentoListaItemVO()
                    {
                        Seq = grupoEscalonamentoItem.Seq,
                        SeqGrupoEscalonamento = grupoEscalonamentoItem.SeqGrupoEscalonamento,
                        SeqEscalonamento = grupoEscalonamentoItem.SeqEscalonamento,
                        SeqServico = grupoEscalonamentoItem.SeqServico,
                        TokenServico = grupoEscalonamentoItem.Token,
                        SeqProcesso = grupoEscalonamentoItem.SeqProcesso,
                        DataInicio = grupoEscalonamentoItem.DataInicio,
                        DataFim = grupoEscalonamentoItem.DataFim,
                        DataEncerramento = grupoEscalonamentoItem.DataEncerramento,
                        SeqProcessoEtapa = grupoEscalonamentoItem.SeqProcessoEtapa,
                        DescricaoEtapa = grupoEscalonamentoItem.DescricaoEtapa,
                        SituacaoEtapa = grupoEscalonamentoItem.SituacaoEtapa,
                        QuantidadeParcelas = grupoEscalonamentoItem.QuantidadeParcelas,
                        SolicitacaoFinalizadaComSucesso = algumItemNaSituacaoFinalSucesso,
                        HabilitaBtnComPermissaoManutencaoProcesso = novoGrupoEscalonamento.HabilitaBtnComPermissaoManutencaoProcesso
                    });

                    /*Senão, se o grupo de escalonamento possui configuração de parcela e o vencimento das parcelas
                    for menor que a data do respectivo escalonamento, o botão deverá ser desabilitado com a seguinte mensagem informativa:*/
                    var parcelas = this.GrupoEscalonamentoItemDomainService.BuscarGrupoEscalonamentoItem(grupoEscalonamentoItem.Seq).Parcelas;
                    var escalonamento = this.EscalonamentoDomainService.BuscarEscalonamento(grupoEscalonamentoItem.SeqEscalonamento);
                    foreach (var parcela in parcelas)
                    {
                        if (parcela.DataVencimentoParcela < DateTime.Parse(escalonamento.DataFim.SMCDataAbreviada()))
                        {
                            novoGrupoEscalonamento.PossuiParcelasVencimentoMenorEscalonamento = true;
                        }
                    }
                }

                listaGrupoEscalonamento.Add(novoGrupoEscalonamento);
            }

            return listaGrupoEscalonamento;
        }

        public List<SMCDatasourceItem> BuscarGruposEscalonamentoSelect(GrupoEscalonamentoFiltroVO filtro)
        {
            var spec = filtro.Transform<GrupoEscalonamentoFilterSpecification>();

            var lista = this.SearchProjectionBySpecification(spec, g => new
            {
                Seq = g.Seq,
                Descricao = g.Descricao,
                DataInicio = g.Itens.Select(i => i.Escalonamento).OrderBy(o => o.DataInicio).FirstOrDefault().DataInicio,
                DataFim = g.Itens.Select(i => i.Escalonamento).OrderBy(o => o.DataFim).FirstOrDefault().DataFim
            }).OrderBy(o => o.DataInicio);

            var retorno = new List<SMCDatasourceItem>();

            foreach (var item in lista)
                retorno.Add(new SMCDatasourceItem(item.Seq, $"{item.Descricao} - {item.DataInicio.ToString("dd/MM/yyyy")} a {item.DataFim.ToString("dd/MM/yyyy")}"));

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarGruposEscalonamentoPorProcessoSelect(long seqProcesso)
        {
            var spec = new GrupoEscalonamentoFilterSpecification() { SeqProcesso = seqProcesso };

            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.Ativo ? x.Descricao + " - Ativo" : x.Descricao + " - Inativo"

            }).ToList();

            return lista.OrderBy(o => o.Descricao).ToList();
        }

        public bool ExistemGruposEscalonamentoPorProcesso(List<long> seqsProcessos)
        {
            var spec = new GrupoEscalonamentoFilterSpecification() { SeqsProcessos = seqsProcessos };

            return this.Count(spec) > 0;
        }

        public GrupoEscalonamentoVO BuscarGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            var spec = new GrupoEscalonamentoFilterSpecification() { Seq = seqGrupoEscalonamento };

            spec.SetOrderBy(x => x.Itens.FirstOrDefault().Escalonamento.ProcessoEtapa.Ordem);
            spec.SetOrderBy(x => x.Descricao);

            var result = SearchProjectionByKey(spec, g => new GrupoEscalonamentoVO()
            {
                Seq = g.Seq,
                SeqProcesso = g.SeqProcesso,
                Descricao = g.Descricao,
                Ativo = g.Ativo,
                NumeroDivisaoParcelas = g.NumeroDivisaoParcelas,
                DataInicio = g.Processo.DataInicio,
                DataFim = g.Processo.DataFim,
                ObrigarNumeroDivisaoParcelas = g.Processo.Servico.InstituicaoNivelServicos.Any(i => !i.InstituicaoNivelTipoVinculoAluno.ExigeCurso),
                TodasParcelasLiberadas = !(g.Itens.Count(i => i.Escalonamento.ProcessoEtapa.SituacaoEtapa != SituacaoEtapa.Liberada) > 0),
                ProcessoEncerrado = g.Processo.DataEncerramento.HasValue && g.Processo.DataEncerramento.Value < DateTime.Now,
                ExibeMensagemTokenDisciplinaIsolada = g.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                Itens = g.Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem).Select(i => new GrupoEscalonamentoItemVO()
                {
                    Seq = i.Seq,
                    SeqGrupoEscalonamento = i.GrupoEscalonamento.Seq,
                    SeqProcessoEtapa = i.Escalonamento.SeqProcessoEtapa,
                    DescricaoEtapa = i.Escalonamento.ProcessoEtapa.DescricaoEtapa,
                    SituacaoEtapa = i.Escalonamento.ProcessoEtapa.SituacaoEtapa,
                    OrdemEtapa = i.Escalonamento.ProcessoEtapa.Ordem,
                    FinalizacaoEtapaAnterior = i.Escalonamento.ProcessoEtapa.FinalizacaoEtapaAnterior,
                    SeqEscalonamento = i.Escalonamento.Seq,
                    DataIncioEscalonamento = i.Escalonamento.DataInicio,
                    DataFimEscalonamento = i.Escalonamento.DataFim,
                    DataEncerramentoEscalonamento = i.Escalonamento.DataEncerramento,
                    QuantidadeParcelas = i.Parcelas.Count,
                    Escalonamentos = i.Escalonamento.ProcessoEtapa.Escalonamentos.Where(w => (!w.DataEncerramento.HasValue))
                    .Select(e => new EscalonamentoEtapaItemVO() { Seq = e.Seq, DataInicio = e.DataInicio, DataFim = e.DataFim, DataEncerramento = e.DataEncerramento })
                }).ToList()
            });

            //Valida se o processo está dentro do periodo de vigência
            //if (DateTime.Now < result.DataInicio || (result.DataFim.HasValue && DateTime.Now > result.DataFim.Value))
            //{
            //    throw new ProcessoForaPeriodoVigenciaException("alterar");
            //}

            ///Aplica a regra de encerrado para os escalonamentos e cria a descrição para exibição caso não exista mais o item no select
            foreach (var item in result.Itens)
            {
                item.DescricaoEscalonamento = $"{item.DataIncioEscalonamento} - {item.DataFimEscalonamento}";

                if (item.DataEncerramentoEscalonamento.HasValue)
                {
                    item.DescricaoEscalonamento += " - Encerrado";
                }

                var solicitacoesPorGrupo = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = result.SeqProcesso, SeqGrupoEscalonamento = item.SeqGrupoEscalonamento });

                item.ExisteSolicitacoes = (solicitacoesPorGrupo.SMCAny()) ? solicitacoesPorGrupo.FirstOrDefault().QuantidadeSolicitacoes > 0 : false;
            }

            result.ExibirLegenda = false;

            ///Exibir legenda e icone da finalização de etapa
            foreach (var item in result.Itens)
            {
                if (item.FinalizacaoEtapaAnterior)
                {
                    item.Legenda = GrupoEscalonamentoParametros.FinalizaçãoEtapaAnterior;
                    result.ExibirLegenda = true;
                }
                else
                {
                    item.Legenda = GrupoEscalonamentoParametros.Nenhum;
                }
            }

            var listaQuantidadeSolicitacoes = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = result.SeqProcesso }).Select(s =>
            new
            {
                s.SeqGrupoEscalonamento,
                s.QuantidadeSolicitacoes

            }).ToList();

            int quantidadeSolicitacoes = 0;

            if (listaQuantidadeSolicitacoes.Any(a => a.SeqGrupoEscalonamento == seqGrupoEscalonamento))
            {
                quantidadeSolicitacoes = listaQuantidadeSolicitacoes.FirstOrDefault(f => f.SeqGrupoEscalonamento == seqGrupoEscalonamento).QuantidadeSolicitacoes;
            }

            result.ExibeNumeroDivisaoParcelasDesabilitado = false;

            if (quantidadeSolicitacoes > 0)
            {
                var specSolicitacaoServico = new SolicitacaoServicoFilterSpecification() { SeqProcesso = result.SeqProcesso, SeqGrupoEscalonamento = seqGrupoEscalonamento };
                var seqsSolicitacoesServicoDoGrupoEscalonamento = SolicitacaoServicoDomainService.SearchProjectionBySpecification(specSolicitacaoServico, x => x.Seq).Distinct().ToList();

                if (seqsSolicitacoesServicoDoGrupoEscalonamento.Any())
                {
                    var solicitacoesMatricula = this.SolicitacaoMatriculaDomainService.SearchBySpecification(new SMCContainsSpecification<SolicitacaoMatricula, long>(p => p.Seq, seqsSolicitacoesServicoDoGrupoEscalonamento.ToArray())).ToList();

                    if (solicitacoesMatricula.Any(a => a.SeqCondicaoPagamentoGra.HasValue))
                        result.ExibeNumeroDivisaoParcelasDesabilitado = true;
                }
            }

            foreach (var item in result.Itens)
            {
                item.ExibeEscalonamentoDesabilitado = true;

                if (quantidadeSolicitacoes == 0 && item.SituacaoEtapa != SituacaoEtapa.Encerrada)
                {
                    item.ExibeEscalonamentoDesabilitado = false;
                }
                else if (quantidadeSolicitacoes > 0 && (item.SituacaoEtapa != SituacaoEtapa.Liberada && item.SituacaoEtapa != SituacaoEtapa.Encerrada))
                {
                    item.ExibeEscalonamentoDesabilitado = false;
                }
            }

            result.CamposReadOnly = false;
            result.ExibeMensagemInformativa = false;
            result.MensagemInformativa = string.Empty;

            if (result.ProcessoEncerrado)
            {
                result.CamposReadOnly = true;
                result.ExibeMensagemInformativa = true;
                result.MensagemInformativa = MessagesResource.MSG_CamposGrupoEscalonamento_ProcessoEncerrado;
            }
            else if (quantidadeSolicitacoes > 0 && result.Itens.Any(a => a.SituacaoEtapa == SituacaoEtapa.Liberada || a.SituacaoEtapa == SituacaoEtapa.Encerrada))
            {
                result.ExibeMensagemInformativa = true;
                result.MensagemInformativa = MessagesResource.MSG_CamposGrupoEscalonamento_SolicitacaoAssociadaBloquearEscalonamento;
            }
            //else if (result.Ativo)
            //{
            //    result.CamposReadOnly = true;
            //    result.ExibeMensagemInformativa = true;
            //    result.MensagemInformativa = MessagesResource.MSG_CamposGrupoEscalonamento_GrupoAtivo;
            //}
            //else if (quantidadeSolicitacoes > 0 && result.Itens.Any(a => a.SituacaoEtapa == SituacaoEtapa.Liberada))
            //{
            //    result.CamposReadOnly = true;
            //    result.ExibeMensagemInformativa = true;
            //    result.MensagemInformativa = MessagesResource.MSG_CamposGrupoEscalonamento_SolicitacaoAssociada;
            //}

            return result.Transform<GrupoEscalonamentoVO>();
        }

        public GrupoEscalonamentoVO ValidarGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            var grupoEscalonamento = BuscarGrupoEscalonamento(seqGrupoEscalonamento);
            ValidarModelo(grupoEscalonamento);

            foreach (var item in grupoEscalonamento.Itens)
            {
                var itemComParcela = this.GrupoEscalonamentoItemDomainService.BuscarGrupoEscalonamentoItem(item.Seq);

                this.GrupoEscalonamentoItemDomainService.ValidarModeloConfigurarParcelas(itemComParcela);
                ValidarParcelasGrupoEscalonamento(itemComParcela);
            }


            var result = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamento));
            result.Ativo = true;

            this.SaveEntity(result);

            return result.Transform<GrupoEscalonamentoVO>();
        }

        public GrupoEscalonamentoVO DesativarGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            var result = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamento));
            result.Ativo = false;

            this.SaveEntity(result);

            return result.Transform<GrupoEscalonamentoVO>();
        }

        public GrupoEscalonamentoCabecalhoVO BuscarCabecalhoGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            var grupoEscalonamento = SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamento), g => new
            {
                g.SeqProcesso,
                DescricaoProcesso = g.Processo.Descricao,
                DescricaoCicloLetivo = g.Processo.CicloLetivo.Descricao,
                g.Processo.DataInicio,
                g.Processo.DataFim,
                g.Processo.DataEncerramento,
                SeqGrupoEscalonamento = g.Seq,
                DescricaoGrupoEscalonamento = g.Descricao,
                g.Ativo,
                ProcessoEncerrado = g.Processo.DataEncerramento.HasValue && g.Processo.DataEncerramento.Value < DateTime.Now,
                ProcessoExpirado = g.Processo.DataFim.HasValue && DateTime.Now > g.Processo.DataFim.Value,
                PossuiParcelas = g.Itens.Any(a => a.Parcelas.Any()),
                g.Processo.Servico.PermiteReabrirSolicitacao,
                g.Processo.Servico.TokenPermissaoManutencaoProcesso,
                PermitirNotificacao = g.Processo.Etapas.FirstOrDefault(f => f.Ordem == 1).ConfiguracoesNotificacao.Any(a => a.TipoNotificacao.Token.Equals(TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PERIODO_VIGENCIA)),
                Itens = g.Itens.Select(i => new
                {
                    i.Seq,
                    i.SeqEscalonamento,
                    i.Escalonamento.DataFim,
                    i.Escalonamento.ProcessoEtapa.SituacaoEtapa
                })
            });

            var modelo = new GrupoEscalonamentoCabecalhoVO()
            {
                SeqProcesso = grupoEscalonamento.SeqProcesso,
                DescricaoProcesso = grupoEscalonamento.DescricaoProcesso,
                DescricaoCicloLetivo = grupoEscalonamento.DescricaoCicloLetivo,
                DataInicio = grupoEscalonamento.DataInicio,
                DataFim = grupoEscalonamento.DataFim,
                DataEncerramento = grupoEscalonamento.DataEncerramento,
                SeqGrupoEscalonamento = grupoEscalonamento.SeqGrupoEscalonamento,
                DescricaoGrupoEscalonamento = grupoEscalonamento.DescricaoGrupoEscalonamento,
                Ativo = grupoEscalonamento.Ativo,
                ProcessoEncerrado = grupoEscalonamento.ProcessoEncerrado,
                ProcessoExpirado = grupoEscalonamento.ProcessoExpirado,
                PossuiParcelas = grupoEscalonamento.PossuiParcelas,
                TodasEtapasEncerradas = grupoEscalonamento.Itens.Any(a => (a.SituacaoEtapa != SituacaoEtapa.Liberada && a.SituacaoEtapa != SituacaoEtapa.EmManutencao && a.SituacaoEtapa != SituacaoEtapa.AguardandoLiberacao)),
                NaoPermiteAssociarSolicitacaoEtapasExpiradas = !grupoEscalonamento.Itens.Any(a => a.DataFim >= DateTime.Now),
                PermiteReabrirSolicitacao = grupoEscalonamento.PermiteReabrirSolicitacao,
                PermitirNotificacao = grupoEscalonamento.PermitirNotificacao
            };

            var listaQuantidadeSolicitacoes = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = grupoEscalonamento.SeqProcesso }).Select(s =>
            new
            {
                s.SeqGrupoEscalonamento,
                s.QuantidadeSolicitacoes
            }).ToList();

            if (listaQuantidadeSolicitacoes.Any(a => a.SeqGrupoEscalonamento == grupoEscalonamento.SeqGrupoEscalonamento))
            {
                modelo.QuantidadeSolicitacoes = listaQuantidadeSolicitacoes.FirstOrDefault(f => f.SeqGrupoEscalonamento == grupoEscalonamento.SeqGrupoEscalonamento).QuantidadeSolicitacoes;
            }

            foreach (var grupoEscalonamentoItem in grupoEscalonamento.Itens)
            {
                var parcelas = this.GrupoEscalonamentoItemDomainService.BuscarGrupoEscalonamentoItem(grupoEscalonamentoItem.Seq).Parcelas;
                var escalonamento = this.EscalonamentoDomainService.BuscarEscalonamento(grupoEscalonamentoItem.SeqEscalonamento);

                foreach (var parcela in parcelas)
                {
                    if (parcela.DataVencimentoParcela < DateTime.Parse(escalonamento.DataFim.SMCDataAbreviada()))
                    {
                        modelo.PossuiParcelasVencimentoMenorEscalonamento = true;
                    }
                }
            }

            modelo.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(grupoEscalonamento.TokenPermissaoManutencaoProcesso);

            return modelo;
        }

        public GrupoEscalonamentoVO BuscarConfiguracaoEscalonamento(long seqProcesso)
        {
            var spec = new ProcessoFilterSpecification() { Seq = seqProcesso };

            spec.SetOrderBy(x => x.Etapas.FirstOrDefault().Ordem);

            //Listar etapas e seus escalonamentos que não foram encerrado e cadastrados para as etapas do processo em questão
            var result = this.ProcessoDomainService.SearchProjectionByKey(spec, p => new GrupoEscalonamentoVO()
            {
                SeqProcesso = p.Seq,
                ObrigarNumeroDivisaoParcelas = p.Servico.InstituicaoNivelServicos.Any(i => !i.InstituicaoNivelTipoVinculoAluno.ExigeCurso),
                ExibeMensagemSalvar = p.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU || p.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                ExibeMensagemTokenDisciplinaIsolada = p.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                ExibeNumeroDivisaoParcelasDesabilitado = false,
                Itens = p.Etapas.Select(e => new GrupoEscalonamentoItemVO()
                {
                    SeqProcessoEtapa = e.Seq,
                    DescricaoEtapa = e.DescricaoEtapa,
                    FinalizacaoEtapaAnterior = e.FinalizacaoEtapaAnterior,
                    SituacaoEtapa = e.SituacaoEtapa,
                    ExibeEscalonamentoDesabilitado = false,
                    Escalonamentos = e.Escalonamentos.Where(w => !w.DataEncerramento.HasValue).OrderBy(o => o.DataInicio)
                    .Select(es => new EscalonamentoEtapaItemVO() { Seq = es.Seq, DataInicio = es.DataInicio, DataFim = es.DataFim, DataEncerramento = es.DataEncerramento })
                }).ToList()
            });

            //Valida se o processo não está vigência ou futuro
            var processoVigente = (DateTime.Now >= result.DataInicio && (!result.DataFim.HasValue || DateTime.Now <= result.DataFim.Value));
            var processoFuturo = result.DataInicio > DateTime.Now;
            if (!processoVigente && !processoFuturo)
            {
                throw new GrupoEscalonamentoVigenciaExpiradaException();
            }

            //Valida se todas as etapas do processo estão encerradas
            if (result.Itens.All(a => a.SituacaoEtapa == SituacaoEtapa.Encerrada))
            {
                throw new GrupoEscalonamentoProcessoComEtapaEncerradaException();
            }

            //Senão, se uma etapa possue um escalonamento vigente ou futuro
            var existeEscalonamentoVigente = result.Itens.SelectMany(s => s.Escalonamentos.Where(w => (DateTime.Now >= w.DataInicio && DateTime.Now <= w.DataFim) || w.DataInicio > DateTime.Now)).ToList().SMCAny();
            if (!existeEscalonamentoVigente)
            {
                throw new GrupoEscalonamentoProcessoTodosEscalonamentosExpiradosException();
            }

            ///Exibir legenda e icone da finalização de etapa
            foreach (var item in result.Itens)
            {
                if (item.FinalizacaoEtapaAnterior)
                {
                    item.Legenda = GrupoEscalonamentoParametros.FinalizaçãoEtapaAnterior;
                }
                else
                {
                    item.Legenda = GrupoEscalonamentoParametros.Nenhum;
                }
            }

            return result.Transform<GrupoEscalonamentoVO>();
        }

        public void ExcluirGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            // Busca o processo do grupo de escalonamento sendo excluido
            var specGrupo = new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamento);
            var seqProcesso = this.SearchProjectionByKey(specGrupo, x => x.SeqProcesso);

            // Busca os outros grupos de escalonamento do mesmo processo
            var specGrupoProcesso = new GrupoEscalonamentoFilterSpecification() { SeqProcesso = seqProcesso };
            var gruposProcesso = this.SearchBySpecification(specGrupoProcesso).ToList();

            // Caso possua outro grupo escalonamento no processo ativo, permite a exclusão. Senão erro
            if (gruposProcesso.Any(x => x.Seq != seqGrupoEscalonamento && x.Ativo))
            {
                // Abre transação para exclusão
                using (var unitOfWork = SMCUnitOfWork.Begin())
                {
                    // Excluir em cascata os parametros de agendamento de notificação
                    var parametroEnvioSpec = new ParametroEnvioNotificacaoFilterSpecification { SeqGrupoEscalonamento = seqGrupoEscalonamento };
                    var listaParametros = ParametroEnvioNotificacaoDomainService.SearchBySpecification(parametroEnvioSpec).ToList();
                    foreach (var parametro in listaParametros)
                    {
                        ParametroEnvioNotificacaoDomainService.DeleteEntity(parametro);
                    }

                    // Excluir em castata os itens do grupo de escalonamento e suas parcelas associadas
                    var itemSpec = new GrupoEscalonamentoItemFilterSpecification() { SeqGrupoEscalonamento = seqGrupoEscalonamento };
                    var listaItens = GrupoEscalonamentoItemDomainService.SearchBySpecification(itemSpec).ToList();
                    foreach (var item in listaItens)
                    {
                        var parcelaSpec = new GrupoEscalonamentoItemParcelaFilterSpecification() { SeqGrupoEscalonamentoItem = item.Seq };
                        var listaParcela = GrupoEscalonamentoItemParcelaDomainService.SearchBySpecification(parcelaSpec).ToList();
                        foreach (var parcela in listaParcela)
                        {
                            GrupoEscalonamentoItemParcelaDomainService.DeleteEntity(parcela);
                        }
                        GrupoEscalonamentoItemDomainService.DeleteEntity(item);
                    }

                    // Exclui o grupo de escalonamento
                    this.DeleteEntity(seqGrupoEscalonamento);

                    // Faz o commit da exclusão
                    unitOfWork.Commit();
                }
            }
            else
            {
                throw new NaoExisteOutroGrupoEscalonamentoAtivoException();
            }
        }

        public long SalvarGrupoEscalonamento(GrupoEscalonamentoVO modelo)
        {
            ValidarModelo(modelo);

            var grupoEscalonamento = modelo.Transform<GrupoEscalonamento>();

            var exibiuAssertAlteracaoFatorDivisao = ValidarAssertSalvar(modelo);

            if (exibiuAssertAlteracaoFatorDivisao)
            {
                grupoEscalonamento.Ativo = false;
            }

            this.SaveEntity(grupoEscalonamento);

            return grupoEscalonamento.Seq;
        }

        /// <summary>
        /// Manter somente até ser validado as novas regras de validação
        /// </summary>
        /// <param name="modelo"></param>
        private void ValidarModelo_old(GrupoEscalonamentoVO modelo)
        {
            //Se o grupo de escalonamento tiver mais do que ois itens (etapa - escalonamento) e a etapa em questão
            //está configurada para “Permite iniciar a vigência da etapa somente após o fim da vigência da etapa
            //anterior ?”, caso esteja, verificar se a Data / hora Início do escalonamento da etapa em questão
            //é MAIOR do que a Data / Hora Fim do escalonamento da etapa anterior.
            //Caso NÃO ocorra, abortar a operação e exibir mensagem de erro
            if (modelo.Itens.Count > 2)
            {
                //Buscar processo em questão
                var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), IncludesProcesso.Etapas_Escalonamentos);

                //para cada item (etapa e escalonamento)
                foreach (var item in modelo.Itens)
                {
                    //Busca o processo etapa em questão
                    var processoEtapa = this.ProcessoEtapaDomainService.BuscarProcessoEtapa(item.SeqProcessoEtapa);

                    //Recupera as etapas do processo no SGF, ordenadas pelo numero de ordem
                    var etapasSgf = this.EtapaService.BuscarEtapas(processo.Etapas.Select(e => e.SeqEtapaSgf).ToArray());

                    //Recupera o numero de ordem da etapa que está sendo gravada
                    var numeroOrdemSgf = etapasSgf.First(e => e.Seq == processoEtapa.SeqEtapaSgf).Ordem;

                    //Recupera a etapa anterior do SGF a que está sendo gravada
                    var etapaAnteriorSgf = etapasSgf.Where(e => e.Ordem < numeroOrdemSgf).OrderByDescending(e => e.Ordem).FirstOrDefault();

                    if (etapaAnteriorSgf != null)
                    {
                        //Caso exista etapa anterior do SGF
                        //Recupera o seq da etapa anterior da lista de etapas do processo
                        var seqEtapaAnterior = processo.Etapas.FirstOrDefault(e => e.SeqEtapaSgf == etapaAnteriorSgf.Seq).Seq;

                        //Recupera o seq do escalonamento da etapa anterior da lista de itens do grupo de escalonamento
                        var seqEscalonamentoEtapaAnterior = modelo.Itens.FirstOrDefault(i => i.SeqProcessoEtapa == seqEtapaAnterior).SeqEscalonamento;

                        //Recupera a data inicio do escalonamento da etapa em questão
                        var dataIniciopEscalonamento = processoEtapa.Escalonamentos.FirstOrDefault(e => e.Seq == item.SeqEscalonamento).DataInicio;

                        //Recupera a data fim do escalonamento da etapa anterior
                        var dataInicioEscalonamentoAnterior = processo.Etapas.FirstOrDefault(e => e.Seq == seqEtapaAnterior).Escalonamentos.FirstOrDefault(e => e.Seq == seqEscalonamentoEtapaAnterior).DataInicio;

                        //Recupera a data fim do escalonamento da etapa anterior
                        var dataFimEscalonamentoAnterior = processo.Etapas.FirstOrDefault(e => e.Seq == seqEtapaAnterior).Escalonamentos.FirstOrDefault(e => e.Seq == seqEscalonamentoEtapaAnterior).DataFim;

                        //Caso a etapa esteja configurada para permitir iniciar a vigência da etapa somente após o fim
                        //da vigência da etapa anterior
                        if (processoEtapa.FinalizacaoEtapaAnterior)
                        {
                            //Caso a data inicio do escalonamento seja menor ou igual a data fim do escalonamento anterior
                            if (dataIniciopEscalonamento <= dataFimEscalonamentoAnterior)
                                throw new GrupoEscalonamentoDataInicioMaiorDataFimAnteriorException();
                        }
                        else
                        {
                            //Caso a data inicio do escalonamento seja menor que a data inicio do escalonamento anterior
                            if (dataIniciopEscalonamento < dataInicioEscalonamentoAnterior)
                                throw new GrupoEscalonamentoDataInicioMenorDataInicioAnteriorException();
                        }
                    }
                }
            }

            ///Se todas as etapas x escalonamento do grupo estiverem expirado, exibir a seguinte mensagem impeditiva:
            int totalEscalonmentosEncerrados = 0;
            foreach (var item in modelo.Itens)
            {
                var dataFimEscalonamento = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(item.SeqEscalonamento), p => p.DataFim);

                if (dataFimEscalonamento < DateTime.Now)
                {
                    totalEscalonmentosEncerrados++;
                }
            }

            ///Cada etapa tem somente um escalonamento
            if (modelo.Itens.Count() == totalEscalonmentosEncerrados)
            {
                throw new GrupoEscalonamentoProcessoEtapasExpiradasException(modelo.Seq == 0 ? INCLUSAO : ALTERACAO);
            }
        }

        /// <summary>
        /// Validar as regras do modelo
        /// </summary>
        /// <param name="modelo"></param>
        public void ValidarModelo(GrupoEscalonamentoVO modelo)
        {
            ///Recupera todas as etapas do processo e as coloca em ordem
            var includesProcesso = IncludesProcesso.Etapas | IncludesProcesso.Etapas_Escalonamentos | IncludesProcesso.Servico_TiposNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_TipoNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_ParametrosEnvioNotificacao;
            var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), x => x.Servico);
            var todasEtapasProcesso = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), includesProcesso).Etapas.OrderBy(o => o.Ordem).ToList();

            ///Se todas as etapas x escalonamento do grupo estiverem expirado, exibir a seguinte mensagem impeditiva:
            int totalEscalonamentosEncerrados = 0;
            foreach (var item in modelo.Itens)
            {
                var dataFimEscalonamento = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(item.SeqEscalonamento), p => p.DataFim);

                if (dataFimEscalonamento < DateTime.Now)
                {
                    totalEscalonamentosEncerrados++;
                }
            }

            ///Cada etapa tem somente um escalonamento
            if (modelo.Itens.Count() == totalEscalonamentosEncerrados)
            {
                throw new GrupoEscalonamentoProcessoEtapasExpiradasException(modelo.Seq == 0 ? INCLUSAO : ALTERACAO);
            }

            for (int i = 0; i < todasEtapasProcesso.Count(); i++)
            {
                var indiceEtapaAnterior = i - 1;
                var indiceEtapaPosterior = i + 1;
                ///Seleciona dados da etapa atual e recupera dados necessarios para continuar validação
                var etapaAtual = modelo.Itens.FirstOrDefault(f => f.SeqProcessoEtapa == todasEtapasProcesso[i].Seq);
                etapaAtual.FinalizacaoEtapaAnterior = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(etapaAtual.SeqProcessoEtapa), p => p.FinalizacaoEtapaAnterior);
                var escalonamentosAtual = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(etapaAtual.SeqEscalonamento), p => new { p.DataInicio, p.DataFim });
                etapaAtual.DataIncioEscalonamento = escalonamentosAtual.DataInicio;
                etapaAtual.DataFimEscalonamento = escalonamentosAtual.DataFim;

                ///Verifica se existe etapa anterior e o ind_finalizacao_etapa_anterior é true
                if (etapaAtual.FinalizacaoEtapaAnterior && indiceEtapaAnterior >= 0)
                {
                    ///Seleciona dados da etapa anterior e recupera dados necessarios para continuar validação
                    var etapaAnterior = modelo.Itens.FirstOrDefault(f => f.SeqProcessoEtapa == todasEtapasProcesso[indiceEtapaAnterior].Seq);
                    var escalonamentosAnterior = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(etapaAnterior.SeqEscalonamento), p => new { p.DataInicio, p.DataFim });
                    etapaAnterior.DataIncioEscalonamento = escalonamentosAnterior.DataInicio;
                    etapaAnterior.DataFimEscalonamento = escalonamentosAnterior.DataFim;

                    ///1. Se o parâmetro da etapa, ind_finalizacao_etapa_anterior for Sim e há etapa anterior:
                    ///A data/hora início do escalonamento deverá ser maior que a maior data/hora fim do escalonamento da
                    ///etapa anterior se houver. Em caso de violação, abortar operação e exibir a mensagem: "Operação não
                    ///permitida.Para a etapa configurada para permitir iniciar a vigência somente após o fim da vigência da
                    ///etapa anterior: a data início do escalonamento deve ser maior do que a data fim do escalonamento da
                    ///etapa anterior."
                    if (etapaAtual.DataIncioEscalonamento < etapaAnterior.DataFimEscalonamento)
                    {
                        throw new GrupoEscalonamentoFinalizacaoSimDataInicioMaiorDataFimAnteriorException();
                    }
                }

                ///Verifica se existe etapa anterior e o ind_finalizacao_etapa_anterior é false
                if (!etapaAtual.FinalizacaoEtapaAnterior && indiceEtapaAnterior >= 0)
                {
                    ///Seleciona dados da etapa anterior e recupera dados necessarios para continuar validação
                    var etapaAnterior = modelo.Itens.FirstOrDefault(f => f.SeqProcessoEtapa == todasEtapasProcesso[indiceEtapaAnterior].Seq);
                    var escalonamentosAnterior = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(etapaAnterior.SeqEscalonamento), p => new { p.DataInicio, p.DataFim });
                    etapaAnterior.DataIncioEscalonamento = escalonamentosAnterior.DataInicio;
                    etapaAnterior.DataFimEscalonamento = escalonamentosAnterior.DataFim;

                    ///2. Se o parâmetro da etapa, ind_finalizacao_etapa_anterior for Não:
                    ///A data / hora fim do escalonamento não pode ser menor do que a data / hora fim da etapa anterior se
                    ///houver.Em caso de violação, abortar a operação e exibir a mensagem: "Operação não permitida.Uma
                    ///etapa não pode terminar antes do término da etapa anterior."
                    if (etapaAtual.DataFimEscalonamento < etapaAnterior.DataFimEscalonamento)
                    {
                        throw new GrupoEscalonamentoFinalizacaoNaoDataFimMenorDataFimAnteriorException();
                    }

                    ///A data/hora início do escalonamento não pode ser menor do que a data/hora início da etapa anterior
                    ///se houver. Em caso de violação, abortar a operação e exibir a mensagem: "Operação não permitida.
                    ///Uma etapa não pode iniciar antes da etapa anterior."
                    if (etapaAtual.DataIncioEscalonamento < etapaAnterior.DataIncioEscalonamento)
                    {
                        throw new GrupoEscalonamentoFinalizacaoNaoDataInicioMenorDataInicioAnteriorException();
                    }
                }

                ///Verifica se existe etapa posterior e o ind_finalizacao_etapa_anterior é false
                if (!etapaAtual.FinalizacaoEtapaAnterior && indiceEtapaPosterior < todasEtapasProcesso.Count())
                {
                    ///Seleciona dados da etapa posterior e recupera dados necessarios para continuar validação
                    var etapaPosterior = modelo.Itens.FirstOrDefault(f => f.SeqProcessoEtapa == todasEtapasProcesso[indiceEtapaPosterior].Seq);
                    var escalonamentosPosterior = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(etapaPosterior.SeqEscalonamento), p => new { p.DataInicio, p.DataFim });
                    etapaPosterior.DataIncioEscalonamento = escalonamentosPosterior.DataInicio;
                    etapaPosterior.DataFimEscalonamento = escalonamentosPosterior.DataFim;

                    ///A data/hora fim do escalonamento não pode ser maior do que a data/hora fim da etapa posterior se
                    ///houver.Em caso de violação, abortar a operação e exibir a mensagem: "Operação não permitida.Uma
                    ///etapa não pode terminar depois do término da etapa posterior."
                    if (etapaAtual.DataFimEscalonamento > etapaPosterior.DataFimEscalonamento)
                    {
                        throw new GrupoEscalonamentoFinalizacaoNaoDataFimMaiorDataFimPosteriorException();
                    }

                    ///A data/ hora início do escalonamento não pode ser maior do que a data / hora início da etapa posterior,
                    ///se houver.Em caso de violação, abortar a operação e exibir a mensagem: "Operação não permitida.
                    ///Uma etapa não pode iniciar antes da etapa anterior."
                    if (etapaAtual.DataIncioEscalonamento > etapaPosterior.DataIncioEscalonamento)
                    {
                        throw new GrupoEscalonamentoFinalizacaoNaoDataInicioMaiorDataInicioPosteriorException();
                    }
                }
            }

            ///3. Caso o serviço possua o token SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA, consistir a
            ///seguinte regra:
            if (processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
            {
                var listaEtapasOrdenadas = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), x => x.Etapas).OrderBy(o => o.Ordem).ToList();
                var etapaDeMenorOrdem = listaEtapasOrdenadas[0];

                foreach (var item in modelo.Itens)
                {
                    var escalonamento = this.EscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<Escalonamento>(item.SeqEscalonamento), x => x.ProcessoEtapa.Processo.UnidadesResponsaveis);

                    if (escalonamento.SeqProcessoEtapa == etapaDeMenorOrdem.Seq)
                    {
                        var seqEntidadeResponsavel = escalonamento.ProcessoEtapa.Processo.UnidadesResponsaveis.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).SeqEntidadeResponsavel;
                        var cursoOfertaLocalidades = CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(seqEntidadeResponsavel);

                        if (cursoOfertaLocalidades.Any() && escalonamento.ProcessoEtapa.Processo.SeqCicloLetivo.HasValue)
                        {
                            var cursoOfertaLocalidade = cursoOfertaLocalidades.First();
                            var cursoOfertaLocalidadeTurnos = CursoOfertaLocalidadeTurnoDomainService.BuscarTurnosPorLocalidadeCusroOfertaSelect(cursoOfertaLocalidade.RecuperarSeqLocalidade(), cursoOfertaLocalidade.SeqCursoOferta);

                            if (cursoOfertaLocalidadeTurnos.Any())
                            {
                                var seqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnos.First().Seq;

                                var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(escalonamento.ProcessoEtapa.Processo.SeqCicloLetivo.Value, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                                #region Recuperando os meses do ciclo letivo

                                //Lista que terá os meses do ciclo letivo
                                List<int> mesesCicloLetivo = new List<int>() { datasEventoLetivo.DataInicio.Month };

                                //Variáveis para auxiliar a verificar qual o último mês do ciclo
                                var achouMesFimCiclo = false;
                                var contadorMeses = 1;

                                //Recupera os meses do ciclo, se a data for menor ou igual que a data fim do ciclo, ela será validada
                                while (!achouMesFimCiclo)
                                {
                                    var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMeses);

                                    /*Validação para adicionar na lista somente as datas que são menores que a data fim
                                    Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
                                    último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
                                    if (dataVerificar <= datasEventoLetivo.DataFim)
                                        mesesCicloLetivo.Add(dataVerificar.Month);
                                    else
                                        achouMesFimCiclo = true;

                                    contadorMeses++;
                                }

                                var ultimoMesCiclo = mesesCicloLetivo.Last();

                                var dataInicioAjustadaMesAno = datasEventoLetivo.DataInicio.Year * 12 + datasEventoLetivo.DataInicio.Month;
                                var dataFimAjustadaMesAno = datasEventoLetivo.DataFim.Year * 12 + datasEventoLetivo.DataFim.Month;
                                var qtdeMesesCicloLetivo = dataFimAjustadaMesAno - dataInicioAjustadaMesAno;
                                if (datasEventoLetivo.DataInicio.Day > datasEventoLetivo.DataFim.Day)
                                {
                                    qtdeMesesCicloLetivo--;
                                }

                                //Trecho para considerar a quantidade de meses do ciclo letivo contando o primeiro mês 
                                qtdeMesesCicloLetivo = mesesCicloLetivo.Count();

                                #endregion

                                ///3.1. Verificar se a data fim de escalonamento da etapa de menor ordem é menor que a data início 
                                ///do [ciclo letivo]*. Caso seja, o fator de divisão pode ser menor ou igual à quantidade de meses 
                                ///compreendidos no ciclo letivo. Se isso não ocorrer, abortar a operação e exibir a seguinte 
                                ///mensagem de erro: "Não é possível prosseguir. As parcelas não podem ultrapassar o fim do ciclo 
                                ///letivo, portanto de acordo com a data fim informada para o escalonamento da primeira etapa, o 
                                ///fator de divisão deve ser menor ou igual a [quantidade de meses do ciclo letivo]."
                                if (escalonamento.DataFim.Date < datasEventoLetivo.DataInicio.Date)
                                {
                                    if (modelo.NumeroDivisaoParcelas.HasValue &&
                                        modelo.NumeroDivisaoParcelas.Value > qtdeMesesCicloLetivo)
                                        throw new GrupoEscalonamentoEscalonamentoParcelasUltrapassamFimCicloLetivoException(qtdeMesesCicloLetivo);
                                }
                                ///3.1.1. Caso não seja, verificar se a data fim do escalonamento está compreendido no último mês 
                                ///do [ciclo letivo]*. Se estiver, o fator de divisão só poderá ser igual a 1. Caso isto não 
                                ///ocorra, abortar a operação e exibir a seguinte mensagem de erro: "Não é possível prosseguir. 
                                ///As parcelas não podem ultrapassar o fim do ciclo letivo, portanto de acordo com a data fim 
                                ///informada para o escalonamento da primeira etapa, o fator de divisão deve ser igual a 1."
                                else if (escalonamento.DataFim.Month == ultimoMesCiclo)
                                {
                                    if (modelo.NumeroDivisaoParcelas.HasValue && modelo.NumeroDivisaoParcelas.Value != 1)
                                        throw new GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloFatorUmException();
                                }
                                ///3.1.1.1. Caso a data fim do escalonamento esteja nos demais meses do [ciclo letivo]*, verificar
                                ///qual é a ordem do mês do ciclo letivo (em número natural) que a data fim do escalamento da 
                                ///etapa de menor ordem está. O fator de divisão poderá ser igual ou menor à quantidade de meses 
                                ///do ciclo letivo menos a ordem do mês encontrado. Se isso não ocorrer, abortar a operação e 
                                ///exibir a seguinte mensagem de erro: "Não é possível prosseguir. As parcelas não podem 
                                ///ultrapassar o fim do ciclo letivo, portanto de acordo com a data fim informada para o 
                                ///escalonamento da primeira etapa, o fator de divisão deve ser menor ou igual a 
                                ///[quantidade de meses do ciclo letivo – ordem do mês no ciclo letivo]."
                                else
                                {
                                    var ordemMesCicloLetivoDataFimEscalonamento = 0;

                                    for (int i = 0; i < mesesCicloLetivo.Count(); i++)
                                    {
                                        var mes = mesesCicloLetivo[i];
                                        var auxiliarIndice = i;

                                        if (mes == escalonamento.DataFim.Month)
                                        {
                                            ordemMesCicloLetivoDataFimEscalonamento = auxiliarIndice + 1;
                                            break;
                                        }
                                    }

                                    var qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento = qtdeMesesCicloLetivo - ordemMesCicloLetivoDataFimEscalonamento;

                                    if (modelo.NumeroDivisaoParcelas.HasValue &&
                                        modelo.NumeroDivisaoParcelas.Value > qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento)
                                        throw new GrupoEscalonamentoEscalonamentoParcelasUltrapassamCicloDemaisMesesException(qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento);
                                }
                            }
                        }
                    }
                }
            }

            /// 4. Verificar se existem tipos de notificação parametrizados como obrigatório nas etapas, 
            /// para o serviço em questão, cujo campo "Permite agendamento" possui valor SIM. 
            /// Caso exista, verificar se esses tipos estão associados em cada etapa em que é obrigatório 
            /// E se possuem pelo menos um parâmetro de envio para o grupo em questão. 
            /// Caso não existir, abortar a operação e exibir a seguinte mensagem impeditiva
            /// OBG: NÃO SE APLICA PARA NOVO GRUPO DE ESCALONAMENTO, POIS O GRUPO É CADASTRADO ANTES DOS PÂRAMETROS DE ENVIO DE NOTIFICAÇÃO.
            if (modelo.Seq != 0)
            {
                string mensagemErro = "";
                foreach (var etapa in todasEtapasProcesso)
                {
                    var tiposNotificacaoObrigatoriosParaEtapa = etapa.Processo.Servico.TiposNotificacao.Where(c => c.SeqEtapaSgf == etapa.SeqEtapaSgf && c.Obrigatorio).ToList();
                    if (tiposNotificacaoObrigatoriosParaEtapa.Count > 0)
                    {
                        var notificacoesObrigatoriasCadastradasNaEtapa = etapa.ConfiguracoesNotificacao.Where(c => tiposNotificacaoObrigatoriosParaEtapa.Select(d => d.SeqTipoNotificacao).Contains(c.SeqTipoNotificacao)).ToList();
                        var notificacoesPermitemAgendamento = notificacoesObrigatoriasCadastradasNaEtapa.Where(c => c.TipoNotificacao.PermiteAgendamento).ToList();

                        var seqsNotificacoesObrigatoriasNaEtapa = tiposNotificacaoObrigatoriosParaEtapa.Select(c => c.SeqTipoNotificacao).ToList();
                        var seqsNotificacoesObrigatoriasCadastradas = notificacoesObrigatoriasCadastradasNaEtapa.Select(c => c.SeqTipoNotificacao).ToList();
                        var seqsObrigatoriosNaoCadastradosNaEtapa = seqsNotificacoesObrigatoriasNaEtapa.Where(c => !seqsNotificacoesObrigatoriasCadastradas.Contains(c)).ToList();

                        // Se a notificação não está cadastrada na etapa então é preciso buscar diretamente em banco para verificar se a mesma permite agendamento
                        if (seqsObrigatoriosNaoCadastradosNaEtapa.Count > 0)
                        {
                            var notificacoesSalvas = TipoNotificacaoDomainService.SearchAll().ToList();
                            var notificacoesBancoNaoPermitemAgendamento = notificacoesSalvas.Where(c => seqsObrigatoriosNaoCadastradosNaEtapa.Contains(c.Seq) && c.PermiteAgendamento == false).ToList();

                            foreach (var item in notificacoesBancoNaoPermitemAgendamento)
                            {
                                seqsObrigatoriosNaoCadastradosNaEtapa.Remove(item.Seq);
                            }
                        }

                        if (notificacoesPermitemAgendamento.Count > 0 || seqsObrigatoriosNaoCadastradosNaEtapa.Count > 0)
                        {
                            bool notificacoesTemParametro = notificacoesPermitemAgendamento.All(c => c.ParametrosEnvioNotificacao.Count(p => p.SeqGrupoEscalonamento == modelo.Seq) > 0);

                            if (!notificacoesTemParametro || seqsObrigatoriosNaoCadastradosNaEtapa.Count > 0)
                            {
                                mensagemErro += $"<br><b>{etapa.DescricaoEtapa}</b>";
                                var notificacoesSemParametros = notificacoesPermitemAgendamento.Where(c => c.ParametrosEnvioNotificacao.Count(p => p.SeqGrupoEscalonamento == modelo.Seq) == 0).ToList();
                                var seqsTiposNotificacao = notificacoesSemParametros.Select(c => c.SeqTipoNotificacao).ToList();

                                if (seqsObrigatoriosNaoCadastradosNaEtapa.Count > 0)
                                    seqsTiposNotificacao.AddRange(seqsObrigatoriosNaoCadastradosNaEtapa);

                                var tiposNotificacaoRetorno = this.NotificacaoService.BuscarTiposNotificacao(seqsTiposNotificacao.ToArray());

                                var ultimoItem = seqsTiposNotificacao.Last();
                                foreach (var tipoNotificacalFaltante in seqsTiposNotificacao)
                                {
                                    mensagemErro += $"<br>- {tiposNotificacaoRetorno.FirstOrDefault(c => c.Seq == tipoNotificacalFaltante).Descricao}";
                                    if (ultimoItem.Equals(tipoNotificacalFaltante))
                                    {
                                        mensagemErro += "<br>";
                                    }
                                }
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    throw new GrupoEscalonamentoProcessoEtapasComNotificacaoObrigatoriaSemParametrosEnvioException(mensagemErro);
                }
            }
        }

        public bool ValidarAssertSalvar(GrupoEscalonamentoVO modelo)
        {
            //IRÁ EXIBIR O ASSERT AO SALVAR UM GRUPO DE ESCALONAMENTO SE O GRUPO DE ESCALONAMENTO POSSUI CONFIGURAÇÕES DE PARCELA E, 
            //O VALOR DO CAMPO FATOR DE DIVISÃO OU ESCALONAMENTO DE ALGUMA ETAPA FOI ALTERADO

            if (modelo.Seq != 0)
            {
                var grupoEscalonamentoOld = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(modelo.Seq), a => a.Itens[0].Parcelas);
                var grupoEscalonamentoNew = modelo;
                var seqsEscalonamentosAssociadosOld = grupoEscalonamentoOld.Itens.Select(a => a.SeqEscalonamento).ToList();
                var seqsEscalonamentosAssociadosNew = grupoEscalonamentoNew.Itens.Select(a => a.SeqEscalonamento).ToList();

                /*Auxiliares para validar se algum escalonamento foi alterado, e se não houve alteração de escalonamento
                também avalia se trocou a ordem que os escalonamentos foram associados*/
                var listaDiferencas1 = seqsEscalonamentosAssociadosOld.Except(seqsEscalonamentosAssociadosNew).ToList();
                var listaDiferencas2 = seqsEscalonamentosAssociadosNew.Except(seqsEscalonamentosAssociadosOld).ToList();
                var listaFinalDiferencas = listaDiferencas1.Union(listaDiferencas2).ToList();

                foreach (var itemOld in grupoEscalonamentoOld.Itens)
                {
                    var itemNew = grupoEscalonamentoNew.Itens.FirstOrDefault(a => a.Seq == itemOld.Seq);

                    if (itemOld.Parcelas.Any() && (grupoEscalonamentoOld.NumeroDivisaoParcelas != grupoEscalonamentoNew.NumeroDivisaoParcelas || listaFinalDiferencas.Any() || itemOld.SeqEscalonamento != itemNew.SeqEscalonamento))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Realiza a cópia de um grupo de escalonamento.
        /// 1. Criar o registro do novo grupo de escalonamento com a nova descrição, para o processo em questão, e copiar o número de parcelas do grupo de origem.
        /// 2. Criar o registro para os itens do grupo de escalonamento, associando-os aos mesmos escalonamentos do grupo de origem, para cada etapa do processo em questão.
        /// 3. Criar um registro para cada item do grupo de escalonamento e copiar as configurações de parcela do grupo de origem:
        ///    - o número de parcelas;
        ///    - data de vencimento da parcela;
        ///    - percentual da parcela;
        ///    - motivo do bloqueio;
        ///    - descrição da parcela.
        /// 4. Criar um registro de parâmetro de envio de notificação para cada parâmetro do grupo de origem e copiar todos os dados.
        /// </summary>
        /// <param name="seqGrupoEscalonamentoOrigem">Sequencial do Grupo de escalonamento que será copiado.</param>
        /// <param name="descricao">Descrição do novo grupo de escalonamento.</param>
        public void CopiarGrupoEscalonamento(long seqGrupoEscalonamentoOrigem, string descricao)
        {
            GrupoEscalonamento obj = SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(seqGrupoEscalonamentoOrigem));


            var specGrupoEscalonamentoItem = new GrupoEscalonamentoItemFilterSpecification { SeqGrupoEscalonamento = seqGrupoEscalonamentoOrigem };
            var grupoEscalonamentoItem = GrupoEscalonamentoItemDomainService.SearchBySpecification(specGrupoEscalonamentoItem);

            //Criando o novo grupo de escalonamento

            obj.Descricao = descricao;
            obj.Ativo = false;
            obj.Itens = grupoEscalonamentoItem.ToList();

            foreach (var item in obj.Itens)
            {
                item.Seq = 0;
                obj.Seq = 0;
                obj.UsuarioAlteracao = null;
                obj.DataAlteracao = null;

                //De acordo com a task 63524 - Não deverão ser copiadas as parcelas ao realizar a cópia do grupo escalonamento
                //foreach (var itemParcela in item.Parcelas)
                //{
                //    itemParcela.Seq = 0;
                //    itemParcela.SeqGrupoEscalonamentoItem = 0;
                //}
            }

            //Salvando o novo grupo de escalonamento.
            SaveEntity(obj);

            //Copiando os parâmetros de envio de notificação...
            List<ParametroEnvioNotificacao> parametros = ParametroEnvioNotificacaoDomainService.BuscarParametroEnvioNotificacaoPorGrupoEscalonamento(seqGrupoEscalonamentoOrigem);
            foreach (var parametro in parametros)
            {
                parametro.Seq = 0;
                parametro.SeqGrupoEscalonamento = obj.Seq;
            }
            ParametroEnvioNotificacaoDomainService.BulkSaveEntity(parametros);
        }

        /// <summary>
        /// Copiar um grupo de escalonamento de um processo
        /// </summary>
        /// <param name="modelo">Dados do modelo</param>
        public void SalvarAssociarSolicitacaoGrupoEscalonamento(GrupoEscalonamentoVO modelo)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                ///Dados Solicitacao atual
                var spec = new SolicitacaoHistoricoSituacaoFilterSpecification() { SeqSolicitacaoServico = modelo.SeqSolicitacaoServico, ValidarDataExclusao = true };
                var solicitacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchProjectionBySpecification(spec,
                    p => new
                    {
                        SeqGrupoEscalonamento = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqGrupoEscalonamento,
                        SeqEtapaAtual = p.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                        SeqSituacaoAtualSGF = p.SeqSituacaoEtapaSgf,
                        SeqPessoaAtuacao = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqPessoaAtuacao,
                        SeqConfiguracaoEtpa = p.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                        SeqSolicitacaoServicoEtapa = p.SolicitacaoServicoEtapa.Seq,
                        Categoria = p.CategoriaSituacao
                    }).FirstOrDefault();

                ///Dados etapa atual da solicitação
                var etapaAtual = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(solicitacaoAtual.SeqEtapaAtual),
                    p => new
                    {
                        DataFim = p.Processo.GruposEscalonamento.Where(w => w.Seq == solicitacaoAtual.SeqGrupoEscalonamento).FirstOrDefault().Itens.Where(w => w.Escalonamento.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual).FirstOrDefault().Escalonamento.DataFim,
                        DataInicio = p.Processo.GruposEscalonamento.Where(w => w.Seq == solicitacaoAtual.SeqGrupoEscalonamento).FirstOrDefault().Itens.Where(w => w.Escalonamento.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual).FirstOrDefault().Escalonamento.DataInicio,
                        SitaucaoAtual = p.SituacaoEtapa,
                        SeqEtapaAtualSGF = p.SeqEtapaSgf,
                        ControleVagas = p.ControleVaga,
                        SeqSituacaoMatriculaItem = p.SituacoesItemMatricula.FirstOrDefault(f => f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq,
                    });

                var servicoAtual = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), p => new
                {
                    p.Servico.PermiteReabrirSolicitacao,
                    p.Servico.TipoPrazoReabertura,
                    p.Servico.NumeroDiasPrazoReabertura,
                    p.SeqServico,
                    p.Servico.Token
                });

                var grupoEscalonamentoNovo = this.BuscarGrupoEscalonamento(modelo.Seq);

                var grupoEscalonamentoAtual = this.BuscarGrupoEscalonamento((long)solicitacaoAtual.SeqGrupoEscalonamento);

                ///Dados solicitante da solicitação atual
                long? seqTipoVinculoSolicitante = null;
                long? seqNivelEnsinoSolicitante = null;
                long? seqInstituicaoEnsino = null;
                if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoAtual.SeqPessoaAtuacao) == TipoAtuacao.Aluno)
                {
                    var result = this.AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(solicitacaoAtual.SeqPessoaAtuacao), p => new
                    {
                        SeqTipoVinculo = p.SeqTipoVinculoAluno,
                        SeqNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                        SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino
                    });

                    seqTipoVinculoSolicitante = result.SeqTipoVinculo;
                    seqNivelEnsinoSolicitante = result.SeqNivelEnsino;
                    seqInstituicaoEnsino = result.SeqInstituicaoEnsino;
                }
                else if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoAtual.SeqPessoaAtuacao) == TipoAtuacao.Ingressante)
                {
                    var result = this.IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(solicitacaoAtual.SeqPessoaAtuacao), p => new
                    {
                        SeqTipoVinculo = p.SeqTipoVinculoAluno,
                        SeqNivelEnsino = p.SeqNivelEnsino,
                        SeqProcessoSeletivo = p.SeqProcessoSeletivo,
                        SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino
                    });

                    seqTipoVinculoSolicitante = result.SeqTipoVinculo;
                    seqNivelEnsinoSolicitante = result.SeqNivelEnsino;
                    seqInstituicaoEnsino = result.SeqInstituicaoEnsino;
                }

                ///Valida se a solicitação é uma disciplina isolada
                ///Considerando que disciplina isolanda são as que não exige curso
                var specInstituicaoTipoVinculoAluno = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsinoSolicitante, SeqTipoVinculoAluno = seqTipoVinculoSolicitante, SeqInstituicao = seqInstituicaoEnsino };
                var disciplinaIsolada = !this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(specInstituicaoTipoVinculoAluno, p => p.ExigeCurso).FirstOrDefault();

                ///Cenário que a etapa atual da solicitacao expirou e a etapa não foi encerrada
                ///Se a etapa atual da solicitação estiver expirada e, a etapa ainda não foi encerrada:
                ///• A solicitação deverá receber a situação inicial parametrizada para a etapa atual,
                ///• Se for uma solicitação de matrícula, a situação dos itens não deverá ser alterada.
                ///NÃO FAZ NADA

                ///Cenarios onde a solicitação seja uma solicitação de matricula
                var includesSolicitacaoMatricula = IncludesSolicitacaoMatricula.Itens | IncludesSolicitacaoMatricula.Itens_DivisaoTurma | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao_SituacaoItemMatricula;
                var solicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>((long)modelo.SeqSolicitacaoServico), includesSolicitacaoMatricula);
                bool atualizouHistoirco = false;
                ///Verifica é uma solicitação de matricula
                if (solicitacaoMatricula != null)
                {
                    ///Cenário que a etapa atual da solicitação foi encerrada - Disciplina isolada
                    ///Se a etapa atual da solicitação foi encerrada, o processo refere - se a solicitação de matrícula e de
                    ///disciplina isolada, deverá verificar se há disponível vaga na respectiva turma, para os itens da
                    ///solicitação que estão cancelados e o motivo igual a etapa finalizada.
                    if (solicitacaoAtual.Categoria == CategoriaSituacao.Encerrado && disciplinaIsolada && etapaAtual.ControleVagas)
                    {
                        var retornoValidacao = ValidacaoQuantidadeVagasPelaSolicitacao((long)modelo.SeqSolicitacaoServico);

                        ///Se não existir vaga para todos os itens, exibir a seguinte mensagem impeditiva:
                        if (retornoValidacao.nenhumaTemVagas)
                        {
                            throw new GrupoEscalonamentoAssociacaoSemVagasException(string.Join("<br/>", retornoValidacao.dscVagas));
                        }

                        ///Senão, se existir vaga para pelo menos um dos itens, mensagem é exibida no botão de salvar onde ele faz a verificação de vagas antes de processeguir
                        ///Senão, se existir vaga para todos os itens:
                        ///A situação dos itens deverá receber a situação finalizada com sucesso e, as vagas deverão ser
                        ///ocupadas conforme a RN_MAT_035 -Ocupar / desocupar vaga processo matrícula.
                        ///Todas as opções saão tatadas no momento em que se esta reservando as vagas
                        ReservaVagas((long)modelo.SeqSolicitacaoServico, etapaAtual.SeqSituacaoMatriculaItem);

                        ///A solicitação deverá receber a situação inicial parametrizada para a etapa atual
                        AtualizarHistoricoSolicitacao(solicitacaoAtual.SeqSolicitacaoServicoEtapa, (long)modelo.SeqSolicitacaoServico, solicitacaoAtual.SeqEtapaAtual, etapaAtual.SeqEtapaAtualSGF, grupoEscalonamentoAtual, grupoEscalonamentoNovo);
                        atualizouHistoirco = true;
                    }

                    ///Cenário que a etapa atual da solicitação foi encerrada
                    ///Se a etapa atual da solicitação foi encerrada, o processo refere - se a solicitação de matrícula e não é de disciplina isolada:
                    ///•A solicitação deverá receber a situação inicial parametrizada para a etapa atual,
                    ///•A situação dos itens de matrícula não deverá ser alterada.
                    if (!disciplinaIsolada && solicitacaoAtual.Categoria == CategoriaSituacao.Encerrado)
                    {
                        AtualizarHistoricoSolicitacao(solicitacaoAtual.SeqSolicitacaoServicoEtapa, (long)modelo.SeqSolicitacaoServico, solicitacaoAtual.SeqEtapaAtual, etapaAtual.SeqEtapaAtualSGF, grupoEscalonamentoAtual, grupoEscalonamentoNovo);
                        atualizouHistoirco = true;
                    }

                    ///Caso a solicitação de matrícula já possua uma condição de pagamento associada e o serviço possuir o token 
                    ///SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA, verificar no financeiro qual a condição de 
                    ///pagamento válida na data corrente, de acordo com a RN_MAT_076 - Exibição dados financeiros condição pagamento. 
                    ///Caso a condição de pagamento retornada seja diferente da associada à solicitação, trocar a condição para o 
                    ///sequencial retornado.
                    if (solicitacaoMatricula.SeqCondicaoPagamentoGra.HasValue &&
                        servicoAtual.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
                    {
                        //Recupera as condições de pagamento considerando o fator de divisão do grupo escalonamento a ser associado
                        var condicoesPagamentoValidas = this.SolicitacaoMatriculaDomainService.BuscarCondicoesPagamentoAcademico(solicitacaoMatricula.Seq, false, grupoEscalonamentoNovo.NumeroDivisaoParcelas);
                        var condicaoPagamentoValida = condicoesPagamentoValidas.FirstOrDefault();

                        if (condicaoPagamentoValida != null && condicaoPagamentoValida.SeqCondicaoPagamento != solicitacaoMatricula.SeqCondicaoPagamentoGra)
                        {
                            var solicitacaoMatriculaAtualizar = this.SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(solicitacaoMatricula.Seq));
                            solicitacaoMatriculaAtualizar.SeqCondicaoPagamentoGra = condicaoPagamentoValida.SeqCondicaoPagamento;

                            this.SolicitacaoMatriculaDomainService.SaveEntity(solicitacaoMatriculaAtualizar);

                            ///Caso a condição de pagamento seja trocada e exista termo de adesão associado à solicitação de matrícula,
                            ///o arquivo deve ser excluído e gerado novamente de acordo com a regra RN_MAT_106 - Geração do termo de 
                            ///adesão. Se não existir arquivo do termo, não realizar nenhuma ação.
                            if (solicitacaoMatricula.SeqTermoAdesao.HasValue)
                            {
                                //Remove o arquivo do termo de adesão atual do aluno
                                if (solicitacaoMatricula.SeqArquivoTermoAdesao.HasValue)
                                {
                                    solicitacaoMatriculaAtualizar.SeqArquivoTermoAdesao = null;
                                    this.SolicitacaoMatriculaDomainService.SaveEntity(solicitacaoMatriculaAtualizar);

                                    this.ArquivoAnexadoDomainService.DeleteEntity(new ArquivoAnexado() { Seq = solicitacaoMatricula.SeqArquivoTermoAdesao.Value });

                                    //Gerar novamente o termo de adesão
                                    ContratoDomainService.GerarTermoAdesaoContrato(solicitacaoMatricula.Seq);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (solicitacaoAtual.Categoria == CategoriaSituacao.Encerrado)
                    {
                        AtualizarHistoricoSolicitacao(solicitacaoAtual.SeqSolicitacaoServicoEtapa, (long)modelo.SeqSolicitacaoServico, solicitacaoAtual.SeqEtapaAtual, etapaAtual.SeqEtapaAtualSGF, grupoEscalonamentoAtual, grupoEscalonamentoNovo);
                        atualizouHistoirco = true;
                    }
                }

                ///Cenário que a etapa atual da solicitação é vigente ou futura
                ///Se a etapa atual da solicitação estiver vigente ou futura:
                ///• A solicitação deverá receber a situação inicial parametrizada para a etapa atual,
                ///• Se for uma solicitação de matrícula, a situação dos itens não deverá ser alterada.
                ///NÃO FAZ NADA

                ///Apos todas as validações atualizamos a solicitacao de servico com o novo grupo de escalonamento
                ///Foi removida a validação para verificar se o grupo de escalonamento é diferente do que já está associado
                ///Para entrar dar commit na base, porque deve ser gravado histórico de situação e navegação mesmo quando não se troca o grupo de escalonamento
                var solicitacaoDB = this.SolicitacaoServicoDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServico>((long)modelo.SeqSolicitacaoServico));

                ///Se por ventura atualizou historico não precisa enviar notificacao pois a prorpia função de atualização de historico efetua o envio de notificacao
                if (!atualizouHistoirco)
                {
                    this.EnviarNotificacao(solicitacaoDB.Seq, solicitacaoAtual.SeqEtapaAtual, grupoEscalonamentoNovo);
                }

                solicitacaoDB.SeqGrupoEscalonamento = modelo.Seq;

                this.SolicitacaoServicoDomainService.SaveEntity(solicitacaoDB);

                ///throw new Exception("roolback");
                ///Rollback caso alguma das funções provoquem erro
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Listar todas as vagas caso existam com respectivas descrições
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial solicitação</param>
        /// <returns>Lista da descrição de vagas, se nenhum dos itens tem vaga, se todos tem vaga e se somente algum tem vaga </returns>
        public (List<string> dscVagas, bool nenhumaTemVagas, bool todasTemVagas, bool algumasTemVagas) ValidacaoQuantidadeVagasPelaSolicitacao(long seqSolicitacaoServico)
        {
            List<string> dscVagas = new List<string>();
            bool nenhumaTemVagas = false;
            bool todasTemVagas = false;
            bool algumasTemVagas = false;
            int contadorTemVagas = 0;
            int contadorNaoTemVagas = 0;

            var includes = IncludesSolicitacaoMatricula.Itens_DivisaoTurma_Turma_ConfiguracoesComponente | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao_SituacaoItemMatricula;
            var solicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico), includes);

            if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoMatricula.SeqPessoaAtuacao) == TipoAtuacao.Ingressante)
            {
                var includesIngressante = IncludesIngressante.Ofertas_CampanhaOferta;

                var ingressante = this.IngressanteDomainService.SearchByKey(new SMCSeqSpecification<Ingressante>(solicitacaoMatricula.SeqPessoaAtuacao), includesIngressante);

                var processoSeletivoReservaVaga = this.ProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(ingressante.SeqProcessoSeletivo), p => p.ReservaVaga);

                if (processoSeletivoReservaVaga)
                {
                    int itemValido = 0;
                    foreach (var item in solicitacaoMatricula.Itens.Where(w => w.SeqDivisaoTurma != null))
                    {
                        var historicoSolicitacao = item.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).Select(s => new { s.MotivoSituacaoMatricula, s.SituacaoItemMatricula.ClassificacaoSituacaoFinal }).FirstOrDefault();

                        if (historicoSolicitacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                            && (historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.EtapaNaoFinalizada ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.ExistenciaBloqueio ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.SolicitacaoCancelada))
                        {
                            foreach (var oferta in ingressante.Ofertas)
                            {
                                ///Verifica se a turma se refere ao processo seletivo corrente
                                if (oferta.CampanhaOfertaItem.SeqTurma == item.DivisaoTurma.SeqTurma)
                                {
                                    var filtro = new ProcessoSeletivoOfertaFilterSpecification() { SeqCampanhaOferta = oferta.SeqCampanhaOferta, SeqProcessoSeletivo = ingressante.SeqProcessoSeletivo };
                                    var processoSeletivoOferta = this.ProcessoSeletivoOfertaDomainService.SearchBySpecification(filtro).FirstOrDefault();

                                    if ((processoSeletivoOferta.QuantidadeVagas - processoSeletivoOferta.QuantidadeVagasOcupadas) <= 0)
                                    {
                                        contadorNaoTemVagas++;
                                    }
                                    else
                                    {
                                        contadorTemVagas++;
                                    }

                                    var dscTurma = item.DivisaoTurma.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao;

                                    dscVagas.Add($"- {dscTurma} - {processoSeletivoOferta.QuantidadeVagas - processoSeletivoOferta.QuantidadeVagasOcupadas} vagas.");

                                    itemValido++;
                                }
                            }

                            if (contadorNaoTemVagas == itemValido)
                            {
                                nenhumaTemVagas = true;
                            }

                            if (contadorTemVagas == itemValido)
                            {
                                todasTemVagas = true;
                            }

                            if (contadorTemVagas > 0 && contadorTemVagas < itemValido)
                            {
                                algumasTemVagas = true;
                            }
                        }
                    }
                }
                else
                {
                    int itemValido = 0;
                    foreach (var item in solicitacaoMatricula.Itens.Where(w => w.SeqDivisaoTurma != null))
                    {
                        var historicoSolicitacao = item.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).Select(s => new { s.MotivoSituacaoMatricula, s.SituacaoItemMatricula.ClassificacaoSituacaoFinal }).FirstOrDefault();

                        if (historicoSolicitacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                            && (historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.EtapaNaoFinalizada ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.ExistenciaBloqueio ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.SolicitacaoCancelada))
                        {
                            if ((item.DivisaoTurma.QuantidadeVagas - item.DivisaoTurma.QuantidadeVagasOcupadas) <= 0)
                            {
                                contadorNaoTemVagas++;
                            }
                            else
                            {
                                contadorTemVagas++;
                            }

                            var dscTurma = item.DivisaoTurma.Turma.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().Descricao;

                            dscVagas.Add($"- {dscTurma} - {item.DivisaoTurma.QuantidadeVagas - item.DivisaoTurma.QuantidadeVagasOcupadas} vagas.");
                            itemValido++;
                        }
                    }

                    if (contadorNaoTemVagas == itemValido && solicitacaoMatricula.Itens.SMCAny())
                    {
                        nenhumaTemVagas = true;
                    }

                    if (contadorTemVagas == itemValido)
                    {
                        todasTemVagas = true;
                    }

                    if (contadorTemVagas > 0 && contadorTemVagas < itemValido)
                    {
                        algumasTemVagas = true;
                    }
                }
            }

            return (dscVagas, nenhumaTemVagas, todasTemVagas, algumasTemVagas);
        }

        /// <summary>
        /// Efetuar a verificação dos casos impeditivos em uma associação de nova solicitação
        /// </summary>
        /// <param name="modelo">Modelo de dados</param>
        public void ValidarAssociacaoSolicitacao(GrupoEscalonamentoVO modelo)
        {
            ///Dados Solicitacao atual
            var spec = new SolicitacaoHistoricoSituacaoFilterSpecification() { SeqSolicitacaoServico = modelo.SeqSolicitacaoServico, ValidarDataExclusao = true };
            var solicitacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchProjectionBySpecification(spec,
                p => new
                {
                    SeqGrupoEscalonamento = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqGrupoEscalonamento,
                    SeqEtapaAtual = p.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                    SeqSituacaoAtualSGF = p.SeqSituacaoEtapaSgf,
                    SeqPessoaAtuacao = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqPessoaAtuacao,
                    SeqConfiguracaoEtpa = p.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                    DataInclusaoSituacaoAtual = p.SolicitacaoServicoEtapa.SolicitacaoServico.SituacaoAtual.DataInclusao,
                    PessoaAtaucaoTipoAtuacao = p.SolicitacaoServicoEtapa.SolicitacaoServico.PessoaAtuacao.TipoAtuacao,
                    SituacaoIngressanteAtual = (SituacaoIngressante?)(p.SolicitacaoServicoEtapa.SolicitacaoServico.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoIngressante,
                    p.SolicitacaoServicoEtapa.SeqSolicitacaoServico,
                    p.CategoriaSituacao,
                    (p.SolicitacaoServicoEtapa.SolicitacaoServico as SolicitacaoMatricula).CodigoAdesao,
                    (p.SolicitacaoServicoEtapa.SolicitacaoServico as SolicitacaoMatricula).SeqTermoAdesao,
                    (p.SolicitacaoServicoEtapa.SolicitacaoServico as SolicitacaoMatricula).SeqArquivoTermoAdesao,
                    (p.SolicitacaoServicoEtapa.SolicitacaoServico as SolicitacaoMatricula).SeqCondicaoPagamentoGra,
                    SeqSolicitacaoMatricula = (p.SolicitacaoServicoEtapa.SolicitacaoServico as SolicitacaoMatricula).Seq
                }).FirstOrDefault();

            ///Dados etapa atual da solicitação
            var etapaAtualSolicitacao = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(solicitacaoAtual.SeqEtapaAtual),
                p => new
                {
                    DataFim = p.Processo.GruposEscalonamento.Where(w => w.Seq == solicitacaoAtual.SeqGrupoEscalonamento).FirstOrDefault().Itens.Where(w => w.Escalonamento.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual).FirstOrDefault().Escalonamento.DataFim,
                    SitaucaoAtual = p.SituacaoEtapa,
                    SeqEtapaAtualSGF = p.SeqEtapaSgf,
                    Descricao = p.DescricaoEtapa
                });

            var servicoAtual = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), p => new
            {
                p.Servico.PermiteReabrirSolicitacao,
                p.Servico.TipoPrazoReabertura,
                p.Servico.NumeroDiasPrazoReabertura,
                p.SeqServico,
                p.Servico.Token
            });

            var grupoEscalonamentoASerAssociado = this.BuscarGrupoEscalonamento(modelo.Seq);

            var grupoEscalonamentoQueEstavaAssociado = this.BuscarGrupoEscalonamento((long)solicitacaoAtual.SeqGrupoEscalonamento);

            if (servicoAtual.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || servicoAtual.Token == TOKEN_SERVICO.MATRICULA_REABERTURA)
            {
                var specTrabalhoAcademico = new TrabalhoAcademicoAutoriaSpecification() { SeqAluno = solicitacaoAtual.SeqPessoaAtuacao };
                var trabalhoAcademicoAutoria = TrabalhoAcademicoAutoriaDomainService.SearchProjectionBySpecification(specTrabalhoAcademico, t => t);
                if (trabalhoAcademicoAutoria != null)
                {
                    foreach (var item in trabalhoAcademicoAutoria)
                    {
                        var specTrabalhoComponente = new TrabalhoAcademicoDivisaoComponenteSpecification { SeqTrabalhoAcademico = item.SeqTrabalhoAcademico };
                        var trabalhoComponente = TrabalhoAcademicoDivisaoComponenteDomainService.SearchByKey(specTrabalhoComponente);
                        var trabalhoAcademico = TrabalhoAcademicoDomainService.SearchByKey(item.SeqTrabalhoAcademico);

                        if (trabalhoComponente != null && trabalhoAcademico != null)
                        {
                            var origemAvaliacao = OrigemAvaliacaoDomainService.SearchByKey(trabalhoComponente.SeqOrigemAvaliacao);
                            var specAplicacaoAvaliacao = new AplicacaoAvaliacaoFilterSpecification { SeqOrigemAvaliacao = origemAvaliacao.Seq };
                            var aplicacaoAvaliacao = AplicacaoAvaliacaoDomainService.SearchBySpecification(specAplicacaoAvaliacao);

                            var specInstituicaoNivelTipoTrabalho = new InstituicaoNivelTipoTrabalhoFilterSpecification()
                            {
                                SeqNivelEnsino = trabalhoAcademico.SeqNivelEnsino,
                                SeqInstituicaoEnsino = trabalhoAcademico.SeqInstituicaoEnsino,
                                SeqTipoTrabalho = trabalhoAcademico.SeqTipoTrabalho
                            };

                            var geraFinanceiro = InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(specInstituicaoNivelTipoTrabalho, i => i.GeraFinanceiroEntregaTrabalho);

                            if (aplicacaoAvaliacao.Any(x => x.DataCancelamento == null) && geraFinanceiro)
                            {
                                var specTrabalhoDivisaoComponente = new TrabalhoAcademicoDivisaoComponenteSpecification { SeqTrabalhoAcademico = trabalhoAcademico.Seq };
                                var trabalhoDivisaoComponente = TrabalhoAcademicoDivisaoComponenteDomainService.SearchBySpecification(specTrabalhoDivisaoComponente).ToList();

                                if (trabalhoDivisaoComponente != null)
                                {
                                    var seqsDivisaoComponente = new List<long>();
                                    var seqsConfiguracaoComponente = new List<long>();

                                    foreach (var trabalho in trabalhoDivisaoComponente)
                                        seqsDivisaoComponente.Add(trabalho.SeqDivisaoComponente);

                                    var specDivisaoComponente = new DivisaoComponenteFilterSpecification { Seqs = seqsDivisaoComponente.ToArray() };
                                    var divisaoComponente = DivisaoComponenteDomainService.SearchBySpecification(specDivisaoComponente).ToList();


                                    foreach (var divisao in divisaoComponente)
                                        seqsConfiguracaoComponente.Add(divisao.SeqConfiguracaoComponente);

                                    var possuiPreRequisitos = RequisitoDomainService.ValidarPreRequisitos(solicitacaoAtual.SeqPessoaAtuacao, seqsDivisaoComponente, seqsConfiguracaoComponente, null, null, null);

                                    if (possuiPreRequisitos.Valido)
                                        throw new AlunoPossuiDefesaAgendadaSolicitacaoException();
                                }
                            }
                        }
                    }
                }
            }

            ///Se o respectivo serviço do processo não permite reabrir solicitação, exibir mensagem impeditiva:
        	///"Associação não permitida. O tipo do processo não permite que uma solicitação seja reaberta.”.
            if (servicoAtual.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.NaoPermite)
            {
                throw new GrupoEscalonamentoAssociacaoNaoPermiteReaberturaException();
            }

            ///Senão, se o respectivo serviço permite reabrir solicitação exceto as finalizadas com sucesso E, o[prazo para reabrir a solicitação expirou] *,
            ///exibir mensagem impeditiva: “Associação não permitida.O prazo permitido para reabrir a solicitação após o encerramento expirou.”.
            DateTime dataLimiteReabertura = DateTime.MinValue;
            ///Verifica se o prazo para reabertura da solicitação está expirado
            if (servicoAtual.TipoPrazoReabertura == TipoPrazoReabertura.DiasCorridos)
            {
                dataLimiteReabertura = solicitacaoAtual.DataInclusaoSituacaoAtual.AddDays(servicoAtual.NumeroDiasPrazoReabertura.GetValueOrDefault());
            }
            else
            {
                dataLimiteReabertura = SMCDateTimeHelper.AddBusinessDays(solicitacaoAtual.DataInclusaoSituacaoAtual, servicoAtual.NumeroDiasPrazoReabertura.GetValueOrDefault(), null);
            }
            //Verifica se a solicitação esta encerrada
            if (solicitacaoAtual.CategoriaSituacao == CategoriaSituacao.Encerrado)
            {
                if ((servicoAtual.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteTodas ||
                    servicoAtual.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso)
                   && DateTime.Now.Date > dataLimiteReabertura.Date)
                {
                    throw new GrupoEscalonamentoAssociacaoPrazoReabrirSolicitacaoEncerradoException();
                }
            }

            ///Senão, se o respectivo serviço permite reabrir solicitação exceto as finalizadas com sucesso E, a situação do solicitante não está de acordo com as
            ///[situações parametrizadas permitindo a reabertura] *, exibir mensagem impeditiva:
            ///"Associação não permitida. Para o tipo de serviço da solicitação, a situação atual do solicitante não permite que a solicitação seja reaberta.".
            var situacaoSolicitantePermiteReabrirSolicitacao = false;

            if (solicitacaoAtual.PessoaAtaucaoTipoAtuacao == TipoAtuacao.Aluno)
            {
                ///Buscar a situacao atual da pessao atuacao desativando o filtro de dados de hierarquia_entidade_organizacional
                var seqSituacaoMatriculaAtual = this.AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(solicitacaoAtual.SeqPessoaAtuacao, true).SeqSituacao;

                var result = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>((long)modelo.SeqSolicitacaoServico), s => new
                {
                    SituacaoSolicitantePermiteReabrirSolicitacao = s.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno.Any(ss => ss.SeqSituacaoMatricula == seqSituacaoMatriculaAtual && ss.PermissaoServico == PermissaoServico.ReabrirSolicitacao),
                });

                situacaoSolicitantePermiteReabrirSolicitacao = result.SituacaoSolicitantePermiteReabrirSolicitacao;
            }
            else if (solicitacaoAtual.PessoaAtaucaoTipoAtuacao == TipoAtuacao.Ingressante)
            {
                var result = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>((long)modelo.SeqSolicitacaoServico), s => new
                {
                    SituacaoSolicitantePermiteReabrirSolicitacao = s.ConfiguracaoProcesso.Processo.Servico.SituacoesIngressante.Any(si => si.SituacaoIngressante == solicitacaoAtual.SituacaoIngressanteAtual && si.PermissaoServico == PermissaoServico.ReabrirSolicitacao),
                });

                situacaoSolicitantePermiteReabrirSolicitacao = result.SituacaoSolicitantePermiteReabrirSolicitacao;
            }

            ///Verifica se serviço permite todas e a situação do solicitante não permite reabrir a solicitação
            if (servicoAtual.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso && !situacaoSolicitantePermiteReabrirSolicitacao)
            {
                throw new GrupoEscalonamentoAssociacaoSituacaoAtualNaoPermiteReaberturaException();
            }

            ///Senão, se o serviço possui parametrização de restrição de solicitação e, para o solicitante há pelo menos uma[solicitação que esteja em aberto]*para os serviços parametrizados como restritivos, exibir mensagem impeditiva:
        	///“Associação não permitida.Não é possível reabrir a solicitação, pois foram identificadas as seguintes solicitações em aberto.E o serviço dessas solicitações são restritivo comparado ao serviço da solicitação selecionada:
            ///[Protocolo] - [Descrição Processo] - [Nro ordem Etapa atual +’ º Etapa’] - [Situação atual]".
            var restricoesSolicitacoesSimultaneas = this.SolicitacaoServicoDomainService.BuscarRestricoesSolicitacaoSimultanea(servicoAtual.SeqServico, solicitacaoAtual.SeqPessoaAtuacao);

            if (restricoesSolicitacoesSimultaneas.SMCAny())
            {
                var restricoes = string.Empty;

                foreach (var item in restricoesSolicitacoesSimultaneas)
                {
                    if (item.SeqSolicitacaoServico != solicitacaoAtual.SeqSolicitacaoServico)
                    {
                        restricoes += $"{item.NumeroProtocolo} - {item.Processo} - {item.Ordem}º Etapa - {item.SituacaoAtual} <br />";
                    }
                }

                if (restricoes != string.Empty)
                {
                    throw new GrupoEscalonamentoAssociacaoNaoPermiteReaberturaComSolicitacoesAbertasException(restricoes);
                }
            }

            ///Senão se todas as etapas do novo grupo de escalonamento estiverem expiradas, exibir mensagem impeditiva:
            int etapasExpiradas = 0;
            foreach (var item in grupoEscalonamentoASerAssociado.Itens)
            {
                var dataFimEscalonamento = this.EscalonamentoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(item.SeqEscalonamento), p => p.DataFim);

                if (dataFimEscalonamento < DateTime.Now)
                {
                    etapasExpiradas++;
                }
            }

            if (etapasExpiradas == grupoEscalonamentoASerAssociado.Itens.Count)
            {
                throw new GrupoEscalonamentoAssociacaoProcessoEtapaExpiradosException();
            }

            ///Senão, se a situação atual da solicitação seja igual a final processo e a classificação igual a finalizado
            ///com sucesso, exibir mensagem impeditiva:
            var situacaoAtualSolicitacao = this.EtapaService.BuscarSituacaoEtapa(solicitacaoAtual.SeqSituacaoAtualSGF);

            if (situacaoAtualSolicitacao.SituacaoFinalProcesso && situacaoAtualSolicitacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso)
            {
                throw new GrupoEscalonamentoAssociacaoClassificacaoStiuacaoFinalException();
            }

            ///Senão, se o atual grupo de escalonamento da solicitação for igual ao novo grupo de escalonamento, verificar se a solicitação está em aberto. Caso estiver, exibir a mensagem impeditiva:
            if (grupoEscalonamentoQueEstavaAssociado.Seq == grupoEscalonamentoASerAssociado.Seq &&
                (situacaoAtualSolicitacao.CategoriaSituacao == CategoriaSituacao.Novo || situacaoAtualSolicitacao.CategoriaSituacao == CategoriaSituacao.EmAndamento))
            {
                throw new GrupoEscalonamentoAssociacaoGrupoNovoIgualGrupoAtualException();
            }

            ///Senão se, a etapa atual da solicitação no novo grupo de escalonamento não estiver com o período
            ///vigente ou futuro, exibir mensagem impeditiva:
            var etapaAtualNovoGrupo = grupoEscalonamentoASerAssociado.Itens.FirstOrDefault(f => f.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual);
            if (etapaAtualNovoGrupo.DataIncioEscalonamento < DateTime.Now && etapaAtualNovoGrupo.DataFimEscalonamento < DateTime.Now)
            {
                throw new GrupoEscalonamentoAssociacaoDataEtapaExpiradaException(etapaAtualSolicitacao.Descricao);
            }

            ///Senão se, a situação da etapa atual da solicitação for diferente de "Liberada", exibir mensagem impeditiva:
            if (etapaAtualSolicitacao.SitaucaoAtual != SituacaoEtapa.Liberada)
            {
                throw new GrupoEscalonamentoAssociacaoSituacaoDiferenteLiberadaException(etapaAtualSolicitacao.Descricao);
            }

            #region Validação do código de adesão e parcelas do grupo temporiariamente comentada conforme Task 40404

            ///Senão se, a solicitação possui código de adesão associado, verificar se o grupo em questão possui a mesma 
            ///quantidade de parcelas que o grupo que já está associado à solicitação e, caso exista preenchimento, se os 
            ///números das parcela são iguais. Se não for, exibir a seguinte mensagem impeditiva:
            ///"Associação não permitida. Este grupo deve possuir a mesma quantidade e os mesmos números das parcelas do grupo já 
            ///associado à solicitação selecionada."    
            //if (solicitacaoAtual.CodigoAdesao.HasValue)
            //{
            //    var grupoEscalonamentoJaAssociadoSolicitacao = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(grupoEscalonamentoQueEstavaAssociado.Seq), x => x.Itens[0].Parcelas);
            //    var grupoEscalonamentoASerAssociadoSolicitacao = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(grupoEscalonamentoASerAssociado.Seq), x => x.Itens[0].Parcelas);

            //    var parcelasGrupoEscalonamentoJaAssociado = grupoEscalonamentoJaAssociadoSolicitacao.Itens.SelectMany(a => a.Parcelas).ToList();
            //    var parcelasGrupoEscalonamentoASerAssociado = grupoEscalonamentoASerAssociadoSolicitacao.Itens.SelectMany(a => a.Parcelas).ToList();

            //    if (parcelasGrupoEscalonamentoJaAssociado.Count() == parcelasGrupoEscalonamentoASerAssociado.Count())
            //    {
            //        var listaNumerosParcelastensGrupoJaAssociado = parcelasGrupoEscalonamentoJaAssociado.Select(a => a.NumeroParcela).ToList();
            //        var listaNumerosParcelastensGrupoASerAssociado = parcelasGrupoEscalonamentoASerAssociado.Select(a => a.NumeroParcela).ToList();

            //        var listaDiferencas1 = listaNumerosParcelastensGrupoJaAssociado.Except(listaNumerosParcelastensGrupoASerAssociado).ToList();
            //        var listaDiferencas2 = listaNumerosParcelastensGrupoASerAssociado.Except(listaNumerosParcelastensGrupoJaAssociado).ToList();
            //        var listaFinalDiferencas = listaDiferencas1.Union(listaDiferencas2).ToList();

            //        if (listaFinalDiferencas.Any())
            //            throw new GrupoEscalonamentoAssociacaoCodigoAdesaoGrupoDiferenteException();
            //    }
            //    else
            //        throw new GrupoEscalonamentoAssociacaoCodigoAdesaoGrupoDiferenteException();
            //}

            #endregion

            ///Caso o processo seja de um serviço que possua o token 
            ///SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA e SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU, 
            ///consistir a regra: RN_ALN_060 - Consistências financeiras ao liberar para matrícula
            if (servicoAtual.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA ||
                servicoAtual.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU)
            {
                var solicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(solicitacaoAtual.SeqSolicitacaoMatricula));

                var listaEtapasOrdenadas = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.SeqProcesso), x => x.Etapas).OrderBy(o => o.Ordem).ToList();
                var etapaDeMenorOrdem = listaEtapasOrdenadas[0];

                //Recuperando os escalonamentos do grupo a ser associado, pois o grupo já associado à solicitação já foi validado
                var grupoEscalonamentoItens = this.GrupoEscalonamentoItemDomainService.SearchBySpecification(new GrupoEscalonamentoItemFilterSpecification() { SeqGrupoEscalonamento = grupoEscalonamentoASerAssociado.Seq }, x => x.Escalonamento).ToList();
                var escalonamentosGrupoASerAssociado = grupoEscalonamentoItens.Select(a => a.Escalonamento).ToList();

                DateTime dataInicioPeriodo = DateTime.MinValue;
                DateTime dataFimPeriodo = DateTime.MinValue;

                foreach (var escalonamento in escalonamentosGrupoASerAssociado)
                {
                    var processoEtapa = this.ProcessoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(escalonamento.SeqProcessoEtapa));

                    if (processoEtapa.Seq == etapaDeMenorOrdem.Seq)
                    {
                        dataInicioPeriodo = escalonamento.DataInicio;
                        dataFimPeriodo = escalonamento.DataFim;

                        break;
                    }
                }

                IngressanteDomainService.ValidarLiberacaoMatriculaIngressanteBeneficio(
                    solicitacaoMatricula.SeqPessoaAtuacao,
                    dataInicioPeriodo,
                    dataFimPeriodo,
                    grupoEscalonamentoASerAssociado.NumeroDivisaoParcelas);
            }
        }

        /// <summary>
        /// Recupera os grupos de escalonamento para o retorno do lookup
        /// </summary>
        /// <param name="seqs">Sequenciais dos grupos de escalonamento selecionados</param>
        /// <returns>Dados dos escalonamentos</returns>
        public List<GrupoEscalonamentoListaVO> BuscarGruposEscalonamentoGridLookup(long[] seqs, bool disparaExcecao = false)
        {
            var spec = new SMCContainsSpecification<GrupoEscalonamento, long>(p => p.Seq, seqs);
            var grupos = SearchProjectionBySpecification(spec, p => new GrupoEscalonamentoListaVO()
            {
                Seq = p.Seq,
                Descricao = p.Descricao,
                Expirado = p.Itens.Any(f => f.Escalonamento.DataFim < DateTime.Today)
            }).ToList();

            if (disparaExcecao && grupos.Any(f => f.Expirado))
            {
                throw new GrupoEscalonamentoExpiradoException();
            }

            return grupos;
        }

        /// <summary>
        /// Efeturará a reserva da vaga no processo seletivo ou turma, alem de atualizar a situação do item conforme a regra de negocio
        /// RN* A situação dos itens que possuem vaga disponível deverá receber a situação finalizada com sucesso para a etapa atual
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitacao de servico</param>
        /// <param name="seqSituacaoMatriculaItem">Sequencial situacao da matricula item atual</param>
        private void ReservaVagas(long seqSolicitacaoServico, long seqSituacaoMatriculaItem)
        {
            var includes = IncludesSolicitacaoMatricula.Itens_DivisaoTurma_Turma_ConfiguracoesComponente | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao | IncludesSolicitacaoMatricula.Itens_HistoricosSituacao_SituacaoItemMatricula;
            var solicitacaoMatricula = this.SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoServico), includes);

            if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoMatricula.SeqPessoaAtuacao) == TipoAtuacao.Ingressante)
            {
                var includesIngressante = IncludesIngressante.Ofertas_CampanhaOferta;

                var ingressante = this.IngressanteDomainService.SearchByKey(new SMCSeqSpecification<Ingressante>(solicitacaoMatricula.SeqPessoaAtuacao), includesIngressante);

                var processoSeletivoReservaVaga = this.ProcessoSeletivoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoSeletivo>(ingressante.SeqProcessoSeletivo), p => p.ReservaVaga);

                if (processoSeletivoReservaVaga)
                {
                    foreach (var item in solicitacaoMatricula.Itens.Where(w => w.SeqDivisaoTurma != null))
                    {
                        var historicoSolicitacao = item.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).Select(s => new { s.MotivoSituacaoMatricula, s.SituacaoItemMatricula.ClassificacaoSituacaoFinal }).FirstOrDefault();

                        if (historicoSolicitacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                            && (historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.EtapaNaoFinalizada ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.ExistenciaBloqueio ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.SolicitacaoCancelada))
                        {
                            foreach (var oferta in ingressante.Ofertas)
                            {
                                ///Verifica se a turma se refere ao processo seletivo corrente
                                if (oferta.CampanhaOfertaItem.SeqTurma == item.DivisaoTurma.SeqTurma)
                                {
                                    var filtro = new ProcessoSeletivoOfertaFilterSpecification() { SeqCampanhaOferta = oferta.SeqCampanhaOferta, SeqProcessoSeletivo = ingressante.SeqProcessoSeletivo };
                                    var processoSeletivoOferta = this.ProcessoSeletivoOfertaDomainService.SearchBySpecification(filtro).FirstOrDefault();

                                    if ((processoSeletivoOferta.QuantidadeVagas - processoSeletivoOferta.QuantidadeVagasOcupadas) > 0)
                                    {
                                        this.AtualizarHistoricoItemMatricula(item.Seq, seqSituacaoMatriculaItem);

                                        processoSeletivoOferta.QuantidadeVagasOcupadas += 1;
                                        this.ProcessoSeletivoOfertaDomainService.UpdateFields(processoSeletivoOferta, u => u.QuantidadeVagasOcupadas);
                                        //this.ProcessoSeletivoOfertaDomainService.SaveEntity(processoSeletivoOferta);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in solicitacaoMatricula.Itens.Where(w => w.SeqDivisaoTurma != null))
                    {
                        var historicoSolicitacao = item.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).Select(s => new { s.MotivoSituacaoMatricula, s.SituacaoItemMatricula.ClassificacaoSituacaoFinal }).FirstOrDefault();

                        if (historicoSolicitacao.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado
                            && (historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.EtapaNaoFinalizada ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.ExistenciaBloqueio ||
                            historicoSolicitacao.MotivoSituacaoMatricula == MotivoSituacaoMatricula.SolicitacaoCancelada))
                        {
                            if ((item.DivisaoTurma.QuantidadeVagas - item.DivisaoTurma.QuantidadeVagasOcupadas) > 0)
                            {
                                this.AtualizarHistoricoItemMatricula(item.Seq, seqSituacaoMatriculaItem);

                                item.DivisaoTurma.QuantidadeVagasOcupadas += 1;
                                this.DivisaoTurmaDomainService.UpdateFields(item.DivisaoTurma, u => u.QuantidadeVagasOcupadas);
                                //this.DivisaoTurmaDomainService.SaveEntity(item.DivisaoTurma.QuantidadeVagasOcupadas);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Atualizar o historico da solicitação, atualização das paginas de navegação e disparo de notificação caso necessario conforme regra de negocio
        /// RN* A solicitação deverá receber a situação inicial parametrizada para a etapa atual
        /// RN* Incluir um novo registro no histórico de navegação com a página inicial da etapa em questão, com a data de entrada igual a data corrente(hoje)
        /// RN* Se a aplicação da etapa atual for SGA.Aluno, deverá ser enviada notificação para o solicitante sobre os novos prazos do processo,
        /// conforme RN_SRC_065 - Grupo escalonamento - Notificação sobre novos prazos da solicitação.
        /// </summary>
        /// <param name="seqSolicitacaoServicoEtapa">Sequencial da solicitação serviço etapa</param>
        /// <param name="seqSolicitacaoServico">Sequencial solicitacao servico</param>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa atual</param>
        /// <param name="seqEtapaSGFAtual">Sequencial da etapa no SGF atual</param>
        /// <param name="dscGrupoEscalonamentoAtual">Descrição do grupo de escalonamento atual</param>
        /// <param name="dscGrupoEscalonamentoNovo">Descrição do grupo de escalonamento novo</param>
        private void AtualizarHistoricoSolicitacao(long seqSolicitacaoServicoEtapa, long seqSolicitacaoServico, long seqProcessoEtapaAtual, long seqEtapaSGFAtual, GrupoEscalonamentoVO grupoEscalonamentoAtual, GrupoEscalonamentoVO grupoEscalonamentoNovo)
        {
            var includesEtapaSGF = IncludesEtapa.Paginas | IncludesEtapa.Situacoes;
            var processoEtapaSGF = this.EtapaService.BuscarEtapa(seqEtapaSGFAtual, includesEtapaSGF);

            var situacaoEtapaSGF = processoEtapaSGF.Situacoes.FirstOrDefault(f => f.SituacaoInicialEtapa);

            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico),
                p => new
                {
                    UltimoHistoricoNavegacao = p.Etapas.FirstOrDefault(e => e.SeqSolicitacaoHistoricoSituacaoAtual == p.SeqSolicitacaoHistoricoSituacaoAtual).NavegacaoAtual,
                    SeqConfiguracaoEtapa = p.Etapas.FirstOrDefault(w => w.ConfiguracaoEtapa.SeqProcessoEtapa == seqProcessoEtapaAtual).SeqConfiguracaoEtapa,
                    SeqPessoaAtuacao = p.SeqPessoaAtuacao
                });

            SolicitacaoHistoricoSituacao modelo = new SolicitacaoHistoricoSituacao();
            modelo.SeqSituacaoEtapaSgf = situacaoEtapaSGF.Seq;
            modelo.CategoriaSituacao = situacaoEtapaSGF.CategoriaSituacao;
            modelo.SeqSolicitacaoServicoEtapa = seqSolicitacaoServicoEtapa;
            modelo.Observacao = string.Format(MessagesResource.MSG_SolicitacaoHistoricoServico, grupoEscalonamentoAtual.Descricao, grupoEscalonamentoNovo.Descricao);

            this.SolicitacaoHistoricoSituacaoDomainService.SaveEntity(modelo);

            ///ATUALIZAÇÃO DO SOLICITANTE
            ///Se o solicitante for um Ingressante com a situação atual igual a Desistente:
            if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoServico.SeqPessoaAtuacao) == TipoAtuacao.Ingressante)
            {
                var specIngressante = new IngressanteHistoricoSituacaoFilterSpecification() { SeqIngressante = solicitacaoServico.SeqPessoaAtuacao };
                specIngressante.SetOrderByDescending(o => o.DataInclusao);
                var ingressanteHistoricoSituacao = this.IngressanteHistoricoSituacaoDomainService.SearchBySpecification(specIngressante).ToList();

                ///Verifica se a última situação e de Desistente, irá criar nova situação com a penultima situação
                if (ingressanteHistoricoSituacao[0].SituacaoIngressante == SituacaoIngressante.Desistente)
                {
                    IngressanteHistoricoSituacao ingressanteHistoricoSituacaoNew = new IngressanteHistoricoSituacao();
                    ingressanteHistoricoSituacaoNew.SeqIngressante = solicitacaoServico.SeqPessoaAtuacao;
                    ingressanteHistoricoSituacaoNew.SituacaoIngressante = ingressanteHistoricoSituacao[1].SituacaoIngressante;

                    this.IngressanteHistoricoSituacaoDomainService.SaveEntity(ingressanteHistoricoSituacaoNew);

                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacaoServico.SeqPessoaAtuacao);
                    var dadosProcessoEtapa = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapaAtual), p => new
                    {
                        Ano = p.Processo.CicloLetivo.Ano,
                        Numero = p.Processo.CicloLetivo.Numero,
                        Token = p.Token
                    });

                    if (dadosProcessoEtapa.Token != TOKENS_ETAPA_SGF.SOLICITACAO_MATRICULA)
                    {
                        string erroGRA = IntegracaoFinanceiroService.AlterarSituacaoMatriculaAcademico(new AlterarMatriculaAcademicoData()
                        {
                            SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao,
                            SeqOrigem = (int)dadosOrigem.SeqOrigem,
                            CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                            AnoCicloLetivo = dadosProcessoEtapa.Ano,
                            NumeroCicloLetivo = dadosProcessoEtapa.Numero,
                            CodigoTipoTransacao = 48 //significa “Desfazer desistência de ingressante”
                        });

                        //Ocorra um erro no GRA ele será exibido.
                        if (!string.IsNullOrEmpty(erroGRA))
                        {
                            throw new Exception(erroGRA);
                        }
                    }
                }

                ///Se o solicitante for um Ingressante que possui oferta de inscrição com a situação atual igual a
                ///CONVOCADO_DESISTENTE_MATRICULA_NAO_EFETIVADA:
                ///Inserir novo histórico na oferta de inscrição com a situação anterior à atual, conforme a
                ///RN_SEL_011 - Gravação da situação da inscrição oferta na convocação
                var ingressanteOferta = this.IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(solicitacaoServico.SeqPessoaAtuacao),
                   p => new
                   {
                       ingressanteOfertasGPI = p.Ofertas.ToList(),
                       inscricaoGPI = (long?)p.Convocado.SeqInscricaoGpi
                   });

                if (ingressanteOferta.inscricaoGPI.HasValue)
                {
                    var seqProcessoGPI = InscricaoService.BuscarDadosProcessoInscricao((long)ingressanteOferta.inscricaoGPI).SeqProcesso;
                    foreach (var item in ingressanteOferta.ingressanteOfertasGPI)
                    {
                        if (item.SeqInscricaoOfertaGpi != null)
                        {
                            var inscricaoGPI = IntegracaoService.BuscarHistoricosSituacaoAtual((long)item.SeqInscricaoOfertaGpi);

                            if (inscricaoGPI.SeqMotivoSituacao != null)
                            {
                                var motivoSituacaoSGF = SituacaoService.BuscarMotivo((long)inscricaoGPI.SeqMotivoSituacao);

                                if (motivoSituacaoSGF.Token == SGFConstants.CONVOCADO_DESISTENTE_MATRICULA_NAO_EFETIVADA)
                                {
                                    AlterarHistoricoSituacaoData AlteracaoSituacao = new AlterarHistoricoSituacaoData();
                                    AlteracaoSituacao.SeqTipoProcessoSituacaoDestino = -1;
                                    AlteracaoSituacao.Justificativa = "Situação da oferta de inscrição atualizada automaticamente durante o processo de troca de grupo de escalonamento no Sistema Acadêmico.";
                                    AlteracaoSituacao.SeqMotivoSGF = inscricaoGPI.SeqMotivoSituacao;
                                    AlteracaoSituacao.SeqInscricoesOferta = new List<long>();
                                    AlteracaoSituacao.SeqInscricoesOferta.Add((long)item.SeqInscricaoOfertaGpi);
                                    AlteracaoSituacao.SeqProcesso = seqProcessoGPI;
                                    this.InscricaoOferta.AlterarHistoricoSituacao(AlteracaoSituacao);
                                }
                            }
                        }
                    }
                }
            }

            ///Envia notificacao conforme regra
            this.EnviarNotificacao(seqSolicitacaoServico, seqProcessoEtapaAtual, grupoEscalonamentoNovo);

            ///4.Incluir um novo registro no histórico de navegação com a página inicial da etapa em questão, com a
            ///data de entrada igual a data corrente do sistema.
            if (solicitacaoServico.UltimoHistoricoNavegacao != null && solicitacaoServico.UltimoHistoricoNavegacao.DataSaida == null)
            {
                this.SolicitacaoHistoricoNavegacaoDomainService.AtualizarDataSaidaSolicitacaoHistoricoNavegacao(solicitacaoServico.UltimoHistoricoNavegacao.Seq);
            }

            var seqPaginaEtapaSGF = processoEtapaSGF.Paginas.OrderBy(o => o.Ordem).FirstOrDefault().Seq;
            var specConfiguracaoEtapaPagina = new ConfiguracaoEtapaPaginaFilterSpecification() { SeqPaginaEtapaSgf = seqPaginaEtapaSGF, SeqConfiguracaoEtapa = solicitacaoServico.SeqConfiguracaoEtapa };
            var seqConfiguracaoEtapaPagina = this.ConfiguracaoEtapaPaginaDomainService.SearchBySpecification(specConfiguracaoEtapaPagina).FirstOrDefault().Seq;

            SolicitacaoHistoricoNavegacao solicitacaoHistoricoNavegacao = new SolicitacaoHistoricoNavegacao();
            solicitacaoHistoricoNavegacao.DataEntrada = DateTime.Now;
            solicitacaoHistoricoNavegacao.SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina;
            solicitacaoHistoricoNavegacao.SeqSolicitacaoServicoEtapa = seqSolicitacaoServicoEtapa;

            this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(solicitacaoHistoricoNavegacao);
        }

        /// <summary>
        /// Enviar notificação conforme o cenario do grupo de escalonamento
        /// </summary>
        /// <param name="seqSolicitacao">Sequencial da solicitação</param>
        private void EnviarNotificacao(long seqSolicitacaoServico, long seqProcessoEtapaAtual, GrupoEscalonamentoVO grupoEscalonamentoNovo)
        {
            var processoEtapa = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapaAtual),
                                                                                        p => new
                                                                                        {
                                                                                            Ordem = p.Ordem
                                                                                        });

            var solicitacaoServico = this.SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico),
                        p => new
                        {
                            NomeSolicitante = string.IsNullOrEmpty(p.PessoaAtuacao.DadosPessoais.NomeSocial) ? p.PessoaAtuacao.DadosPessoais.Nome : p.PessoaAtuacao.DadosPessoais.NomeSocial,
                            DescricaoProcesso = p.ConfiguracaoProcesso.Processo.Descricao,
                            PessoaAtuacao = new
                            {
                                SeqPessoaAtauacao = p.SeqPessoaAtuacao,
                                TipoAtuacao = p.PessoaAtuacao.TipoAtuacao
                            }
                        });

            ///3.Se a pessoa atuação for um ingresstne e a situação atual for igual a Apto para matricula ou,
            ///a pessoa atuação for um aluno, deverá ser enviada notificação para os
            ///solicitantes sobre os novos prazos do processo, conforme RN_SRC_065 -Grupo escalonamento -
            ///Notificação sobre novos prazos da solicitação.
            var spec = new IngressanteHistoricoSituacaoFilterSpecification() { SeqIngressante = solicitacaoServico.PessoaAtuacao.SeqPessoaAtauacao };
            var ingressanteHistoricoSituacao = this.IngressanteHistoricoSituacaoDomainService.SearchBySpecification(spec).OrderByDescending(o => o.DataInclusao).FirstOrDefault();

            if (solicitacaoServico.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno
                || (solicitacaoServico.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante
                    && ingressanteHistoricoSituacao.SituacaoIngressante == SituacaoIngressante.AptoMatricula))
            {
                //string etapaSolicitacao = string.Join("<br />", solicitacaoServico.Etapas);
                string etapaSolicitacao = string.Join("<br />", grupoEscalonamentoNovo.Itens.OrderBy(w => w.OrdemEtapa).Select(s => s.DescricaoEtapa + " - " + s.DataIncioEscalonamento + " a " + s.DataFimEscalonamento));
                ///Usa o metodo criado em escalonamento pois a regra é a mesma
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add("{{NOM_PESSOA}}", solicitacaoServico.NomeSolicitante);
                dadosMerge.Add("{{DSC_PROCESSO}}", solicitacaoServico.DescricaoProcesso);
                dadosMerge.Add("{{DSC_ETAPA_SOLICITACAO}}", etapaSolicitacao);

                // Envia a notificação
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PERIODO_VIGENCIA,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = true,
                    ConfiguracaoPrimeiraEtapa = true
                };
                this.SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
            }
        }

        /// <summary>
        /// Atualiza o historico dos itens da matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial matricula item</param>
        /// <param name="seqSituacaoItemMatricula">Sequencial situação matricula item</param>
        private void AtualizarHistoricoItemMatricula(long seqSolicitacaoMatriculaItem, long seqSituacaoItemMatricula)
        {
            SolicitacaoMatriculaItemHistoricoSituacao solicitacaoMatriculaItemHistoricoSituacao = new SolicitacaoMatriculaItemHistoricoSituacao();
            solicitacaoMatriculaItemHistoricoSituacao.SeqSolicitacaoMatriculaItem = seqSolicitacaoMatriculaItem;
            solicitacaoMatriculaItemHistoricoSituacao.SeqSituacaoItemMatricula = seqSituacaoItemMatricula;

            this.SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(solicitacaoMatriculaItemHistoricoSituacao);
        }

        /// <summary>
        /// Verifica se todos os escalonamentos dos itens do grupo possui data final maior que a data atual para continuar o processo
        /// </summary>
        /// <param name="seq">Sequencial do grupo de escalonamento</param>
        /// <returns>Retorna true para válido</returns>
        public bool ValidarDataFimEscalonamentoPorGrupoEscalonamento(long seq)
        {
            try
            {
                var spec = new GrupoEscalonamentoItemFilterSpecification() { SeqGrupoEscalonamento = seq, EscalonamentoVencido = true };

                return this.GrupoEscalonamentoItemDomainService.Count(spec) == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Enviar notificações referentes aos prazos de vigencia das etapas do grupo de escalonamento
        /// </summary>
        /// <param name="seqGrupoEscalonamento">Sequencial do grupo de escalonamento</param>
        public void EnviarNotificacaoPrazoVigencia(long seqGrupoEscalonamento)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var seqsSolicitacoes = SolicitacaoServicoDomainService.SearchProjectionBySpecification(new SolicitacaoServicoFilterSpecification() { SeqGrupoEscalonamento = seqGrupoEscalonamento },
                    p => new
                    {
                        p.Seq
                    }).ToList();

                var grupoEscalonamento = this.BuscarGrupoEscalonamento(seqGrupoEscalonamento);

                foreach (var item in seqsSolicitacoes)
                {
                    var spec = new SolicitacaoHistoricoSituacaoFilterSpecification() { SeqSolicitacaoServico = item.Seq, ValidarDataExclusao = true };
                    var solicitacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchProjectionBySpecification(spec,
                        p => new
                        {
                            SeqGrupoEscalonamento = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqGrupoEscalonamento,
                            SeqEtapaAtual = p.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                            SeqSituacaoAtualSGF = p.SeqSituacaoEtapaSgf,
                            SeqPessoaAtuacao = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqPessoaAtuacao,
                            SeqConfiguracaoEtpa = p.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                            SeqSolicitacaoServicoEtapa = p.SolicitacaoServicoEtapa.Seq,
                            Categoria = p.CategoriaSituacao
                        }).FirstOrDefault();

                    if (solicitacaoAtual.Categoria != CategoriaSituacao.Concluido && solicitacaoAtual.Categoria != CategoriaSituacao.Encerrado)
                    {
                        this.EnviarNotificacao(item.Seq, solicitacaoAtual.SeqEtapaAtual, grupoEscalonamento);
                    }
                }

                ///Rollback caso alguma das funções provoquem erro
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Validar se a solicitação é uma disciplina isolada e irá chamar validação de vagas e consequentemente o assert.
        /// </summary>
        /// <param name="seqSoliciatacaoServico">Seq solicitação de serviço</param>
        /// <returns></returns>
        public bool ValidarAssertAssociacaoSolicitacaoGrupoEscalonamento(long seqSoliciatacaoServico)
        {
            ///Dados Solicitacao atual
            var spec = new SolicitacaoHistoricoSituacaoFilterSpecification() { SeqSolicitacaoServico = seqSoliciatacaoServico, ValidarDataExclusao = true };
            var solicitacaoAtual = this.SolicitacaoHistoricoSituacaoDomainService.SearchProjectionBySpecification(spec,
                p => new
                {
                    SeqGrupoEscalonamento = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqGrupoEscalonamento,
                    SeqEtapaAtual = p.SolicitacaoServicoEtapa.ConfiguracaoEtapa.SeqProcessoEtapa,
                    SeqSituacaoAtualSGF = p.SeqSituacaoEtapaSgf,
                    SeqPessoaAtuacao = p.SolicitacaoServicoEtapa.SolicitacaoServico.SeqPessoaAtuacao,
                    SeqConfiguracaoEtpa = p.SolicitacaoServicoEtapa.SeqConfiguracaoEtapa,
                    SeqSolicitacaoServicoEtapa = p.SolicitacaoServicoEtapa.Seq,
                    Categoria = p.CategoriaSituacao
                }).FirstOrDefault();

            ///Dados etapa atual da solicitação
            var etapaAtual = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(solicitacaoAtual.SeqEtapaAtual),
                p => new
                {
                    DataFim = p.Processo.GruposEscalonamento.Where(w => w.Seq == solicitacaoAtual.SeqGrupoEscalonamento).FirstOrDefault().Itens.Where(w => w.Escalonamento.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual).FirstOrDefault().Escalonamento.DataFim,
                    DataInicio = p.Processo.GruposEscalonamento.Where(w => w.Seq == solicitacaoAtual.SeqGrupoEscalonamento).FirstOrDefault().Itens.Where(w => w.Escalonamento.SeqProcessoEtapa == solicitacaoAtual.SeqEtapaAtual).FirstOrDefault().Escalonamento.DataInicio,
                    SitaucaoAtual = p.SituacaoEtapa,
                    SeqEtapaAtualSGF = p.SeqEtapaSgf,
                    ControleVagas = p.ControleVaga,
                    SeqSituacaoMatriculaItem = p.SituacoesItemMatricula.FirstOrDefault(f => f.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq,
                });

            ///Dados solicitante da solicitação atual
            long? seqTipoVinculoSolicitante = null;
            long? seqNivelEnsinoSolicitante = null;
            long? seqInstituicaoEnsino = null;
            if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoAtual.SeqPessoaAtuacao) == TipoAtuacao.Aluno)
            {
                var result = this.AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(solicitacaoAtual.SeqPessoaAtuacao), p => new
                {
                    SeqTipoVinculo = p.SeqTipoVinculoAluno,
                    SeqNivelEnsino = p.Historicos.FirstOrDefault(f => f.Atual).SeqNivelEnsino,
                    SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino
                });

                seqTipoVinculoSolicitante = result.SeqTipoVinculo;
                seqNivelEnsinoSolicitante = result.SeqNivelEnsino;
                seqInstituicaoEnsino = result.SeqInstituicaoEnsino;
            }
            else if (this.PessoaAtuacaoDomainService.BuscarTipoAtuacaoPessoaAtuacao(solicitacaoAtual.SeqPessoaAtuacao) == TipoAtuacao.Ingressante)
            {
                var result = this.IngressanteDomainService.SearchProjectionByKey(new SMCSeqSpecification<Ingressante>(solicitacaoAtual.SeqPessoaAtuacao), p => new
                {
                    SeqTipoVinculo = p.SeqTipoVinculoAluno,
                    SeqNivelEnsino = p.SeqNivelEnsino,
                    SeqProcessoSeletivo = p.SeqProcessoSeletivo,
                    SeqInstituicaoEnsino = p.Pessoa.SeqInstituicaoEnsino
                });

                seqTipoVinculoSolicitante = result.SeqTipoVinculo;
                seqNivelEnsinoSolicitante = result.SeqNivelEnsino;
                seqInstituicaoEnsino = result.SeqInstituicaoEnsino;
            }

            ///Valida se a solicitação é uma disciplina isolada
            ///Considerando que disciplina isolanda são as que não exige curso
            var specInstituicaoTipoVinculoAluno = new InstituicaoNivelTipoVinculoAlunoFilterSpecification() { SeqNivelEnsino = seqNivelEnsinoSolicitante, SeqTipoVinculoAluno = seqTipoVinculoSolicitante, SeqInstituicao = seqInstituicaoEnsino };
            var disciplinaIsolada = !this.InstituicaoNivelTipoVinculoAlunoDomainService.SearchProjectionBySpecification(specInstituicaoTipoVinculoAluno, p => p.ExigeCurso).FirstOrDefault();

            ///Cenário que a etapa atual da solicitação foi encerrada - Disciplina isolada
            ///Se a etapa atual da solicitação foi encerrada, o processo refere - se a solicitação de matrícula e de
            ///disciplina isolada, deverá verificar se há disponível vaga na respectiva turma, para os itens da
            ///solicitação que estão cancelados e o motivo igual a etapa finalizada.
            if (solicitacaoAtual.Categoria == CategoriaSituacao.Encerrado && disciplinaIsolada && etapaAtual.ControleVagas)
            {
                return true;
            }

            return false;
        }

        public void ValidarParcelasGrupoEscalonamento(GrupoEscalonamentoItemVO model)
        {
            var escalonamento = this.EscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<Escalonamento>(model.SeqEscalonamento));

            if (model.Parcelas.Count == 0)
            {
                var parcelasGrupoEscalonamento = this.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(model.SeqGrupoEscalonamento), IncludesGrupoEscalonamento.Itens
                                                                                                                         | IncludesGrupoEscalonamento.Itens_Escalonamento
                                                                                                                         | IncludesGrupoEscalonamento.Itens_Parcelas);



                var servicoProcesso = ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(model.SeqServico));
                var possuiIntegracaoFinanceira = servicoProcesso.IntegracaoFinanceira == IntegracaoFinanceira.NaoSeAplica ? false : true;

                if (possuiIntegracaoFinanceira && parcelasGrupoEscalonamento.Itens.Where(c => c.Seq != model.Seq).SelectMany(c => c.Parcelas).Count() == 0)
                {
                    throw new GrupoEscalonamentoEtapasSemParcelasException();
                }
            }

            var dadosProcesso = this.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(model.SeqGrupoEscalonamento), p => new
            {
                p.SeqProcesso,
                p.Processo.Servico.Token,
                p.Processo.DataFim,
                p.Processo.SeqCicloLetivo,
                p.Processo.Servico.ObrigatorioIdentificarParcela,
                p.Processo.UnidadesResponsaveis.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).SeqEntidadeResponsavel
            });

            /*1.1 Para os processos do serviço que possui o token SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA, 
            considerar a seguinte regra:"*/
            if (dadosProcesso.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
            {
                var cursoOfertaLocalidades = CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(dadosProcesso.SeqEntidadeResponsavel);

                if (cursoOfertaLocalidades.Any() && dadosProcesso.SeqCicloLetivo.HasValue)
                {
                    var cursoOfertaLocalidade = cursoOfertaLocalidades.First();
                    var cursoOfertaLocalidadeTurnos = CursoOfertaLocalidadeTurnoDomainService.BuscarTurnosPorLocalidadeCusroOfertaSelect(cursoOfertaLocalidade.RecuperarSeqLocalidade(), cursoOfertaLocalidade.SeqCursoOferta);

                    if (cursoOfertaLocalidadeTurnos.Any())
                    {
                        var seqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnos.First().Seq;

                        var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosProcesso.SeqCicloLetivo.Value, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                        #region Recuperando os meses do ciclo letivo

                        //Lista que terá os meses do ciclo letivo
                        List<int> mesesCicloLetivo = new List<int>() { datasEventoLetivo.DataInicio.Month };

                        //Variáveis para auxiliar a verificar qual o último mês do ciclo
                        var achouMesFimCiclo = false;
                        var contadorMeses = 1;

                        //Recupera os meses do ciclo, se a data for menor ou igual que a data fim do ciclo, ela será validada
                        while (!achouMesFimCiclo)
                        {
                            var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMeses);

                            /*Validação para adicionar na lista somente as datas que são menores que a data fim
                            Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
                            último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
                            if (dataVerificar <= datasEventoLetivo.DataFim)
                                mesesCicloLetivo.Add(dataVerificar.Month);
                            else
                                achouMesFimCiclo = true;

                            contadorMeses++;
                        }

                        var ultimoMesCiclo = mesesCicloLetivo.Last();

                        #endregion

                        var numeroDivisaoParcelas = this.SearchProjectionByKey(new SMCSeqSpecification<GrupoEscalonamento>(model.SeqGrupoEscalonamento), x => x.NumeroDivisaoParcelas);

                        #region Validação do fator de divisão temporiariamente comentada conforme Task 40404

                        //if (numeroDivisaoParcelas.HasValue)
                        //{
                        //    foreach (var parcela in modelo.Parcelas)
                        //    {
                        //        switch (numeroDivisaoParcelas.Value)
                        //        {
                        //            case 4:

                        //                //Fator de divisão = 4: a data de vencimento deve estar dentro do primeiro mês do ciclo letivo
                        //                if (parcela.DataVencimentoParcela.Month != datasEventoLetivo.DataInicio.Month)
                        //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

                        //                break;

                        //            case 3:

                        //                //Fator de divisão = 3: a data de vencimento deve estar dentro do segundo mês do ciclo letivo
                        //                var segundoMesCiclo = datasEventoLetivo.DataInicio.AddMonths(1);

                        //                if (parcela.DataVencimentoParcela.Month != segundoMesCiclo.Month)
                        //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

                        //                break;

                        //            case 2:

                        //                //Fator de divisão = 2: a data de vencimento deve estar dentro do terceiro mês do ciclo letivo
                        //                var terceiroMesCiclo = datasEventoLetivo.DataInicio.AddMonths(2);

                        //                if (parcela.DataVencimentoParcela.Month != terceiroMesCiclo.Month)
                        //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

                        //                break;

                        //            case 1:

                        //                //Fator de divisão = 1: a data de vencimento deve estar dentro do quarto mês do ciclo letivo ou demais meses
                        //                var quartoMesCiclo = datasEventoLetivo.DataInicio.AddMonths(3);

                        //                //Lista que terá os meses a serem comparados com a data de vencimento
                        //                List<int> mesesCicloLetivoAPartirQuartoMes = new List<int>() { quartoMesCiclo.Month };

                        //                //Variáveis para auxiliar a verificar qual o último mês do ciclo, considerando que pegará do quarto mês em diante
                        //                var achouMesFimCicloDepoisQuartoMes = false;
                        //                var contadorMesesDepoisQuartoMes = 4;

                        //                //Recupera os meses do ciclo do quarto mês em diante, se a data for menor ou igual que a data fim do ciclo, ela será validada
                        //                while (!achouMesFimCicloDepoisQuartoMes)
                        //                {
                        //                    var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMesesDepoisQuartoMes);

                        //                    /*Validação para adicionar na lista somente as datas que são menores que a data fim
                        //                    Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
                        //                    último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
                        //                    if (dataVerificar <= datasEventoLetivo.DataFim)
                        //                        mesesCicloLetivoAPartirQuartoMes.Add(dataVerificar.Month);
                        //                    else
                        //                        achouMesFimCicloDepoisQuartoMes = true;

                        //                    contadorMesesDepoisQuartoMes++;
                        //                }

                        //                //Verifica se a data de vencimento da parcela não está contida do quarto mês em diante da data de início do ciclo letivo
                        //                if (!mesesCicloLetivoAPartirQuartoMes.Contains(parcela.DataVencimentoParcela.Month) && parcela.DataVencimentoParcela <= datasEventoLetivo.DataFim)
                        //                    throw new GrupoEscalonamentoItemParcelaDataVencimentoForaFatorDivisaoException();

                        //                break;
                        //        }
                        //    }
                        //}

                        #endregion

                        ///Se a data fim do escalonamento da etapa for menor que a data início do [ciclo letivo]* do processo, a 
                        ///data de vencimento da parcela deverá ser maior ou igual a data fim do escalonamento e menor ou igual a 
                        ///data início do ciclo letivo. Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem de 
                        ///erro: "Configuração não permitida. A data de vencimento da parcela deverá ser maior ou igual à 
                        ///data/hora fim do escalonamento e menor ou igual a data início do ciclo letivo do processo."
                        if (escalonamento.DataFim.Date < datasEventoLetivo.DataInicio.Date)
                        {
                            foreach (var parcela in model.Parcelas)
                            {
                                if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
                                    parcela.DataVencimentoParcela.Date > datasEventoLetivo.DataInicio.Date)
                                    throw new GrupoEscalonamentoItemDataVencimentoMenorDataFimEscalonamentoException();
                            }
                        }
                        ///Se a data fim do escalonamento estiver no último mês do [ciclo letivo]* do processo, a data de 
                        ///vencimento da parcela deverá ser maior ou igual a data fim do escalonamento e menor que a data fim do 
                        ///ciclo letivo. Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem de erro:
                        ///"Configuração não permitida. A data de vencimento da parcela deverá ser maior ou igual à data/hora fim 
                        ///do escalonamento e menor que a data fim do ciclo letivo do processo."
                        else if (escalonamento.DataFim.Month == ultimoMesCiclo)
                        {
                            foreach (var parcela in model.Parcelas)
                            {
                                if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
                                    parcela.DataVencimentoParcela.Date >= datasEventoLetivo.DataFim.Date)
                                    throw new GrupoEscalonamentoItemVencimentoDeveSerMaiorDataFimEscalonamentoException();
                            }
                        }
                        ///Se a data fim do escalonamento não estiver em nenhum destes casos, a data de vencimento da parcela 
                        ///deverá ser maior ou igual a data fim do escalonamento e menor ou igual ao dia primeiro do mês 
                        ///subsequente à data fim do escalonamento. Caso isto não ocorra, abortar a operação e exibir a seguinte
                        ///mensagem de erro: "Configuração não permitida. A data de vencimento da parcela deverá ser maior ou 
                        ///igual à data/hora fim do escalonamento e menor ou igual ao dia primeiro do mês subsequente à data 
                        ///fim do escalonamento."
                        else
                        {
                            var dataMesSubsequenteDataFimEscalonamento = escalonamento.DataFim.AddMonths(1);
                            var dataPrimeiroDiaMesSubsequenteDataFimEscalonamento = new DateTime(dataMesSubsequenteDataFimEscalonamento.Year, dataMesSubsequenteDataFimEscalonamento.Month, 1);

                            foreach (var parcela in model.Parcelas)
                            {
                                if (parcela.DataVencimentoParcela.Date < escalonamento.DataFim.Date ||
                                    parcela.DataVencimentoParcela.Date > dataPrimeiroDiaMesSubsequenteDataFimEscalonamento.Date)
                                    throw new GrupoEscalonamentoItemDatavencimentoMenorIgualPrimeiroDiaException();
                            }
                        }
                    }
                }
            }
        }
    }
}