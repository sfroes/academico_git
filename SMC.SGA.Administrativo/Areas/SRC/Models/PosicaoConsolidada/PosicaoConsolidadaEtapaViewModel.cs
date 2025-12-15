using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class PosicaoConsolidadaEtapaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Etapa { get; set; }

        public int NaoIniciada { get; set; }

        public int EmAndamento { get; set; }

        public int FinalizadaComSucesso { get; set; }

        public int FinalizadaSemSucesso { get; set; }

        public int Cancelada { get; set; }

        public int Total { get; set; }
    }
}