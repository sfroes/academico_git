using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.APR.Models.ConsultaNotaFrequencia
{
    public class HistoricoViewModel : ISMCMappable
    {

        public bool PermitirCredito { get; set; }

        public SMCPagerModel<ComponeteCreditoViewModel> ComponentesConcluidos { get; set; }

        public SMCPagerModel<ComponeteCreditoViewModel> AproveitamentoCredito { get; set; }

        public SMCPagerModel<ComponeteCreditoViewModel> ComponentesSemApuracao { get; set; }

        public SMCPagerModel<ComponeteCreditoViewModel> ComponentesExame { get; set; }

        public bool PossuiDadosHistoricoEscolar { get; set; }
    }
}