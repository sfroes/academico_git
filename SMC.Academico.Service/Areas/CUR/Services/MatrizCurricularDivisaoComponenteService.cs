using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class MatrizCurricularDivisaoComponenteService : SMCServiceBase, IMatrizCurricularDivisaoComponenteService
    {
        #region [ DomainService ]

        private DivisaoMatrizCurricularComponenteDomainService DivisaoMatrizCurricularComponenteDomainService
        {
            get { return this.Create<DivisaoMatrizCurricularComponenteDomainService>(); }
        }

        private MatrizCurricularDivisaoComponenteDomainService MatrizCurricularDivisaoComponenteDomainService
        {
            get { return this.Create<MatrizCurricularDivisaoComponenteDomainService>(); }
        }
        
        #endregion [ DomainService ]

        /// <summary>
        /// Busca a matriz curricular divisão componente cadastrada e configurada
        /// </summary>
        /// <param name="seqMatrizCurricularDivisaoComponente">Sequencial da matriz curricular divisão componente</param>
        /// <returns>Registro da matriz curricular divisão configurado</returns>
        public MatrizCurricularDivisaoComponenteData BuscarConfiguracaoMatrizCurricular(long seqMatrizCurricularDivisaoComponente)
        {
            var matrizCurricularDivisaoComponente = MatrizCurricularDivisaoComponenteDomainService.BuscarMatrizCurricularDivisaoComponente(seqMatrizCurricularDivisaoComponente);
            return matrizCurricularDivisaoComponente.Transform<MatrizCurricularDivisaoComponenteData>();
        }

        /// <summary>
        /// Busca as divisoes de uma divisão matriz curricular compoenete
        /// </summary>
        /// <param name="seq">Sequencial da  divisão matriz curricular compoenete</param>
        /// <returns>ados da matriz curricular divisão componente</returns>
        public List<MatrizCurricularDivisaoComponenteData> BuscarDivisaoMatrizCurricularComponenteDivisoes(long seq)
        {
            var matrizCurricularDivisaoComponente = DivisaoMatrizCurricularComponenteDomainService.BuscarDivisaoMatrizCurricularComponenteDivisoes(seq);
            return matrizCurricularDivisaoComponente.TransformList<MatrizCurricularDivisaoComponenteData>();
        }
    }
}
