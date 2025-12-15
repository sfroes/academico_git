using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeListarDynamicModel : SMCDynamicViewModel
    {
        [SMCOrder(0)]
        [SMCSortable(true, false)]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        [SMCSortable(true, false)]
        public string Nome { get; set; }

        [SMCHidden]
        public long SeqTipoEntidade { get; set; }

        [SMCOrder(2)]
        [SMCMapProperty("TipoEntidade.Descricao")]
        [SMCInclude("TipoEntidade")]
        [SMCSortable(true, false, "TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCOrder(3)]
        public string Sigla { get; set; }

        [SMCOrder(4)]
        public string NomeReduzido { get; set; }

        [SMCOrder(5)]
        [SMCMapProperty("SituacaoAtual.Descricao")]
        [SMCInclude("HistoricoSituacoes.SituacaoEntidade")]
        public string DescricaoSituacaoAtual { get; set; }
    }
}