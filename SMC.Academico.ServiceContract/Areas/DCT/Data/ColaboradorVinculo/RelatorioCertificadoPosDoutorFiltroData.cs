using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class RelatorioCertificadoPosDoutorFiltroData : ISMCMappable
    {
        public long SeqEntidadeResponsavel { get; set; }

        public long SeqColaboradorPosDoutorando { get; set; }

        public long SeqColaboradorVinculo { get; set; }
    }
}