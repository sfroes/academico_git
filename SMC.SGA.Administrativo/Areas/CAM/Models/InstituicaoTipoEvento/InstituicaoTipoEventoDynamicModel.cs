using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.SGA.Administrativo.Areas.CAM.Views.InstituicaoTipoEvento.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class InstituicaoTipoEventoDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(ApenasAtivos), nameof(SeqTipoAgenda) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCHidden]
        public bool EventoLetivo { get { return true; } }

        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #endregion Propriedades Auxiliares

        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCReadOnly]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCSelect("TiposAgenda")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCRequired]
        public long? SeqTipoAgenda { get; set; }

        [SMCOrder(2)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCSelect("TiposEvento")]
        [SMCRequired]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(InstituicaoTipoEventoController.BuscarTiposEventosAGDSelect), "InstituicaoTipoEvento", true, new string[] { nameof(ApenasAtivos) })]
        public long? SeqTipoEventoAgd { get; set; }

        [SMCOrder(3)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public AbrangenciaEvento? AbrangenciaEvento { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public bool? ApenasUmaParametrizacao { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public bool? DiaUtilEscolarDocente { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public bool? DiaUtilAdministrativo { get; set; }

        [SMCOrder(7)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        public bool? Ativo { get; set; }

        [SMCOrder(8)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMaxLength(255)]
        public string Token { get; set; }

        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular, DisplayAsGrid = true)]
        public SMCMasterDetailList<InstituicaoTipoEventoParametroViewModel> Parametros { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Large)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoTipoEventoListarDynamicModel)x).DescricaoInstituicaoEnsino,
                                ((InstituicaoTipoEventoListarDynamicModel)x).DescricaoTipoEvento))
                   .Service<IInstituicaoTipoEventoService>(index: nameof(IInstituicaoTipoEventoService.BuscarInstituicoesTiposEventos),
                                                           save: nameof(IInstituicaoTipoEventoService.SalvarInstituicaoTipoEvento))
                   .Tokens(tokenInsert: UC_CAM_003_02_02.MANTER_PARAMETROS_INSTITUICAO_TIPO_EVENTO,
                       tokenEdit: UC_CAM_003_02_02.MANTER_PARAMETROS_INSTITUICAO_TIPO_EVENTO,
                       tokenRemove: UC_CAM_003_02_02.MANTER_PARAMETROS_INSTITUICAO_TIPO_EVENTO,
                       tokenList: UC_CAM_003_02_01.PESQUISAR_PARAMETROS_INSTITUICAO_TIPO_EVENTO)
                   .Detail<InstituicaoTipoEventoListarDynamicModel>("_DetailList", allowSort: true); ;
        }
    }
}