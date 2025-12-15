using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoCobrancaTaxaItemVO : ISMCMappable
    {
        public string DescricaoTaxa { get; set; }

        public string ValorTaxa { get; set; }
    }
}
