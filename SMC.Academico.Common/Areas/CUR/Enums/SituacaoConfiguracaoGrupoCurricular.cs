using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    [Flags]
    public enum SituacaoConfiguracaoGrupoCurricular : short
    {
        [Description("")]
        [EnumMember]
        [SMCIgnoreValue]      
        Nenhum = 0,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Diamond, SMCLegendColors.Yellow2, "Formação específica associada")]
        FormacaoEspecificaAssociada = 1 << 0,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Star, SMCLegendColors.Blue1, "Benefícios associados")]
        BeneficiosAssociados = 1 << 1,

        [EnumMember]
        [Description("")]
        [SMCLegendItem(SMCGeometricShapes.Hexagon, SMCLegendColors.Red1, "Condições de obrigatoriedade associadas")]
        CondicoesObrigatoriedadeAssociadas = 1 << 2
    }
}