using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaFiltroViewModel : SMCViewModelBase
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public string Descricao { get; set; }
    }
}