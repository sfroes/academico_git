using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class InstituicaoNivelRegimeLetivoDomainService : AcademicoContextDomain<InstituicaoNivelRegimeLetivo>
    {
        #region DomainService

        private NivelEnsinoDomainService NivelEnsinoDomainService
        {
            get { return this.Create<NivelEnsinoDomainService>(); }
        }

        private InstituicaoNivelDomainService InstituicaoNivelDomainService
        {
            get { return this.Create<InstituicaoNivelDomainService>(); }
        }

        #endregion DomainService

        /// <summary>
        /// Busca os regimes letivos disponíveis para programas stricto sensu
        /// </summary>
        /// <returns>Lista de regimes letivos</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivosStrictoSelect()
        {
            // Cria uma lista com os níveis de ensino filhos de stricto que são folhas
            List<long> seqNiveis = null;
            try
            {
                NivelEnsinoDomainService.DisableFilter(FILTER.NIVEL_ENSINO);
                seqNiveis = NivelEnsinoDomainService.BuscarSeqsNiveisEnsinoStrictoSensu();
            }
            finally
            {
                NivelEnsinoDomainService.EnableFilter(FILTER.NIVEL_ENSINO);
            }

            // Busca as configurações de instituição nivel
            var specIN = new SMCContainsSpecification<InstituicaoNivel, long>(i => i.SeqNivelEnsino, seqNiveis.ToArray());
            var instituicaoNiveis = InstituicaoNivelDomainService.SearchBySpecification(specIN);

            // Busca as configurações de regime letivo para os niveis de ensino encontrados
            var spec = new SMCContainsSpecification<InstituicaoNivelRegimeLetivo, long>(i => i.SeqInstituicaoNivel, instituicaoNiveis.Select(i => i.Seq).ToArray());
            var regimes = this.SearchBySpecification(spec, IncludesInstituicaoNivelRegimeLetivo.RegimeLetivo);

            // Monta o retorno
            List<SMCDatasourceItem> lista = new List<SMCDatasourceItem>();
            foreach (var regime in regimes.Select(i => i.RegimeLetivo).Distinct())
            {
                lista.Add(new SMCDatasourceItem(regime.Seq, regime.Descricao));
            }
            return lista;
        }

        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada no regime informado
        /// </summary>
        /// <param name="seqRegimeLetivo">Sequencial do regime letivo</param>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        public List<SMCDatasourceItem> BuscarNiveisEnsinoDoRegimeSelect(long seqRegimeLetivo)
        {
            var spec = new InstituicaoNivelRegimeLetivoFilterSpecification() { SeqRegimeLetivo = seqRegimeLetivo };
            return this.SearchBySpecification(spec, IncludesInstituicaoNivelRegimeLetivo.InstituicaoNivel_NivelEnsino)
                .Select(s => new SMCDatasourceItem() { Seq = s.InstituicaoNivel.SeqNivelEnsino, Descricao = s.InstituicaoNivel.NivelEnsino.Descricao })
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        /// <summary>
        /// Busca a lista de regime letivo de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de regime letivo</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivoPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            var spec = new InstituicaoNivelRegimeLetivoFilterSpecification() { SeqNivelEnsino = seqNivelEnsino };

            return this.SearchBySpecification(spec, IncludesInstituicaoNivelRegimeLetivo.InstituicaoNivel_NivelEnsino | IncludesInstituicaoNivelRegimeLetivo.RegimeLetivo)
                .Select(s => new SMCDatasourceItem() { Seq = s.SeqRegimeLetivo, Descricao = s.RegimeLetivo.Descricao })
                .OrderBy(o => o.Descricao)
                .ToList();
        }

        /// <summary>
        /// Busca todos os regimes letivos da intituição atual
        /// </summary>
        /// <returns>Lista com os dados de select dos regimes da instituição atual</returns>
        public List<SMCDatasourceItem> BuscarRegimesLetivosInstituicaoSelect()
        {
            // Recupera configuração de níveis da instituição de ensino
            var regimesLetivo = this.SearchProjectionAll(s => new SMCDatasourceItem() { Seq = s.SeqRegimeLetivo, Descricao = s.RegimeLetivo.Descricao }, true)
                .OrderBy(o => o.Descricao)
                .ToList();

            return regimesLetivo;
        }
    }
}