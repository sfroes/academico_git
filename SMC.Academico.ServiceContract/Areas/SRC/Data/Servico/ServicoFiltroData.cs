using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ServicoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqTipoServico { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public string Descricao { get; set; }
    }
}