using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORT.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum FiltroSituacaoTrabalhoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Aguardando autorização pelo aluno")]
        [SMCLegendItem(SMCGeometricShapes.Square, SMCLegendColors.Red2, "Aguardando autorização pelo aluno")]
        AguardandoAutorizacaoAluno,

        [EnumMember]
        [Description("Autorizado e liberado para secretaria")]
        [SMCLegendItem(SMCGeometricShapes.Star, SMCLegendColors.Green2, "Autorizado e liberado para secretaria")]
        AutorizadaLiberadaSecretaria,

        [EnumMember]
        [Description("Liberado para conferência da biblioteca")]
        [SMCLegendItem(SMCGeometricShapes.Triangle, SMCLegendColors.Yellow2, "Liberado para conferência da biblioteca")]
        LiberadaBiblioteca,

        [EnumMember]
        [Description("Liberado para consulta")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green4, "Liberado para consulta")]
        LiberadaConsulta
    }
}
