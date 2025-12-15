using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoFormacaoEspecificaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.SeqTipoFormacaoEspecifica))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataInicioVinculo))]
        [SMCHidden]
        public DateTime? DataMinima { get; set; }

        /// <summary>
        /// Estas despendecias são utilizadas no colaborador e colaborador vinculo
        /// </summary>
        [SMCMaxDate(nameof(DataValidacaoInicio))]
        [SMCMinDate(nameof(DataMinima))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo), nameof(ColaboradorVinculoController.CalcularDataMaxima), "ColaboradorVinculo", false, nameof(DataFim))]
        [SMCDependency(nameof(DataFim), nameof(ColaboradorVinculoController.CalcularDataMaxima), "ColaboradorVinculo", false, nameof(ColaboradorVinculoDynamicModel.DataFimVinculo))]
        [SMCHidden]
        public DateTime? DataValidacaoInicio { get; set; }

        [SMCDependency(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo))]
        [SMCHidden]
        public DateTime? DatatMaxima { get; set; }

        /// <summary>
        /// Estas despendecias são utilizadas no colaborador e colaborador vinculo
        /// </summary>
        [SMCConditionalRequired(nameof(ColaboradorVinculoDynamicModel.DataFimVinculo), SMCConditionalOperation.NotEqual, "")]
        [SMCMaxDate(nameof(DatatMaxima))]
        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }
    }
}