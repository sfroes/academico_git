using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class CurriculoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long SeqCurso { get; set; }

        public bool Ativo { get; set; }
    }
}
