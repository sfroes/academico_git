using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class MatrizCurricularConfiguracaoComponenteFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqMatrizCurricular { get; set; }

        public long? SeqDivisaoMatrizCurricular { get; set; }
    }
}
