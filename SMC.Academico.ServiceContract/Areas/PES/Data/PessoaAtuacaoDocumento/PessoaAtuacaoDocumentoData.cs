using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoDocumentoData : ISMCMappable
    {
        public long? Seq { get; set; }
        public long SeqTipoDocumento { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime DataEntrega { get; set; }
        public string DescricaoTipoDocumento { get; set; }
        public long? SeqSolicitacaoDocumentoRequerido { get; set; }
        public string Observacao { get; set; }
        public SMCUploadFile ArquivoAnexado { get; set; }
        public long? SeqArquivoAnexado { get; set; }
    }
}
