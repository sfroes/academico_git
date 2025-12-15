using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.APR.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class RelatorioBancasAgendadasFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region [ DataSources ]

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        //[SMCDataSource]
        //public List<SMCDatasourceItem> OrdenarPor { get; set; }

        #endregion [ DataSources ]

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid12_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        public List<long> SeqEntidadesResponsaveis { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid7_24)]
        public List<long> SeqNiveisEnsino { get; set; }

        [SMCDependency(nameof(SeqNiveisEnsino), nameof(RelatorioBancasAgendadasController.BuscarTipoEventoNivelEnsino), "RelatorioBancasAgendadas", true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid7_24)]
        [SMCSelect(nameof(TiposEvento), autoSelectSingleItem: true)]
        public long? SeqTipoEvento { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFim { get; set; }

        [SMCOrientation(SMCOrientation.Horizontal)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public SituacaoBanca? SituacaoBanca { get; set; }

        [SMCOrientation(SMCOrientation.Horizontal)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public OrdenacaoBancasAgendadasRelatorio Ordenacao { get; set; }

        [SMCDataSource]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(SituacaoBanca), 1, 3)] // Canceladas
        public bool? ExibirBancasComNota { get; set; }

        [ColaboradorLookup]
        [SMCUnique]
        [SMCSize(SMCSize.Grid8_24)]
        public ColaboradorLookupViewModel SeqColaborador { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(SeqColaborador), SMCConditionalOperation.Equals, "", PersistentValue = true)]
        public TipoMembroBanca TipoMembroBanca { get; set; }
    }
}