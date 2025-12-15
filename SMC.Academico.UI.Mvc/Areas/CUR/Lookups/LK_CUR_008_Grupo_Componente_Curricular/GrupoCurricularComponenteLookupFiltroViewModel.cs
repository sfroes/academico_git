using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularComponenteLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }

        [SMCHidden]
        public long? SeqMatrizCurricular { get; set; }

        [SMCHidden]
        public bool FiltrarFormacoesEspecificasAluno { get; set; }

    }
}