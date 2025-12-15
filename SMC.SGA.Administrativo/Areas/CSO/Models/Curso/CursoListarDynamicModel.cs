using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoEntidade { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCDescription]
        public string Titulo { get { return string.Format("{0:d4} - {1}", Seq, Nome); } }

        public string DescricaoInstituicaoNivelEnsino { get; set; }

        [SMCHidden]
        public long SeqSituacaoAtual { get; set; }

        public string DescricaoSituacaoAtual { get; set; }

        public List<CursoOfertaDynamicModel> CursosOferta { get; set; }

        public List<CursoEntidadeListarViewModel> HierarquiasEntidades { get; set; }
    }
}