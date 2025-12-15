using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class DivisaoTurmaCicloLetivoData : ISMCMappable
    {
        public long? SeqDivisaoTurma { get; set; }

        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqDivisaoComponente { get; set; }

        public string DivisaoComponente { get; set; }       

        public List<ItemDivisaoComponenteData> ItensDivisaoComponente { get; set; }
    }
}