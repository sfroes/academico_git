using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class RegistroDocumentoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public List<DocumentoVO> Documentos { get; set; }

        public long? SeqConfiguracaoEtapa { get; set; }

        public bool? PermiteUploadArquivo { get; set; }
    }
}