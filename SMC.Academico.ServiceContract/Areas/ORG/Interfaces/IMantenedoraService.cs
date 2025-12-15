using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IMantenedoraService : ISMCService
    {   
        /// <summary>
        /// Buscar Mantenedora
        /// </summary>
        /// <param name="seq">Sequencial Mantenedora</param>
        /// <returns>Mantenedora</returns>
        MantenedoraData BuscarMantenedora(long seq);

        /// <summary>
        /// Salvar Mantenedora
        /// </summary>
        /// <param name="mantenedora">Dados da mantenedora</param>
        /// <returns>Sequencial da mantenedora</returns>
        long SalvarMantenedora(MantenedoraData mantenedora);
    }
}
