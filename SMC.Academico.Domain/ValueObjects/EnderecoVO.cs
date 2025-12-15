using SMC.Framework.Mapper;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.Domain.ValueObjects
{
    public class EnderecoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEndereco { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }
        
        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public int? CodigoCidade { get; set; }

        public string NomeCidade { get; set; }

        public string SiglaUf { get; set; }

        public int CodigoPais { get; set; }

        public string NomePais { get; set; }

        public TipoEndereco TipoEndereco { get; set; }

        public bool? Correspondencia { get; set; }
    }
}
