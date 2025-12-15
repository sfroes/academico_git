using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AssociacaoOrientadorIngressanteItemVO : ISMCMappable
    {
        public long TipoParticipacaoOrientacao { get; set; }

        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        public long? SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }
    }
}