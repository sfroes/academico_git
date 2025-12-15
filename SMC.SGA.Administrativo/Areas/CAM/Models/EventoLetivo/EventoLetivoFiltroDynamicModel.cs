using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class EventoLetivoFiltroDynamicModel : EventoLetivoFiltroViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoAgendaService), nameof(ITipoAgendaService.BuscarTiposAgendaSelect), values: new string[] { nameof(EventoLetivo), nameof(SeqInstituicaoEnsino) })]
        [SMCDataSource]
        public List<SMCDatasourceItem> TiposAgenda { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEventoService), nameof(IInstituicaoTipoEventoService.BuscarTiposEventosAGDSelect), values: new string[] { nameof(ApenasAtivos), nameof(SeqTipoAgenda) })]
        [SMCDataSource()]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelModalidadeService), nameof(IInstituicaoNivelModalidadeService.BuscarModalidadesPorInstituicaoSelect))]
        [SMCDataSource()]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCHidden]
        public bool ApenasAtivos { get { return true; } }

        [SMCHidden]
        public bool EventoLetivo { get { return true; } }

        #endregion Propriedades Auxiliares

        [SMCHidden]
        public long? Seq { get; set; }

        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposAgenda), AutoSelectSingleItem = true)]
        public long? SeqTipoAgenda { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposEvento), AutoSelectSingleItem = true)]
        [SMCDependency(nameof(SeqTipoAgenda), nameof(EventoLetivoController.BuscarTiposEventosAGDSelect), "EventoLetivo", true, new string[] { nameof(ApenasAtivos), nameof(SeqInstituicaoEnsino) })]
        public long? SeqTipoEvento { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCMask("9999")]
        public int? AnoCiclo { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCMask("99")]
        public int? NumeroCiclo { get; set; }

        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(EventoLetivoController.BuscarModalidadesPorNivelEnsinoSelect), "EventoLetivo", true)]
        [SMCSelect(nameof(Modalidades), AutoSelectSingleItem = true)]
        public long? SeqModalidade { get; set; }

        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect(AutoSelectSingleItem = true)]
        public TipoAluno? TipoAluno { get; set; }

        [SMCIgnoreProp]
        public override long? SeqTurno { get => base.SeqTurno; set => base.SeqTurno = value; }
    }
}