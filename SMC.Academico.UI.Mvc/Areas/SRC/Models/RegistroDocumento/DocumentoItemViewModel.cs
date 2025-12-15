using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using System;
using System.Web.UI.WebControls;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DocumentoItemViewModel : SMCViewModelBase
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
        public bool BloquearTodosOsCampos { get; set; }

        [SMCHidden]
        public bool EntregueAnteriormente { get; set; }

        [SMCHidden]
        public VersaoDocumento VersaoExigida { get; set; }

        [SMCHidden]
        public bool? PossuiArquivoAnexado { get { return ArquivoAnexado != null; } }

        [SMCHidden]
        public DateTime DataMinimaEntrega { get; set; }
        
        [SMCHidden]
        public DateTime DataMaximaEntrega { get; set; }

        [SMCHidden]
        public string Observacao { get; set; }

        [SMCMapForceFromTo]
        [SMCLegendItemDisplay]
        [SMCSize(SMCSize.Grid1_24)]
        public SituacaoEntregaDocumento SituacaoEntregaDocumentoInicial { get; set; }

        [SMCHidden]
        public string DescricaoSituacaoInicial { get { return SituacaoEntregaDocumentoInicial.SMCGetDescription(); } }

        [SMCSize(SMCSize.Grid2_24)]
        [SMCCssClass("smc-academico-situacao-entrega-documento")]
        [SMCSelect("SolicitacoesEntregaDocumento", NameDescriptionField = nameof(DescricaoSituacaoInicial))]
        [SMCConditionalRequired(nameof(BloquearTodosOsCampos), SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName ="R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName ="R2")]
        [SMCConditionalRule("R1 || R2")]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, RuleName = "R1")]        
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.Indeferido}, RuleName = "R2")]        
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.Indeferido})]
        [SMCConditionalRule("R1 || R2")]
        [SMCMinDate(nameof(DataMinimaEntrega))]
        [SMCMaxDate(nameof(DataMaximaEntrega))]
        public DateTime? DataPrazoEntrega { get; set; }

        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid2_24)]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(PossuiArquivoAnexado), SMCConditionalOperation.Equals, true, RuleName = "R3", PersistentValue = true)]
        [SMCConditionalRule("(R1 || R4) || R2 || (R2 && R3)")]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RegistroDocumentoController.PreencherDataEntrega), "RegistroDocumento", true, includedProperties: new string[] { nameof(DataEntrega), nameof(ArquivoAnexado) })]
        public DateTime? DataEntrega { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RegistroDocumentoController.PreencherVersaoDocumento), "RegistroDocumento", true, includedProperties: new string[] { nameof(VersaoDocumento), nameof(VersaoExigida), nameof(ArquivoAnexado) })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(PossuiArquivoAnexado), SMCConditionalOperation.Equals, true, RuleName = "R3", PersistentValue = true)]
        [SMCConditionalRule("R1 || R4 || R2 || (R2 && R3)")]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public string InformacaoVersaoExigida { get { return $"Versão exigida: {SMCEnumHelper.GetDescription(this.VersaoExigida)}"; } }

        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid2_24)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(PossuiArquivoAnexado), SMCConditionalOperation.Equals, true, RuleName = "R3", PersistentValue = true)]
        [SMCConditionalRule("R1 || R4 || R2 || (R2 && R3)")]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RegistroDocumentoController.PreencherFormaEntregaDocumento), "RegistroDocumento", true, includedProperties: new string[] { nameof(FormaEntregaDocumento), nameof(ArquivoAnexado) })]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        [SMCCssClass("smc-sga-btn-upload-ico")]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" })]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCConditionalRequired(nameof(FormaEntregaDocumento), SMCConditionalOperation.Equals, (int)DadosMestres.Common.Areas.PES.Enums.FormaEntregaDocumento.Email, RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(FormaEntregaDocumento), SMCConditionalOperation.Equals, (int)DadosMestres.Common.Areas.PES.Enums.FormaEntregaDocumento.Upload, RuleName = "Rule2")]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, PersistentValue = false, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(PossuiArquivoAnexado), SMCConditionalOperation.Equals, true, RuleName = "R3", PersistentValue = true)]
        [SMCConditionalRule("R1 || R4 || R2 || (R2 && R3)")]
        [SMCConditionalRule("Rule1 || Rule2")]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHideLabel]
        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCCssClass("smc-sga-btn-observacao")]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public string ObservacaoAluno { get { return Observacao; } }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(BloquearTodosOsCampos), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R4")]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RegistroDocumentoController.PreencherObservacao), "RegistroDocumento", true, includedProperties: new string[] { nameof(ObservacaoSecretaria), nameof(ArquivoAnexado) })]
        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaAutenticada), RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.Original), RuleName = "Rule2")]
        [SMCConditionalRequired(nameof(VersaoDocumento), SMCConditionalOperation.Equals, (int)DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaSimples, RuleName = "Rule3")]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Pendente }, RuleName = "R2")]
        [SMCConditionalReadonly(nameof(PossuiArquivoAnexado), SMCConditionalOperation.Equals, true, RuleName = "R3", PersistentValue = true)]
        [SMCConditionalRule("R1 || R4 || R2 || (R2 && R3)")]
        [SMCConditionalRule("(Rule1 || Rule2) && Rule3")]
        public string ObservacaoSecretaria { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, SituacaoEntregaDocumento.Indeferido, RuleName ="R1")]
        [SMCConditionalReadonly(nameof(EntregueAnteriormente), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, SituacaoEntregaDocumento.Indeferido)]
        [SMCConditionalRule("R1 || R2")]
        public string DescricaoInconformidade { get; set; }
    }
}