using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularComponenteViewModel : SMCViewModelBase, ISMCSeq, ISMCLookupViewModel
    {
        public long Seq { get; set; }

        [SMCKey]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        [SMCDescription]
        public string DescricaoCompleta { get => $"{Codigo} - {Descricao}"; }
    }
}