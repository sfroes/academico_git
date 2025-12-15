using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoUnidadeResponsavelData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcesso { get; set; }

        public string Nome { get; set; }

        [SMCMapProperty("EntidadeResponsavel.NomeReduzido")]
        public string NomeReduzido { get; set; }
    }
}