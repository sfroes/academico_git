using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.GRD.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoPagamentoAula : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aula Semanal")]
        AulaSemanal = 1,

        [EnumMember]
        [Description("Aula Executada")]
        AulaExecutada = 2,

        [EnumMember]
        [Description("Aula Fracionada")]
        AulaFracionada = 3
    }
}