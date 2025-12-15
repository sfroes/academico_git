using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class ConsultaDivisoesMatrizCurricularService : SMCServiceBase, IConsultaDivisoesMatrizCurricularService
    {
        #region [ DomainService ]

        private MatrizCurricularDomainService MatrizCurricularDomainService
        {
            get { return this.Create<MatrizCurricularDomainService>(); }
        }

        #endregion              

        /// <summary>
        /// Busca os dados das divisões da mariz curricular com seus grupos e componentes
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões da matriz curricular</returns>
        public ConsultaDivisoesMatrizCurricularData BuscarConsultaDivisoesMatrizCurricular(long seq)
        {
            return this.MatrizCurricularDomainService
                .BuscarConsultaDivisoesMatrizCurricular(seq)
                .Transform<ConsultaDivisoesMatrizCurricularData>();
        }
    }
}
