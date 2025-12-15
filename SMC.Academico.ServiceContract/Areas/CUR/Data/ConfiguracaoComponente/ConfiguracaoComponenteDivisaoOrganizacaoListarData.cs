using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponenteDivisaoOrganizacaoListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public long SeqComponenteCurricularOrganizacao { get; set; }

        public short CargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string Descricao { get; set; }
    }
}
