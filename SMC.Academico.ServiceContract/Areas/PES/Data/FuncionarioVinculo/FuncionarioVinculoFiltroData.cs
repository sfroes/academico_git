using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class FuncionarioVinculoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqTipoFuncionario { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }
        public long? SeqFuncionario { get; set; }
    }
}