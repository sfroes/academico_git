using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using SMC.Localidades.Common.Areas.LOC.Enums;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoLookupViewModel : ISMCMappable, ISMCSeq
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqEndereco { get; set; }

        [SMCHidden]
        public long SeqPessoaEndereco { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public TipoEndereco TipoEndereco { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string DescPais { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string CEP { get; set; }

        [SMCHidden]
        public string Logradouro { get; set; }

        [SMCHidden]
        public string Numero { get; set; }

        [SMCHidden]
        public string Complemento { get; set; }

        [SMCHidden]
        public string Bairro { get; set; }

        /// <summary>
        /// Endereço com Logradouro, Número e Barrio separados por "vírgula espaço" e ignorando os campos vazios
        /// </summary>
        [SMCSize(SMCSize.Grid10_24)]
        public string EnderecoCompleto { get => SMCStringHelper.JoinIgnoringNullOrEmpty(", ", Logradouro, Numero, Complemento, Bairro); }

        [SMCHidden]
        public string NomeCidade { get; set; }

        [SMCHidden]
        public string SiglaUf { get; set; }

        /// <summary>
        /// Cidade e/ou estado concatenados com "/" como separador
        /// </summary>
        [SMCSize(SMCSize.Grid6_24)]
        public string DescCidadeEstado { get => SMCStringHelper.JoinIgnoringNullOrEmpty("/", NomeCidade, SiglaUf); }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(PessoaEnderecoLookupFiltroViewModel.EnderecosCorrespondencia))]
        [SMCSize(SMCSize.Grid6_24)]
        public EnderecoCorrespondencia EnderecoCorrespondencia { get; set; }
    }
}