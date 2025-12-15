using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.ValueObjects
{
    public class EtapaPaginaVO : ISMCMappable
    {
        public string Titulo { get; set; }

        public long Seq { get; set; }

        public long? SeqSituacaoEtapaInicial { get; set; }

        public long? SeqSituacaoEtapaFinal { get; set; }
    }
}