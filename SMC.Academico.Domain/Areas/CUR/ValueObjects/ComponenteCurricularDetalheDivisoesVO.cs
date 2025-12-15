using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularDetalheDivisoesVO : ISMCMappable
    {
        public long Seq { get; set; }

        public short Numero { get; set; }

        public string TipoDivisao { get; set; }

        
        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                var result = $"{Numero}";

                if (!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
                    result += $" - {DescricaoComponenteCurricularOrganizacao}";

                if (!string.IsNullOrEmpty(TipoDivisao))
                    result += $" - {TipoDivisao}";

                if (CargaHoraria.HasValue)
                    result += $" - {CargaHoraria}";

                if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
                    result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria.Value)}";

                return result;
            }
        }

        public string CargaHorariaGradeFormatada
        {
            get
            {
                string formatoCargaHoraria = this.FormatoCargaHoraria.HasValue ? SMCEnumHelper.GetDescription(FormatoCargaHoraria) : null;
                return CargaHorariaGrade > 0 ? $"{CargaHorariaGrade} {formatoCargaHoraria}" : string.Empty;
            }
        }
    }
}
