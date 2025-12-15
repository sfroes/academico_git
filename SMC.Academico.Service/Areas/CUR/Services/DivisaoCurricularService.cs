using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class DivisaoCurricularService : SMCServiceBase, IDivisaoCurricularService
    {
        #region [ DomainService ] 

        public DivisaoCurricularDomainService DivisaoCurricularDomainService
        {
            get { return this.Create<DivisaoCurricularDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar a divisão curricular e o nivel de ensino
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Objeto divisão curricular</returns>
        public DivisaoCurricularData BuscarDivisaoCurricular(long seq)
        {           
            return DivisaoCurricularDomainService.BuscarDivisaoCurricular(seq).Transform<DivisaoCurricularData>();
        }

        /// <summary>
        /// Salvar uma divisão curricular
        /// </summary>
        /// <param name="divisaoCurricularData"></param>
        /// <returns>Sequencial da Divisão Curricular</returns>
        public long SalvarDivisaoCurricular(DivisaoCurricularData divisaoCurricularData)
        {
            return DivisaoCurricularDomainService.SalvarDivisaoCurricular(divisaoCurricularData.Transform<DivisaoCurricularVO>());
        }

        /// <summary>
        /// Buscar lista de divisões curriculares cadastradas para o nível de ensino do curso 
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencia do curriculo curso oferta</param>
        /// <returns>Lista de divisões curriculares do mesmo nível de ensino</returns>
        public List<SMCDatasourceItem> BuscarDivisoesCurricularesPorCurriculoCursoOferta(long seqCurriculoCursoOferta)
        {
            return DivisaoCurricularDomainService.BuscarDivisoesCurricularesPorCurriculoCursoOferta(seqCurriculoCursoOferta);
        }
        
    }
}