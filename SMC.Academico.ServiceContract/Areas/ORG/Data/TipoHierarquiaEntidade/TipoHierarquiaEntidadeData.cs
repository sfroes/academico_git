using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoHierarquiaEntidadeData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoVisao TipoVisao { get; set; }
    }
}
