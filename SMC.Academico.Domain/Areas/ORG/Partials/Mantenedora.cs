using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.ORG.Models
{
    public partial class Mantenedora
    {
        // Propriedade criada apenas para auxiliar na transformação de Mantenedora em SMCDatasourceItem.
        [NotMapped]
        public string Descricao
        {
            get { return this.Nome; }
        }
    }
}
