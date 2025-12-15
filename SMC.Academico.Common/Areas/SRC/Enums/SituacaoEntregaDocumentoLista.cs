using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    public enum SituacaoEntregaDocumentoLista : short
    {
        [EnumMember]
        [SMCIgnoreValue]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando registro de documento")]
        [SMCLegendItem("smc-legenda-status-aguardandoentrega", "Aguardando registro de documento", order: 0)]
        AguardandoRegistroDocumento = 1,

        [EnumMember]
        [Description("Registro de documento realizado")]
        [SMCLegendItem("smc-legenda-status-deferido", "Registro de documento realizado", order: 1)]
        RegistroDocumentoOK = 2,

        [EnumMember]
        [Description("Registro de documento indeferido")]
        [SMCLegendItem("smc-legenda-status-indeferido", "Registro de documento indeferido", order: 2)]
        RegistroDocumentoIndeferido = 3,
    }
}