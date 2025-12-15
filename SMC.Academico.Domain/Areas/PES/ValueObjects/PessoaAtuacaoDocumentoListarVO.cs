using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoDocumentoListarVO : ISMCMappable
    {
        public long? Seq { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        public long? SeqTipoDocumento { get; set; }
        public string DescricaoTipoDocumento { get; set; }
        public DateTime DataEntrega { get; set; }
        public string NumeroProtocoloSolicitado { get; set; }
        public string Observacao { get; set; }
        public Guid? UidArquivo { get; set; }
        public long? SeqArquivoAnexado { get; set; }
        public SMCUploadFile ArquivoAnexado { get; set; }
        public long? SeqSolicitacaoDocumentoRequerido { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public string DescricaoSolicitacaoServico { get; set; }

    }

}
