using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoAnexoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public Guid? UidArquivoAnexado { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCGridLegend]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataPrazoEntrega { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public DateTime? DataEntrega { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        public SMCUploadFile ArquivoAnexo { get; set; }

        [SMCCssClass("smc-size-md-9 smc-size-xs-9 smc-size-sm-9 smc-size-lg-9")]
        public string Observacao { get; set; }
    }
}