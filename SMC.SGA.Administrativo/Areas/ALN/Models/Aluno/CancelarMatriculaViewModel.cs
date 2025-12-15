using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMC.SGA.Administrativo.Areas.ALN.Views.Aluno.App_LocalResources;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class CancelarMatriculaViewModel : SMCViewModelBase
    {
        #region DataSources

        public List<SMCDatasourceItem> SituacoesMatricula { get; set; }

        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        #endregion

        [SMCHidden]
        public long SeqAluno { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqSituacaoMatricula), nameof(AlunoController.BuscarSituacaoMatricula), "Aluno", true)]
        public string TokenSituacaoMatricula { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(CiclosLetivos), AutoSelectSingleItem = true)]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public long SeqCicloLetivo { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(AlunoController.BuscarDataInicioCicloLetivo), "Aluno", true, includedProperties: new string[] { nameof(SeqAluno) })]
        public DateTime DataInicioCicloLetivo { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(AlunoController.BuscarDataFimCicloLetivo), "Aluno", true, includedProperties: new string[] { nameof(SeqAluno) })]
        public DateTime DataFimCicloLetivo { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid5_24)]
        [SMCMapForceFromTo]
        [SMCMinDate(nameof(DataInicioCicloLetivo))]
        [SMCMaxDate(nameof(DataFimCicloLetivo))]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(AlunoController.LimparDataInicio), "Aluno", true)]
        public DateTime DataInicio { get; set; } = DateTime.Now;

        [SMCRequired]
        [SMCSelect(nameof(SituacoesMatricula), AutoSelectSingleItem = true)]
        [SMCSize(Framework.SMCSize.Grid15_24)]
        public long SeqSituacaoMatricula { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, AllowedFileExtensions = new string[] { "doc", "docx", "xls", "xlsx", "jpg", "jpeg", "png", "pdf", "rar", "zip", "ps" })]
        [SMCConditionalRequired(nameof(TokenSituacaoMatricula), SMCConditionalOperation.Equals, TOKENS_SITUACAO_MATRICULA.CANCELADO_MOTIVO_FALECIMENTO)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCMultiline]
        public string Observacao { get; set; }

        [SMCConditionalDisplay(nameof(TokenSituacaoMatricula), SMCConditionalOperation.Equals, TOKENS_SITUACAO_MATRICULA.CANCELADO_MOTIVO_FALECIMENTO)]
        [SMCCssClass("smc-sga-mensagem-alerta smc-sga-mensagem")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get => UIResource.MSG_MensagemDataFalecimento; }
    }
}