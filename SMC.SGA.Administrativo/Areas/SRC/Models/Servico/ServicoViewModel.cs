using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposServico { get; set; }

        public List<SMCDatasourceItem> TemplatesSGF { get; set; }

        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        public List<SMCDatasourceItem> TiposVinculo { get; set; }

        public List<SMCDatasourceItem> TiposTransacao { get; set; }

        public List<SMCDatasourceItem> Situacoes { get; set; }

        public List<SMCDatasourceItem> ServicosRestricao { get; set; }

        public List<SMCDatasourceItem> MotivosBloqueio { get; set; }

        public List<SMCDatasourceItem> TiposNotificacaoSGA { get; set; }

        public List<SMCDatasourceItem> TaxasGRA { get; set; }

        public List<SMCDatasourceItem> TiposEmissaoTaxas { get; set; }

        public List<SMCDatasourceItem> BancosAgenciasContasCarteiras { get; set; }

        public List<SMCDatasourceItem> TiposDocumentoSelect { get; set; }

        public List<SMCDatasourceItem> EtapasTemplateProcessoSgf { get; set; }

        #endregion

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposServico), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoTipoServico))]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long SeqTipoServico { get; set; }

        public string DescricaoTipoServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TemplatesSGF), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoTemplateSGF))]
        [SMCDependency(nameof(SeqTipoServico), nameof(ServicoController.BuscarTemplatesSGFPorTipoServicoSelect), "Servico", true)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long SeqTemplateProcessoSgf { get; set; }

        public string DescricaoTemplateSGF { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        //[SMCRegularExpression(REGEX.TOKEN)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string TokenAcessoAtendimento { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        //[SMCRegularExpression(REGEX.TOKEN)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public string TokenPermissaoManutencaoProcesso { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(CampoOrigemReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public OrigemSolicitacaoServico OrigemSolicitacaoServico { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(TipoAtuacao), nameof(ServicoController.BuscarValidarSituacaoFutura), "Servico", true)]
        [SMCConditionalReadonly(nameof(TipoAtuacao), SMCConditionalOperation.Equals, TipoAtuacao.Ingressante, PersistentValue = true)]
        public bool? ValidarSituacaoFutura { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public bool? ExigeJustificativaSolicitacao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public PermiteReabrirSolicitacao PermiteReabrirSolicitacao { get; set; }

        [SMCHidden]
        public bool ExibirSecaoReabrir { get; set; }

        [SMCSelect]
        [SMCConditionalDisplay(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteTodas, RuleName = "CDP1")]
        [SMCConditionalDisplay(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso, RuleName = "CDP2")]
        [SMCConditionalRule("CDP1 || CDP2")]
        [SMCConditionalRequired(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteTodas, RuleName = "CRP1")]
        [SMCConditionalRequired(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso, RuleName = "CRP2")]
        [SMCConditionalRule("CRP1 || CRP2")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoPrazoReabertura? TipoPrazoReabertura { get; set; }

        [SMCConditionalDisplay(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteTodas, RuleName = "CDP3")]
        [SMCConditionalDisplay(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso, RuleName = "CDP4")]
        [SMCConditionalRule("CDP3 || CDP4")]
        [SMCConditionalRequired(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteTodas, RuleName = "CRP3")]
        [SMCConditionalRequired(nameof(PermiteReabrirSolicitacao), SMCConditionalOperation.Equals, PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso, RuleName = "CRP4")]
        [SMCConditionalRule("CRP3 || CRP4")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public short? NumeroDiasPrazoReabertura { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public AcaoLiberacaoTrabalho? AcaoLiberacaoTrabalho { get; set; }
        
        //Add here for DEVG        
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public IntegracaoFinanceira IntegracaoFinanceira { get; set; }

        
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public bool? ObrigatorioIdentificarParcela { get; set; }
        //Add here for DEVG

        [SMCRequired]
        [SMCMultiline(Rows = 3)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string OrientacaoDeferimento { get; set; }

        [SMCHidden]
        public bool ExibirCampoTipoTransacao { get; set; }

        [SMCHidden]
        public bool ExibirSecaoMotivosBloqueioParcelas { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        [SMCHidden]
        public bool CampoOrigemReadOnly { get; set; }

        [SMCHidden]
        public bool CamposTaxaReadOnly { get; set; }

        [SMCHidden]
        public OrigemSolicitacaoServico OrigemSolicitacaoServicoAuxiliarTaxas { get; set; }

        public string MensagemInformativaTaxasNaoParametrizadas { get; set; }

        public string MensagemInformativaConfiguracaoTaxaNaoPermitida { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelServicoViewModel> InstituicaoNivelServicos { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelServicoSemTipoTransacaoViewModel> InstituicaoNivelServicosSemTipoTransacao => InstituicaoNivelServicos?.TransformMasterDetailList<InstituicaoNivelServicoSemTipoTransacaoViewModel>();

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoSituacaoSolicitarViewModel> SituacoesSolicitar { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoSituacaoAtenderViewModel> SituacoesAtender { get; set; }   

        [SMCDetail]
        [SMCConditionalDisplay(nameof(ExibirSecaoReabrir), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoSituacaoReabrirViewModel> SituacoesReabrir { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay(nameof(ExigeJustificativaSolicitacao), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<JustificativaSolicitacaoServicoViewModel> Justificativas { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<RestricaoSolicitacaoSimultaneaViewModel> RestricoesSolicitacaoSimultanea { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay(nameof(ExibirSecaoMotivosBloqueioParcelas), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoMotivoBloqueioParcelaViewModel> MotivosBloqueioParcela { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoTipoDocumentoViewModel> TiposDocumento { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ServicoTipoNotificacaoViewModel> TiposNotificacao { get; set; }        

        [SMCDetail]
        [SMCConditionalReadonly(nameof(CamposTaxaReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ServicoTaxaViewModel> Taxas { get; set; }

        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Largest)]
        [SMCConditionalReadonly(nameof(CamposTaxaReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ServicoParametroEmissaoTaxaViewModel> ParametrosEmissaoTaxa { get; set; }

        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Largest)]
        [SMCConditionalReadonly(nameof(CamposTaxaReadOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ServicoParametroEmissaoTaxaSemCamposEmissaoViewModel> ParametrosEmissaoTaxaSemCamposEmissao => ParametrosEmissaoTaxa?.TransformMasterDetailList<ServicoParametroEmissaoTaxaSemCamposEmissaoViewModel>();
    
        [SMCHidden]
        [SMCIgnoreProp]
        public SMCMasterDetailList<ServicoTipoNotificacaoViewModel> ListaHistoricoTipoNotificacao { get; set; }
    
    
    }
}