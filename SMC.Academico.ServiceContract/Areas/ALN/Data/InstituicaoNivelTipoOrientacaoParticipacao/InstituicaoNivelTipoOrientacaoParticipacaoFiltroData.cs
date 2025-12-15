using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoOrientacaoParticipacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoOrientacao { get; set; }

        public long? SeqTipoIntercambio { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqTermoIntercambio { get; set; }

        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }
    }
}