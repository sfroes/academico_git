using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaEnderecoEletronicoLookupData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqEnderecoEletronico { get; set; }

        public bool BloquearEdicao { get; set; }

        [SMCMapProperty("EnderecoEletronico.TipoEnderecoEletronico")]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        [SMCMapProperty("EnderecoEletronico.Descricao")]
        public string Descricao { get; set; }
    }
}