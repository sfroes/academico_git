using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoMensagemTipoUsoData : ISMCMappable, ISMCSeq​​
    { 
        public long Seq { get; set; }
         
        public long SeqTipoMensagem { get; set; }
         
        public TipoUsoMensagem TipoUsoMensagem { get; set; }
    }
}
