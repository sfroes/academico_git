using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.MAT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TurmaOfertaMatricula : short
    {        
        [EnumMember]
        [SMCLegendItem("smc-sga-legenda-desbloqueio", "Sim")]
        ComponentePertence = 1,

        [EnumMember]
        [SMCLegendItem("smc-sga-legenda-bloqueio", "Não")]
        ComponenteNaoPertence = 2,
    }
}
