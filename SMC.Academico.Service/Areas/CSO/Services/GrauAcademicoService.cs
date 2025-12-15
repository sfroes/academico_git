using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class GrauAcademicoService : SMCServiceBase, IGrauAcademicoService
    {
        #region Services

        private GrauAcademicoDomainService GrauAcademicoDomainService
        {
            get { return this.Create<GrauAcademicoDomainService>(); }
        }

        #endregion Services

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoSelect(GrauAcademicoFiltroData filtro)
        {
            var filtroVO = filtro.Transform<GrauAcademicoFiltroVO>();
            return GrauAcademicoDomainService.BuscarGrauAcademicoSelect(filtroVO);
        }

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select
        /// </summary>
        /// <returns>Lista de grau academico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoAtivoSelect()
        {
            var filtroVO = new GrauAcademicoFiltroVO() { Ativo = true };

            return GrauAcademicoDomainService.BuscarGrauAcademicoSelect(filtroVO);
        }


        /// <summary>
        /// Grava um grau acadêmico
        /// </summary>
        /// <param name="GrauAcademicoData">Dados do grau acadêmico a ser gravado</param>
        /// <returns>Sequencial do grau acadêmico gravado</returns>
        public long SalvarGrauAcademico(GrauAcademicoData GrauAcademicoData)
        {
            GrauAcademico GrauAcademico = SMCMapperHelper.Create<GrauAcademico>(GrauAcademicoData);

            GrauAcademicoDomainService.SaveEntity(GrauAcademico);
            return GrauAcademico.Seq;
        }

        /// <summary>
        /// Busca a lista de Grau Academico para popular um Select do lookup
        /// </summary>
        /// <param name="filtro">Todos os filtros que podem ser aplicados na listagem de grau academico</param>
        /// <returns>Lista de grau academico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoLookupSelect(GrauAcademicoFiltroData filtro)
        {
            return this.GrauAcademicoDomainService.BuscarGrauAcademicoLookupSelect(filtro.Transform<GrauAcademicoFiltroVO>());
        }


        /// <summary>
        /// Busca a lista de Grau Academico para popular de acordo com a Entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial entidade</param>
        /// <param name="seqAtoNormativo">Sequencial ato normativo</param>
        /// <param name="seq">Sequencial entidade ato normativo</param>
        /// <returns>Lista de grau acadêmico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoPorEntidade(long? seqEntidade, long? seqAtoNormativo, long seq)
        {
            return this.GrauAcademicoDomainService.BuscarGrauAcademicoPorEntidade(seqEntidade, seqAtoNormativo, seq);
        }


        /// <summary>
        /// Busca a lista de Grau Academico para popular de acordo com a Entidade Curso
        /// </summary>
        /// /// <param name="seqCurso">Sequencial entidade curso</param>
        /// <returns>Lista de grau acadêmico</returns>
        public List<SMCDatasourceItem> BuscarGrauAcademicoPorNivelEnsinoCurso(long seqCurso)
        {
            return this.GrauAcademicoDomainService.BuscarGrauAcademicoPorNivelEnsinoCurso(seqCurso);
        }
    }
}