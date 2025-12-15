using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TermoConcessaoBolsa : short
    {
        [EnumMember]
        [SMCIgnoreValue]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Pendente")]
        [SMCLegendItem("smc-legenda-status-aguardandoentrega", "Pendente", order: 0)]
        Pendente = 1,

        [EnumMember]
        [Description("Aderido")]
        [SMCLegendItem("smc-legenda-status-deferido", "Aderido", order: 1)]
        Aderido = 2
    }
}
