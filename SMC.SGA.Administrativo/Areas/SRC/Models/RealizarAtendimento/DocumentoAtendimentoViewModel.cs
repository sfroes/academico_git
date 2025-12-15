using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Exceptions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class DocumentoAtendimentoViewModel
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public string Token { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCHidden]
        public long SeqTipoDocumento { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }


        [SMCHidden]
        public bool EntregueAnteriormente { get; set; }

        [SMCHidden]
        public VersaoDocumento VersaoExigida { get; set; }

        [SMCHidden]
        public DateTime DataMinimaEntrega { get; set; }

        [SMCHidden]
        public DateTime DataMaximaEntrega { get; set; }

        [SMCHidden]
        public string Observacao { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid1_24)]
        public SituacaoEntregaDocumento SituacaoEntregaDocumentoInicial { get; set; }

        [SMCHidden]
        public string DescricaoSituacaoInicial { get { return SituacaoEntregaDocumentoInicial.SMCGetDescription(); } }

        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect("SolicitacoesEntregaDocumento", NameDescriptionField = nameof(DescricaoSituacaoInicial))]
        public SituacaoEntregaDocumento SituacaoEntregaDocumento { get; set; }


        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R1")]
        [SMCConditionalRule("R1")]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RealizarAtendimentoController.PreencherDataEntrega), "RealizarAtendimento", true, includedProperties: new string[] { nameof(DataEntrega), nameof(ArquivoAnexado) })]
        public DateTime? DataEntrega { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RealizarAtendimentoController.PreencherVersaoDocumento), "RealizarAtendimento", true, includedProperties: new string[] { nameof(VersaoDocumento), nameof(VersaoExigida)})]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R1")]
        [SMCConditionalRule("R1")]
        public VersaoDocumento? VersaoDocumento { get; set; }

        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)] 
        [SMCCssClass("smc-sga-registro-documentos-dica")]
        public string InformacaoVersaoExigida { get { return $"Versão exigida: {SMCEnumHelper.GetDescription(this.VersaoExigida)}"; } }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega })]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, new object[] { SituacaoEntregaDocumento.Pendente, SituacaoEntregaDocumento.AguardandoEntrega }, RuleName = "R1")]
        [SMCConditionalRule("R1")]
        [SMCDependency(nameof(SituacaoEntregaDocumento), nameof(RealizarAtendimentoController.PreencherFormaEntregaDocumento), "RealizarAtendimento", true, includedProperties: new string[] { nameof(FormaEntregaDocumento), nameof(ArquivoAnexado) })]
        public FormaEntregaDocumento? FormaEntregaDocumento { get; set; }

        [SMCSize(SMCSize.Grid3_24)]
        [SMCHideLabel]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadDocumentoEnviado", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps", "xml" })]
        [SMCConditionalDisplay(nameof(Token), SMCConditionalOperation.Equals, new object[] { TOKEN_SERVICO.ATUALIZACAO_DOCUMENTACAO_EMISSAO_DIPLOMA })]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHideLabel]
        [SMCDisplay]
        [SMCCssClass("smc-sga-btn-entregaposterior")]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public string ArquivoAnexadoDownload { get; set; }
      
        [SMCHideLabel]
        [SMCDisplay(DisplayAsInstructions = true)]
        [SMCCssClass("smc-sga-btn-observacao")]
        [SMCSize(SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24, SMCSize.Grid1_24)]
        public string ObservacaoAluno { get { return Observacao; } }


        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaAutenticada), RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(VersaoExigida), SMCConditionalOperation.Equals, nameof(DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.Original), RuleName = "Rule2")]
        [SMCConditionalRequired(nameof(VersaoDocumento), SMCConditionalOperation.Equals, (int)DadosMestres.Common.Areas.PES.Enums.VersaoDocumento.CopiaSimples, RuleName = "Rule3")]
        [SMCConditionalRule("(Rule1 || Rule2) && Rule3")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public string ObservacaoSecretaria { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCConditionalReadonly(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.NotEqual, SituacaoEntregaDocumento.Indeferido)]
        [SMCConditionalRequired(nameof(SituacaoEntregaDocumento), SMCConditionalOperation.Equals, SituacaoEntregaDocumento.Indeferido)]
        public string DescricaoInconformidade { get; set; }

    }
}