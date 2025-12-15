using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoApostilamentoViewModel : SMCViewModelBase
    {
        #region Datasource    

        public List<SMCDatasourceItem> TiposApostilamento { get; set; }

        public List<SMCDatasourceItem> FormacoesAluno { get; set; }

        #endregion

        [SMCReadOnly]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposApostilamento))]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long SeqTipoApostilamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoApostilamento), nameof(DocumentoConclusaoApostilamentoController.BuscarTokenTipoApostilamento), "DocumentoConclusaoApostilamento", true)]
        public string TokenTipoApostilamento { get; set; }

        [SMCSelect(nameof(FormacoesAluno), NameDescriptionField = nameof(SeqAlunoFormacaoDescription), UseCustomSelect = true)] 
        [SMCConditionalDisplay(nameof(TokenTipoApostilamento), SMCConditionalOperation.Equals, TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO)]
        [SMCConditionalRequired(nameof(TokenTipoApostilamento), SMCConditionalOperation.Equals, TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid15_24, SMCSize.Grid15_24)]
        public long? SeqAlunoFormacao { get; set; }

        public string SeqAlunoFormacaoDescription { get; set; }

        [SMCRequired]
        [SMCDescription]
        [SMCMultiline(Rows = 2)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCMultiline(Rows = 2)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string Justificativa { get; set; }

        [SMCHidden]
        public long? SeqArquivoAnexado { get; set; }

        [SMCFile(HideDescription = true, DisplayFilesInContextWindow = true, MaxFileSize = 26214400, ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", AreaDownload = "")]
        [SMCConditionalDisplay(nameof(TokenTipoApostilamento), SMCConditionalOperation.Equals, TOKEN_TIPO_APOSTILAMENTO.DADOS_PESSOAIS)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid4_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public DateTime? DataInclusao { get; set; }

        [SMCDisplay]
        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string UsuarioInclusao { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        public string Mensagem { get; set; }

        public string MensagemTipoFormacaoEspecifica { get; set; }

        public string MensagemTokenFormacao { get; set; }

        public string MensagemTokenDadosPessoais { get; set; }
    }
}