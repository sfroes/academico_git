using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularComponenteLookupAttribute : SMCLookupAttribute
    {
        public GrupoCurricularComponenteLookupAttribute(bool usarRetornoCustomizado = false) :
            base("GrupoCurricularComponente", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            HideSeq = true;
            Filter = typeof(GrupoCurricularComponenteLookupFiltroViewModel);
            Model = typeof(GrupoCurricularComponenteLookupViewModel);
            Service<IGrupoCurricularComponenteService>(nameof(IGrupoCurricularComponenteService.BuscarGruposCurricularesComponentesLookup));
            SelectService<IGrupoCurricularComponenteService>(nameof(IGrupoCurricularComponenteService.BuscarGruposCurricularesComponentesLookupSelecionado));
            Transformer = typeof(GrupoCurricularComponenteLookupTransformer);
            if (usarRetornoCustomizado)
                CustomReturn = @"SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_008_Grupo_Componente_Curricular/Views/_ValoresSelecionadosLookup";
        }
    }
}