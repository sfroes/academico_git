using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosInstituicaoNivelSelect))]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCFilter]
        public long? Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public bool RetornarInstituicaoEnsinoLogada { get; set; } = false;

        [SMCHidden]
        [SMCParameter]
        public bool ListarSomenteInstituicoesEnsino { get; set; } = true;

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCFilter]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCSelect]
        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        [SMCFilter]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect(nameof(TiposTermosIntercambios), SortBy = SMCSortBy.Description)]
        public long? SeqTipoTermoIntercambio { get; set; }

        [SMCFilter]
        [SMCOrder(4)]
        [InstituicaoExternaLookup]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(ListarSomenteInstituicoesEnsino))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid11_24)]
        public ParceriaIntercambioInstituicaoExternaFiltroViewModel SeqInstituicaoExterna { get; set; }

        [SMCFilter]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCSelect]
        public bool? ProcessoNegociacao { get; set; }

        /// <summary>
        /// Calculado na busca dependendo da data de vigência dos itens da lista
        /// </summary>
        [SMCFilter]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid3_24)]
        [SMCSelect]
        public bool? Ativo { get; set; }
    }
}