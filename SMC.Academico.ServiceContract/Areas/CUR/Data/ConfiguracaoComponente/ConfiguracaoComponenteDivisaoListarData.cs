using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteDivisaoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public string ModalidadeDescricao { get; set; }

        public short Numero { get; set; }

        public short CargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }
        
        public bool PermiteGrupo { get; set; }

        public string DescricaoFormatada { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public string CargaHorariaGradeFormatada { get; set; }
    }
}
