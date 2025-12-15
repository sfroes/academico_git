using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Service.Areas.PES.Services
{
	public class PessoaAtuacaoAmostraPpaService : SMCServiceBase, IPessoaAtuacaoAmostraPpaService
	{
		#region [ DomainServices ]

		private PessoaAtuacaoAmostraPpaDomainService PessoaAtuacaoAmostraPpaDomainService
		{
			get { return this.Create<PessoaAtuacaoAmostraPpaDomainService>(); }
		}

		private ConfiguracaoAvaliacaoPpaDomainService ConfiguracaoAvaliacaoPpaDomainService => Create<ConfiguracaoAvaliacaoPpaDomainService>();
		#endregion [ DomainServices ]

		/// <summary>
		/// Busca os dados da amostra PPA que ainda não foi preenchida para apresentar banner
		/// </summary>
		/// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação a ser pesquisada</param>
		/// <param name="tipoAvaliacao">Tipo de avaliação a ser pesquisada</param>
		/// <returns>Dados da amostra PPA encontrada ou NULL</returns>
		public int? BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(long seqPessoaAtuacao, TipoAvaliacaoPpa tipoAvaliacao)
		{
			return PessoaAtuacaoAmostraPpaDomainService.BuscarPessoaAtuacaoAmostraPpaNaoPreenchida(seqPessoaAtuacao, tipoAvaliacao);
		}

		public PessoaAtuacaoAmostraPpaCabecalhoData BuscarCabecalhoPessoaAtuacaoAmostraPpa(long seqConfiguracaoAvaliacaoPpa,long? seqConfiguracaoAvaliacaoPpaTurma) 
		{
			// Se seqConfiguracaoAvaliacaoPpaTurma turma nao for informado, retorna nulo nas propriedades turma e descricao turma.
			var result = this.ConfiguracaoAvaliacaoPpaDomainService.SearchProjectionByKey(
				new SMCSeqSpecification<ConfiguracaoAvaliacaoPpa>(seqConfiguracaoAvaliacaoPpa),
				i => new PessoaAtuacaoAmostraPpaCabecalhoData
				{
					Seq = i.Seq,
					TipoAvaliacao = i.TipoAvaliacaoPpa,
					EntidadeResponsavel = i.EntidadeResponsavel.Nome,
					DescricaoConfiguracaoAvaliacaoPpa = i.Descricao,
					Turma = seqConfiguracaoAvaliacaoPpaTurma.HasValue ? i.Turmas.FirstOrDefault().Turma.Codigo.ToString() : null,
					DescricaoTurma = seqConfiguracaoAvaliacaoPpaTurma.HasValue ? i.Turmas.FirstOrDefault().Turma.ConfiguracoesComponente.FirstOrDefault().Descricao : null
				});

			return result;

		}

        public SMCPagerData<PessoaAtuacaoAmostraPpaListaData> ListarAmostras(PessoaAtuacaoAmostraPpaFiltroData filtro)
		{
            var list = PessoaAtuacaoAmostraPpaDomainService.ListarAmostras(filtro.Transform<PessoaAtuacaoAmostraPpaFiltroVO>());
			return new SMCPagerData<PessoaAtuacaoAmostraPpaListaData>(list.TransformList<PessoaAtuacaoAmostraPpaListaData>(), list.Total);
        }

    }
}
