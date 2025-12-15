using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class ColaboradorVinculoCursoListarViewModel : SMCViewModelBase
    {

        public long Seq { get; set; }

        public string NomeCursoOfertaLocalidade { get; set; }

        public string NomeLocalidade { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public List<TipoAtividadeColaborador> TipoAtividadeColaborador { get; set; }

        public List<string> AtividadesLista => TipoAtividadeColaborador?.Select(s => s.SMCGetDescription()).OrderBy(o => o).ToList();
    }
}