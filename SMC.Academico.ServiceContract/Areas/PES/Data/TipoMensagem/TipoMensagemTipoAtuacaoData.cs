using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoMensagemTipoAtuacaoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }
         
        public long SeqTipoMensagem { get; set; }
         
        public TipoAtuacao TipoAtuacao { get; set; }
    }
}
