using SMC.Academico.Common.Constants;
using SMC.Framework.DataAnnotations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoEtapaSolicitacaoMatricula : short
    {
        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-sga-legenda-matricula-naoiniciada", "Não Iniciada")]
        NaoIniciada = 1,

        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-sga-legenda-matricula-emandamento", "Em andamento")]
        EmAndamento = 2,

        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-sga-legenda-matricula-aguardandopagamento", "Aguardando Pagamento")]
        AguardandoPagamento = 4,

        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-sga-legenda-matricula-finalizada", "Finalizada")]
        Finalizada = 8,

        [EnumMember]
        [Description("")]
        [SMCLegendItem("smc-sga-legenda-matricula-naofinalizada", "Não Finalizada")]
        NaoFinalizada = 16
    }
}