using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SolicitacaoMatriculaItemData : ISMCMappable
    {
        public long Seq { get; set; }
                
        public long SeqSolicitacaoMatricula { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public string DescricaoFormatada { get; set; }

        public short? Credito { get; set; }

        public string Situacao { get; set; }

        public string SituacaoEtapa { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public MotivoSituacaoMatricula? MotivoEtapa { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }

        public bool? PertencePlanoEstudo { get; set; }

        //Campo de controle para grupo divisão com turmas de mesmo componente
        public long SeqTurmaControle { get; set; }

        public long? SeqTurma { get; set; }
        public int? AnoTurmaSga { get; set; }
        public int? SemestreTurmaSga { get; set; }
    }
}