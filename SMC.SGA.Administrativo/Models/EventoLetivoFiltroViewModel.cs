using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Models
{
    public abstract class EventoLetivoFiltroViewModel : SMCDynamicFilterViewModel, ISMCMappable
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService), nameof(IHierarquiaEntidadeService.BuscarEntidadesHierarquiaSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        #endregion Data Sources

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public virtual long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(5)]
        [SMCSelect(nameof(NiveisEnsino), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long? SeqNivelEnsino { get; set; }

        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(EntidadesResponsaveis), AutoSelectSingleItem = true)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(Localidades), AutoSelectSingleItem = true)]
        public long? SeqLocalidade { get; set; }

        [SMCOrder(8)]
        [CursoOfertaLookup]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCOrder(9)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCDependency(nameof(SeqCursoOferta), nameof(EventoLetivoController.BuscarTurnosPorCursoOferta), "EventoLetivo", true)]
        public virtual long? SeqTurno { get; set; }
    }
}