using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocadoImpedimentoIngressanteViewModel : SMCViewModelBase
    {
        public long SeqIngressante { get; set; }

        public string NomeIngressante { get; set; }

        public List<string> Impedimentos { get; set; }
    }
}