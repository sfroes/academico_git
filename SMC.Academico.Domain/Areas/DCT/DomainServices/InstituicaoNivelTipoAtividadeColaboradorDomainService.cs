using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
    public class InstituicaoNivelTipoAtividadeColaboradorDomainService : AcademicoContextDomain<InstituicaoNivelTipoAtividadeColaborador>
    {
        #region [ DomainService ]

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService => Create<CursoOfertaLocalidadeDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Retorna os tipos de atividade configurados para os colaboradoes na instituição logada
        /// </summary>
        /// <param name="filtroVO">Dados do filtro</param>
        /// <returns>Dados das atividades configuradas para os colaboradoes na instituição logada</returns>
        public List<SMCDatasourceItem> BuscarTiposAtividadeColaboradorSelect(InstituicaoNivelTipoAtividadeColaboradorFiltroVO filtroVO)
        {
            if (filtroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
                FilterHelper.AtivarApenasFiltros(CursoOfertaLocalidadeDomainService, FILTER.INSTITUICAO_ENSINO);
            }

            var spec = new InstituicaoNivelTipoAtividadeColaboradorFilterSpecification();
            if (filtroVO.SeqCursoOfertaLocalidade.HasValue)
            {
                spec.SeqNivelEnsino = CursoOfertaLocalidadeDomainService
                    .SearchProjectionByKey(filtroVO.SeqCursoOfertaLocalidade.Value, p => p.CursoOferta.Curso.SeqNivelEnsino);
            }

            // Recupera os tipos de atividade distintos para todos os níveis de ensino.
            // Por utilizar como entidade base InstituicaoNivelTipoAtividadeColaborador serão aplicados os filtros por instituição e nível de ensino
            var atividades = this.SearchProjectionBySpecification(spec, p => p.TipoAtividadeColaborador, isDistinct: true).ToList();

            if (filtroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
                FilterHelper.AtivarFiltros(CursoOfertaLocalidadeDomainService);
            }

            return atividades.Select(s => new SMCDatasourceItem((long)s, SMCEnumHelper.GetDescription(s)))
                             .OrderBy(o => o.Descricao)
                             .ToList();
        }
    }
}