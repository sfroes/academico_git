using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelTipoDivisaoComponenteViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNivelTipoComponenteCurricularDynamicModel.TiposDivisao), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCUnique]
        public long? SeqTipoDivisaoComponente { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoDivisaoComponente), nameof(InstituicaoNivelTipoComponenteCurricularController.BuscarTipoGestaoDivisaoComponente), "InstituicaoNivelTipoComponenteCurricular", "CUR", true, null)]
        public short? TipoGestaoDivisaoComponente { get; set; }

        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, SMC.Academico.Common.Areas.CUR.Enums.TipoGestaoDivisaoComponente.Trabalho)]
        [SMCConditionalRequired(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.Equals, SMC.Academico.Common.Areas.CUR.Enums.TipoGestaoDivisaoComponente.Trabalho)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(InstituicaoNivelTipoComponenteCurricularDynamicModel.TiposTrabalho), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid5_24)]
        public long? SeqTipoTrabalho { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCMapProperty("TipoDivisaoComponente.Descricao")]
        public string DescricaoTipoDivisao { get; set; }

        [SMCConditionalReadonly(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.NotEqual, SMC.Academico.Common.Areas.CUR.Enums.TipoGestaoDivisaoComponente.Trabalho)]
        [SMCConditionalRequired(nameof(TipoGestaoDivisaoComponente), SMCConditionalOperation.Equals, SMC.Academico.Common.Areas.CUR.Enums.TipoGestaoDivisaoComponente.Trabalho)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(InstituicaoNivelTipoComponenteCurricularDynamicModel.TiposEventoAGD), SortBy = SMCSortBy.Description, NameDescriptionField = nameof(DescricaoTipoEventoAgd))]
        [SMCSize(SMCSize.Grid5_24)]
        public long? SeqTipoEventoAgd { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRadioButtonList]
        [SMCRequired]
        public bool? PermiteCargaHorariaGrade { get; set; }

        [SMCHidden]
        public string DescricaoTipoEventoAgd { get; set; }
    }
}