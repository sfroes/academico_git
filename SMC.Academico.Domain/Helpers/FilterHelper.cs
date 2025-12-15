using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Domain;
using System.Linq;

namespace SMC.Academico.Domain.Helpers
{
    public static class FilterHelper
    {
        /// <summary>
        /// Filtros do SGA
        /// </summary>
        private static readonly string[] _filtros = typeof(FILTER)
            .GetFields()
            .Where(w => w.Name != nameof(FILTER.INSTITUICAO_ENSINO))
            .Select(s => (string)s.GetValue(null)).ToArray();

        /// <summary>
        /// Desativa os filtros informados no domain
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade do domain service</typeparam>
        /// <param name="domain">Domain service para alteração dos filtros</param>
        /// <param name="filtros">Filtros a serem desativados, caso nenhum seja informado desativa todos os filtros</param>
        public static void DesativarFiltros<TEntity>(SMCDomainServiceBase<TEntity> domain, params string[] filtros) where TEntity : class
        {
            if (!filtros.SMCAny())
                filtros = _filtros;
            filtros.SMCForEach(f => domain.DisableFilter(f));
        }

        /// <summary>
        /// Ativa os filtros informados no domain
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade do domain service</typeparam>
        /// <param name="domain">Domain service para alteração dos filtros</param>
        /// <param name="filtros">Filtros a serem ativados, caso nenhum seja informado ativa todos os filtros</param>
        public static void AtivarFiltros<TEntity>(SMCDomainServiceBase<TEntity> domain, params string[] filtros) where TEntity : class
        {
            if (!filtros.SMCAny())
                filtros = _filtros;
            filtros.SMCForEach(f => domain.EnableFilter(f));
        }

        /// <summary>
        /// Desativa todos filtro e ativa apenas os filtros informados
        /// </summary>
        /// <typeparam name="TEntity">Tipo da entidade do domain service</typeparam>
        /// <param name="domain">Domain service para alteração dos filtros</param>
        /// <param name="filtros">Filtros a serem ativados</param>
        public static void AtivarApenasFiltros<TEntity>(SMCDomainServiceBase<TEntity> domain, params string[] filtros) where TEntity : class
        {
            DesativarFiltros(domain);
            AtivarFiltros(domain, filtros);
        }
    }
}