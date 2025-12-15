using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaTurmaCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
        public string DescricaoConfiguracao { get; set; }
        public string EntidadeResponsavel { get; set; }
        public string TipoAvaliacao { get; set; }
    }
}