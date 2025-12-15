using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoFormacaoEspecificaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCurso { get; set; }
    }
}