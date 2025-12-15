using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoRelatorio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Acompanhamento da atualização do vínculo do docente")]
        LogAtualizacaoColaborador = 1,

        [EnumMember]
        [Description("Certificado de pós-doutor")]
        CertificadoPosDoutor = 2,
    }
}
