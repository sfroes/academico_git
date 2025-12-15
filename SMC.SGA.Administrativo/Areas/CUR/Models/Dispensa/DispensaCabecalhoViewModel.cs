using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public List<DispensaComponenteCurricularViewModel> GrupoOrigens { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        public List<DispensaComponenteCurricularViewModel> GrupoDispensados { get; set; }

        //FIX: Remover ao preparar o framework para imprimir listas no header
        public string GrupoOrigensDisplay => string.Join("", GrupoOrigens?.Select(s => $"<div class = \"smc-size-lg-24\"><p>{s.DescricaoCompleta}</p></div>"));

        //FIX: Remover ao preparar o framework para imprimir listas no header
        public string GrupoDispensadosDisplay => string.Join("", GrupoDispensados?.Select(s => $"<div class = \"smc-size-lg-24\"><p>{s.DescricaoCompleta}</p></div>"));
    }
}