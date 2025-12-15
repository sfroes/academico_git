using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public ParceriaIntercambioInstituicaoExternaData SeqInstituicaoExterna { get; set; }

        public bool? ProcessoNegociacao { get; set; }

        public bool? Ativo { get; set; }
    }
}