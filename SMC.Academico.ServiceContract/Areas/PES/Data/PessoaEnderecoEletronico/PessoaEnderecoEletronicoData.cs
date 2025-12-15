using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Runtime.Serialization;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    [DataContract(Namespace = NAMESPACES.MODEL, IsReference = true)]
    public class PessoaEnderecoEletronicoData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqPessoa")]
        public long SeqPessoaEnderecoEletronico { get; set; }

        public long SeqEnderecoEletronico { get; set; }

        [DataMember]
        public EnderecoEletronicoData EnderecoEletronico { get; set; }
    }
}