using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using SMC.Academico.UI.Mvc.Areas.SRC.Views.SolicitacaoServicoFluxoBase.App_LocalResources;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoCobrancaTaxaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        #region Data Source

        [SMCDataSource(SMCStorageType.Session)]
        public List<SMCDatasourceItem> TiposEmissaoTaxas { get; set; }

        #endregion

        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SOLICITACAO_COBRANCA_TAXA;

        [SMCHidden]
        public int? CodigoNucleo { get; set; }

        [SMCHidden]
        public bool ExibeMensagemInformativaTaxasSemValor { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCConditionalDisplay(nameof(ExibeMensagemInformativaTaxasSemValor), SMCConditionalOperation.Equals, true)]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativaTaxasSemValor => UIResource.MensagemInformativaTaxasSemValor;

        [SMCHidden]
        public bool ExisteTaxaAssociada { get; set; }

        public List<SolicitacaoCobrancaTaxaItemViewModel> Taxas { get; set; }

        public decimal ValorTotalTaxas { get; set; }

        [SMCHidden]
        public bool ExisteTaxaSemValor { get; set; }

        [SMCHidden]
        public bool ExisteTaxaComValorIncorreto { get; set; }

        [SMCSelect(nameof(TiposEmissaoTaxas))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoEmissaoTaxa TipoEmissaoTaxa { get; set; }
    }
}
