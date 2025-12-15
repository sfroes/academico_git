using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class TipoAgendaDomainService : AcademicoContextDomain<TipoAgenda>
    {
        public long SalvarTipoAgenda(TipoAgendaVO modelo)
        {
            TipoAgenda tipoAgenda = modelo.Transform<TipoAgenda>();

            this.SaveEntity(tipoAgenda);

            return tipoAgenda.Seq;
        }
    }
}