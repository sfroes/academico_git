using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoMotivoBloqueioParcelaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public long SeqServico { get; set; }

        public bool Obrigatorio { get; set; }
    }
}
