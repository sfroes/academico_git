using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SolicitacaoDocumentoDocumentoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public bool TemAnexoAnterior { get; set; }

        [SMCHidden]
        public bool ExibirEntregaPosterior { get; set; }

        [SMCHidden]
        public bool AplicacaoAluno { get; set; }

        [SMCHidden]
        public bool ExibirDataPrazoEntrega { get; set; }

        [SMCHidden]
        public string ObservacaoSecretaria { get; set; }

        [SMCHidden]
        public bool EhPrimeiraEtapa { get; set; }

        [SMCHidden]
        public DateTime? DataLimiteEntrega { get; set; }

        [SMCHidden]
        public bool DocumentoNaoEntregue
        {
            get
            {
                if(TokenServico != TOKEN_SERVICO.ENTREGA_DOCUMENTACAO && TokenServico != TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA)
                {
                    var documentoEstaPendente = SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente ||
                            SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao ||
                            SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                            SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido ||
                            SituacaoEntregaDocumento == SituacaoEntregaDocumento.Nenhum;

                    return documentoEstaPendente;
                }

                return true;
            }
        }

        [SMCHidden]

        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCHidden]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public DateTime? DataPrazoEntrega { get; set; }

        //Exibição para trabalhar com label e texto da data prazo de entrega
        [SMCHideLabel]
        [SMCCssClass("smc-sgaaluno-label-prazoentrega")]
        [SMCConditionalDisplay(nameof(ExibirDataPrazoEntrega), true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string DataPrazdoEntregaLabel
        {
            get
            {
                if (string.IsNullOrEmpty(this.DataPrazoEntrega.SMCDataAbreviada()))
                {
                    return string.Empty;
                }

                return "Prazo de entrega: ";
            }
        }

        [SMCHideLabel]
        [SMCConditionalDisplay(nameof(ExibirDataPrazoEntrega), true)]
        [SMCCssClass("smc-sgaaluno-input-prazoentrega")]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string DataPrazdoEntregaTexto { get => this.DataPrazoEntrega.SMCDataAbreviada(); }

        [SMCLegendItemDisplay()]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        [SMCHideLabel]
        public SituacaoEntregaDocumento SituacaoEntregaDocumentoAuxiliar
        {
            get
            {
                if (!this.ExibirLegendaDocumento)
                {
                    return SituacaoEntregaDocumento.Nenhum;
                }
                else
                {
                    return this.SituacaoEntregaDocumento;
                }
            }
        }

        [SMCConditionalReadonly(nameof(EntregaPosterior), true, RuleName = "Rule1")]
        [SMCConditionalReadonly(nameof(AplicacaoAluno), SMCConditionalOperation.Equals, true, RuleName = "Rule2")]
        [SMCConditionalReadonly(nameof(ExibirEntregaPosterior), true, RuleName = "Rule3")]
        [SMCConditionalReadonly(nameof(DocumentoNaoEntregue), false, RuleName = "Rule4")]
        [SMCConditionalRule("(Rule1 && Rule2 && Rule3) || (Rule2 && Rule4)")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid18_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public string Observacao { get; set; }

        [SMCFile(AreaDownload = "", ActionDownload = "DownloadDocumentoEnviado", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" })]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCConditionalRequired(nameof(Observacao), SMCConditionalOperation.NotEqual, "", RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(AplicacaoAluno), SMCConditionalOperation.Equals, true, RuleName = "Rule2")]
        [SMCConditionalReadonly(nameof(AplicacaoAluno), SMCConditionalOperation.Equals, true, RuleName = "Rule3", PersistentValue = true)]
        [SMCConditionalReadonly(nameof(EntregaPosterior), true, RuleName = "Rule4")]
        [SMCConditionalReadonly(nameof(ExibirEntregaPosterior), true, RuleName = "Rule5")]
        [SMCConditionalReadonly(nameof(DocumentoNaoEntregue), false, RuleName = "Rule8")]
        [SMCConditionalRequired(nameof(EntregaPosterior), SMCConditionalOperation.Equals, false, RuleName = "Rule9")]
        [SMCConditionalRule("Rule1 && Rule2 && Rule9")]
        [SMCConditionalRule("(Rule3 && Rule4 && Rule5)")]
        [SMCConditionalRule("Rule2 && Rule8 && Rule9")]
        [SMCConditionalDisplay(nameof(EntregaPosterior), SMCConditionalOperation.Equals, false, RuleName = "Rule6")]
        [SMCConditionalDisplay(nameof(EhPrimeiraEtapa), SMCConditionalOperation.Equals, false, RuleName = "Rule7")]
        [SMCConditionalRule("Rule6 || Rule7")]
        public SMCUploadFile ArquivoAnexado { get; set; }

        /// <summary>
        /// Esta propriedade foi criada somente para desabilitar o componente e traze-lo vazio caso o usuario tenha escolhido um arquivo.
        /// </summary>
        [SMCConditionalDisplay(nameof(EntregaPosterior), SMCConditionalOperation.Equals, true, RuleName = "Rule1")]
        [SMCConditionalDisplay(nameof(EhPrimeiraEtapa), SMCConditionalOperation.Equals, true, RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadDocumentoEnviado", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" })]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCReadOnly]
        [SMCHideLabel]
        [SMCCssClass("smc-sgaaluno-upload-bloqueado")]
        public SMCUploadFile ArquivoAnexado1 { get; set; }

        [SMCDisplay]
        [SMCCssClass("smc-sga-btn-entregaposterior")]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid1_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public string AnexoAnterior { get; set; }

        [SMCHideLabel]
        [SMCCssClass("smc-sga-detalhe-entregaposterior")]
        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public string DescricaoInconformidade { get; set; }

        [SMCHideLabel]
        [SMCConditionalDisplay(nameof(ExibirEntregaPosterior), true)]
        [SMCConditionalReadonly(nameof(DocumentoNaoEntregue), false)]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public bool? EntregaPosterior { get; set; }

        [SMCHideLabel]
        [SMCCssClass("smc-sga-check-entregaposterior")]
        [SMCReadOnly]
        [SMCConditionalDisplay(nameof(ExibirEntregaPosterior), true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid7_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public string TextoEntregaPosterior { get; set; }

        [SMCHidden]
        public bool EntregueAnteriormente { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }

        [SMCHidden]
        public bool ExibirLegendaDocumento { get; set; }

    }
}