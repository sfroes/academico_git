using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoAvaliacaoPpa : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Ingressante = 1,

        [EnumMember]
        [Description("Semestral (Disciplina)")]
        SemestralDisciplina = 2,

        [EnumMember]
        Concluinte = 3,

        [EnumMember]
        Egresso = 4,

        [EnumMember]
        [Description("Autoavaliação (Aluno)")]
        AutoavaliacaoAluno = 5,

        [EnumMember]
        [Description("Autoavaliação (Professor)")]
        AutoavaliacaoProfessor = 6
    }
}