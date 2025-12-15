using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.FIN.Lookups.LK_FIN_001_Pessoa_Juridica
{
    public class PessoaJuridicaLookupAttribute : SMCLookupAttribute
    {
        public PessoaJuridicaLookupAttribute()
           : base("PessoaJuridica")
        {
            this.Filter = typeof(PessoaJuridicaLookupFiltroViewModel);
            this.Model = typeof(PessoaJuridicaLookupViewModel);
            this.Service<IPessoaJuridicaService>(nameof(IPessoaJuridicaService.BuscarPessoasJuridicas));
        }
    }
}
