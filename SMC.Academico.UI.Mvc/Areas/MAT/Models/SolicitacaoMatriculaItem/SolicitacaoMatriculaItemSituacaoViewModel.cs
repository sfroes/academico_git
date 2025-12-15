using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class SolicitacaoMatriculaItemSituacaoViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }
        
        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string TurmaFormatado { get; set; }
                
        public string DescricaoConfiguracaoComponente { get; set; }
       
        public string Situacao { get; set; }

        public MotivoSituacaoMatricula Motivo { get; set; }

        public string SituacaoMotivo
        {
            get
            {
                if (string.IsNullOrEmpty(Motivo.SMCGetDescription()))
                    return Situacao;

               return $"{Situacao} - {Motivo.SMCGetDescription()}";
            }
        }

        public List<SolicitacaoMatriculaItemDivisoesViewModel> TurmaMatriculaDivisoes { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        //Colocado a legenda por questão de layout porque a atividade sempre pertence a matriz curricular
        public TurmaOfertaMatricula PertenceAtividade { get { return TurmaOfertaMatricula.ComponentePertence; } }

        public string ProgramaTurma { get; set; }
        
        public bool ExibirEntidadeResponsavelTurma { get; set; }
    }
}
