using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoEntidade { get; set; }

        [SMCHidden]
        public long SeqHierarquiaEntidadeItem { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCDescription]
        public string Titulo { get { return string.Format("{0:d4} - {1}", Seq, Nome); } }

        public TipoPrograma TipoPrograma { get; set; }

        public string DescricaoSituacaoAtual { get; set; }

        public List<ProgramaListarCursoViewModel> Cursos { get; set; }
    }
}