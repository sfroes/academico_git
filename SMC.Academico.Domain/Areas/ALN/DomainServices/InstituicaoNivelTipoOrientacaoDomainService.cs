using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
	public class InstituicaoNivelTipoOrientacaoDomainService : AcademicoContextDomain<InstituicaoNivelTipoOrientacao>
	{
		#region [ DomainService ]

		private TermoIntercambioDomainService TermoIntercambioDomainService { get => Create<TermoIntercambioDomainService>(); }

		private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService { get => Create<InstituicaoNivelTipoTermoIntercambioDomainService>(); }

		#endregion [ DomainService ]

		#region Propriedade

		private const string INCLUSAO = "Inclusão";
		private const string ALTERAR = "Alteração";

		#endregion Propriedade

		/// <summary>
		/// Busca os tipos de orientação que atendam aos filtros informados
		/// </summary>
		/// <param name="filtros">Dados dos filtros</param>
		/// <returns>Tipos de orientação ordenados por descrição</returns>
		public List<SMCDatasourceItem> BuscarTiposOrientacaoSelect(InstituicaoNivelTipoOrientacaoFiltroVO filtroVO)
		{
			var filtros = filtroVO.Transform<InstituicaoNivelTipoOrientacaoFilterSpecification>();

			if (filtroVO.SeqTermoIntercambio.HasValue)
			{
				var specTermo = new SMCSeqSpecification<TermoIntercambio>(filtroVO.SeqTermoIntercambio.Value);
				filtros.SeqTipoIntercambio = TermoIntercambioDomainService.SearchProjectionByKey(specTermo, p => p.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio);
			}

			filtros.SetOrderBy(o => o.TipoOrientacao.Descricao);

			return this.SearchProjectionBySpecification(filtros, p => new SMCDatasourceItem()
			{
				Seq = p.SeqTipoOrientacao,
				Descricao = p.TipoOrientacao.Descricao,
				DataAttributes = new List<SMCKeyValuePair>
				{
					new SMCKeyValuePair { Key = "orientacao-aluno", Value = p.CadastroOrientacaoAluno.ToString() },
					new SMCKeyValuePair { Key = "orientacao-ingressante", Value = p.CadastroOrientacaoIngressante.ToString() },
				}
			}).SMCDistinct(o => o.Seq).OrderBy(o => o.Descricao).ToList();
		}

        /// <summary>
        /// Busca os tipos de orientação que permite orientação manual
        /// </summary>
        /// <returns>Tipos de orientação select</returns>
        public List<SMCDatasourceItem> BuscarTiposOrientacaoPermiteManutencaoManualSelect()
        {
            var spec = new InstituicaoNivelTipoOrientacaoFilterSpecification() { PermiteManutencaoManual = true };

            spec.SetOrderBy(o => o.TipoOrientacao.Descricao);

            return this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.SeqTipoOrientacao,
                Descricao = p.TipoOrientacao.Descricao,
                DataAttributes = new List<SMCKeyValuePair>
                {
                    new SMCKeyValuePair { Key = "orientacao-aluno", Value = p.CadastroOrientacaoAluno.ToString() },
                    new SMCKeyValuePair { Key = "orientacao-ingressante", Value = p.CadastroOrientacaoIngressante.ToString() },
                }
            }).SMCDistinct(o => o.Seq).OrderBy(o => o.Descricao).ToList();
        }

        public long SalvarInstituicaoNivelTipoOrientacao(InstituicaoNivelTipoOrientacao instituicaoNivelTipoOrientacao)
		{
			if (!instituicaoNivelTipoOrientacao.TiposParticipacao.Any(x => x.ObrigatorioOrientacao == true))
			{
				if (instituicaoNivelTipoOrientacao.Seq == 0)
				{
					throw new InstituicaoNivelTipoOrientacaoParticipacaoObrigatoriaException(INCLUSAO);
				}
				else
				{
					throw new InstituicaoNivelTipoOrientacaoParticipacaoObrigatoriaException(ALTERAR);
				}
			}

			///Caso um tipo de termo de intercambio tenha sido selecionado, verificar se existe um tipo de orientação associado
			///ao tipo de termo.
			if (instituicaoNivelTipoOrientacao.SeqInstituicaoNivelTipoTermoIntercambio != null)
			{
				var spec = new InstituicaoNivelTipoOrientacaoFilterSpecification()
				{
					SeqInstituicaoNivelTipoTermoIntercambio = instituicaoNivelTipoOrientacao.SeqInstituicaoNivelTipoTermoIntercambio,
					SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoOrientacao.SeqInstituicaoNivelTipoVinculoAluno
				};

				var result = this.SearchBySpecification(spec).ToList();

				if (result.Count > 0)
				{
					throw new InstituicaoNivelTipoOrientacaoTipoTermoException(instituicaoNivelTipoOrientacao.Seq == 0 ? INCLUSAO : ALTERAR);
				}
			}

			this.SaveEntity(instituicaoNivelTipoOrientacao);

			return instituicaoNivelTipoOrientacao.Seq;
		}

		/// <summary>
		/// Buscar o numero máximo de alunos por orientação
		/// </summary>
		/// <param name="filtro">Filtros a serem selecionados</param>
		/// <returns>O numero de alunos possivel em uma orientação</returns>
		public short? BuscarNumeroMaximoAlunosOrientacao(InstituicaoNivelTipoOrientacaoFilterSpecification filtro)
		{
			return this.SearchProjectionBySpecification(filtro, p => p.QuantidadeMaximaAluno).FirstOrDefault();
		}

		/// <summary>
		/// Buscar todas os tipos de orientação
		/// </summary>
		/// <param name="filtros">Filtros a serem selecionados</param>
		/// <returns>Lista de tipos de orientação</returns>
		public List<InstituicaoNivelTipoOrientacao> BuscarTiposOritencoes(InstituicaoNivelTipoOrientacaoFilterSpecification filtros)
		{
			var includes = IncludesInstituicaoNivelTipoOrientacao.TipoOrientacao
						 | IncludesInstituicaoNivelTipoOrientacao.TiposParticipacao
						 | IncludesInstituicaoNivelTipoOrientacao.InstituicaoNivelTipoTermoIntercambio;

			return this.SearchBySpecification(filtros, includes).ToList();
		}

		/// <summary>
		/// Buscar os sequenciais dos tipos de orientação de acordo com o tipo de intercambio e vinculo aluno
		/// </summary>
		/// <param name="seqInstituicaoNivelTipoVinculoAluno">Sequencial da instituicao nivel tipo vinculo aluno</param>
		/// <param name="seqsTipoTermoIntercambio">Sequencial do tipo de termo intercambio</param>
		/// <returns>Lista com os sequenciais de tipos de orientacao</returns>
		public List<long> BuscarTiposOrientacoesVinculo(long seqInstituicaoNivelTipoVinculoAluno, long[] seqsTipoTermoIntercambio)
		{
			var spec = new InstituicaoNivelTipoOrientacaoFilterSpecification()
			{
				SeqInstituicaoNivelTipoVinculoAluno = seqInstituicaoNivelTipoVinculoAluno,
				CadastroOrientacaoIngressante = CadastroOrientacao.Exige
			};

			if (seqsTipoTermoIntercambio != null && seqsTipoTermoIntercambio.Length > 0)
				spec.SeqsTipoTermoIntercambio = seqsTipoTermoIntercambio;
			else
				spec.PossuiTipoIntercambio = false;

			spec.MaxResults = int.MaxValue;

			var registros = this.SearchProjectionBySpecification(spec, p => p.SeqTipoOrientacao).ToList();
			return registros;
		}
	}
}