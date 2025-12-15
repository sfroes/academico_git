using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CabecalhoProcessoSeletivoOfertaViewModel : SMCViewModelBase
    {
        public long SeqCampanha { get; set; }

        public string Campanha { get; set; }

        public List<string> CiclosLetivos { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string TipoProcesso { get; set; }

        public List<string> NiveisEnsino { get; set; }

        public bool? ExibirNivelEnsino { get; set; }
    }
}