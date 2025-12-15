using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoRegistroDocumentoItemData : ISMCMappable
    {
        public long SeqTipoDocumento { get; set; }

        public long? SeqArquivoAnexado { get; set; }

        public new SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public DateTime? DataEntrega { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public VersaoDocumento VersaoExigida { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }
    }
}