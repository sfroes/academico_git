using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeClassificacaoData : ISMCMappable, ISMCSeq​​
    {

        public  long Seq { get; set; }


        public  long SeqEntidade { get; set; }

     
        public  long SeqClassificacao { get; set; }
    }
}
