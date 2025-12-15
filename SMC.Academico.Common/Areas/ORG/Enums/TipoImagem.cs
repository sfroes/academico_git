using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoImagem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Logotipo timbrado")]
        LogotipoTimbrado = 1,

        [EnumMember]
        [Description("Logotipo padrão")]
        LogotipoPadrao = 2
    }
}