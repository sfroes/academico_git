using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Lookups
{
    public class InstituicaoExternaLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string Sigla { get; set; }

        [SMCSortable(true)]
        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string Nome { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DescricaoPais { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string DescricaoCategoria { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public bool? Ativo { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCDescription]
        public string Descricao
        {
            get
            {
                if (Nome != null || Sigla != null)
                {
                    return $"{Nome} - {Sigla}";
                }
                return null;
            }
        }
    }
}