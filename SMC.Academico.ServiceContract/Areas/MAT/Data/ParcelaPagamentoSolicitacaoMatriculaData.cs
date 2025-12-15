using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ParcelaPagamentoSolicitacaoMatriculaData : ISMCMappable
    {
        public string NomeParcela { get; set; }

        public int NumeroParcela { get; set; }

        public decimal ValorParcela { get; set; }

        public decimal ValorBeneficio { get; set; }

        public decimal ValorOutros { get; set; }

        public decimal ValorPagar { get; set; }

        public DateTime DataVencimento { get; set; }

        public string NomeBeneficio { get; set; }


        public List<ParcelaPagamentoResponsavelSolicitacaoMatriculaData> Titulos { get; set; }

        public bool EscalonamentoPossuiDataEncerramento { get; set; }

    }
}
