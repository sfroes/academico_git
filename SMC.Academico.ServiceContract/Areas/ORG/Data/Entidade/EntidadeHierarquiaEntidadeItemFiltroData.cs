using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeHierarquiaEntidadeItemFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public long SeqTipoEntidade { get; set; }
        
        public long SeqTipoHierarquiaEntidadeItem { get; set; }

        public bool PossuiTipoVisao { get; set; }
    }
}