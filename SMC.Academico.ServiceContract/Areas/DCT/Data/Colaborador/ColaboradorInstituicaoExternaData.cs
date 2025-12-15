using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorInstituicaoExternaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqColaborador { get; set; }

        public long SeqInstituicaoExterna { get; set; }

        public string Nome { get; set; }

        public string NomeSigla { get; set; }

        public bool Ativo { get; set; }
    }
}