using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ChancelaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoUnidadeResponsavel { get; set; }

        public string DescricaoOfertaCampanha { get; set; }

        public string DescricaoSituacaoAtual { get; set; }

        public string DescricaoJustificativa { get; set; }

        public List<SMCDatasourceItem> SituacoesItens { get; set; }

        public List<ChancelaTurmaData> Turmas { get; set; }

        public List<ChancelaAtividadeData> Atividades { get; set; }

        public string Observacao { get; set; }

        public bool ExigirCurso { get; set; }

        public bool ExigirMatrizCurricularOferta { get; set; }

        public long SeqIngressante { get; set; }

        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public bool ChancelaFinalizada { get; set; }

        public bool ChancelaReaberta { get; set; }

        public bool ExibirSelecaoTurmaAtividade { get; set; }

        public bool? LegendaPertencePlanoEstudo { get; set; }

        public long? SeqSituacaoFinalSucessoChancela { get; set; }

        public long? SeqSituacaoInicioChancela { get; set; }

        public bool NaoAtualizarPlano { get; set; }

        public bool ExibirJustificativa { get; set; }

		public bool ExibirVisualizacaoDadosIntercambio { get; set; }

		public long SeqCicloLetivoSituacao { get; set; }

		public List<string> FormacoesEspecificas { get; set; }

		public string DescricaoVinculo { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long SeqPeriodoIntercambio { get; set; }
    }
}