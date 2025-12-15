using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class UnidadeCurricularEquivalenteVO : ISMCMappable
    {
        public List<string> CodigosUnidadesEquivalentes { get; set; }
    }
}
