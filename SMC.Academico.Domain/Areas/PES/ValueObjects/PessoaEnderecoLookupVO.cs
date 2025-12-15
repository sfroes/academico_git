using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaEnderecoLookupVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqEndereco { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.TipoEndereco")]
        public TipoEndereco TipoEndereco { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.NomeCidade")]
        public string NomeCidade { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.SiglaUf")]
        public string SiglaUf { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.CodigoPais")]
        public int CodigoPais { get; set; }

        public string DescPais { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.Cep")]
        public string CEP { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.Logradouro")]
        public string Logradouro { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.Numero")]
        public string Numero { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.Complemento")]
        public string Complemento { get; set; }

        [SMCMapProperty("PessoaEndereco.Endereco.Bairro")]
        public string Bairro { get; set; }

        public bool? Correspondencia { get; set; }
    }
}