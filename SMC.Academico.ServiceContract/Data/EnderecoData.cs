using SMC.Framework.Mapper;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.Common.Constants;

namespace SMC.Academico.ServiceContract.Data
{
    public class EnderecoData
    {
        public EnderecoData()
        {
            this.CodigoPais = (int)LocalidadesDefaultValues.SEQ_PAIS_BRASIL;
        }

        [SMCMapProperty("SeqEndereco")]
        public long Seq { get; set; }

        public string Cep { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        [SMCMapProperty("SeqCidade")]
        public int? CodigoCidade { get; set; }

        [SMCMapProperty("Cidade")]
        public string NomeCidade { get; set; }

        [SMCMapProperty("Estado")]
        public string SiglaUf { get; set; }

        [SMCMapForceFromTo]
        [SMCMapProperty("SeqPais")]
        public long CodigoPais { get; set; }

        [SMCMapProperty("Pais")]
        public string NomePais { get; set; }

        [SMCMapProperty("SeqTipoEndereco")]
        public TipoEndereco TipoEndereco { get; set; }

        public bool? Correspondencia { get; set; }
    }
}