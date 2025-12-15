using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class InstituicaoTipoEntidadeVinculoColaboradorData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEntidade { get; set; }

        public long SeqTipoVinculoColaborador { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public long SeqTipoEntidade { get; set; }
    }
}
