using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ChancelaVagaVO
    {
        public bool ControleVagaAnterior { get; set; }

        public long AguardandoChancela { get; set; }

        public long Deferido { get; set; }
        
        public long Indeferido { get; set; }
        
        public long InicialProximaEtapa { get; set; }
        
        public bool PertencePlanoEstudo { get; set; }
        
        public long SeqItem { get; set; }

        public long SeqUltimoSituacao { get; set; }
        
        public long SeqSituacaoAtual { get; set; }

        public string TurmaFormatado { get; set; }

        public bool ProximaEtapa { get; set; }

        public bool TrocaGrupo { get; set; }
    }
}
