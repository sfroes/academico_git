using SMC.Framework.Mapper;
using SMC.Framework;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeImagemData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }
        
        public long SeqEntidade { get; set; }
        
        public TipoImagem TipoImagem { get; set; }
        
        public long SeqArquivoAnexado { get; set; }
        
        public SMCUploadFile ArquivoAnexado { get; set; }

        public EntidadeData Entidade { get; set; }
    }
}
