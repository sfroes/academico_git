using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IntegracaoBiblioteca : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não Integra")]
        NaoIntegra = 1,

        [EnumMember]
        Pergarmun = 2
    }
}