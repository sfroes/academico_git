using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Specifications;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;

namespace SMC.Academico.Service.Areas.GRD.Services
{
    public class GradeHoraraCompartilhadaService : SMCServiceBase, IGradeHorariaCompartilhadService
    {
        #region [ DomainServices ]

        private GradeHorariaCompartilhadaDomainService GradeHorariaCompartilhadaDomainService => Create<GradeHorariaCompartilhadaDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// Buscar Grades de horarias compartilhadas
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Grades comprtilhadas filtradas</returns>
        public SMCPagerData<GradeHorariaCompartilhadaListarData> BuscarGradesHorariasCompartilhada(GradeHorariaCompartilhadaFitroData filtro)
        {
            return GradeHorariaCompartilhadaDomainService.BuscarGradesHorariasCompartilhada(filtro.Transform<GradeHorariaCompartilhadaFilterSpecification>())
                                                         .Transform<SMCPagerData<GradeHorariaCompartilhadaListarData>>();
        }

        /// <summary>
        /// Grava um compartilhamento de grade
        /// </summary>
        /// <param name="data">Modelo</param>
        /// <returns>Sequencial do compartilhamento criado</returns>
        public long SalvarGradeHorariaCompartilhada(GradeHorariaCompartilhadaData data)
        {
            return GradeHorariaCompartilhadaDomainService.SalvarGradeHorariaCompartilhada(data.Transform<GradeHorariaCompartilhadaVO>());
        }

        /// <summary>
        /// Busca um compartilhamento de grade
        /// </summary>
        /// <param name="seq">Sequencial do compartilhamento</param>
        /// <returns>Dados do compartilhamento</returns>
        public GradeHorariaCompartilhadaData BuscarGradeHorariaCompartilhada(long seq)
        {
            return GradeHorariaCompartilhadaDomainService.BuscarGradeHorariaCompartilhada(seq).Transform<GradeHorariaCompartilhadaData>();
        }
    }
}