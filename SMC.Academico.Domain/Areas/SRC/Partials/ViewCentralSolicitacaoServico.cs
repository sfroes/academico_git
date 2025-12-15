using SMC.Framework.Mapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.Academico.Domain.Areas.SRC.Models
{
    public partial class ViewCentralSolicitacaoServico : ISMCMappable
    {
        [Key]
        [Required]
        [ForeignKey("SolicitacaoServico")]
        public virtual long SeqSolicitacaoServico { get; set; }
    }
}