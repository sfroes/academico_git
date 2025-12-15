using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.SGA.Administrativo.Models
{
    public class EnderecoViewModel : SMCViewModelBase, ISMCMappable
    {
        public long Seq { get; set; }

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

        public string CidadeUF { get { return $"{NomeCidade}/{SiglaUf}"; } }
    }
}