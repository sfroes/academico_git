using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoFinalEtapa : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [SMCLegendItem("smc-sga-legenda-final-etapa", "Sim")]
        Sim = 1,

        [EnumMember]
        [SMCIgnoreValue]
        [Description("Não")]
        Nao = 2,
    }
}