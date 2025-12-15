using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoComponenteListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.Descricao")]
        public string TipoDivisaoDescricao { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.GeraOrientacao")]
        public bool GerarOrientacao { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.SeqTipoOrientacao")]
        public long? SeqTipoOrientacao { get; set; }

        [SMCMapProperty("TipoDivisaoComponente.Modalidade.Descricao")]
        public string ModalidadeDescricao { get; set; }

        [SMCMapProperty("ComponenteCurricularOrganizacao.Descricao")]
        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public short Numero { get; set; }

        public short CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public bool PermiteGrupo { get; set; }

        public string DescricaoFormatada
        {
            get
            {
                var result = $"{Numero}";

                if (!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
                    result += $" - {DescricaoComponenteCurricularOrganizacao}";

                if (!string.IsNullOrEmpty(TipoDivisaoDescricao))
                    result += $" - {TipoDivisaoDescricao}";

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
