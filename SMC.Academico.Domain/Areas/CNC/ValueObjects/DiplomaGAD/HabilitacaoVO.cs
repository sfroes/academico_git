using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class HabilitacaoVO : ISMCMappable
    {
        public string NomeHabilitacao { get; set; }
        public DateTimeOffset? DataHabilitacao { get; set; }
    }
}
