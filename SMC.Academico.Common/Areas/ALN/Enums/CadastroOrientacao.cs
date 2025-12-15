using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CadastroOrientacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não Permite")]
        NaoPermite = 1,

        [EnumMember]
        Permite = 2,

        [EnumMember]
        Exige = 3
    }
}