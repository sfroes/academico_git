using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data.Ingressante
{
    public class IngressanteOfertaData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqCampanhaOferta { get; set; }

        public long? SeqCampanhaOfertaOrigem { get; set; }

        public long? SeqInscricaoOfertaGpi { get; set; }

        public long? SeqCampanhaOfertaItem { get; set; }

        public bool? InteressePermanecerOfertaOrigem { get; set; }
    }
}