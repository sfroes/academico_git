using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CNC.Data
{
    public class GrupoRegistroFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Descricao { get; set; }
    }
}