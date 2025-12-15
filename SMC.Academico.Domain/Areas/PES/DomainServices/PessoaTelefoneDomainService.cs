using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaTelefoneDomainService : AcademicoContextDomain<PessoaTelefone>
    {
        public SMCPagerData<PessoaTelefone> BuscarPessoaTelefonesLookup(PessoaTelefoneFilterSpecification filtro)
        {
            int total;
            filtro.SetOrderByDescending(o => o.DataInclusao);
            var telefones = this.SearchBySpecification(filtro, out total, IncludesPessoaTelefone.Telefone);

            return new SMCPagerData<PessoaTelefone>(telefones, total);
        }
    }
}