using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class MotivoBloqueioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource("TipoBloqueio")]
        [SMCServiceReference(typeof(ITipoBloqueioService), nameof(ITipoBloqueioService.BuscarTiposBloqueiosSelect))]
        public List<SMCDatasourceItem> TiposBloqueio { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCGroupedProperty("DadosGerais")]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect(nameof(TiposBloqueio))]
        [SMCFilter(true, true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid5_24)]
        [SMCGroupedProperty("DadosGerais")]
        public long SeqTipoBloqueio { get; set; }

        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCInclude("TipoBloqueio")]
        [SMCMapProperty("TipoBloqueio.Descricao")]
        [SMCSortable(true, true, "TipoBloqueio.Descricao")]
        [SMCGroupedProperty("DadosGerais")]
        public string DescricaoTipoBloqueio { get; set; }

        [SMCOrder(3)]
        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        [SMCGroupedProperty("DadosGerais")]
        public string Descricao { get; set; }

        [SMCOrder(4)]
        [SMCSortable(true)]
        [SMCRequired]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("DadosGerais")]
        public string Token { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public string OrientacoesDesbloqueio { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public FormaBloqueio FormaBloqueio { get; set; }

        [SMCOrder(7)]
        [SMCMaxLength(255)]
        [SMCHidden(SMCViewMode.List)]
        [SMCConditionalReadonly(nameof(FormaBloqueio), SMCConditionalOperation.Equals, FormaBloqueio.Integracao)]
        [SMCConditionalRequired(nameof(FormaBloqueio), SMCConditionalOperation.NotEqual, FormaBloqueio.Integracao)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public string TokenPermissaoCadastro { get; set; }

        [SMCOrder(8)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public FormaBloqueio FormaDesbloqueio { get; set; }

        [SMCOrder(9)]
        [SMCDependency(nameof(FormaDesbloqueio), nameof(MotivoBloqueioController.ValidarObrigatoriedadeComprovante), "MotivoBloqueio", true)]
        [SMCConditionalReadonly(nameof(FormaDesbloqueio), SMCConditionalOperation.Equals, FormaBloqueio.Integracao, PersistentValue =true)]
        [SMCConditionalRequired(nameof(FormaDesbloqueio), SMCConditionalOperation.NotEqual, FormaBloqueio.Integracao)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public bool? ObrigatorioAnexoDesbloqueio { get; set; }

        [SMCOrder(10)]
        [SMCMaxLength(255)]
        [SMCHidden(SMCViewMode.List)]
        [SMCConditionalReadonly(nameof(FormaDesbloqueio), SMCConditionalOperation.Equals, FormaBloqueio.Integracao)]
        [SMCConditionalRequired(nameof(FormaDesbloqueio), SMCConditionalOperation.NotEqual, FormaBloqueio.Integracao)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public string TokenPermissaoDesbloqueio { get; set; }

        [SMCOrder(11)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public bool? PermiteDesbloqueioTemporario { get; set; }

        [SMCOrder(12)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public bool? PermiteItem { get; set; }

        [SMCOrder(13)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCGroupedProperty("ParametroMotivoBloqueio")]
        public bool? IntegracaoLegado { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_PES_004_02_01.MANTER_MOTIVO_BLOQUEIO,
                           tokenEdit: UC_PES_004_02_01.MANTER_MOTIVO_BLOQUEIO,
                           tokenRemove: UC_PES_004_02_01.MANTER_MOTIVO_BLOQUEIO,
                           tokenList: UC_PES_004_01_01.MANTER_TIPO_BLOQUEIO);
        }
    }
}