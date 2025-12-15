using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoTipoEventoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        #endregion [ DataSources ]

        #region [ Propriedades Auxiliares ]

        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }

        [SMCHidden]
        public bool EventoLetivo { get { return true; } }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }

        #endregion [ Propriedades Auxiliares ]

        [SMCFilter]
        [SMCSelect("TiposAgenda")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoAgenda { get; set; }

        [SMCFilter]
        [SMCSelect("TiposEvento")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(CicloLetivoTipoEventoController.BuscarTiposEventosAGDSelect), "CicloLetivoTipoEvento", true, new string[] { nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino) })]
        public long? SeqTipoEventoAgd { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect]
        public TipoParametroEvento? TipoParametroEvento { get; set; }
    }
}