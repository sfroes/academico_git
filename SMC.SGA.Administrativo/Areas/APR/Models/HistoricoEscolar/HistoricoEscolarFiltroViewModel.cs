using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class HistoricoEscolarFiltroViewModel : SMCViewModelBase, ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public bool ConsiderarMatriz { get; set; }
    }
}