using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoNivelEnsinoVO :  ISMCMappable
    {       
        public long Seq { get; set; }
    
        public long SeqConfiguracaoProcesso { get; set; }
       
        public long SeqNivelEnsino { get; set; }
    }
}