using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration(ActionStep = nameof(EventoLetivoController.CarregarDados))]
    public class EventoLetivoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region Data Sources

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(ApenasAtivos), nameof(SeqTipoAgenda) })]
        [SMCDataSource()]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> ListaNiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect))]
        public List<SMCDatasourceItem> ListaLocalidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService), nameof(IHierarquiaEntidadeService.BuscarEntidadesHierarquiaSelect))]
        public List<SMCDatasourceItem> ListaEntidadesResponsaveis { get; set; }

        #endregion Data Sources

        #region Propriedades Auxiliares

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        [SMCStep(0)]
        public virtual long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCStep(0)]
        public bool ApenasAtivos { get { return true; } }

        [SMCHidden]
        [SMCStep(0)]
        public bool EventoLetivo { get { return true; } }

        [SMCHidden]
        [SMCStep(1)]
        public bool ExibirNivelEnsino { get; set; }

        [SMCHidden]
        [SMCStep(1)]
        public bool ExibirLocalidade { get; set; }

        [SMCHidden]
        [SMCStep(1)]
        public bool ExibirEntidadeResponsavel { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCStep(0)]
        public long SeqEventoLetivo { get { return this.Seq; } }

        #endregion Propriedades Auxiliares

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCReadOnly]
        [SMCHidden]
        [SMCStep(0)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCStep(0)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(nameof(TiposAgenda), AutoSelectSingleItem = true)]
        [SMCRequired]
        [SMCStep(0)]
        public long SeqTipoAgenda { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCSelect(nameof(TiposEvento), AutoSelectSingleItem = true)]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(EventoLetivoController.BuscarTiposEventosAGDSelect), "EventoLetivo", true, new string[] { nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino), nameof(SeqCicloLetivo) })]
        [SMCStep(0)]
        public long SeqTipoEvento { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCStep(1)]
        public string Descricao { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCRequired]
        [SMCMultiline]
        [SMCStep(1)]
        public string Observacao { get; set; }

        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCStep(1)]
        public DateTime DataInicio { get; set; }

        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRequired]
        [SMCStep(1)]
        public DateTime DataFim { get; set; }

        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCStep(1)]
        public TipoAluno TipoAluno { get; set; }

        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCConditionalDisplay(nameof(ExibirNivelEnsino), SMCConditionalOperation.Equals, true)]
        [SMCStep(1)]
        public SMCMasterDetailList<EventoLetivoNivelEnsinoViewModel> NiveisEnsino { get; set; }

        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCConditionalDisplay(nameof(ExibirLocalidade), SMCConditionalOperation.Equals, true)]
        [SMCStep(1)]
        public SMCMasterDetailList<EventoLetivoLocalidadeViewModel> Localidades { get; set; }

        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Tabular)]
        [SMCConditionalDisplay(nameof(ExibirEntidadeResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCStep(1)]
        public SMCMasterDetailList<EventoLetivoEntidadeResponsavelViewModel> EntidadesResponsaveis { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Wizard()
                    .Tokens(tokenEdit: UC_CAM_002_03_02.MANTER_EVENTO_LETIVO,
                           tokenInsert: UC_CAM_002_03_02.MANTER_EVENTO_LETIVO,
                           tokenRemove: UC_CAM_002_03_02.MANTER_EVENTO_LETIVO,
                           tokenList: UC_CAM_002_03_01.PESQUISAR_EVENTO_LETIVO)
                   .Grid(allowSelect: true)
                   .DisableInitialListing(true)
                   .Service<IEventoLetivoService>(index: nameof(IEventoLetivoService.BuscarEventosLetivos),
                                                  save: nameof(IEventoLetivoService.SalvarEventoLetivo));
        }

        #endregion [ Configuração ]
    }
}