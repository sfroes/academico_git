using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoEntregaOnline : short
    {
        [EnumMember]
        [Description("Aguardando entrega")]
        [SMCLegendItem("smc-sga-legenda-entrega-aguardando", "Aguardando entrega")]
        AguardandoEntrega = 0,

        [EnumMember]
        [SMCLegendItem("smc-sga-legenda-entrega-entregue", "Entregue")]
        Entregue = 1,

        [EnumMember]
        [Description("Liberado para correção")]
        [SMCLegendItem("smc-sga-legenda-entrega-liberadocorrecao", "Liberado para correção")]
        LiberadoParaCorrecao = 2,

        [EnumMember]
        [Description("Solicitado nova entrega")]
        [SMCLegendItem("smc-sga-legenda-entrega-solicitadonovaentrega", "Solicitado nova entrega")]
        SolicitadoNovaEntrega = 3,

        [EnumMember]
        [Description("Corrigido")]
        [SMCLegendItem("smc-sga-legenda-entrega-corrigido", "Corrigido")]
        Corrigido = 4
    }
}