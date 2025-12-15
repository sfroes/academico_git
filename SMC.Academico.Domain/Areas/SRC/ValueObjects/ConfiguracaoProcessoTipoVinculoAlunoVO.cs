using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoTipoVinculoAlunoVO : ISMCMappable
    {
        public long Seq { get; set; }
      
        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqTipoVinculoAluno { get; set; }
    }
}