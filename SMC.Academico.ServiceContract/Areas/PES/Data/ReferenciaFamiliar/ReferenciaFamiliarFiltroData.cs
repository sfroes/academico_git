using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class ReferenciaFamiliarFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public TipoParentesco? TipoParentesco { get; set; }

        public string NomeParente { get; set; }
    }
}