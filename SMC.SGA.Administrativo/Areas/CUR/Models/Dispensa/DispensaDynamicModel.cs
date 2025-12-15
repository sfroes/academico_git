using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "ComponentesOrigem", Size = SMCSize.Grid11_24, CssClass = "smc-size-sm-24")]
    [SMCGroupedPropertyConfiguration(GroupId = "ComponentesDispensado", Size = SMCSize.Grid11_24, CssClass = "smc-size-sm-24 col-md-offset-1 col-lg-offset-1 col-sm-offset-0")]
    [SMCGroupedPropertyConfiguration(GroupId = "ModoExibicao", Size = SMCSize.Grid11_24, CssClass = "col-lg-offset-1")]
    public class DispensaDynamicModel : SMCDynamicViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        /// <summary>
        /// Grupo de componentes substituitos
        /// </summary>
        [SMCHidden]
        public long SeqGrupoOrigem { get; set; }

        /// <summary>
        /// Grupo de componentes a serem dispensados
        /// </summary>
        [SMCHidden]
        public long SeqGrupoDispensado { get; set; }

        [SMCHidden]
        public bool TipoComponenteDispensa { get { return true; } }

        /// <summary>
        /// Componentes substitutos
        /// </summary>
        [ComponenteCurricularLookup(true)]
        [SMCDependency(nameof(TipoComponenteDispensa))]
        [SMCGroupedProperty("ComponentesDispensado")]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-remove-bordas smc-sga-grid-remove-header")]
        public List<ComponenteCurricularLookupViewModel> GrupoOrigens { get; set; }

        /// <summary>
        /// Componentes a serem dispensados
        /// </summary>
        [ComponenteCurricularLookup(true)]
        [SMCDependency(nameof(TipoComponenteDispensa))]
        [SMCGroupedProperty("ComponentesOrigem")]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCCssClass("smc-sga-remove-bordas smc-sga-grid-remove-header")]
        public List<ComponenteCurricularLookupViewModel> GrupoDispensados { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCCssClass("smc-clear")]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        public SMCMasterDetailList<DispensaHistoricoVigenciaViewModel> HistoricosVigencia { get; set; }

        [SMCGroupedProperty("ModoExibicao")]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Button("AssociacaoMatrizExcecao", "Editar", "DispensaMatriz",
                         UC_CUR_003_02_03.ASSOCIAR_MATRIZ_DISPENSA_COMPONENTE,
                         i => new { seq = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) }
                     )
                   .Detail<DispensaListarDynamicModel>("_DetailList")
                   .HeaderIndexList("CabecalhoLista")
                   .DisableInitialListing(true)
                   .Service<IDispensaService>(delete: nameof(IDispensaService.ExcluirDispensa),
                                                edit: nameof(IDispensaService.BuscarDispensa),
                                               index: nameof(IDispensaService.BuscarDispensas),
                                                save: nameof(IDispensaService.SalvarDispensa))
                   .Tokens(tokenList: UC_CUR_003_02_01.PESQUISAR_DISPENSA,
                           tokenInsert: UC_CUR_003_02_02.MANTER_DISPENSA,
                           tokenEdit: UC_CUR_003_02_02.MANTER_DISPENSA,
                           tokenRemove: UC_CUR_003_02_02.MANTER_DISPENSA);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
            if (viewMode == SMCViewMode.Insert)
            {
                ModoExibicaoHistoricoEscolar = ModoExibicaoHistoricoEscolar.AproveitamentoCreditos;
            }
        }

        #endregion [ Configurações ]
    }
}