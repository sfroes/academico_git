using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaCicloLetivoViewModel : ISMCMappable
    {
        public long? SeqOfertaCurso { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public List<TurmaConfiguracaoCicloLetivoViewModel> TurmasConfiguracao { get; set; }
    }
}