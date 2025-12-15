using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoVinculoAlunoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqTipoOrientacao { get; set; }
    }
}