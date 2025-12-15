using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EtapaPaginaData : ISMCMappable
    {
        public string Titulo { get; set; }

        public long Seq { get; set; }

        public long? SeqSituacaoEtapaInicial { get; set; }

        public long? SeqSituacaoEtapaFinal { get; set; }
    }
}