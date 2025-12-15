using SMC.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Models
{
    public partial class ArquivoAnexado : ISMCFile
    {
        [NotMapped]
        public string Description
        {
            get
            {
                return this.Nome;
            }
            set
            {

            }
        }

        [NotMapped]
        public SMCUploadFileState State { get; set; }
    }
}
