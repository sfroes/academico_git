using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeImagemVO : ISMCMappable
    {
        #region Primitive Properties

        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public TipoImagem TipoImagem { get; set; }
        
        public long SeqArquivoAnexado { get; set; }

        #endregion Primitive Properties

        
        #region Navigation Properties

        public SMCUploadFile ArquivoAnexado { get; set; }

        public Entidade Entidade { get; set; }

        #endregion Navigation Properties
    }
}
