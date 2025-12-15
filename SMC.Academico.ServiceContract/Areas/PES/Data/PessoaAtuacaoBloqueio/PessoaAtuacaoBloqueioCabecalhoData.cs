using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoBloqueioCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("PessoaAtuacao.DadosPessoais.NomeSocial")]
        public string NomeSocial { get; set; }

        [SMCMapProperty("PessoaAtuacao.TipoAtuacao")]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCMapProperty("MotivoBloqueio.Descricao")]
        public string DescricaoMotivoBloqueio { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("DataInclusao")]
        public DateTime DataBloqueio { get; set; }

        [SMCMapProperty("UsuarioInclusao")]
        public string ResponsavelBloqueio { get; set; }

        [SMCMapProperty("MotivoBloqueio.TipoBloqueio.Descricao")]
        public string DescricaoTipoBloqueio { get; set; }

        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        public string DescricaoReferenciaAtuacao { get; set; }
    }
}