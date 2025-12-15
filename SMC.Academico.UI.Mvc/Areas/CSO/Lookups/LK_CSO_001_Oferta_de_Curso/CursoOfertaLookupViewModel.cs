using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoOfertaLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string DescricaoInstituicaoNivelEnsino { get; set; }

        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string DescricaoCurso { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-12 smc-size-xs-12 smc-size-sm-12 smc-size-lg-12")]
        [SMCSortable(true)]
        public string DescricaoOfertaCurso { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public bool Ativo { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecifica { get; set; }
    }
}