using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoListaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        [SMCMapProperty("DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        [SMCMapProperty("Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public string Descricao { get; set; }
    }
}