using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class TermoIntercambioLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource(SMCStorageType.None)]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCGroupedProperty("LookupParceriaFiltro")]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid18_24)]
        public string DescricaoParceria { get; set; }

        [SMCGroupedProperty("LookupParceriaFiltro")]
        [SMCOrder(2)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid6_24)]
        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        [SMCDescription]
        [SMCGroupedProperty("LookupTermoIntercambioFiltro")]
        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCGroupedProperty("LookupTermoIntercambioFiltro")]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposTermosIntercambios), SortBy = SMCSortBy.Description)]
        public long? SeqTipoTermoIntercambio { get; set; }

        [SMCHidden]
        public bool ListarSomenteInstituicoesEnsino => true;

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada => false;

        [InstituicaoExternaLookup]
        [SMCDependency(nameof(ListarSomenteInstituicoesEnsino))]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCGroupedProperty("LookupTermoIntercambioFiltro")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        public InstituicaoExternaLookupViewModel SeqInstituicaoExterna { get; set; }

        [SMCHidden]
        public string Cpf { get; set; }

        [SMCHidden]
        public string NumeroPassaporte { get; set; }

        [SMCHidden]
        public TipoMobilidade? TipoMobilidade { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        public long? SeqTipoVinculoAluno { get; set; }
    }
}