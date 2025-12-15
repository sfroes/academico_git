using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.CSO.Models
{
    public partial class Curso
    {
        [NotMapped]
        public virtual long SeqInstituicaoNivelEnsino { get; set; }

    }
}
