using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioListaData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        [SMCMapProperty("PessoaAtuacao.TipoAtuacao")]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCMapProperty("PessoaAtuacao.Pessoa.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("PessoaAtuacao.Pessoa.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        public List<PessoaAtuacaoBloqueioDetalheData> Bloqueios { get; set; }
    }
}