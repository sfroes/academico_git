using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Orientacao", Size = SMCSize.Grid24_24)]
    public class PessoaAtuacaoTermoIntercambioViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCDependency(nameof(IngressanteDynamicModel.Cpf))]
        [SMCDependency(nameof(IngressanteDynamicModel.NumeroPassaporte))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqInstituicaoEnsino))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqNivelEnsino))]
        [SMCDependency(nameof(IngressanteDynamicModel.SeqTipoVinculoAluno))]
        [SMCDependency(nameof(IngressanteDynamicModel.TipoMobilidade))]
        [SMCHidden(SMCViewMode.ReadOnly)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [TermoIntercambioLookup]
        public TermoIntercambioLookupViewModel SeqTermoIntercambio { get; set; }

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.ReadOnly)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24)]
        public string DescricaoTipoIntercambio { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid4_24)]
        public string TipoTermoIntercambioConfirmacao => DescricaoTipoIntercambio;

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid8_24)]
        public TermoIntercambioLookupViewModel TermoIntercambioConfirmacao => SeqTermoIntercambio;

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true)]
        [SMCHidden]
        public long SeqInstituicaoEnsinoExterna { get; set; }

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid12_24)]
        public string InstituicaoExterna { get; set; }

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true, new[] { nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCHidden]
        public bool ExistePeriodo { get; set; }

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true, new[] { nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCHidden]
        public bool PermiteOrientacao { get; set; }

        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true, new[] { nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCHidden]
        public bool RequerOrientacao { get; set; }

        [SMCConditionalDisplay(nameof(ExistePeriodo), true)]
        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambioDatas), "Ingressante", true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataInicio { get; set; }

        [SMCConditionalDisplay(nameof(ExistePeriodo), true)]
        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambioDatas), "Ingressante", true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataFim { get; set; }

        [SMCConditionalDisplay(nameof(PermiteOrientacao), true)]
        [SMCConditionalRequired(nameof(RequerOrientacao), true)]
        [SMCDependency(nameof(SeqTermoIntercambio), nameof(IngressanteController.BuscarDependenciasTermoIntercambio), "Ingressante", true, new[] { nameof(IngressanteDynamicModel.SeqNivelEnsino), nameof(IngressanteDynamicModel.SeqTipoVinculoAluno) })]
        [SMCGroupedProperty("Orientacao")]
        [SMCSelect(nameof(IngressanteDynamicModel.TiposOrientacao), NameDescriptionField = nameof(DescricaoTipoOrientacao))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqTipoOrientacao { get; set; }

        [SMCHidden]
        public string DescricaoTipoOrientacao { get; set; }

        [SMCConditionalDisplay(nameof(PermiteOrientacao), true, RuleName = "OrientacaoIngressantePermitida")]
        [SMCConditionalDisplay(nameof(SeqTipoOrientacao), SMCConditionalOperation.NotEqual, "", RuleName = "TipoOrientacaoIngressantePreenchida")]
        [SMCConditionalRule("OrientacaoIngressantePermitida && TipoOrientacaoIngressantePreenchida")]
        [SMCDetail]
        [SMCGroupedProperty("Orientacao")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<IngressanteOrientacaoViewModel> OrientacaoParticipacoesColaboradores { get; set; }
    }
}