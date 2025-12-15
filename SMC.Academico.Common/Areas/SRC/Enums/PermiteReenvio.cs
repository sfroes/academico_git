using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum PermiteReenvio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Não permite reenvio")]
        NaoPermiteReenvio = 1,

        [EnumMember]
        [Description("Reenvio para o solicitante")]
        ReenvioParaSolicitante = 2,

        [EnumMember]
        [Description("Reenvio para o(s) orientador(es) do solicitante")]
        ReenvioParaOrientadorSolicitante = 3,

        [EnumMember]
        [Description("Reenvio para o(s) destinatario(s) da configuração")]
        ReenvioParaDestinatarioConfiguracao = 4
    }
}