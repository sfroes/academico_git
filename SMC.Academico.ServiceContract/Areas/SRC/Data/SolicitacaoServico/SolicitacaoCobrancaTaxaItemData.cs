using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoCobrancaTaxaItemData : ISMCMappable
    {
        public string DescricaoTaxa { get; set; }

        public string ValorTaxa { get; set; }
    }
}
