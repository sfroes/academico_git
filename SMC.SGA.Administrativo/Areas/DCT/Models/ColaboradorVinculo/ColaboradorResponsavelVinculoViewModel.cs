using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorResponsavelVinculoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [ColaboradorLookup]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.TipoAtuacao))]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.SeqEntidadeVinculo))]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataInicioVinculo))]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid16_24)]
        public ColaboradorLookupViewModel SeqColaboradorResponsavel { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataInicioVinculo))]
        [SMCHidden]
        public DateTime? DataMinima { get; set; }

        [SMCMaxDate(nameof(DataValidacaoInicio))]
        [SMCMinDate(nameof(DataMinima))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo), nameof(ColaboradorVinculoController.CalcularDataMaxima), "ColaboradorVinculo", false, nameof(DataFim))]
        [SMCDependency(nameof(DataFim), nameof(ColaboradorVinculoController.CalcularDataMaxima), "ColaboradorVinculo", false, nameof(ColaboradorVinculoDynamicModel.DataFimVinculo))]
        [SMCHidden]
        public DateTime? DataValidacaoInicio { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo))]
        [SMCHidden]
        public DateTime? DatatMaxima { get; set; }

        [SMCConditionalRequired(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo), SMCConditionalOperation.NotEqual, "")]
        [SMCMaxDate(nameof(DatatMaxima))]
        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }
    }
}