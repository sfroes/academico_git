using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpIdiomaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid12_24)]
        public Linguagem Idioma { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCReadOnly]
        public string DescricaoIdioma { get => Idioma.SMCGetDescription(); }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public bool? IdiomaTrabalho { get; set; }

        [SMCRequired]
        [SMCMultiline(Rows = 2)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public string Titulo { get; set; }

        [SMCRequired]
        [SMCMultiline(Rows = 5)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public string Resumo { get; set; }

        [SMCDetail(min: 1)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public SMCMasterDetailList<PublicacaoBdpPalavraChaveViewModel> PalavrasChave { get; set; }
    }
}