using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoHistoricoEscolar : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aprovado")]
        Aprovado = 1,

        [EnumMember]
        [Description("Aluno sem nota/conceito")]
        AlunoSemNota = 2,

        [EnumMember]
        [Description("Dispensado")]
        Dispensado = 3,

        [EnumMember]
        [Description("Reprovado")]
        Reprovado = 4
    }
}