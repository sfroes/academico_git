using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RelatoriosServicosFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCSelectListItem> TiposRelatorioServico { get; set; }

        /// <summary>
        /// NV01 Listar somente os serviços de acordo com as regras: 
        /// RN_USG_001 - Filtro por Instituição de Ensino e RN_USG_004 - Filtro por Nível de Ensino, em ordem alfabética.
        /// </summary>       
        public List<SMCDatasourceItem> Servicos { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV02 Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética.
        /// </summary>
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        #region Parametros dos Relatórios

        /// <summary>
        /// LK_CAM_002 - Ciclo Letivo
        /// </summary>
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo, true, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true, RuleName = "R2")]
        [SMCConditionalRequired(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo, true, RuleName = "R3")]
        [SMCConditionalRequired(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true, RuleName = "R4")]
        [SMCConditionalRule("(R1 || R2) && (R3 || R4)")]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        /// <summary>
        /// NV01 Listar somente os serviços de acordo com as regras: 
        /// RN_USG_001 - Filtro por Instituição de Ensino e RN_USG_004 - Filtro por Nível de Ensino, em ordem alfabética.
        /// </summary>
        [SMCSelect(nameof(Servicos))]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo, true, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true, RuleName = "R2")]
        [SMCConditionalRequired(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo, true, RuleName = "R3")]
        [SMCConditionalRequired(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true, RuleName = "R4")]
        [SMCConditionalRule("(R1 || R2) && (R3 || R4)")]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(RelatoriosServicosController.BuscarServicosPorCicloLetivoSelect), "RelatoriosServicos", "SRC", true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        public long? SeqServico { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV02 Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética.
        /// </summary>
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.PosicaoConsolidadaServicoCicloLetivo, true, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public List<long> SeqsEntidadeResponsavel { get; set; }

        [SMCSelect]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true)]
        [SMCConditionalRequired(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true)]
        [SMCDependency(nameof(SeqServico), nameof(RelatoriosServicosController.BuscarProcessosPorCicloLetivoServicoEntidadesResponsaveisSelect), "RelatoriosServicos", "", true, new string[] { nameof(SeqCicloLetivo), nameof(SeqsEntidadeResponsavel) })]
        [SMCDependency(nameof(SeqsEntidadeResponsavel), nameof(RelatoriosServicosController.BuscarProcessosPorCicloLetivoServicoEntidadesResponsaveisSelect), "RelatoriosServicos", "", false, new string[] { nameof(SeqCicloLetivo), nameof(SeqServico) })]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public List<long> SeqsProcessos { get; set; }

        [SMCSelect]
        [SMCConditionalDisplay(nameof(TipoRelatorioServico), SMCConditionalOperation.Equals, TipoRelatorioServico.SolicitacoesBloqueio, true)]
        [SMCDependency(nameof(SeqServico), nameof(RelatoriosServicosController.BuscarProcessoEtapasPorServicoSelect), "RelatoriosServicos", "", true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public long? SeqProcessoEtapa { get; set; }

        #endregion Parametros dos Relatórios

        [SMCRequired]
        [SMCSelect(nameof(TiposRelatorioServico))]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoRelatorioServico TipoRelatorioServico { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}