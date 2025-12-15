using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum UsoCalendario : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [Description("Banca examinadora")]
        BancaExaminadora = 1,

        [Description("Turma")]
        Turma = 2
    }
}