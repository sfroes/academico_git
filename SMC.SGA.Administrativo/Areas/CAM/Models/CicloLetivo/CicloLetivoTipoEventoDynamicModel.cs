using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration(ActionStep = nameof(CicloLetivoTipoEventoController.CarregarDados))]
    [SMCStepConfiguration(Partial = "_StepConfirmacao")]
    public class CicloLetivoTipoEventoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(SeqTipoAgenda), nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(INivelEnsinoService), nameof(INivelEnsinoService.BuscarNiveisEnsinoPorCicloLetivoSelect), values: new string[] { nameof(SeqCicloLetivo) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> ListaNiveisEnsino { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarParametrosInstituicaoTipoEventoSelect), values: new string[] { nameof(SeqTipoAgenda), nameof(SeqTipoEventoAgd) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> ListaParametros { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        [SMCStep(0)]
        public override long Seq { get; set; }

        [SMCStep(0)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        public long SeqCicloLetivo { get; set; }

        [SMCStep(0)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string DescricaoCicloLetivo { get; set; }

        [SMCStep(0)]
        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCStep(0)]
        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }

        [SMCStep(0)]
        [SMCHidden]
        public bool EventoLetivo { get { return true; } }

        [SMCStep(1)]
        [SMCHidden]
        public bool? ExibirNivelEnsino { get; set; }

        [SMCStep(1)]
        [SMCHidden]
        public bool? ExibirParametro { get; set; }

        [SMCStep(1)]
        [SMCHidden]
        public bool? ExibirMensagem { get; set; }

        [SMCStep(0)]
        [SMCRequired]
        [SMCSelect("TiposAgenda")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqTipoAgenda { get; set; }

        [SMCStep(0)]
        [SMCRequired]
        [SMCSelect("TiposEvento")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(CicloLetivoTipoEventoController.BuscarTiposEventosAGDSelect), "CicloLetivoTipoEvento", true, new string[] { nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino) })]
        public long? SeqTipoEventoAgd { get; set; }

        [SMCStep(1)]
        [SMCHideLabel]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirMensagem), SMCConditionalOperation.Equals, true)]
        public string Mensagem { get; set; }

        [SMCStep(1)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirNivelEnsino), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<CicloLetivoTipoEventoNivelEnsinoViewModel> NiveisEnsino { get; set; }

        [SMCStep(1)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibirParametro), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<CicloLetivoTipoEventoParametroViewModel> Parametros { get; set; }

        [SMCIgnoreProp]
        public int Step { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard()
                   .HeaderIndex("CabecalhoCicloLetivo")
                   .Header("CabecalhoCicloLetivo")
                   .Tokens(tokenList: UC_CAM_002_01_03.PESQUISAR_PARAMETRIZACAO_TIPO_EVENTO_LETIVO,
                           tokenInsert: UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO,
                           tokenEdit: UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO,
                           tokenRemove: UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)
                   .ButtonBackIndex("Index", "CicloLetivo")
                   .Service<ICicloLetivoTipoEventoService>(index: nameof(ICicloLetivoTipoEventoService.BuscarCiclosLetivosTiposEventos),
                                                           save: nameof(ICicloLetivoTipoEventoService.SalvarCicloLetivoTipoEvento),
                                                           edit: nameof(ICicloLetivoTipoEventoService.BuscarCicloLetivoTipoEvento));
        }
    }
}