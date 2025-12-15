using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupAttribute : SMCLookupAttribute
    {
        public FormacaoEspecificaLookupAttribute()
            : base("FormacaoEspecifica", SMCDisplayModeType.TreeView)
        {
            ModalWindowSize = SMCModalWindowSize.Largest;
            HideSeq = true;
            Filter = typeof(FormacaoEspecificaLookupFiltroViewModel);
            PrepareFilter = typeof(FormacaoEspecificaLookupPrepareFilter);
            Model = typeof(FormacaoEspecificaLookupViewModel);
            Service<IFormacaoEspecificaService>(nameof(IFormacaoEspecificaService.BuscarFormacoesEspecificasLookup));
            SelectService<IFormacaoEspecificaService>(nameof(IFormacaoEspecificaService.BuscarFormacoesEspecificasGridLookup));
            Transformer = typeof(FormacaoEspecificaLookupTransformer); //O lookup já devolve somente os ativos conforme documentação do mesmo.
        }
    }
}