using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoProcessoFiltroVO : SMCPagerFilterData, ISMCMappable
    {     
        public string DescricaoProcesso { get; set; }
       
        public bool ProcessoEncerrado { get; set; }
               
        public long SeqProcesso { get; set; }
    }
}
