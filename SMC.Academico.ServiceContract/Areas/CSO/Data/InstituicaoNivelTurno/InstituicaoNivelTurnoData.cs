using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class InstituicaoNivelTurnoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public string DescricaoInstituicaoNivel { get; set; }

        public long SeqTurno { get; set; }

        public string DescricaoTurno { get; set; }

        public TimeSpan HoraLimiteInicio { get; set; }

        public TimeSpan HoraLimiteFim { get; set; }
    }
}
