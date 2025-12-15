using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaTelefoneLookupAttribute : SMCLookupAttribute
    {
        public PessoaTelefoneLookupAttribute()
            : base("PessoaTelefone")
        {
            Filter = typeof(PessoaTelefoneLookupFiltroViewModel);
            Model = typeof(PessoaTelefoneLookupViewModel);
            Service<IPessoaTelefoneService>(nameof(IPessoaTelefoneService.BuscarPessoaTelefonesLookup));
            SelectService<IPessoaTelefoneService>(nameof(IPessoaTelefoneService.BuscarPessoaTelefonesLookup));
            CustomFilter = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_003_Associacao_Telefone/Views/CustomFilter";
            CustomView = "SMC.Academico.UI.Mvc.dll/Areas/PES/Lookups/LK_PES_003_Associacao_Telefone/Views/CustomView";
            ModalWindowSize = SMCModalWindowSize.Medium;
            AutoSearch = true;
        }
    }
}