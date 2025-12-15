using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaTelefoneData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqPessoa")]
        public long SeqPessoaTelefone { get; set; }

        public long SeqTelefone { get; set; }

        public TelefoneData Telefone { get; set; }
    }
}