using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioLookupFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string DescricaoParceria { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public string CPF { get; set; }

        public string Passaporte { get; set; }

        public TipoMobilidade? TipoMobilidade { get; set; }
    }
}
