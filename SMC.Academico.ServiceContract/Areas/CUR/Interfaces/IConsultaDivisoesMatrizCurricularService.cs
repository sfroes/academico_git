using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IConsultaDivisoesMatrizCurricularService : ISMCService
    {  
        /// <summary>
        /// Busca os dados das divisões da mariz curricular com seus grupos e componentes
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões da matriz curricular</returns>
        ConsultaDivisoesMatrizCurricularData BuscarConsultaDivisoesMatrizCurricular(long seq);
    }
}
