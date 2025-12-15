using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaEmissaoBoletoParcelaMatriculaViewModel : SMCViewModelBase
    {
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public string NomeParcela { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        public int NumeroParcela { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCCurrency(true)]
        public decimal ValorParcela { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCCurrency(true)]
        public decimal ValorBeneficio { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCCurrency(true)]
        public decimal ValorOutros { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCCurrency(true)]
        public decimal ValorPagar { get; set; }

        [SMCSize(Framework.SMCSize.Grid4_24)]
        public DateTime DataVencimento { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long CodigoMigracaoAluno { get; set; }

        [SMCHidden]
        public bool BeneficioDeferidoCobranca { get; set; }

        [SMCHidden]
        public bool BeneficioIncluiDesbloqueioTemporario { get; set; }

        public string NomeBeneficio { get; set; }


        public List<PaginaEmissaoBoletoParcelaResponsavelMatriculaViewModel> Titulos { get; set; }

        [SMCHidden]
        public bool EscalonamentoPossuiDataEncerramento { get; set; }
    }
}