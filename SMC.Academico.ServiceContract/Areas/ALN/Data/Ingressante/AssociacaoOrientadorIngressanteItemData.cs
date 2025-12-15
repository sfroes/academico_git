using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AssociacaoOrientadorIngressanteItemData : ISMCMappable
    {
        public long TipoParticipacaoOrientacao { get; set; }

        public string DescricaoTipoParticipacaoOrientacao { get; set; }

        public long? SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }
    }
}