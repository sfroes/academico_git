using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class LivroRegistroVO : ISMCMappable
    {
        public string LivroRegistro { get; set; }
        public string NumeroFolhaDoDiploma { get; set; }
        public string NumeroSequenciaDoDiploma { get; set; }
        public string NumeroRegistro { get; set; }
        public string ProcessoDoDiploma { get; set; }
        public DateTimeOffset DataColacaoGrau { get; set; }
        public DateTimeOffset DataExpedicaoDiploma { get; set; }
        public DateTimeOffset DataRegistroDiploma { get; set; }
        public ResponsavelRegistroVO ResponsavelRegistro { get; set; }
    }
}
