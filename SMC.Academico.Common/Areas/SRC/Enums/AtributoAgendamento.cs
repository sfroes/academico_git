using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum AtributoAgendamento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Data de fim do escalonamento")]
        DataFimEscalonamento = 1,

        [EnumMember]
        [Description("Data de início do escalonamento")]
        DataInicioEscalonamento = 2,

        [EnumMember]
        [Description("Data de vencimento da parcela")]
        DataVencimentoParcela = 3

    }
}