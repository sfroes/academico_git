using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class RelatorioFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> TiposRelatorio { get; set; }

        #endregion [ DataSources ]

        #region Parametros dos Relatórios


        #endregion Parametros dos Relatórios

        [SMCSelect(nameof(TiposRelatorio), AutoSelectSingleItem = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoRelatorio TipoRelatorio { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        public DateTime? DataInicioReferencia { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCMinDate(nameof(DataInicioReferencia))]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        public DateTime? DataFimReferencia { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis), ForceMultiSelect = true, UseCustomSelect = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [ColaboradorLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.LogAtualizacaoColaborador)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }
                
        [SMCHidden]
        public List<long> SelectedValues { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCFilter(true, true)]
        public long? SeqEntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqEntidadeResponsavel), nameof(RelatoriosColaboradorController.BuscarPosDoutorandosSelect), "RelatoriosColaborador", true)]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCSelect(AutoSelectSingleItem =true)]
        [SMCFilter(true, true)]
        public long? SeqColaboradorPosDoutorando  { get; set; }

        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
        [SMCDependency(nameof(SeqColaboradorPosDoutorando), nameof(RelatoriosColaboradorController.BuscarVinculosColaboradorSelect), "RelatoriosColaborador", true, includedProperties: new string[] {nameof(SeqEntidadeResponsavel) })]
        [SMCConditionalDisplay(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCConditionalRequired(nameof(TipoRelatorio), SMCConditionalOperation.Equals, TipoRelatorio.CertificadoPosDoutor)]
        [SMCSelect(AutoSelectSingleItem =true)]
        [SMCFilter(true, true)]
        public long? SeqColaboradorVinculo { get; set; }
    }
}