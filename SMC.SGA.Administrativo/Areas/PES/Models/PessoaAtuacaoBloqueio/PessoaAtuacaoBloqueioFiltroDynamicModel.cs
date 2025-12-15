using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource("TipoBloqueio")]
        [SMCServiceReference(typeof(ITipoBloqueioService), nameof(ITipoBloqueioService.BuscarTiposBloqueiosSelect), null, new string[] { nameof(Ativo) })]
        public List<SMCDatasourceItem> TiposBloqueio { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("MotivoBloqueio")]
        public List<SMCDatasourceItem> MotivosBloqueio { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCHidden]
        public bool Ativo { get { return true; } }

        #endregion Propriedades Auxiliares

        [SMCOrder(0)]
        [SMCSelect("TiposBloqueio", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        public List<long> SeqTipoBloqueio { get; set; }

        [SMCOrder(1)]
        [SMCSelect("MotivosBloqueio")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCDependency(nameof(SeqTipoBloqueio), nameof(MotivoBloqueioController.BuscarMotivosBloqueioInstituicaoSelect), "MotivoBloqueio", true)]
        public List<long> SeqMotivoBloqueio { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCSelect]
        public SituacaoBloqueio? SituacaoBloqueio { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(SituacaoBloqueio), SMCConditionalOperation.NotEqual, SMC.Academico.Common.Areas.PES.Enums.SituacaoBloqueio.Desbloqueado)]
        public TipoDesbloqueio? TipoDesbloqueio { get; set; }

        [SMCOrder(4)]
        [PessoaLookup(ModalWindowSize = SMCModalWindowSize.Largest)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCFilter]
        public PessoaLookupViewModel SeqPessoa { get; set; }

        [SMCOrder(5)]
        [SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid6_24, Framework.SMCSize.Grid4_24)]
        public DateTime? DataBloqueioInicio { get; set; }

        [SMCOrder(6)]
        [SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid6_24, Framework.SMCSize.Grid4_24)]
        public DateTime? DataBloqueioFim { get; set; }
    }
}