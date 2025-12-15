using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioDetalheData : ISMCMappable
    {
        [SMCKeyModel]
        public long Seq { get; set; }

        [SMCMapProperty("PessoaAtuacao.Seq")]
        public long SeqPessoaAtuacao { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.Cpf")]
        public string Cpf { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.NumeroPassaporte")]
        public string NumeroPassaporte { get; set; }

        [SMCMapProperty("MotivoBloqueio.TipoBloqueio.Descricao")]
        public string DescricaoTipoBloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.Descricao")]
        public string DescricaoMotivoBloqueio { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCMapProperty("PessoaAtuacaoBloqueio.DataInclusao")]
        public DateTime DataBloqueio { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("PessoaAtuacao.TipoAtuacao")]
        public string DescricaoAtuacao { get; set; }

        [SMCMapProperty("MotivoBloqueio.FormaDesbloqueio")]
        public FormaBloqueio FormaDesbloqueioMotivo { get; set; }

        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.TokenPermissaoDesbloqueio")]
        public string TokenPermissaoDesbloqueio { get; set; }

        public bool HabilitaBotaoDesbloqueio { get; set; }
    }
}