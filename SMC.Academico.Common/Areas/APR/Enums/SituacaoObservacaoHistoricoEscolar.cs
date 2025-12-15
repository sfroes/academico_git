using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoObservacaoHistoricoEscolar : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Em branco")]
        [SMCLegendItem("smc-grupo-legenda-sga-notas-frequencias-em-branco", "Em branco")]
        EmBranco = 1,

        [EnumMember]
        [Description("Preenchida")]
        [SMCLegendItem("smc-grupo-legenda-sga-notas-frequencias-preenchida", "Preenchida")]
        Preenchida = 2
    }
}