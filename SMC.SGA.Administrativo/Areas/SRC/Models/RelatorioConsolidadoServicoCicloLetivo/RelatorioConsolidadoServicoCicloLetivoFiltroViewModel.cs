using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RelatorioConsolidadoServicoCicloLetivoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

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

        /// <summary>
        /// LK_CAM_002 - Ciclo Letivo
        /// </summary>
        [SMCRequired]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        /// <summary>
        /// NV01 Listar somente os serviços de acordo com as regras: 
        /// RN_USG_001 - Filtro por Instituição de Ensino e RN_USG_004 - Filtro por Nível de Ensino, em ordem alfabética.
        /// </summary>
        [SMCRequired]
        [SMCSelect(nameof(Servicos))]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(RelatorioConsolidadoServicoCicloLetivoController.BuscarServicosPorCicloLetivoSelect), "RelatorioConsolidadoServicoCicloLetivo", "SRC", true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        public long SeqServico { get; set; }

        /// <summary>
        /// Seleção múltipla
        /// NV02 Listar somente as entidades do tipo de entidade "Grupo de Programa" e de acordo com as regras: RN_USG_001 -
        /// Filtro por Instituição de Ensino e RN_USG_005 - Filtro por Entidade Responsável, em ordem alfabética.
        /// </summary>
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        public List<long> SeqsEntidadeResponsavel { get; set; }

    }
}