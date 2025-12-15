using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularService), nameof(IDivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularTipoSelect),
           values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> DivisoesMatrizCurricular { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IComponenteCurricularService), nameof(IComponenteCurricularService.BuscarComponenteCurricularPorMatrizRequisitoSelect),
           values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> ComponentesCurricular { get; set; }

        #endregion [ DataSource ]

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCParameter(IsFilter = true)]
        [SMCSelect(nameof(DivisoesMatrizCurricular))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqDivisaoCurricularItem { get; set; }

        [SMCDependency(nameof(SeqDivisaoCurricularItem), nameof(RequisitoController.BuscarComponentesCurricularPorMatrizDivisao), "Requisito", false, new string[] { nameof(SeqMatrizCurricular) })]
        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCParameter(IsFilter = true)]
        [SMCSelect(nameof(ComponentesCurricular))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqComponenteCurricular { get; set; }

        [SMCGroupedProperty("Requisito")]
        [SMCOrder(3)]
        [SMCSelect(SortBy = SMCSortBy.Value)]
        [SMCSize(SMCSize.Grid5_24)]
        public TipoRequisitoAssociado Associado { get; set; }

        [SMCGroupedProperty("Requisito")]
        [SMCOrder(4)]
        [SMCSelect(SortBy = SMCSortBy.Value)]
        [SMCSize(SMCSize.Grid5_24)]
        public TipoRequisito TipoRequisito { get; set; }

        [SMCGroupedProperty("Requisito")]
        [SMCOrder(5)]
        [SMCSelect(SortBy = SMCSortBy.Value, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCDependency(nameof(TipoRequisito), nameof(RequisitoController.BuscarTiposRequisitosItensPorTipoRequisitoSelec), "Requisito", true)]
        public TipoRequisitoItem TipoRequisitoItem { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.DivisaoMatriz)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.DivisaoMatriz)]
        [SMCGroupedProperty("Requisito")]
        [SMCOrder(6)]
        [SMCSelect("DivisoesMatrizCurricular")]
        [SMCSize(SMCSize.Grid5_24)]
        public long? ItemSeqDivisaoCurricularItem { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.ComponenteCurricular)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.ComponenteCurricular)]
        [SMCGroupedProperty("Requisito")]
        [SMCOrder(7)]
        [SMCSelect("ComponentesCurricular")]
        [SMCSize(SMCSize.Grid5_24)]
        public long? ItemSeqComponenteCurricular { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCGroupedProperty("Requisito")]
        [SMCOrder(8)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24)]
        public OutroRequisito OutroRequisitoItem { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCGroupedProperty("Requisito")]
        [SMCMask("999")]
        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid4_24)]
        public short? QuantidadeOutroRequisito { get; set; }

        #region [ Configuração ]

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);
            if (viewMode == SMCViewMode.Filter)
            {
                Associado = TipoRequisitoAssociado.Associado;
            }
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }

        #endregion [ Configuração ]
    }
}