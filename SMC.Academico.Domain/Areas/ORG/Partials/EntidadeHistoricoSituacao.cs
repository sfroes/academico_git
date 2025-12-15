using SMC.Academico.Domain.Areas.ORG.Specifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.ORG.Models
{
    public partial class EntidadeHistoricoSituacao
    {
        [NotMapped]
        public bool Atual
        {
            get
            {
                return new EntidadeHistoricoSituacaoAtualSpecification().IsSatisfiedBy(this);
            }
        }
    }
}
