using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TitulacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrauAcademicoService), nameof(IGrauAcademicoService.BuscarGrauAcademicoSelect), values: new string[] { nameof(GrauAcademicoAtivo) })]
        public List<SMCDatasourceItem> GrauAcademicoSelect { get; set; }

        #endregion DataSources

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid6_24)]
        [SMCOrder(0)]
        public string Descricao { get; set; }

        [CursoLookup]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        [SMCInclude("Curso")]
        public CursoLookupViewModel SeqCurso { get; set; }

        [SMCOrder(2)]
        [SMCSelect(nameof(GrauAcademicoSelect), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid8_24)]
        public long? SeqGrauAcademico { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid4_24)]
        public bool? Ativo { get; set; }

        [SMCHidden]
        public bool GrauAcademicoAtivo { get { return true; } }
    }
}