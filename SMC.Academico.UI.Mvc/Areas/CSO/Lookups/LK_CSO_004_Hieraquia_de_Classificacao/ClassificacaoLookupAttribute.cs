using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    /// <summary>
    /// Lookup de classificações.
    /// Os parâmetros de quantidade máxima e mínima não são passados por dependency como os demais parâmetros, devem ser definidos como na EntidadeClassificacoesViewModel.
    /// </summary>
    public class ClassificacaoLookupAttribute : SMCLookupAttribute
    {
        public ClassificacaoLookupAttribute()
            : base("Classificacao", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Large;
            HideSeq = true;
            Filter = typeof(ClassificacaoLookupFiltroViewModel);
            Model = typeof(ClassificacaoLookupViewModel);
            Service<IClassificacaoService>(nameof(IClassificacaoService.BuscarClassificacaoPorHierarquiaClassificacao));
            SelectService<IClassificacaoService>(nameof(IClassificacaoService.BuscarClassificacaoPorHierarquiaClassificacaoLookup));
            Transformer = typeof(ClassificacaoLookupTransformer);
        }
    }
}