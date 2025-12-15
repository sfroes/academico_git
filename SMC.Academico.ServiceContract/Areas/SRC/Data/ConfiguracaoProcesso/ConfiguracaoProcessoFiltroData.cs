using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoProcessoFiltroData : SMCPagerFilterData, ISMCMappable
    {               
        public string DescricaoProcesso { get; set; }
      
        public bool ProcessoEncerrado { get; set; }      
      
        public long SeqProcesso { get; set; }
    }
}
