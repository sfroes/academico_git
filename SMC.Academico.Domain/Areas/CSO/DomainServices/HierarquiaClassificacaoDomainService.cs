using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Framework.Domain;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class HierarquiaClassificacaoDomainService : AcademicoContextDomain<HierarquiaClassificacao>
    {
        public long BuscarSeqHierarquiaClassificacao(string descricao)
        {
            var spec = new HierarquiaClassificacaoFilterSpecification() { Descricao = descricao };
            return SearchProjectionBySpecification(spec, x => x.Seq).FirstOrDefault();
        }
    }
}
