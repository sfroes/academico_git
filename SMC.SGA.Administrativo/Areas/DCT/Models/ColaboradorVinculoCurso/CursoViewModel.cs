using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class CursoViewModel : SMCViewModelBase
    {
        #region [ Datasource ]

        [SMCHidden]
        [SMCIgnoreProp]
        [SMCDataSource(storageType: SMCStorageType.None)]
        public List<SMCSelectListItem> TiposAtividades { get; set; }

        #endregion [ Datasource ]

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqColaboradorVinculo { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ColaboradorVinculoCursoViewModel.CursoOfertasLocalidades))]
        [SMCSize(SMCSize.Grid11_24)]
        [SMCUnique]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCDependency(nameof(SeqCursoOfertaLocalidade), nameof(ColaboradorVinculoController.BuscarTiposAtividadeColaborador), "ColaboradorVinculo", true)]
        [SMCRequired]
        [SMCSelect(nameof(TiposAtividades), AutoSelectSingleItem = true, ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid11_24)]
        public List<TipoAtividadeColaborador> TipoAtividadeColaborador { get; set; }
    }
}