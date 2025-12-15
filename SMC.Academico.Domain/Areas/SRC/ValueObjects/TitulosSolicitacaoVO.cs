using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TitulosSolicitacaoVO : ISMCMappable
    {
        public int SeqTituloGra { get; set; }
      
        public decimal ValorTitulo { get; set; }
     
        public SituacaoTitulo SituacaoTitulo { get; set; }
      
        public DateTime DataGeracaoTitulo { get; set; }

        public DateTime DataVencimento { get; set; }
      
        public DateTime? DataPagamento { get; set; }
    }
}
