using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoTaxaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqServico { get; set; }

        public int SeqTaxaGra { get; set; }
    }
}
