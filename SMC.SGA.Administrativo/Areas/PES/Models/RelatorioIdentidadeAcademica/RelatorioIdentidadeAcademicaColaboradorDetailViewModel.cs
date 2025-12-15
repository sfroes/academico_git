using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class RelatorioIdentidadeAcademicaColaboradorDetailViewModel : SMCViewModelBase
    {
        [ColaboradorLookup]
        [SMCDependency(nameof(RelatorioIdentidadeAcademicaFiltroViewModel.CriaVinculoInstitucional))]
        [SMCDependency(nameof(RelatorioIdentidadeAcademicaFiltroViewModel.VinculoAtivo))]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCUnique]
        public ColaboradorLookupViewModel Colaborador { get; set; }
    }
}