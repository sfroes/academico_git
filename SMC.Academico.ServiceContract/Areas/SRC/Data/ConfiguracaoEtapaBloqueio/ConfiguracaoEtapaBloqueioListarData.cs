using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaBloqueioListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public string DescricaoMotivo { get; set; }

        public AmbitoBloqueio AmbitoBloqueio { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}
