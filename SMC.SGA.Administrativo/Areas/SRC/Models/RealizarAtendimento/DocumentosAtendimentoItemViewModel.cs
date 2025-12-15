using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentosAtendimentoItemViewModel
    {
        public long Seq { get; set; }

        [SMCFile(ActionDownload = "DownloadAnexo")]
        public SMCUploadFile ArquivoAnexado { get; set; }

        public DateTime? DataEntrega { get; set; }

        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public VersaoDocumento? VersaoDocumento { get; set; }

        public DateTime? DataPrazoEntrega { get; set; }

        public string Observacao { get; set; }

        public string ObservacaoSecretaria { get; set; }

        public string DescricaoTipoDocumento { get; set; }
    }
}