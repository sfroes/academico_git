using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaEnderecoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqEndereco { get; set; }

        public EnderecoData Endereco { get; set; }
    }
}