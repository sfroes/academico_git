using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Data.DivisaoComponente;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CUR.Services
{
	public class DivisaoComponenteService : SMCServiceBase, IDivisaoComponenteService
	{
		#region [ DomainServices ]

		private DivisaoComponenteDomainService DivisaoComponenteDomainService
		{
			get { return this.Create<DivisaoComponenteDomainService>(); }
		}

		#endregion [ DomainServices ]

		/// <summary>
		/// Busca as divisões de uma configuração de componente curricular
		/// </summary>
		/// <param name="seqConfiguracaoCompoente">Sequencial da configuração de componente curricular</param>
		/// <returns>Dados das divisões de componentes curriculares</returns>
		public List<SMCDatasourceItem> BuscarDivisoesCompoentePorConfiguracao(long seqConfiguracaoCompoente)
		{
			return this.DivisaoComponenteDomainService
				.BuscarDivisoesCompoentePorConfiguracao(seqConfiguracaoCompoente);
		}

		public List<SMCDatasourceItem> BuscarDivisaoComponenteAluno(long seqAluno)
		{
			return DivisaoComponenteDomainService.BuscarDivisaoComponenteAluno(seqAluno);
		}

		public ConfiguracaoDivisaoComponenteData BuscarDadosConfiguracaoDivisaoComponente(long seqDivisaoComponente, long seqMatrizCurricular)
		{
			return this.DivisaoComponenteDomainService.SearchProjectionByKey(seqDivisaoComponente, x => x.ConfiguracaoComponente.DivisoesMatrizCurricularComponente.Where(d => d.SeqMatrizCurricular == seqMatrizCurricular).Select(d => new ConfiguracaoDivisaoComponenteData
			{
				ApuracaoNota = d.CriterioAprovacao.ApuracaoNota,
				NotaMaxima = d.CriterioAprovacao.NotaMaxima,
				ApuracaoEscala = d.CriterioAprovacao.SeqEscalaApuracao.HasValue,
				ApuracaoFrequencia = d.CriterioAprovacao.ApuracaoFrequencia,
				SeqEscalaApuracao = d.CriterioAprovacao.SeqEscalaApuracao,
				PercentualFrequenciaAprovado = d.CriterioAprovacao.PercentualFrequenciaAprovado,
				PercentualNotaAprovado = d.CriterioAprovacao.PercentualNotaAprovado,
				PermiteAlunoSemNota = d.ConfiguracaoComponente.PermiteAlunoSemNota,
			}).FirstOrDefault());
		}
	}
}