using SMC.Academico.Domain.Areas.ORG.Specifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.ORG.Models
{
    public partial class NivelEnsino
    {
        [NotMapped]
        public bool Folha
        {
            get
            {
                return new NivelEnsinoFolhaSpecification().IsSatisfiedBy(this);
            }
        }
    }
}
