using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Domain;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.CUR.DomainServices
{
    public class MatrizCurricularDivisaoComponenteDomainService : AcademicoContextDomain<MatrizCurricularDivisaoComponente>
    {
        /// <summary>
        /// Busca a matriz curricular divisão componente configurada
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular divisão componente</param>
        /// <returns>Dados da matriz curricular divisão componente</returns>
        public MatrizCurricularDivisaoComponente BuscarMatrizCurricularDivisaoComponente(long seq)
        {
            var matrizCurricularDivisaoComponente = this.SearchByKey(new SMCSeqSpecification<MatrizCurricularDivisaoComponente>(seq));
            
            return matrizCurricularDivisaoComponente;
        }
    }
}
