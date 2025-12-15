using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORT.Services
{
	public class TipoTrabalhoService : SMCServiceBase, ITipoTrabalhoService
	{
		#region DomainServices

		private InstituicaoNivelTipoTrabalhoDomainService InstituicaoNivelTipoTrabalhoDomainService { get => Create<InstituicaoNivelTipoTrabalhoDomainService>(); }

		#endregion DomainServices

		public List<SMCDatasourceItem> BuscarTiposTrabalhoSelect(long seqInstituicaoEnsino)
		{
			var spec = new InstituicaoNivelTipoTrabalhoFilterSpecification()
			{
				SeqInstituicaoEnsino = seqInstituicaoEnsino
			};
			return InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
			{
				Seq = x.SeqTipoTrabalho,
				Descricao = x.TipoTrabalho.Descricao
			}).GroupBy(g => g.Seq).Select(f => f.FirstOrDefault()).ToList();
		}

		/// <summary>
		/// Busca os tipos de trabalho que tem a flag PublicacaoBibliotecaObrigatoria para acesso do BDP
		/// </summary>
		/// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
		/// <returns>Lista com os tipos de trabalho</returns>
		public List<SMCDatasourceItem> BuscarTiposTrabalhoSelectBDP(long seqInstituicaoEnsino)
		{
			var spec = new InstituicaoNivelTipoTrabalhoFilterSpecification()
			{
				SeqInstituicaoEnsino = seqInstituicaoEnsino,
				PublicacaoBibliotecaObrigatoria = true,
			};
			return InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
			{
				Seq = x.SeqTipoTrabalho,
				Descricao = x.TipoTrabalho.Descricao
			}).GroupBy(g => g.Seq).Select(f => f.FirstOrDefault()).ToList();
		}

        /// <summary>
        /// Busca os tipos de trabalho para uma instituição e nivel de ensino
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa de instituição e nivel de ensino</param>
        /// <returns>Lista de tipos de trabalho da instituição x nivel de ensino</returns>
        public List<SMCDatasourceItem> BuscarTiposTrabalhoInstituicaoNivelEnsinoSelect(InstituicaoNivelTipoTrabalhoFiltroData filtros)
        {
            // Se não informou o nível de ensino, retorna lista vazia
            if (!filtros.SeqNivelEnsino.HasValue && !filtros.SeqInstituicaoNivel.HasValue)
                return new List<SMCDatasourceItem>();

            // Filtra os tipos de trabalho por instituição e nível de ensino
            var spec = new InstituicaoNivelTipoTrabalhoFilterSpecification()
            {
                SeqInstituicaoEnsino = filtros.SeqInstituicaoEnsino,
                SeqNivelEnsino = filtros.SeqNivelEnsino,
                SeqInstituicaoNivel = filtros.SeqInstituicaoNivel,
				PermiteInclusaoManual = filtros.PermiteInclusaoManual
            };
            return InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.SeqTipoTrabalho,
                Descricao = x.TipoTrabalho.Descricao
            }).ToList();
        }

        /// <summary>
        /// Busca os tipos de pesquisa para trabalhos acadêmicos do BDP ordenados pela prioridade de cada tipo
        /// </summary>
        /// <returns>Lista com os tipos de pesquisa</returns>
        public List<TipoPesquisaTrabalhoAcademico> BuscarTipoPesquisaTrabalhoAcademicoOrder()
		{
			var retorno = new List<TipoPesquisaTrabalhoAcademico>();
			retorno.Add(TipoPesquisaTrabalhoAcademico.Autor);
			retorno.Add(TipoPesquisaTrabalhoAcademico.Orientador);
			retorno.Add(TipoPesquisaTrabalhoAcademico.Coorientador);

			return retorno;
		}

		public bool BuscarGeraFinanceiroEntregaTrabalho(long seqInstituicaoEnsino, long seqTipoTrabalho, long seqNivelEnsino)
		{
			return InstituicaoNivelTipoTrabalhoDomainService.SearchProjectionByKey(new InstituicaoNivelTipoTrabalhoFilterSpecification
			{
				SeqInstituicaoEnsino = seqInstituicaoEnsino,
				SeqNivelEnsino = seqNivelEnsino,
				SeqTipoTrabalho = seqTipoTrabalho
			}, x => x.GeraFinanceiroEntregaTrabalho);
		}
	}
}