using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoTaxaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public int SeqTaxaGra { get; set; }
    }
}
