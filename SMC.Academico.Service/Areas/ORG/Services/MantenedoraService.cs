using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class MantenedoraService : SMCServiceBase, IMantenedoraService
    {
        private MantenedoraDomainService MantenedoraDomainService => this.Create<MantenedoraDomainService>();


        public MantenedoraData BuscarMantenedora(long seq)
        {
            return MantenedoraDomainService.BuscarMantenedora(seq).Transform<MantenedoraData>();
        }

        /// <summary>
        /// Salvar Mantenedora
        /// </summary>
        /// <param name="mantenedora">Dados da mantenedora</param>
        /// <returns>Sequencial da mantenedora</returns>
        public long SalvarMantenedora(MantenedoraData mantenedora)
        {
            return MantenedoraDomainService.SalvarMantenedora(mantenedora.Transform<MantenedoraVO>());
        }
    }
}
