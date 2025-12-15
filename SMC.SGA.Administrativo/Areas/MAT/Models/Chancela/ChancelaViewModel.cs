using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
               
        public string Nome { get; set; }

        [SMCCpf]
        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoUnidadeResponsavel { get; set; }

        public string DescricaoOfertaCampanha { get; set; }

        public string DescricaoSituacaoAtual { get; set; }

        public string DescricaoSituacaoAtualFormatada
        {
            get { return DescricaoSituacaoAtual + (ChancelaReaberta ? " (Chancela Reaberta)" : string.Empty); }
            set
            {
            }
        }

        //public SolicitacaoHistoricoSituacaoViewModel SituacaoEtapaAtual { get; set; }

        [SMCDataSource(SMCStorageType.TempData)]
        public List<SMCDatasourceItem> SituacoesItens { get; set; }

        public List<ChancelaTurmaViewModel> Turmas { get; set; }

        public List<ChancelaAtividadeViewModel> Atividades { get; set; }

        public bool ExigirCurso { get; set; }

        public bool ExigirMatrizCurricularOferta { get; set; }

        public long SeqIngressante { get; set; }

        [SMCParameter]
        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public bool ChancelaFinalizada { get; set; }

        public bool ChancelaReaberta { get; set; }

        public bool? Orientacao { get; set; }
    }
}