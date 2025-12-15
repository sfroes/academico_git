using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ComponenteCurricularDetalheTopicosData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public short? CargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string DescricaoCompleta
        {
            get
            {
                if (CargaHoraria.HasValue)
                    return $"{Descricao} - {CargaHoraria} {FormatoCargaHoraria}";
                else
                    return $"{Descricao}";
            }
        }
    }
}
