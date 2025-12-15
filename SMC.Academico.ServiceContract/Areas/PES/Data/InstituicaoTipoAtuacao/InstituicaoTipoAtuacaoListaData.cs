using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class InstituicaoTipoAtuacaoListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public short QuantidadeMinimaFiliacaoObrigatoria { get; set; }
              
        public short QuantidadeMaximaFiliacao { get; set; }
    }
}
