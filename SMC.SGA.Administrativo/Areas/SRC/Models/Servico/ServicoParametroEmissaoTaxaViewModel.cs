using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoParametroEmissaoTaxaViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        #region [ Campos Edição ]

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TiposEmissaoTaxas))]
        [SMCDependency(nameof(ServicoViewModel.OrigemSolicitacaoServicoAuxiliarTaxas), nameof(ServicoController.BuscarTiposEmissaoTaxa), "Servico", true, includedProperties: new[] { nameof(TipoEmissaoTaxaAuxiliar) })]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        public TipoEmissaoTaxa TipoEmissaoTaxa { get; set; }

        [SMCHidden]
        public TipoEmissaoTaxa TipoEmissaoTaxaAuxiliar => TipoEmissaoTaxa;

        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        public short? NumeroDiasPrazoVencimentoTitulo { get; set; }

        [SMCSelect(nameof(ServicoViewModel.BancosAgenciasContasCarteiras), NameDescriptionField = nameof(DescricaoBancoAgenciaContaCarteira))]
        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCDependency(nameof(ServicoViewModel.OrigemSolicitacaoServicoAuxiliarTaxas), nameof(ServicoController.BuscarBancosAgencias), "Servico", true, includedProperties: new[] { nameof(SeqBancoAgenciaContaCarteiraAuxiliar) })]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.List)]
        public long? SeqBancoAgenciaContaCarteira { get; set; }

        [SMCHidden]
        public string DescricaoBancoAgenciaContaCarteira { get; set; }

        [SMCHidden]
        public long? SeqBancoAgenciaContaCarteiraAuxiliar => SeqBancoAgenciaContaCarteira;

        [SMCHidden]
        public bool BloquearCamposFinanceiro { get { return true; } }

        [SMCDependency(nameof(SeqBancoAgenciaContaCarteira), nameof(ServicoController.BuscarCodigoBanco), "Servico", true)]
        [SMCConditionalReadonly(nameof(BloquearCamposFinanceiro), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.List)]
        public short? CodigoBancoEmissaoTitulo { get; set; }

        [SMCDependency(nameof(SeqBancoAgenciaContaCarteira), nameof(ServicoController.BuscarCodigoAgencia), "Servico", true)]
        [SMCConditionalReadonly(nameof(BloquearCamposFinanceiro), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.List)]
        public string CodigoAgenciaEmissaoTitulo { get; set; }

        [SMCDependency(nameof(SeqBancoAgenciaContaCarteira), nameof(ServicoController.BuscarCodigoConta), "Servico", true)]
        [SMCConditionalReadonly(nameof(BloquearCamposFinanceiro), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.List)]
        public string CodigoContaEmissaoTitulo { get; set; }

        [SMCDependency(nameof(SeqBancoAgenciaContaCarteira), nameof(ServicoController.BuscarCodigoCarteira), "Servico", true)]
        [SMCConditionalReadonly(nameof(BloquearCamposFinanceiro), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        public string CodigoCarteiraEmissaoTitulo { get; set; }

        //[SMCConditionalDisplay(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        //[SMCConditionalRequired(nameof(TipoEmissaoTaxa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoEmissaoTaxa.EmissaoBoleto)]
        //[SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        //[SMCHidden(SMCViewMode.List)]
        //public int? SeqMensagemTitulo { get; set; }

        #endregion [ Campos Edição ]

        #region [ Campos Lista ]

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        //[SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string ListaTipoEmissaoTaxa => TipoEmissaoTaxa.SMCGetDescription();

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        //[SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public short? ListaNumeroDiasPrazoVencimentoTitulo => NumeroDiasPrazoVencimentoTitulo;

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        //[SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public short? ListaCodigoBancoEmissaoTitulo => CodigoBancoEmissaoTitulo;

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        //[SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2s smc-size-lg-2")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string ListaCodigoAgenciaEmissaoTitulo => CodigoAgenciaEmissaoTitulo;

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2s smc-size-lg-2")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string ListaCodigoContaEmissaoTitulo => CodigoContaEmissaoTitulo;

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2s smc-size-lg-2")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string ListaCodigoCarteiraEmissaoTitulo => CodigoCarteiraEmissaoTitulo;

        //[SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        //[SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        //public int? ListaDescricaoMensagemTitulo => SeqMensagemTitulo;

        #endregion [ Campos Lista ]   
    }
}