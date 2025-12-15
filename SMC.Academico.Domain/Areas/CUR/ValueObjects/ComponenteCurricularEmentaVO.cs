using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularEmentaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string Ementa { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public ComponenteCurricularVO ComponenteCurricular { get; set; }
    }
}
