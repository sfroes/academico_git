using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoEntregaAvaliacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        [SMCLegendItem(SMCGeometricShapes.Circle, SMCLegendColors.Green2, "Entrega via SGA")]
        OnLine = 1,

        [EnumMember]
        Presencial = 2
    }
}