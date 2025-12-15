using SMC.Academico.Common.Areas.CSO.Includes;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoNivelTipoOfertaCursoDomainService : AcademicoContextDomain<InstituicaoNivelTipoOfertaCurso>
    {
        #region [ DomainServices ]

        private CursoDomainService CursoDomainService
        {
            get { return this.Create<CursoDomainService>(); }
        }

        #endregion Services

        /// <summary>
        /// Busca a lista de Tipo Oferta para o Curso e Instituicao para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de Tipo Oferta</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOfertaCursoSelect(long seqCurso)
        {
            var cursoNivel = CursoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Curso>(seqCurso), p => p.SeqNivelEnsino);

            InstituicaoNivelTipoOfertaCursoFilterSpecification spec = new InstituicaoNivelTipoOfertaCursoFilterSpecification();
            spec.SeqNivelEnsino = cursoNivel;

            var listTipoOfertaCurso = this.SearchBySpecification(spec, IncludesInstituicaoNivelTipoOfertaCurso.InstituicaoNivel | IncludesInstituicaoNivelTipoOfertaCurso.TipoOfertaCurso).Select(s => s.TipoOfertaCurso);

            return listTipoOfertaCurso.TransformList<SMCDatasourceItem>();
        }
    }
}
