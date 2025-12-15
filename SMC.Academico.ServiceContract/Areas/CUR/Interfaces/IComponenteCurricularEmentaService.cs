using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IComponenteCurricularEmentaService : ISMCService
    {
        /// <summary>
        /// Buscar os dados de um componente curricular ementa
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo para validar o período de vigência</param>
        /// <returns>Informações do componente curricular ementa</returns>
        ComponenteCurricularEmentaData BuscarComponenteCurricularEmentaPorComponenteCiclo(long seqComponenteCurricular, long seqCicloLetivo, long seqCursoOfertaLocalidadeTurno);
    }
}
