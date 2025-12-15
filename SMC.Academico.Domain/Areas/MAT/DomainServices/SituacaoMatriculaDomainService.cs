using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SituacaoMatriculaDomainService : AcademicoContextDomain<SituacaoMatricula>
    {
        #region [ DomainServices ]

        private InstituicaoNivelSituacaoMatriculaDomainService InstituicaoNivelSituacaoMatriculaDomainService => Create<InstituicaoNivelSituacaoMatriculaDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// Busca as situações de matrícula configuradas na instituição
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>Dados das situaçoes de matrícula configuradas na instituição</returns>
        public List<SMCDatasourceItem> BuscarSituacoesMatriculasDaInstituicaoSelect(SituacaoMatriculaFiltroVO filtros)
        {
            var spec = filtros.Transform<InstituicaoNivelSituacaoMatriculaFilterSpecification>();
            spec.SetOrderBy(o => o.SituacaoMatricula.Descricao);

            return this.InstituicaoNivelSituacaoMatriculaDomainService.SearchProjectionBySpecification(spec, p =>
                new SMCDatasourceItem()
                {
                    Seq = p.SeqSituacaoMatricula,
                    Descricao = p.SituacaoMatricula.Descricao
                }, isDistinct: true)
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        public List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipo(long seq)
        {
            var retono = this.SearchBySpecification(new SituacaoMatriculaFilterSpecification() { SeqTipoSituacaoMatricula = seq }).ToList();

            List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();

            foreach (var item in retono)
            {
                lista.Add(new SMCDatasourceItem { Seq = item.Seq, Descricao = item.Descricao });
            }

            return lista;
        }

        public List<SMCDatasourceItem> BuscarSituacoesMatricula(SituacaoMatriculaFiltroVO filtro)
        {
            var spec = filtro.Transform<SituacaoMatriculaFilterSpecification>();
            spec.SetOrderBy(o => o.Descricao);

            var retorno = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao

            }).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca uma situação de matrícula pelo seu token
        /// </summary>
        /// <param name="token">Token para recuperar a situação de matrícula</param>
        /// <returns></returns>
        public long BuscarSituacaoMatriculaPorToken(string token)
        {
            return SearchProjectionByKey(new SituacaoMatriculaPorTokenSpecification(token), x => x.Seq);
        }

        /// <summary>
        /// Busca uma situação de matricula por seu token e recupera o item como um SMCDataSourceItem
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public SMCDatasourceItem BuscarSituacaoMatriculaItemSelectPorToken(string token)
        {
            var situacaoMatricula = SearchProjectionByKey(new SituacaoMatriculaPorTokenSpecification(token), x => new SMCDatasourceItem()
            {
                Seq  =x.Seq,
                Descricao = x.Descricao
            });

            return situacaoMatricula;
        }
    }
}