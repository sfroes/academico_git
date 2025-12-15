using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using SMC.SGA.Administrativo.Areas.CUR.Views.Requisito.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCOrder(2)]
        [SMCSelect(SortBy = SMCSortBy.Value, IgnoredEnumItems = new object[] { TipoRequisito.Nenhum, TipoRequisito.PreRequisito, TipoRequisito.CoRequisito })]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCDependency(nameof(RequisitoDynamicModel.SeqComponenteCurricular), nameof(RequisitoController.BuscarTiposRequisito), "Requisito", false, includedProperties: new string[] { nameof(TipoRequisitoAuxiliar) })]
        [SMCRequired]
        public TipoRequisito TipoRequisito { get; set; }

        [SMCHidden]
        public TipoRequisito TipoRequisitoAuxiliar
        {
            get
            {
                return TipoRequisito;
            }
        }

        [SMCOrder(3)]
        [SMCSelect(SortBy = SMCSortBy.Description, IgnoredEnumItems = new object[] { TipoRequisitoItem.Nenhum, TipoRequisitoItem.ComponenteCurricular, TipoRequisitoItem.DivisaoMatriz, TipoRequisitoItem.OutrosRequisitos, TipoRequisitoItem.GrupoCurricular })]
        [SMCDependency(nameof(TipoRequisito), "BuscarTiposRequisitosItensPorTipoRequisitoSelec", "Requisito", true, includedProperties: new string[] { nameof(TipoRequisitoItemAuxiliar) })]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRequired]
        public TipoRequisitoItem TipoRequisitoItem { get; set; }

        [SMCHidden]
        public TipoRequisitoItem TipoRequisitoItemAuxiliar
        {
            get
            {
                return TipoRequisitoItem;
            }
        }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.DivisaoMatriz)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.DivisaoMatriz)]
        [SMCDependency(nameof(TipoRequisitoItem), "BuscarPreOuCoRequisitoItem", "Requisito", true, new string[] { "TipoRequisito", "SeqMatrizCurricular", "SeqDivisaoCurricularItem", "SeqComponenteCurricular", "SeqDivisaoCurricularItemDetalheAuxiliar" })]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCMapProperty("SeqDivisaoCurricularItem")]
        [SMCSelect("DivisoesMatrizCurricular", NameDescriptionField = nameof(DescricaoRequisitoDivisao))]
        [SMCSize(SMCSize.Grid10_24)]
        public long? SeqDivisaoCurricularItemDetalhe { get; set; }

        [SMCHidden]
        public long? SeqDivisaoCurricularItemDetalheAuxiliar
        {
            get
            {
                return SeqDivisaoCurricularItemDetalhe;
            }
        }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.ComponenteCurricular)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.ComponenteCurricular)]
        [SMCDependency(nameof(TipoRequisitoItem), "BuscarPreOuCoRequisitoItem", "Requisito", true, new string[] { "TipoRequisito", "SeqMatrizCurricular", "SeqDivisaoCurricularItem", "SeqComponenteCurricular", "SeqComponenteCurricularDetalheAuxiliar" })]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(5)]
        [SMCMapProperty("SeqComponenteCurricular")]
        [SMCSelect("ComponentesCurricular", NameDescriptionField = nameof(DescricaoRequisitoComponente))]
        [SMCSize(SMCSize.Grid10_24)]
        public long? SeqComponenteCurricularDetalhe { get; set; }

        [SMCHidden]
        public long? SeqComponenteCurricularDetalheAuxiliar
        {
            get
            {
                return SeqComponenteCurricularDetalhe;
            }
        }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(6)]
        [SMCSelect(NameDescriptionField = nameof(DescricaoRequisitoOutro))]
        [SMCSize(SMCSize.Grid5_24)]
        public OutroRequisito? OutroRequisito { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.GrupoCurricular)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.GrupoCurricular)]
        [SMCDependency(nameof(RequisitoDynamicModel.SeqComponenteCurricular), nameof(RequisitoController.BuscarGruposCurriculares), "Requisito", false, includedProperties: new[] { nameof(RequisitoDynamicModel.SeqCurriculoCursoOferta) })]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(7)]
        [SMCSelect(nameof(RequisitoDynamicModel.GruposCurriculares))]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqGrupoCurricular { get; set; }

        [SMCConditionalDisplay(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCConditionalRequired(nameof(TipoRequisitoItem), SMCConditionalOperation.Equals, TipoRequisitoItem.OutrosRequisitos)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMask("9999")]
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid5_24)]
        public short? QuantidadeOutroRequisito { get; set; }

        [SMCHidden]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string DescricaoRequisitoDivisao { get; set; }

        [SMCHidden]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string DescricaoRequisitoComponente { get; set; }

        [SMCHidden]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public string DescricaoRequisitoOutro { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqGrupoCurricular), nameof(RequisitoController.BuscaGrupoCurricularDescricaoFormatada), "Requisito", true)]
        public string DescricaoRequisitoGrupoCurricular { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid10_24)]
        public string DescricaoRequisitoItem
        {
            get
            {
                if (SeqDivisaoCurricularItemDetalhe.HasValue)
                    return DescricaoRequisitoDivisao;

                if (SeqComponenteCurricularDetalhe.HasValue)
                    return DescricaoRequisitoComponente;

                if (OutroRequisito.HasValue && OutroRequisito.Value != Academico.Common.Areas.CUR.Enums.OutroRequisito.Nenhum)
                {
                    if (OutroRequisito == Academico.Common.Areas.CUR.Enums.OutroRequisito.CargaHoraria)
                        return $"{DescricaoRequisitoOutro} - {QuantidadeOutroRequisito} {(QuantidadeOutroRequisito == 1 ? UIResource.Label_Hora : UIResource.Label_Horas)}";
                    return $"{DescricaoRequisitoOutro} - {QuantidadeOutroRequisito}";
                }

                if (SeqGrupoCurricular.HasValue)
                    return DescricaoRequisitoGrupoCurricular;

                return string.Empty;
            }
        }
    }
}