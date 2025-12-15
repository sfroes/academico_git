using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ParcelasPagamentoSolicitacaoMatriculaData : ISMCMappable
    {
        public double ValorTotalMatricula { get; set; }

        public List<ParcelaPagamentoSolicitacaoMatriculaData> Parcelas { get; set; }

        public bool BeneficioIncluiDesbloqueioTemporario { get; set; }

        public bool BeneficioDeferidoCobranca { get; set; }

    }
}