using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum MotivoFimVinculo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Afastamento = 1,

        [EnumMember]
        [Description("Demissão")]
        Demissao = 2,

        [EnumMember]
        [Description("Encerramento de contrato")]
        EncerramentoContrato = 3
    }
}