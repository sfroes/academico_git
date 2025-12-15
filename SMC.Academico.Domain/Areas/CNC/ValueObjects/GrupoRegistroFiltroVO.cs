using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class GrupoRegistroFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Descricao { get; set; }
    }
}
