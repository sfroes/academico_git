using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class MotivoBloqueioDomainService : AcademicoContextDomain<MotivoBloqueio>
    {
        /// <summary>
        /// Busca o sequencial do motivo de bloqueio a partir de um token informado.
        /// </summary>
        /// <param name="token">Token do motivo de bloqueio</param>
        /// <returns>Sequencial do motivo de bloqueio encontrado</returns>
        public long BuscarSeqMotivoBloqueioPorToken(string token)
        {
            var spec = new MotivoBloqueioFilterSpecification { Token = token };
            return this.SearchProjectionByKey(spec, x => x.Seq);
        }

    }
}