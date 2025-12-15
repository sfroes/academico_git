using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroRegistro { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicioValidade { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string DataFimValidade { get; set; } 

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ContratoCursoListarViewModel> Cursos { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ContratoNiveisEnsinoListarViewModel> NiveisEnsino { get; set; }
    }
}