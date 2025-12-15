using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoImpedimentosDeMatriculaViewModel : SMCViewModelBase
    {
        public string DescricaoCampanha { get; set; }

        public string DescricaoProcessoSeletivo { get; set; }

        public int NumeroConvocados { get; set; }

        public bool SemImpedimento { get; set; }

        public List<ConvocadoImpedimentoIngressanteViewModel> Impedimentos { get; set; }
    }
}