using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class DetalhesDivisaoTurmaColaboradorOrganizacaoViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string DescricaoTipoOrganizacaoComponente { get; set; }

        public short QuantidadeCargaHoraria { get; set; }
    }
}
