using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConsultaDadosAlunoCiclosLetivosViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string AnoNumeroCicloLetivo { get; set; }

        public string DescricaoFormatada { get; set; }

        public List<ConsultaDadosAlunoCiclosLetivosSituacoesViewModel> CiclosLetivosSituacoes { get; set; }

        public List<ConsultaDadosAlunoCiclosLetivosPlanoEstudoViewModel> PlanosEstudos { get; set; }
    }
}