using SMC.Academico.Domain.Areas.ORG.Specifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.ORG.Models
{
    public partial class HierarquiaEntidade
    {
        [NotMapped]
        public bool Vigente
        {
            get
            {
                return new HierarquiaEntidadeVigenteSpecification().IsSatisfiedBy(this);
            }
        }
    }
}
