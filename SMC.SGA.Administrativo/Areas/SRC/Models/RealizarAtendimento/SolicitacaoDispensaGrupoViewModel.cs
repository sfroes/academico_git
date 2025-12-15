using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoDispensaGrupoViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> ItensOrigensInternas { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> ItensOrigensExternas { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> ItensDestinos { get; set; }

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoDispensa { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long? SeqDispensa { get; set; }

        [SMCDetail(SMCDetailType.Tabular, DisplayAsGrid = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaGrupoOrigemExternaViewModel> OrigensExternas { get; set; }

        [SMCDetail(SMCDetailType.Tabular, DisplayAsGrid = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaGrupoOrigemInternaViewModel> OrigensInternas { get; set; }

        [SMCDetail(SMCDetailType.Tabular, DisplayAsGrid = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SolicitacaoDispensaGrupoDestinoViewModel> Destinos { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRequired]
        public ModoExibicaoHistoricoEscolar ModoExibicaoHistoricoEscolar { get; set; }


    }
}