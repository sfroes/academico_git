using SMC.Academico.Common.Constants;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPlanoEstudoService : ISMCService
    {
        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do plano de estudo
        /// </summary>
        /// <param name="seq">Sequencial do plano de estudo</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        (long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoPlanoEstudo(long seq);
    }   
}
