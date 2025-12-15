using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoDocumentoViewModel
    {
        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string TipoDocumento { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

    }
}
