using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class InstituicaoNivelTipoOfertaCursoService : SMCServiceBase, IInstituicaoNivelTipoOfertaCursoService
    {
        #region Services

        private InstituicaoNivelTipoOfertaCursoDomainService InstituicaoNivelTipoOfertaCursoDomainService
        {
            get { return this.Create<InstituicaoNivelTipoOfertaCursoDomainService>(); }
        }

        #endregion Services

        /// <summary>
        /// Busca a lista de Tipo Oferta para o Curso e Instituicao para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de Tipo Oferta</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOfertaCursoSelect(long seqCurso)
        {
            return this.InstituicaoNivelTipoOfertaCursoDomainService.BuscarInstituicaoNivelTipoOfertaCursoSelect(seqCurso);
        }
    }
}
