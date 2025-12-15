using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class DivisaoMatrizCurricularService : SMCServiceBase, IDivisaoMatrizCurricularService
    {
        #region [ DomainService ] 

        public DivisaoMatrizCurricularDomainService DivisaoMatrizCurricularDomainService
        {
            get { return this.Create<DivisaoMatrizCurricularDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca as divisões de matrizes curriculares com as descrições
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularDescricaoSelect(long seqMatrizCurricular)
        {
            return this.DivisaoMatrizCurricularDomainService.BuscarDivisoesMatrizCurricularDescricaoSelect(seqMatrizCurricular);
        }

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoSelect(long seqMatrizCurricular)
        {
            return this.DivisaoMatrizCurricularDomainService.BuscarDivisoesMatrizCurricularTipoSelect(seqMatrizCurricular);
        }

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição de acordo com o tipo de requisito (Pré ou Có)
        /// </summary>       
        /// <param name="tipoRequisito">Tipo do requisito selecionado</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular selecionada</param>
        /// <param name="seqDivisaoMatrizCurricular">Sequencial da divisão matriz curricular selecionada</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        public List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoPorTipoRequisitoSelect(TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqDivisaoMatrizCurricular)
        {
            return this.DivisaoMatrizCurricularDomainService.BuscarDivisoesMatrizCurricularTipoPorTipoRequisitoSelect(tipoRequisito, seqMatrizCurricular, seqDivisaoMatrizCurricular);
        }
    }
}
