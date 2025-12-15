using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class InstituicaoNivelTurnoVO : ISMCMappable
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
