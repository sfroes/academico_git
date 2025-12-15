using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class PlanoEstudoService : SMCServiceBase, IPlanoEstudoService
    {
        #region [ DomainService ]

        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();      

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do plano de estudo
        /// </summary>
        /// <param name="seq">Sequencial do plano de estudo</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        public (long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoPlanoEstudo(long seq)
        {
            return PlanoEstudoDomainService.BuscarCicloLetivoLocalidadeTurnoPlanoEstudo(seq);
        }
    }
}
