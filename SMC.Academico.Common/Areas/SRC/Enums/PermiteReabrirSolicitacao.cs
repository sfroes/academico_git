using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum PermiteReabrirSolicitacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não permite")]
        NaoPermite = 1,

        [EnumMember]
        [Description("Permite – Todas")]
        PermiteTodas = 2,

        [EnumMember]
        [Description("Permite – Exceto finalizada com sucesso")]
        PermiteExcetoFinalizadaComSucesso = 3
    }
}