using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularDetalheDivisoesData : ISMCMappable
    {
        public long Seq { get; set; }

        public short Numero { get; set; }

        public string TipoDivisao { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string DescricaoCompleta { get; set; }

        public string CargaHorariaGradeFormatada { get; set; }
    }
}
