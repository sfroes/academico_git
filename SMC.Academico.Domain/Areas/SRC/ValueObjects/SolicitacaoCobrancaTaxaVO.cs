using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoCobrancaTaxaVO : ISMCMappable
    {
        public int? CodigoNucleo { get; set; }

        public bool ExibeMensagemInformativaTaxasSemValor { get; set; }

        public string MensagemInformativaTaxasSemValor { get; set; }

        public bool ExisteTaxaAssociada { get; set; }

        public List<SolicitacaoCobrancaTaxaItemVO> Taxas { get; set; }

        public decimal ValorTotalTaxas { get; set; }

        public bool ExisteTaxaSemValor { get; set; }

        public bool ExisteTaxaComValorIncorreto { get; set; }

        public TipoEmissaoTaxa TipoEmissaoTaxa { get; set; }
    }
}
