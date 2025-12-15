using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularLookupAttribute : SMCLookupAttribute
    {
        public GrupoCurricularLookupAttribute()
            : base ("GrupoCurricular", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Medium;
            HideSeq = true;
            Filter = typeof(GrupoCurricularLookupFiltroViewModel);
            Model = typeof(GrupoCurricularLookupViewModel);
            Service<IGrupoCurricularService>(nameof(IGrupoCurricularService.BuscarGruposCurricularesLookup));
            SelectService<IGrupoCurricularService>(nameof(IGrupoCurricularService.BuscarGruposCurricularesLookupSelecionado));
            Transformer = typeof(GrupoCurricularLookupTransformer);
        }
    }
}
