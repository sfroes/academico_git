using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class InformacaoFinanceiraTermoAcademicoVO : ISMCMappable
    {
        public decimal ValorParcela { get; set; }

        public decimal ValorTotalCurso { get; set; }

        public int QuantidadeParcelas { get; set; }

        public int DiaVencimentoParcelas { get; set; }

        public string DescricaoServico { get; set; }

        public DateTime DataInicioParcela { get; set; }

        public bool ServicoAdicional { get; set; }

        public int QuantidadeParcelasRestantes { get; set; }
    }
}