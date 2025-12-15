using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using SMC.SGA.Administrativo.Areas.CUR.Views.ConfiguracaoComponente.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteDivisaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        [SMCOrder(1)]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCOrder(2)]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa")]
        [SMCDisplay]
        [SMCConditionalDisplay(nameof(CargaHoraria), SMCConditionalOperation.GreaterThen, 0, RuleName ="R1")]
        [SMCConditionalDisplay(nameof(QuantidadeSemanasComponentePreenchida), false, RuleName = "R2")]
        [SMCConditionalDisplay(nameof(PermiteCargaHorariaGrade), SMCConditionalOperation.Equals, true, RuleName = "R3")]
        [SMCConditionalRule("R1 && R2 && R3")]
        [SMCHideLabel]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; } = UIResource.Texto_MensagemInformativa;

        [SMCOrder(3)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        [SMCSortable(true, true)]
        public short? Numero { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect(nameof(ConfiguracaoComponenteDynamicModel.TiposDivisao))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public long SeqTipoDivisaoComponente { get; set; }

        [SMCHidden]
        public short CargaHorariaComponente { get; set; }

        [SMCMask("9999")]
        [SMCMinValue(0)]
        [SMCOrder(5)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)] 
        public short? CargaHoraria { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCConditionalRequired(nameof(CargaHoraria), SMCConditionalOperation.GreaterThen, 0)]
        [SMCConditionalReadonly(nameof(QuantidadeSemanasComponentePreenchida), false)]
        [SMCConditionalDisplay(nameof(PermiteCargaHorariaGrade), SMCConditionalOperation.Equals, true)]
        [SMCMask("9999")]
        [SMCMinValue(0)]
        [SMCMaxValue(nameof(CargaHoraria))]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public short? CargaHorariaGrade { get; set; }

        [SMCDependency(nameof(SeqTipoDivisaoComponente), nameof(ConfiguracaoComponenteController.VerificarPermissaoCargaHorariaGrade), "ConfiguracaoComponente", true, includedProperties: new string[] { nameof(ConfiguracaoComponenteDynamicModel.SeqComponenteCurricular) })]
        [SMCHidden]
        public bool PermiteCargaHorariaGrade { get; set; }

        [SMCDependency(nameof(SeqTipoDivisaoComponente), nameof(ConfiguracaoComponenteController.VerificarQuantidadeSemanasComponentePreenchida), "ConfiguracaoComponente", true, includedProperties: new string[] { nameof(ConfiguracaoComponenteDynamicModel.SeqComponenteCurricular) })]
        [SMCHidden]
        public bool QuantidadeSemanasComponentePreenchida { get; set; }

        [SMCConditionalReadonly(nameof(PermiteGrupoSomenteLeitura), true)]
        [SMCConditionalRequired(nameof(PermiteGrupoSomenteLeitura), false)]
        [SMCOrder(7)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public bool PermiteGrupo { get; set; }

        [SMCDependency(nameof(SeqTipoDivisaoComponente), nameof(ConfiguracaoComponenteController.VerificarTipoDivisaoGestao), "ConfiguracaoComponente", true)]
        [SMCHidden]
        public bool PermiteGrupoSomenteLeitura { get; set; }

        [SMCConditionalDisplay(nameof(ExibirArtigo), true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(8)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public QualisCapes? QualisCapes { get; set; }

        [SMCConditionalDisplay(nameof(ExibirArtigo), true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(9)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoPublicacao? TipoPublicacao { get; set; }

        [SMCConditionalDisplay(nameof(ExibirArtigo), true)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(10)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoEventoPublicacao? TipoEventoPublicacao { get; set; }

        [SMCDependency(nameof(SeqTipoDivisaoComponente), nameof(ConfiguracaoComponenteController.VerificarTipoDivisaoArtigo), "ConfiguracaoComponente", true)]
        [SMCHidden]
        public bool ExibirArtigo { get; set; }


        //[SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(ConfiguracaoComponenteDynamicModel.ComponentesCurricularesOrganizacoes))]
        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqComponenteCurricularOrganizacao { get; set; }

    }
}