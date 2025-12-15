using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpHistoricoSituacaoData : ISMCMappable
    {
        public virtual long Seq { get; set; }

        public virtual long SeqPublicacaoBdp { get; set; }

        public virtual SituacaoTrabalhoAcademico SituacaoTrabalhoAcademico { get; set; }

        public long? SeqNotificacaoEmailDestinatario { get; set; }
    }
}
