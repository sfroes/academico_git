using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CategoriaMensagem : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Mensagem para documento")]
        Documento = 1,

        [EnumMember]
        [Description("Mensagem para linha do tempo")]
        LinhaDoTempo = 2,

        [EnumMember]
        [Description("Mensagem para ocorrência")]
        Ocorrencia = 3
    }
}