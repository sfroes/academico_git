using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoConfiguracaoComponenteItemData : ISMCMappable
    {
        public long Seq { get; set; }
        
        public long SeqConfiguracaoComponente { get; set; }
                
        public string ConfiguracaoComponenteCodigo { get; set; }

        public string ConfiguracaoComponenteDescricao { get; set; }

        public string ConfiguracaoComponenteDescricaoComplementar { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public short? ComponenteCurricularCargaHoraria { get; set; }

        public short? ComponenteCurricularCredito { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }
    }
}
