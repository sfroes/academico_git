using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class RegistroDocumentoAtendimentoVO : ISMCMappable
    {
        public bool NecessitaConfirmacaoEntregaDocumentos { get; set; }
        public List<string> DocumentosAlterados { get; set; }
        public List<RegistroDocumentoAtendimentoDocumentoVO> Documentos { get; set; }
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }
        public long? SeqConfiguracaoEtapa { get; set; }

        public bool? PermiteUploadArquivo { get; set; }


    }
}