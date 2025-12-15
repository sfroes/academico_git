using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class VagasOfertaCampanhaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCHidden]
        public List<ConfigurarVagasOfertaCampanhaListaViewModel> CampanhaOfertas { get; set; }
    }
}