using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class DetalhesDivisaoTurmaViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string TurmaDescricaoFormatado { get; set; }
        [SMCCssClass("smc-size-lg-3 smc-size-md-3 smc-size-sm-3 smc-size-xs-3 ")]

        public string DescricaoFormatada { get; set; }

        public string GrupoFormatado { get; set; }

        public string DescricaoLocalidade { get; set; }
        [SMCCssClass("smc-size-lg-7 smc-size-md-7 smc-size-sm-7 smc-size-xs-7 ")]

        public string InformacoesAdicionais { get; set; }
        [SMCCssClass("smc-size-lg-7 smc-size-md-7 smc-size-sm-7 smc-size-xs-7 ")]

        public bool Destaque { get; set; }

        public List<DetalhesDivisaoTurmaColaboradorViewModel> Colaboradores { get; set; }
        [SMCCssClass("smc-size-lg-7 smc-size-md-7 smc-size-sm-7 smc-size-xs-7 ")]

        [SMCHideLabel]
        public List<string> NomesColaboradores => Colaboradores?.Select(s => s.NomeFormatado).ToList();
    }
}
