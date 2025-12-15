using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoTipoVinculoAlunoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqTipoVinculoAluno { get; set; }
    }
}