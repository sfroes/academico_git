using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoProcessoMatriculaViewModel : SMCViewModelBase
    {
        #region DataSources
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCSelectListItem> Processos { get; set; }
        #endregion

        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ProcessoSeletivoDynamicModel.CiclosLetivos))]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public long? SeqCicloLetivo { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(Processos), AutoSelectSingleItem = true)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(ProcessoSeletivoController.BuscarProcessoMatricula), "ProcessoSeletivo",  true, new[] { nameof(ProcessoSeletivoDynamicModel.SeqCampanha) })]
        [SMCSize(Framework.SMCSize.Grid18_24)]
        public long? SeqProcesso { get; set; }
    }
}