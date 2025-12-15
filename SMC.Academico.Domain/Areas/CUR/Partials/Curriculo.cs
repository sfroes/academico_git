using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.CUR.Models
{
    public partial class Curriculo
    {
        /// <summary>
        /// Configuração obtida na InstituicaoNivel do Curso associado a este Currículo.
        /// Replicada no domínio para ser utilizada no validator.
        /// </summary>
        [NotMapped]
        public bool PermiteCreditoComponenteCurricular { get; set; }
    }
}
