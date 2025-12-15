using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum MatrizExcecaoDispensa : short
    {
        [EnumMember]
        [Description("Todos")]
        Todos = 0,

        [EnumMember]
        [Description("Sim")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red2, "A dispensa aplica-se a todas as matrizes curriculares, exceto as matrizes associadas")]
        MatrizAssociado = 1,

        [Description("Não")]
        [EnumMember]
        SemMatrizAssociado = 2
    }
}