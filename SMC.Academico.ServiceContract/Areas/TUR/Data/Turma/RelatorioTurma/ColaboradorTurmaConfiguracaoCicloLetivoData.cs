using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class ColaboradorTurmaConfiguracaoCicloLetivoData : ISMCMappable
    {
        public long? SeqColaboradorTurmaConfiguracaoCicloLetivo { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public string Nome { get; set; }
    }
}