using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosDaParceriaSelect), values: new string[] { nameof(SeqParceriaIntercambio) })]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IParceriaIntercambioInstituicaoExternaService), nameof(IParceriaIntercambioInstituicaoExternaService.BuscarParceriaIntercambioInstituicoesExternasSelect), values: new string[] { nameof(SeqParceriaIntercambio) })]
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(INivelEnsinoService), nameof(INivelEnsinoService.BuscarNiveisEnsinoParceriaIntercambioSelect), values: new string[] { nameof(SeqParceriaIntercambio) })]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCFilter]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqParceriaIntercambio { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        [SMCSelect(nameof(NiveisEnsino), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        public long? SeqNivelEnsino { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSelect(nameof(TiposTermosIntercambios), SortBy = SMCSortBy.Description)]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(TermoIntercambioController.BuscarTermosPorNivelEnsinoSelect), "TermoIntercambio", false, includedProperties: new string[] { nameof(SeqParceriaIntercambio) })]
        public long? SeqParceriaIntercambioTipoTermo { get; set; }

        [SMCFilter]
        [SMCSelect]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        public bool? Ativo { get; set; }

        [SMCFilter]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCSelect("InstituicoesExternas")]
        public long? SeqParceriaIntercambioInstituicaoExterna { get; set; }

    }
}