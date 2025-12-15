using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Data
{
    public class TurmaCicloLetivoData : ISMCMappable
    {
        public long? SeqOfertaCurso { get; set; }

        public string OfertaCurso { get; set; }

        public string Localidade { get; set; }

        public string Turno { get; set; }

        public List<TurmaConfiguracaoCicloLetivoData> TurmasConfiguracao { get; set; }
    }
}