using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ParcelasPagamentoSolicitacaoMatriculaVO : ISMCMappable
    {
        public decimal ValorTotalMatricula { get; set; }

        public List<ParcelaPagamentoSolicitacaoMatriculaVO> Parcelas { get; set; }

        public bool BeneficioIncluiDesbloqueioTemporario { get; set; }

        public bool BeneficioDeferidoCobranca { get; set; }
    }
}