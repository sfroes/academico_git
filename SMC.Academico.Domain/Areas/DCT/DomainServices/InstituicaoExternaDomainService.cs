using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.DCT.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.DCT.DomainServices
{
	public class InstituicaoExternaDomainService : AcademicoContextDomain<InstituicaoExterna>
	{
		#region [ DomainServices ]

		ColaboradorInstituicaoExternaDomainService ColaboradorInstituicaoExternaDomainService => Create<ColaboradorInstituicaoExternaDomainService>();

		TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();

		InstituicaoNivelTipoOrientacaoParticipacaoDomainService InstituicaoNivelTipoOrientacaoParticipacaoDomainService => Create<InstituicaoNivelTipoOrientacaoParticipacaoDomainService>();

		#endregion [ DomainServices ]

		#region [ Services ]

		private ILocalidadeService LocalidadeService
		{
			get { return this.Create<ILocalidadeService>(); }
		}

		#endregion [ Services ]

		/// <summary>
		/// Busca as instituições externas que atendam aos filtros informados
		/// </summary>
		/// <param name="filtros">Filtros das instituições externas ou sequenciais selecionados</param>
		/// <returns>Dados das instituições externas paginados</returns>
		public SMCPagerData<InstituicaoExternaListaVO> BuscarInstituicoesExternas(InstituicaoExternaFilterSpecification filtros)
		{
			int total;
			var paises = LocalidadeService.BuscarPaisesValidosCorreios();

			Dictionary<int, string> paisesDc = paises.SMCToDictionary(x => x.Codigo, x => x.Nome);

			var instituicoes = SearchBySpecification(filtros, out total, IncludesInstituicaoExterna.CategoriaInstituicaoEnsino).TransformList<InstituicaoExternaListaVO>();

			instituicoes.SMCForEach(i => i.DescricaoPais = paisesDc[i.CodigoPais]);

			return new SMCPagerData<InstituicaoExternaListaVO>(instituicoes, total);
		}

		public List<SMCDatasourceItem> BuscarInstituicaoExternaPorColaboradorSelect(InstituicaoExternaFiltroVO filtros)
		{
			this.DisableFilter(FILTER.INSTITUICAO_ENSINO);

			var spec = new ColaboradorInstituicaoExternaFilterSpecification { SeqColaborador = filtros.SeqColaborador, Ativo = filtros.Ativo };

			// Caso seja para recuperar as instituições de um orientador para intercambio
			if (filtros.SeqTermoIntercambio.HasValue)
			{
				var dadosTermo = TermoIntercambioDomainService.SearchProjectionByKey(new SMCSeqSpecification<TermoIntercambio>(filtros.SeqTermoIntercambio.Value), p => new
				{
					p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
					p.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna
				});

				filtros.SeqInstituicaoEnsinoExterna = filtros.SeqInstituicaoEnsinoExterna ?? dadosTermo.SeqInstituicaoExterna;
				var seqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio;

				var specConfiguracaoVinculo = new InstituicaoNivelTipoOrientacaoParticipacaoFilterSpecification()
				{
					SeqTipoIntercambio = seqTipoTermoIntercambio,
					SeqNivelEnsino = filtros.SeqNivelEnsino,
					SeqTipoVinculoAluno = filtros.SeqTipoVinculoAluno,
					TipoParticipacaoOrientacao = filtros.TipoParticipacaoOrientacao,
					SeqTipoOrientacao = filtros.SeqTipoOrientacao,
					SeqTipoVinculo = filtros.SeqTipoVinculoAluno
				};

				// Recupera todas as origens possíveis
				var origensColaborador = InstituicaoNivelTipoOrientacaoParticipacaoDomainService.SearchProjectionBySpecification(specConfiguracaoVinculo, p => p.OrigemColaborador);
				OrigemColaborador origemColaborador = OrigemColaborador.Nenhum;

				// Verifica se possui nas origens do colaborador interno e externo
				if (origensColaborador.Any(o => o == OrigemColaborador.InternoExterno))
					origemColaborador = OrigemColaborador.InternoExterno;
				else
				{
					// Não tem InternoExterno..
					// Verifica se tem interno E externo
					if (origensColaborador.Any(o => o == OrigemColaborador.Interno) && origensColaborador.Any(o => o == OrigemColaborador.Externo))
						origemColaborador = OrigemColaborador.InternoExterno;
					else if (origensColaborador.Any(o => o == OrigemColaborador.Interno))
						origemColaborador = OrigemColaborador.Interno;
					else if (origensColaborador.Any(o => o == OrigemColaborador.Externo))
						origemColaborador = OrigemColaborador.Externo;
				}

				switch (origemColaborador)
				{
					// Considera apenas a instituição logada para interno
					case OrigemColaborador.Interno:
						spec.SeqInstituicaoEnsino = filtros.SeqInstituicaoEnsino;
						break;

					// Considera apenas a instituição do termo para externo
					case OrigemColaborador.Externo:
						spec.SeqInstituicaoExterna = filtros.SeqInstituicaoEnsinoExterna;
						break;

					// Considera a instituição logada e a instituição externa do termo
					case OrigemColaborador.InternoExterno:
						var seqInstituicaoExternaLogada = SearchProjectionByKey(new InstituicaoExternaFilterSpecification() { SeqInstituicaoEnsino = filtros.SeqInstituicaoEnsino }, p => p.Seq);
						spec.SeqsInstituicoesExternas = new[] { seqInstituicaoExternaLogada, filtros.SeqInstituicaoEnsinoExterna.GetValueOrDefault() };
						break;
				}
			}

			var retorno = ColaboradorInstituicaoExternaDomainService.SearchProjectionBySpecification(spec,
																		  p => new SMCDatasourceItem
																		  {
																			  Seq = p.SeqInstituicaoExterna,
																			  Descricao = p.InstituicaoExterna.Nome
																		  }).ToList();
			this.EnableFilter(FILTER.INSTITUICAO_ENSINO);
			return retorno;
		}

		/// <summary>
		/// Valida se uma instiuição é interna = true ou externa = false
		/// </summary>
		/// <param name="filtros">Filtros a serem pesquisados</param>
		/// <returns>Retorna true se a instituição for interna</returns>
		public bool ValidarInstituicaoInternaOuExterna(long seqInstituicaoExterna)
		{
			var retorno = this.SearchByKey(new SMCSeqSpecification<InstituicaoExterna>(seqInstituicaoExterna));

			if (retorno.SeqInstituicaoEnsino != null)
			{
				return true;
			}

			return false;
		}
	}
}