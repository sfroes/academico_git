using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class RestricaoSolicitacaoSimultaneaDomainService : AcademicoContextDomain<RestricaoSolicitacaoSimultanea>
    {
        public List<RestricaoSolicitacaoSimultanea> BuscarRestricoesSolicitacaoSimultanea(RestricaoSolicitacaoSimultaneaFilterSpecification filtro)
        {
            return this.SearchBySpecification(filtro).ToList();
        }
    }
}