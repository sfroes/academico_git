using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoRegistroDocumentoItemViewModel : SMCViewModelBase
    {
        // public PessoaAtuacaoRegistroDocumentoItemViewModel()
        // {
        //     SituacaoEntregaDocumentoInicial = SituacaoEntregaDocumento.AguardandoEntrega;
        // }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        public DateTime DataMinimaPrazoEntrega { get { return DateTime.Now.AddDays(1).Date; } }

        [SMCMapForceFromTo]
        [SMCLegendItemDisplay]
        [SMCSize(SMCSize.Grid1_24)]
        [SMCHidden]
        public SituacaoEntregaDocumento SituacaoEntregaDocumentoInicial { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid3_24)]
        [SMCCssClass("smc-academico-situacao-entrega-documento")]
        [SMCSelect("SituacoesEntregaDocumento")]
        [SMCRequired]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid3_24)]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, SituacaoEntregaDocumento.Pendente)]
        [SMCMinDate(nameof(DataMinimaPrazoEntrega))]
        public DateTime? DataPrazoEntrega { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid3_24)]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(PessoaAtuacaoController.PreencherDataEntrega), "PessoaAtuacao", true, includedProperties: nameof(DataEntrega))]
        public DateTime? DataEntrega { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid3_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid2_24, SMCSize.Grid2_24, SMCSize.Grid1_24)]
        public string InformacaoVersaoExigida { get { return $"Versão exigida: {SMCEnumHelper.GetDescription(this.VersaoExigida)}"; } }

        [SMCHidden]
        public VersaoDocumento VersaoExigida { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid3_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(PessoaAtuacaoController.PreencherFormaEntregaDocumento), "PessoaAtuacao", true, includedProperties: new string[] { nameof(FormaEntregaDocumento) })]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" })]
        [SMCConditionalRequired(nameof(FormaEntregaDocumento), SMCConditionalOperation.Equals, new object[] { (int)DadosMestres.Common.Areas.PES.Enums.FormaEntregaDocumento.Email, (int)DadosMestres.Common.Areas.PES.Enums.FormaEntregaDocumento.Upload })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid3_24)]
        //[SMCDependency(nameof(SituacaoEntregaDocumento), nameof(PessoaAtuacaoController.PreencherObservacao), "PessoaAtuacao", true, includedProperties: new string[] { nameof(Observacao) })]
        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaAutenticada), RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.Original), RuleName = "Rule2")]
        [SMCConditionalRequired(nameof(VersaoDocumento), SMCConditionalOperation.Equals, (int)DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaSimples, RuleName = "Rule3")]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.AguardandoEntrega, SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.Nenhum }, PersistentValue = false)]
        [SMCConditionalRule("(Rule1 || Rule2) && Rule3")]
        public string Observacao { get; set; }
    }
}