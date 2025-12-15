using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class DetalhesDivisaoTurmaColaboradorViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string NomeFormatado { get; set; }

        public List<DetalhesDivisaoTurmaColaboradorOrganizacaoViewModel> ColaboradorOrganizacaoComponente { get; set; }
    }
}
