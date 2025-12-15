using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParceriaIntercambioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public ParceriaIntercambioInstituicaoExternaVO SeqInstituicaoExterna { get; set; }

        public bool? ProcessoNegociacao { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }
    }
}