using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ExpressaoIntegralizacaoVO : ISMCMappable
    {
        public List<string> SomatorioCodigos { get; set; }
        public string Codigo { get; set; }
        public LimitesCargaHorariaVO LimitesCargaHoraria { get; set; }
    }
}
