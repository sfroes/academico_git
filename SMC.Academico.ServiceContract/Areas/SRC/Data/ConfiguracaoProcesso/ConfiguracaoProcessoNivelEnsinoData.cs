using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoNivelEnsinoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqNivelEnsino { get; set; }
    }
}