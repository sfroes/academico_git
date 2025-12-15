using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ProgramaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public TipoPrograma? TipoPrograma { get; set; }
    }
}
