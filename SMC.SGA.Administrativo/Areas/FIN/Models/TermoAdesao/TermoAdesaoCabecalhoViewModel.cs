using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TermoAdesaoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqContrato { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string NumeroRegistro { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string NomeContrato { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public List<TermoAdesaoListarNivelEnsinoViewModel> NiveisEnsino { get; set; } 

        [SMCSize(SMCSize.Grid10_24)] 
        public List<TermoAdesaoListarCursoViewModel> Cursos { get; set; }
         
        [SMCSize(SMCSize.Grid10_24)]
        public string DataInicioValidade { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string DataFimValidade { get; set; }
    }
}