using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoDomainService : AcademicoContextDomain<InstituicaoNivelTipoOrientacaoParticipacao>
    {
        #region [ DomainServices ]

        private TermoIntercambioDomainService TermoIntercambioDomainService { get => Create<TermoIntercambioDomainService>(); }

        #endregion [ DomainServices ]

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoComObrigatoriedadeSelect(InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification filtros)
        {
            var participacoes = this.SearchProjectionBySpecification(filtros, p => new
            {
                p.Seq,
                p.TipoParticipacaoOrientacao,
                p.ObrigatorioOrientacao
            }).ToList();

            return participacoes.Select(s => new SMCDatasourceItem(s.Seq, GerarDescricaoComObrigatoriedade(s.TipoParticipacaoOrientacao, s.ObrigatorioOrientacao)))
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification>();

            if (!filtroVO.SeqTermoIntercambio.HasValue && !filtroVO.SeqTipoIntercambio.HasValue)
            {
                filtros.PossuiTipoIntercambio = false;
            }
            else if (filtroVO.SeqTermoIntercambio.HasValue)
            {
                var specTermo = new SMCSeqSpecification<TermoIntercambio>(filtroVO.SeqTermoIntercambio.Value);
                filtros.SeqTipoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(specTermo, p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);
            }

            return this.SearchProjectionBySpecification(filtros, p => p.TipoParticipacaoOrientacao, isDistinct: true)
                .ToList()
                .Select(s => new SMCDatasourceItem((long)s, SMCEnumHelper.GetDescription(s)))
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        public List<(TipoParticipacaoOrientacao TipoParticipacaoOrientacao, OrigemColaborador OrigemColaborador)> BuscarTipoParticipacaoEOrigemColaborador(InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification>();

            if (!filtroVO.SeqTermoIntercambio.HasValue && !filtroVO.SeqTipoIntercambio.HasValue)
            {
                filtros.PossuiTipoIntercambio = false;
            }
            else if (filtroVO.SeqTermoIntercambio.HasValue)
            {
                var specTermo = new SMCSeqSpecification<TermoIntercambio>(filtroVO.SeqTermoIntercambio.Value);
                filtros.SeqTipoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(specTermo, p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);
            }

            var dados = this.SearchProjectionBySpecification(filtros, x => new { x.TipoParticipacaoOrientacao, x.OrigemColaborador }).Distinct().ToList();
            return dados.Select(x => (x.TipoParticipacaoOrientacao, x.OrigemColaborador)).ToList();
        }

        public InstituicaoNivelTipoOrientacaoParticipacao BuscarInstituicaoNivelTipoOrientacaoParticipacao(long seq)
        {
            var spec = new SMCSeqSpecification<InstituicaoNivelTipoOrientacaoParticipacao>(seq);
            return this.SearchByKey(spec);
        }

        /// <summary>
        /// Busca os tipos de orientação participação por instituição nível com descição e indicador de obrigatoriedade
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>Dados dos tipos de orientação participação</returns>
        public List<InstituicaoNivelTipoOrientacaoParticipacaoVO> BuscarInstituicaoNivelTipoOrientacaoParticipacoes(InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification filtros)
        {
            var participacoes = this.SearchBySpecification(filtros).TransformList<InstituicaoNivelTipoOrientacaoParticipacaoVO>();
            participacoes.SMCForEach(f => f.DescricaoTipoParticipacaoOrientacao = GerarDescricaoComObrigatoriedade(f.TipoParticipacaoOrientacao, f.ObrigatorioOrientacao));
            return participacoes;
        }

        /// <summary>
        /// Recupera os as origens de orientador para os filtros informados
        /// </summary>
        /// <param name="filtroVO">Dados dos filtros</param>
        /// <returns>Origens em ordem alfabética</returns>
        public List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOrientacaoParticipacaoOrigemSelect(InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO filtroVO)
        {
            var filtros = filtroVO.Transform<InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification>();

            if (filtroVO.SeqTermoIntercambio.HasValue)
            {
                var specTermo = new SMCSeqSpecification<TermoIntercambio>(filtroVO.SeqTermoIntercambio.Value);
                filtros.SeqTipoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(specTermo, p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);
            }

            var participacoes = this.SearchProjectionBySpecification(filtros, p => p.OrigemColaborador, true).ToList();

            return participacoes.Select(s => new SMCDatasourceItem((long)s, SMCEnumHelper.GetDescription(s)))
                .OrderBy(o => o.Descricao)
                .SMCDistinct(s => s.Descricao)
                .ToList();
        }

        private string GerarDescricaoComObrigatoriedade(TipoParticipacaoOrientacao tipoParticipacaoOrientacao, bool obrigatorioOrientacao)
        {
            return $"{SMCEnumHelper.GetDescription(tipoParticipacaoOrientacao)} {(obrigatorioOrientacao ? "*" : "")}";
        }
    }
}