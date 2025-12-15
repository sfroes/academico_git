using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesVinculoColaboradorSelect), values: new[] { nameof(IgnorarFiltros) })]
        public List<SMCDatasourceItem> EntidadesColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoColaboradorService), nameof(ITipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect))]
        public List<SMCDatasourceItem> TipoVinculoColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoAtividadeColaboradorService), nameof(IInstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect))]
        public List<SMCDatasourceItem> TiposAtividadeColaborador { get; set; }

        #endregion [ DataSources ]

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        [SMCHidden]
        public bool IgnorarFiltros => true;

        [SMCHidden]
        [SMCParameter]
        public long SeqColaborador { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesColaborador))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public long? SeqEntidadeVinculo { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarTiposVinculoColaboradorPorEntidadeSelect), "ColaboradorVinculo", "DCT", false)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TipoVinculoColaborador))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqTipoVinculoColaborador { get; set; }

        [SMCFilter(true, true)]
        [SMCConditionalRequired(nameof(DataFim), SMCConditionalOperation.NotEqual, "")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public DateTime? DataInicio { get; set; }

        [SMCFilter(true, true)]
        [SMCMinDate(nameof(DataInicio))]
        [SMCConditionalRequired(nameof(DataInicio), SMCConditionalOperation.NotEqual, "")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposAtividadeColaborador))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarEntidadesFilhas), "ColaboradorVinculo", true)]
        [SMCHidden]
        [SMCSelect]
        [SMCMultiSelect]
        public List<long> SeqsEntidadesResponsaveis { get; set; }
    }
}