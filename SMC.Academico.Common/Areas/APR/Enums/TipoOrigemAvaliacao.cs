using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoOrigemAvaliacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Divisão de trabalho acadêmico")]
        DivisaoTrabalhoAcademico = 1,

        [EnumMember]
        [Description("Divisão de turma")]
        DivisaoTurma = 2,

        [EnumMember]
        [Description("Trabalho acadêmico")]
        TrabalhoAcademico = 3,

        [EnumMember]
        Turma = 4
    }
}