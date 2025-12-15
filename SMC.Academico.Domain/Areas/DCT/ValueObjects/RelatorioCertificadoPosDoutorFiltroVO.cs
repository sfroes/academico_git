using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class RelatorioCertificadoPosDoutorFiltroVO : ISMCMappable
    {
        public long SeqEntidadeResponsavel { get; set; }

        public long SeqColaboradorPosDoutorando { get; set; }

        public long SeqColaboradorVinculo { get; set; }
    }
}