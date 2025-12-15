using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class InformacaoFinanceiraTermoAcademicoViewModel : SMCViewModelBase
    {
        [SMCCurrency(true)]
        public decimal ValorParcela { get; set; }

        [SMCCurrency(true)]
        public decimal ValorTotalCurso { get; set; }

        public int QuantidadeParcelas { get; set; }

        public int DiaVencimentoParcelas { get; set; }

        public string DescricaoServico { get; set; }

        public DateTime DataInicioParcela { get; set; }

        public bool ServicoAdicional { get; set; }
    }
}