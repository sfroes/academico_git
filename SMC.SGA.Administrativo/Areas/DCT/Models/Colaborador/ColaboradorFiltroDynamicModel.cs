using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroVinculo", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroAtividade", Size = SMCSize.Grid12_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroInstituicao", Size = SMCSize.Grid12_24)]
    public class ColaboradorFiltroDynamicModel : SMCDynamicFilterViewModel, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesVinculoColaboradorSelect), values: new[] { nameof(IgnorarFiltros) })]
        public List<SMCDatasourceItem> EntidadesColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelTipoAtividadeColaboradorService), nameof(IInstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect))]
        public List<SMCDatasourceItem> TiposAtividadeColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoVinculoColaboradorService), nameof(ITipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect), values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCDatasourceItem> TiposVinculoColaborador { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        public TipoAtuacao TipoAtuacao { get; set; } = TipoAtuacao.Colaborador;

        [SMCFilter(true, true)]
        [SMCMapProperty("Nome")]
        [SMCSize(SMCSize.Grid15_24)]
        public string NomeFiltro { get; set; }

        [SMCCpf]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public string Cpf { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public string NumeroPassaporte { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid3_24)]
        public string Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCSelect(nameof(EntidadesColaborador))]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public long? SeqEntidadeVinculo { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorController.BuscarTiposVinculoEntidadeColaborador), "Colaborador", true)]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroVinculo")]
        [SMCSelect(nameof(TiposVinculoColaborador))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqTipoVinculoColaborador { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("FiltroVinculo")]
        public DateTime? DataInicio { get; set; }

        [SMCFilter(true, true)]
        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCGroupedProperty("FiltroVinculo")]
        public DateTime? DataFim { get; set; }

        #region [ AdvancedFilters ]

        [CursoOfertaLocalidadeLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(IgnorarFiltros))]
        [SMCFilter(true, true)]
        // [SMCFilter(save: true, displayDynamic: true, AdvancedFilter = true)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        [SMCGroupedProperty("FiltroAtividade")]
        public CursoOfertaLocalidadeLookupViewModel SeqCursoOfertaLocalidade { get; set; }

        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(ColaboradorController.BuscarTiposAtividadeColaboradorSelect), "Colaborador", true)]
        [SMCFilter(true, true)]
        // [SMCFilter(save: true, displayDynamic: true, AdvancedFilter = true)]
        [SMCGroupedProperty("FiltroAtividade")]
        [SMCSelect(nameof(TiposAtividadeColaborador))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public TipoAtividadeColaborador? TipoAtividade { get; set; }

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada => true;

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [InstituicaoExternaLookup]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        [SMCDependency(nameof(SeqInstituicaoEnsino))]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("FiltroInstituicao")]
        //[SMCFilter(save: true, displayDynamic: true, AdvancedFilter = true)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid16_24)]
        [SMCStep(4, 2)]
        public InstituicaoExternaViewModel SeqInstituicaoExterna { get; set; }

        //FIX: Remover método do controller ao corrigir o dependency
        [SMCDependency(nameof(SeqInstituicaoExterna), nameof(ColaboradorController.BuscarSituacoesColaborador), "Colaborador", true)]
        // [SMCFilter(save: true, displayDynamic: true, AdvancedFilter = true)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCGroupedProperty("FiltroInstituicao")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }

        #endregion [ AdvancedFilters ]

        /// <summary>
        /// Desativa todos os filtros exceto o filtro de instituição
        /// </summary>
        [SMCHidden]
        public bool IgnorarFiltros => true;

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorController.BuscarEntidadesFilhas), "Colaborador", true)]
        [SMCHidden]
        [SMCSelect]
        [SMCMultiSelect]
        public List<long> SeqsEntidadesResponsaveis { get; set; }
    }
}