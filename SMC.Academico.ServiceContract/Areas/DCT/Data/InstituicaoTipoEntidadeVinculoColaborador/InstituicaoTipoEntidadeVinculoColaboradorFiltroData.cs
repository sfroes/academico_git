using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoTipoEntidadeVinculoColaboradorFiltroData : ISMCMappable
    {
        public long? SeqTipoVinculoColaborador { get; set; }

        public long? SeqEntidadeInstituicao { get; set; }

        public long? SeqTipoEntidade { get; set; }
    }
}
