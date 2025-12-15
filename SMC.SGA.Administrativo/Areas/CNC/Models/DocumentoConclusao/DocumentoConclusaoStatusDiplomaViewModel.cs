using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoStatusDiplomaViewModel : SMCViewModelBase, ISMCMappable
    {
        #region DataSources

        public List<SMCDatasourceItem> MotivosCancelamento { get; set; }
        public List<SMCDatasourceItem> TiposCancelamento { get; set; }
        public List<SMCSelectListItem> ClassificacoesInvalidadeDocumento { get; set; }

        #endregion DataSources

        [SMCHidden]
        public bool ExigeTipoCancelamento { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        [SMCHidden]
        public string TokenAcao { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCSelect(nameof(MotivosCancelamento), AutoSelectSingleItem = true)]
        public MotivoInvalidadeDocumento MotivoInvalidadeDocumento { get; set; }

        [SMCSelect(nameof(TiposCancelamento))]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        public TipoInvalidade? TipoCancelamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(TipoCancelamento), nameof(DocumentoConclusaoController.HabilitaCampoClassificacaoInvalidadeDocumento), "DocumentoConclusao", false)]
        public bool HabilitaCampoClassificacaoInvalidadeDocumento { get; set; }

        [SMCSelect(nameof(ClassificacoesInvalidadeDocumento), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaCampoClassificacaoInvalidadeDocumento), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCConditionalRequired(nameof(HabilitaCampoClassificacaoInvalidadeDocumento), SMCConditionalOperation.Equals, true)]
        [SMCDependency(nameof(TipoCancelamento), nameof(DocumentoConclusaoController.BuscarDadosSelectClassificacaoInvalidadeDocumento), "DocumentoConclusao", true, Remote = true)]
        public long? SeqClassificacaoInvalidadeDocumento { get; set; }

        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        public string MensagemInformativa { get; set; }

        [SMCMultiline]
        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }
    }
}