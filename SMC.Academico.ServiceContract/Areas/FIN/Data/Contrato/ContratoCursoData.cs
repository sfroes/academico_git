using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class ContratoCursoData : ISMCMappable
    {
        [SMCMapProperty("Seq")]
        public long? SeqCurso { get; set; }

        [SMCMapProperty("Curso.Nome")]
        public string Nome { get; set; } 
    }
}
