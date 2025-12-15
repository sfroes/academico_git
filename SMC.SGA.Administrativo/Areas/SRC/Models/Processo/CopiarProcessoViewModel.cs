using Microsoft.IdentityModel.Protocols.WSIdentity;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CopiarProcessoViewModel : SMCViewModelBase
    {
        public string MensagemInformativa { get; set; }

        public CopiarProcessoOrigemViewModel ProcessoOrigem { get; set; }

        [SMCReadOnly]
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCRequired]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        [SMCRequired]
        public string Descricao { get; set; }

        [CicloLetivoLookup]
        [SMCMapProperty("CicloLetivo.Seq")]
        [SMCInclude("CicloLetivo")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCRequired]
        public DateTime DataInicio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCMinDateNow]
        [SMCMinDate(nameof(DataInicio))]
        public DateTime? DataFim { get; set; }

        [SMCHidden]
        public string TokenTipoServico { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCCurrency]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(TokenTipoServico), SMCConditionalOperation.Equals, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, RuleName = "R2")]
        [SMCConditionalDisplay(nameof(TokenServico), SMCConditionalOperation.Equals, TOKEN_SERVICO.PRORROGACAO_PRAZO_CONCLUSAO, RuleName = "R3")]
        [SMCConditionalRule("R1 || R2 || R3")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public decimal? ValorPercentualServicoAdicional { get; set; }

        [SMCHidden]
        public bool ExibirSecaoGrupoEscalonamento { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<CopiarProcessoDetalheEtapaViewModel> EtapasCopiar { get; set; }

        [SMCDetail]
        [SMCConditionalDisplay(nameof(ExibirSecaoGrupoEscalonamento), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<CopiarProcessoDetalheGrupoEscalonamentoViewModel> GruposEscalonamentoCopiar { get; set; }
    }
}