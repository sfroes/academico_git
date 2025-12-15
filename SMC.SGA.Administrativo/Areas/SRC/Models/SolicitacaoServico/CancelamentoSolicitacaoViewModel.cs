using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.SRC.Views.SolicitacaoServico.App_LocalResources;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CancelamentoSolicitacaoViewModel : SMCViewModelBase
    {
        #region DataSources

        public List<SMCDatasourceItem> MotivosCancelamento { get; set; }
        public List<SMCDatasourceItem> TiposCancelamento { get; set; }
        public List<SMCSelectListItem> ClassificacoesInvalidadeDocumento { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCHidden]
        public bool ExigeMotivosCancelamento { get; set; }

        [SMCHidden]
        public bool ExigeTipoCancelamento { get; set; }

        #endregion Propriedades Auxiliares

        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCSelect(nameof(MotivosCancelamento), AutoSelectSingleItem = true)]
        [SMCConditionalDisplay(nameof(ExigeMotivosCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExigeMotivosCancelamento), SMCConditionalOperation.Equals, true)]
        public long? SeqMotivoCancelamento { get; set; }

        [SMCSelect(nameof(TiposCancelamento))]
        [SMCConditionalDisplay(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        public TipoInvalidade? TipoCancelamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(TipoCancelamento), nameof(SolicitacaoServicoController.HabilitaCampoClassificacaoInvalidadeDocumento), "SolicitacaoServico", false, Remote = true)]
        public bool HabilitaCampoClassificacaoInvalidadeDocumento { get; set; }

        [SMCSelect(nameof(ClassificacoesInvalidadeDocumento), autoSelectSingleItem: true)]
        [SMCConditionalDisplay(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ExigeTipoCancelamento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(HabilitaCampoClassificacaoInvalidadeDocumento), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCDependency(nameof(TipoCancelamento), nameof(SolicitacaoServicoController.BuscarDadosSelectClassificacaoInvalidadeDocumento), "SolicitacaoServico", true, Remote = true)]
        public long? SeqClassificacaoInvalidadeDocumento { get; set; }

        [SMCHideLabel]
        [SMCConditionalDisplay(nameof(TipoCancelamento), SMCConditionalOperation.Equals, TipoInvalidade.Permanente)]
        public string TextoInformativoPermanente => UIResource.MSG_TextoInformativoPermanente;

        [SMCHideLabel]
        [SMCConditionalDisplay(nameof(TipoCancelamento), SMCConditionalOperation.Equals, TipoInvalidade.Temporario)]
        public string TextoInformativoTemporario => UIResource.MSG_TextoInformativoTemporario; 

        [SMCRequired]
        [SMCMultiline]
        public string Observacao { get; set; }
    }
}