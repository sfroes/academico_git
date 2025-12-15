using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoServicoListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqSolicitacaoServicoEtapa { get; set; }

        [SMCHidden]
        public long? SeqIngressante { get; set; }

        [SMCHidden]
        public bool SituacaodDocumentacaoNaoRequerida { get; set; }

        [SMCHidden]
        public bool SituacaoAtualSolicitacaoEFimProcesso { get; set; }

        [SMCHidden]
        public bool TokenEntregaDocumentacao { get; set; }
        [SMCHidden]
        public DateTime DataInclusao { get; set; }

        [SMCHidden]
        public DateTime DataSolicitacao { get; set; }
        
        [SMCHidden]
        public bool ConfigEtapaPossuiPaginaUploadDocumento { get; set; }

        [SMCGridLegend]
        [SMCSize(SMCSize.Grid1_24)]
        [SMCHidden]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        [SMCOrder(0)]
        public PossuiBloqueio PossuiBloqueio { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCSortable(true, false, "NumeroProtocolo")]
        [SMCOrder(1)]
        public string NumeroProtocolo { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSortable(true, false, "NomeSolicitanteFormatado")]
        [SMCOrder(2)]
        public string Solicitante { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCSortable(true, false, "DescricaoProcesso")]
        [SMCOrder(4)]
        public string Processo { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCSortable(true, true, "DataSolicitacao")]
        [SMCOrder(5)]
        public string DataSolicitacaoFormatada { get { return this.DataSolicitacao.ToString("dd/MM/yy"); } }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCOrder(7)]
        public string EtapaAtual { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCOrder(8)]
        public string SituacaoAtual { get; set; }

        [SMCGridLegend]
        [SMCHidden]
        [SMCSize(SMCSize.Grid1_24)]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        [SMCOrder(9)]
        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }
    }
}