using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum SituacaoConfiguracaoComponenteCurricular : short
    {
        [Description("")]
        [EnumMember]
        [SMCIgnoreValue]      
        Nenhum = 0,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Red2, "Exige associação de assunto de componente")]
        ExigeAssunto = 1,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Yellow3, "Assunto de componente associado")]
        AssuntoCadastrado = 2,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green1, "Componente com requisito cadastrado")]
        RequisitoCadastrado = 3
    }
}