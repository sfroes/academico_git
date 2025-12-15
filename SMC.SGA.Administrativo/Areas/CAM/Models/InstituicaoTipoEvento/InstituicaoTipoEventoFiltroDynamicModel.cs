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
    public class InstituicaoTipoEventoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(ApenasAtivos), nameof(SeqTipoAgenda) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        #endregion DataSources

        [SMCOrder(0)]
        [SMCFilter(true, true)]
        [SMCSelect("TiposAgenda")]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoAgenda { get; set; }

        [SMCOrder(1)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect("TiposEvento")]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(InstituicaoTipoEventoController.BuscarTiposEventosAGDSelect), "InstituicaoTipoEvento", true, new string[] { nameof(ApenasAtivos) })]
        public long? SeqTipoEventoAgd { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(2)]
        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public AbrangenciaEvento? AbrangenciaEvento { get; set; }

        [SMCHidden]
        public bool? EventoLetivo { get { return true; } }

        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }
    }
}