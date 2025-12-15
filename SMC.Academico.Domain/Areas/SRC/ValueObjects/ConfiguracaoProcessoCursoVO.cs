using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoCursoVO : ISMCMappable
    {
        public long Seq { get; set; }
      
        public long SeqConfiguracaoProcesso { get; set; }
      
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade")]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.SeqTurno")]
        public long SeqTurno { get; set; }

        [SMCMapProperty("CursoOfertaLocalidadeTurno.Turno.Descricao")]
        public string DescricaoTurno { get; set; }
    }
}