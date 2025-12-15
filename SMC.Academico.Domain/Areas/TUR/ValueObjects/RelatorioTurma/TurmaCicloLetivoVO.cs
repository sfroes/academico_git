using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.ValueObjects
{
    public class TurmaCicloLetivoVO : ISMCMappable
    {
        public long? SeqTurma { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public List<TurmaConfiguracaoCicloLetivoVO> TurmasConfiguracao { get; set; }
    }
}