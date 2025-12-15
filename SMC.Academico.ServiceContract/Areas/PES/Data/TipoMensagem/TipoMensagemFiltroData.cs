using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class TipoMensagemFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public string Descricao { get; set; }
         
        public CategoriaMensagem CategoriaMensagem { get; set; }

        public TipoTag? TipoTag { get; set; }

        public bool? PermiteCadastroManual { get; set; }
    }
}
