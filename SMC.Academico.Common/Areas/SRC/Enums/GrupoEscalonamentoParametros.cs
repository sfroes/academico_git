using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum GrupoEscalonamentoParametros : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Finalização etapa anterior")]
        [SMCLegendItem("smc-legenda-status-pendente", "A vigência da etapa deve iniciar somente após o fim da vigência da etapa anterior", order: 0)]
        FinalizaçãoEtapaAnterior = 1,
    }
}