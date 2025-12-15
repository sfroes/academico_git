using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
	public class NivelEnsinoDomainService : AcademicoContextDomain<NivelEnsino>
	{
		#region [ Domain Services ]

		private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

		private AlunoHistoricoDomainService AlunoHistoricoDomainService { get => Create<AlunoHistoricoDomainService>(); }

		private InstituicaoNivelServicoDomainService InstituicaoNivelServicoDomainService { get => this.Create<InstituicaoNivelServicoDomainService>(); }

		#endregion [ Domain Services ]

		public List<long> BuscarSeqsNiveisEnsinoStrictoSensu()
		{
			// Busca o nivel de ensino STRICTO
			NivelEnsinoFilterSpecification specNivel = new NivelEnsinoFilterSpecification()
			{
				Token = TOKEN_NIVEL_ENSINO.STRICTO_SENSU
			};
			IncludesNivelEnsino includes = IncludesNivelEnsino.NiveisEnsinoFilhos;
			NivelEnsino nivelStricto = this.SearchByKey(specNivel, includes);

			// Cria uma lista com os níveis de ensino filhos de stricto que são folhas
			List<long> seqNiveis = new List<long>();
			foreach (var nivel in nivelStricto.NiveisEnsinoFilhos)
			{
				seqNiveis.AddRange(RetornaNivelFolha(nivel.Seq));
			}

			return seqNiveis;
		}

		/// <summary>
		/// Retorna o nível folha (recursivo)
		/// </summary>
		/// <param name="seqNivel">Sequencial do nível de ensino</param>
		/// <returns>Lista de níveis folhas</returns>
		private List<long> RetornaNivelFolha(long seqNivel)
		{
			// Busca os niveis filhos do informado
			SMCSeqSpecification<NivelEnsino> spec = new SMCSeqSpecification<NivelEnsino>(seqNivel);
			IncludesNivelEnsino includes = IncludesNivelEnsino.NiveisEnsinoFilhos;
			NivelEnsino nivel = this.SearchByKey(spec, includes);

			// Retorna os níveis folha
			List<long> lista = new List<long>();
			if (nivel.Folha)
			{
				lista.Add(nivel.Seq);
			}
			else
			{
				foreach (var item in nivel.NiveisEnsinoFilhos)
				{
					lista.AddRange(RetornaNivelFolha(item.Seq));
				}
			}

			return lista;
		}

		public NivelEnsino BuscarNivelEnsinoAluno(long seqPessoaAtuacao)
		{
			AlunoHistoricoFilterSpecification spec = new AlunoHistoricoFilterSpecification();
			spec.SeqAluno = seqPessoaAtuacao;
			spec.Atual = true;
			var hist = AlunoHistoricoDomainService.SearchBySpecification(spec, a => a.NivelEnsino).ToList();
			return (hist != null && hist.Count > 0) ? hist.FirstOrDefault().NivelEnsino : new NivelEnsino();
		}

		public List<long> BuscarNiveisEnsinoProfessor(long seqPessoaAtuacao)
		{
			var spec = new TurmaFilterSpecification() { SeqColaborador = seqPessoaAtuacao };
			spec.MaxResults = int.MaxValue;

			var registros = TurmaDomainService.SearchProjectionBySpecification(spec, p => p.ConfiguracoesComponente.Where(w => w.Principal).FirstOrDefault().ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.Where(w => w.Responsavel).FirstOrDefault().SeqNivelEnsino).Distinct().ToList();
			return registros;
		}

		public List<SMCDatasourceItem> BuscarNiveisEnsinoPorServicoSelect(long seqServico)
		{
			List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

			var spec = new InstituicaoNivelServicoFilterSpecification() { SeqServico = seqServico };

			var instituicaoNivelServico = InstituicaoNivelServicoDomainService.SearchBySpecification(spec,
				IncludesInstituicaoNivelServico.InstituicaoNivelTipoVinculoAluno
				| IncludesInstituicaoNivelServico.InstituicaoNivelTipoVinculoAluno_InstituicaoNivel
				| IncludesInstituicaoNivelServico.InstituicaoNivelTipoVinculoAluno_InstituicaoNivel_NivelEnsino);

			foreach (var item in instituicaoNivelServico)
			{
				var nivelEnsino = item.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.NivelEnsino;
				retorno.Add(new SMCDatasourceItem(nivelEnsino.Seq, nivelEnsino.Descricao));
			}

			return retorno.SMCDistinct(s => s.Seq).OrderBy(a => a.Descricao).ToList();
		}
	}
}