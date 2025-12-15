using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaRelatorioFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        public List<SMCDatasourceItem> TiposTurma { get; set; }

        #endregion [ DataSources ]

        [SMCRequired]
        [CicloLetivoLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

        #region [BI_CSO_001]

        /// <summary>
        /// Sequencial da entidade responsável com o nome esperado pelo lookup (para facilitar o depency)
        /// e mapeado para o nome adequando para os dtos
        /// </summary>
        [SMCFilter(true, true)]
        [SMCMapProperty("SeqEntidadesResponsaveis")]
        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid12_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        
        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid18_24)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        #endregion [BI_CSO_001]
        /// <summary>
        /// O campo “Tipo de turma” deve ser filtrado de acordo com o valor do 
        /// campo “Nível de ensino”, de acordo com parametrização de 
        /// instituição-nível-tipo de turma.
        /// </summary>
        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposTurma), autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqTipoTurma { get; set; }

    }
}