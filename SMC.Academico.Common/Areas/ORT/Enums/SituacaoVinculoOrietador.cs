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
    public enum SituacaoVinculoOrietador : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Colaborador com vínculo não ativo no Programa / Instituição.")]
        [SMCLegendItem(SMCGeometricShapes.Triangle, SMCLegendColors.Yellow2, "Colaborador com vínculo não ativo no Programa / Instituição.")]
        VinculoNaoAtivo,
    }
}
