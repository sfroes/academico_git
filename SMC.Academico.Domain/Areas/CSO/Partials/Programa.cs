using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Models
{
    public partial class Programa
    {
        [NotMapped]
        public long SeqHierarquiaEntidadeItem { get { return this.HierarquiasEntidades?.FirstOrDefault()?.Seq ?? 0; } }
    }
}
