using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OperacaoTurma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Novo = 0,

        [EnumMember]
        Copiar = 1,

        [EnumMember]
        Desdobrar = 2
    }
}