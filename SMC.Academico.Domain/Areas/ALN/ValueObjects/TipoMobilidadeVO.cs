using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TipoMobilidadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Titulo { get; set; }

        public string DatasInicioPeriodos { get; set; }
        public string DatasFimPeriodos { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string Instituicao { get; set; }

        public string OrientadorInstituicao { get; set; }

        public string PaisInstituicao { get; set; }
    }
}