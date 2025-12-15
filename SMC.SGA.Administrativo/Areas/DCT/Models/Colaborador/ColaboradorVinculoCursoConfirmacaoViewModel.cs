using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoCursoConfirmacaoViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid12_24)]
        public string NomeCursoOfertaLocalidade { get; set; }

        //[SMCSize(SMCSize.Grid12_24)]
        //public string NomeLocalidade { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public List<string> Atividades { get; set; }
    }
}