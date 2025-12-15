using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    public enum SituacaoEntregaDocumentoAluno : short
    {
        [EnumMember]
        [SMCIgnoreValue]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Documento pendente")]
        [SMCLegendItem("smc-legenda-status-aguardandoentrega", "Documento pendente", order: 0)]
        AguardandoRegistroDocumento = 1,

        [EnumMember]
        [Description("Documento entregue")]
        [SMCLegendItem("smc-legenda-status-deferido", "Documento entregue", order: 1)]
        RegistroDocumentoOK = 2,

        [EnumMember]
        [Description("Entrega deve ser feita presencialmente")]
        [SMCLegendItem("smc-legenda-status-pendente", "Entrega deve ser feita presencialmente", order: 2)]
        EntregaPresencialmente = 3
    }
}
