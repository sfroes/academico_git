using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework.Extensions;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TurmaHistoricoFechamentoDiarioDomainService : AcademicoContextDomain<TurmaHistoricoFechamentoDiario>
    {
        public long ReabrirDiario(TurmaHistoricoFechamentoDiarioVO data)
        {
            var entity = data.Transform<TurmaHistoricoFechamentoDiario>();
            SaveEntity(entity);

            return entity.Seq;
        }
    }
}