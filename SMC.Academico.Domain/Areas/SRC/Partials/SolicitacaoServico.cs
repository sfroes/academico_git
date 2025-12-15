using SMC.Framework;
using SMC.Framework.Audit;
using SMC.Framework.Mapper;
using System.ComponentModel.DataAnnotations;

namespace SMC.Academico.Domain.Areas.SRC.Models
{
    public partial class SolicitacaoServico : ISMCSeq, ISMCAuditData, ISMCMappable
    {
        //[Required]
        [StringLength(255)]
        public virtual string NumeroProtocolo { get; set; }
    }
}