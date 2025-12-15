using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using SMC.SGA.Administrativo.Areas.PES.Views.InstituicaoMotivoBloqueio.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class InstituicaoMotivoBloqueioDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoBloqueioService), nameof(ITipoBloqueioService.BuscarTiposBloqueiosSelect))]
        public List<SMCDatasourceItem> TiposBloqueio { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("MotivoBloqueio")]
        [SMCServiceReference(typeof(IMotivoBloqueioService), nameof(IMotivoBloqueioService.BuscarMotivosBloqueioSelect), null, new string[] { nameof(SeqTipoBloqueio) })]
        public List<SMCDatasourceItem> MotivosBloqueio { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCInclude("MotivoBloqueio.TipoBloqueio")]
        [SMCMapProperty("MotivoBloqueio.TipoBloqueio.Seq")]
        [SMCSelect("TiposBloqueio")]
        public long? SeqTipoBloqueio { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("MotivoBloqueio.TipoBloqueio")]
        [SMCMapProperty("MotivoBloqueio.TipoBloqueio.Descricao")]
        [SMCSortable(true, true, "MotivoBloqueio.TipoBloqueio.Descricao")]
        public string DescricaoTipoBloqueio { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid16_24)]
        [SMCSelect("MotivosBloqueio")]
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCDependency(nameof(SeqTipoBloqueio), nameof(MotivoBloqueioController.BuscarMotivosBloqueioSelect), "MotivoBloqueio", true)]
        public long? SeqMotivoBloqueio { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("MotivoBloqueio")]
        [SMCMapProperty("MotivoBloqueio.Descricao")]
        [SMCSortable(true, true, "MotivoBloqueio.Descricao")]
        public string DescricaoMotivoBloqueio { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Medium)
                   .Tokens(tokenInsert: UC_PES_003_02_01.MANTER_MOTIVO_BLOQUEIO_INSTITUICAO,
                           tokenEdit: UC_PES_003_02_01.MANTER_MOTIVO_BLOQUEIO_INSTITUICAO,
                           tokenRemove: UC_PES_003_02_01.MANTER_MOTIVO_BLOQUEIO_INSTITUICAO,
                           tokenList: UC_PES_003_02_01.MANTER_MOTIVO_BLOQUEIO_INSTITUICAO);
        }
    }
}