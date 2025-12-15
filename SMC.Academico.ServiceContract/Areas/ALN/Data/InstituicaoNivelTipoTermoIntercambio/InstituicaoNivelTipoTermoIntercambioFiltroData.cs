using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class InstituicaoNivelTipoTermoIntercambioFiltroData : ISMCMappable
    {
        public long? SeqTipoTermoIntercambio { get; set; }
        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }
    }
}