using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class ColaboradorVinculoCursoService : SMCServiceBase, IColaboradorVinculoCursoService
    {
        #region DomainServices

        private ColaboradorVinculoCursoDomainService ColaboradorVinculoCursoDomainService => Create<ColaboradorVinculoCursoDomainService>();

        #endregion

        /// <summary>
        /// Listar todos os cursos do vinculados ao colaborador
        /// </summary>
        /// <param name="spec">Filtros de pesquisa</param>
        /// <returns>Lista de colaboradores vinculo curso</returns>
        public List<ColaboradorVinculoCursoData> ListarColaboradorVinculoCursos(ColaboradorVinculoCursoFiltroData filtro)
        {
            return this.ColaboradorVinculoCursoDomainService.ListarColaboradorVinculoCursos(filtro.Transform<ColaboradorVinculoCursoFiltroVO>())
                                                                                            .TransformList<ColaboradorVinculoCursoData>();
        }

        /// <summary>
        /// Salvar colaborador vinculo curso
        /// </summary>
        /// <param name="modelo">Dados do vinculo a ser gravado</param>
        public void SalvarColaboradorVinculoCurso(ColaboradorVinculoData modelo)
        {
            this.ColaboradorVinculoCursoDomainService.SalvarColaboradorVinculoCurso(modelo.Transform<ColaboradorVinculoVO>());
        }
    }
}