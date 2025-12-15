using SMC.Academico.Common.Constants;
using SMC.Framework.DataAnnotations;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum LegendaConsultaHistoricoEscolar : short
    {
        [EnumMember]
        [SMCLegendItem("", "CH: Carga Horária (Hora Relógio)")]
        CargaHoraria = 1,

        [EnumMember]
        [SMCLegendItem("", "NA: Nota de Aprovação")]
        NotaAprovacao = 2,
    }
}