using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
	public class SelecaoChancelaPlanoViewModel : SolicitacaoServicoPaginaViewModelBase
	{
		[SMCHidden]
		public override string Token => TOKEN_SOLICITACAO_SERVICO.CHANCELA_PLANO_ESTUDO;

		[SMCHidden]
		public override string ChaveTextoBotaoProximo => "Botao_Chancelar";

		public long Seq { get; set; }

		public string Nome { get; set; }

		[SMCCpf]
		[SMCValueEmpty("-")]
		public string CPF { get; set; }

		[SMCValueEmpty("-")]
		public string Passaporte { get; set; }

		[SMCIgnoreProp]
		public List<string> FormacoesEspecificas { get; set; }

		[SMCIgnoreProp]
		public string DescricaoVinculo { get; set; }


		public string DescricaoUnidadeResponsavel { get; set; }

		public string DescricaoOfertaCampanha { get; set; }

		public string DescricaoSituacaoAtual { get; set; }

		public string DescricaoSituacaoAtualFormatada
		{
			get { return DescricaoSituacaoAtual + (ChancelaReaberta ? " (Chancela Reaberta)" : string.Empty); }
		}

		public override string Protocolo { get; set; }

		public string DescricaoJustificativa { get; set; }

		[SMCDataSource(SMCStorageType.TempData)]
		public List<SMCDatasourceItem> SituacoesItens { get; set; }

		public List<SelecaoChancelaPlanoTurmaViewModel> Turmas { get; set; }

		public List<SelecaoChancelaPlanoAtividadeViewModel> Atividades { get; set; }

        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        public bool ExigirCurso { get; set; }

		public bool ExigirMatrizCurricularOferta { get; set; }

		public long SeqIngressante { get; set; }

		public long SeqProcessoEtapa { get; set; }

		public bool ChancelaFinalizada { get; set; }

		public bool ChancelaReaberta { get; set; }

		public bool Orientador { get; set; }

		public bool ExibirSelecaoTurmaAtividade { get; set; }

		public bool ExibirVisualizacaoDadosIntercambio { get; set; }

		public long SeqCicloLetivoSituacao { get; set; }

		public bool ExibirJustificativa { get; set; }

		[SMCHidden]
		public string TokenEtapa { get; set; }

		public bool? LegendaPertencePlanoEstudo { get; set; }

		[SMCHidden]
		public long? SeqSituacaoFinalSucessoChancela { get; set; }

		[SMCHidden]
		public long? SeqSituacaoInicioChancela { get; set; }

		[SMCHidden]
		public bool ExibirCancelados { get; set; }

		public long? SeqCicloLetivo { get; set; }

		public long SeqPeriodoIntercambio { get; set; }
	}
}