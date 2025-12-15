using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ParcelaPagamentoSolicitacaoMatriculaVO : ISMCMappable
    {
        public string NomeParcela { get; set; }

        public int NumeroParcela { get; set; }

        public decimal ValorParcela { get; set; }

        public decimal ValorBeneficio { get; set; }

        public decimal ValorOutros { get; set; }

        public decimal ValorPagar { get; set; }

        public DateTime DataVencimento { get; set; }

        public string NomeBeneficio { get; set; }

        public List<ParcelaPagamentoResponsavelSolicitacaoMatriculaVO> Titulos { get; set; }

        public bool EscalonamentoPossuiDataEncerramento { get; set; }
    }
}