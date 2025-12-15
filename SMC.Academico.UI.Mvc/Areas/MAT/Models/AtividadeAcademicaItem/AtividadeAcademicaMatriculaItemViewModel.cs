using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class AtividadeAcademicaMatriculaItemViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoComponente { get; set; }

        [SMCHidden]
        public string Codigo { get; set; }

        public string DescricaoFormatada { get; set; }

        [SMCHidden]
        public bool PreRequisito { get; set; }

        [SMCHidden]
        public List<long> SeqsAtividadeCoRequisitos { get; set; }

        [SMCHidden]
        public string Situacao { get; set; }

        [SMCHidden]
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

        [SMCHidden]
        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        [SMCHidden]
        public bool? SituacaoInicial { get; set; }

        [SMCHidden]
        public bool? SituacaoFinal { get; set; }

        [SMCHidden]
        public bool? PertencePlanoEstudo { get; set; }

        [SMCHidden]
        public bool ExibirMatriculaPertence { get; set; }

        public MatriculaPertencePlanoEstudo MatriculaPertence { get; set; }

        //Colocado a legenda por questão de layout porque a atividade sempre pertence a matriz curricular
        public TurmaOfertaMatricula Pertence { get { return TurmaOfertaMatricula.ComponentePertence; } }
    }
}