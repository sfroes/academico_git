using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoCursoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoProcesso { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }
        
        public long SeqCursoOfertaLocalidade { get; set; }
     
        public long SeqTurno { get; set; }

        public string DescricaoTurno { get; set; }
    }
}