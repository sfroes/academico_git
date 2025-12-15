using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaEnderecoLookupData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqEndereco { get; set; }

        public TipoEndereco TipoEndereco { get; set; }

        public string DescPais { get; set; }

        public string NomeCidade { get; set; }

        public string SiglaUf { get; set; }

        public string DescCidadeEstado { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string EnderecoCompleto { get; set; }
    }
}