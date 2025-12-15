using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum UsoSistemaOrigem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [Description("Arquivo PDF")]
        ArquivoPDF = 1,

        [Description("Arquivo XML")]
        ArquivoXML = 2
    }
}