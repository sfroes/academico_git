using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class DivisaoTurmaCicloLetivoViewModel : ISMCMappable
    {
        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public string DivisaoComponente { get; set; }       

        public List<ItemDivisaoComponenteViewModel> ItensDivisaoComponente { get; set; }

    }
}