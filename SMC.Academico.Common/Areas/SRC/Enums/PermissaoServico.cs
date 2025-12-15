using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum PermissaoServico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Criar solicitação")]
        CriarSolicitacao = 1,

        [EnumMember]
        [Description("Reabrir solicitação")]
        ReabrirSolicitacao = 2,

        [EnumMember]
        [Description("Atender solicitação")]
        AtenderSolicitacao = 3
    }
}