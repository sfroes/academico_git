using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Linq;
using SMC.SGA.Aluno.Areas.ORT.Views.PublicacaoBdp.App_LocalResources;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class PublicacaoBdpIdiomaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect(SortDirection = SMCSortDirection.Ascending)]
        [SMCUnique]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public Linguagem Idioma { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public bool? IdiomaTrabalho { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCDescription]
        public string Titulo { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMultiline]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string Resumo { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa")]
        [SMCDisplay]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.List)]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativaPalavraChave { get => UIResource.Mensagem_Informativa_Palavra_Chave; }

        [SMCDetail(min: 1)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public SMCMasterDetailList<PublicacaoBdpPalavraChaveViewModel> PalavrasChave { get; set; }

        [SMCHidden(SMCViewMode.List | SMCViewMode.Edit)]
        public string ListaPalavrasChave
        {
            get { return PalavrasChave != null ? string.Join("; ", PalavrasChave?.Select(p => p.PalavraChave)) : string.Empty; }
        }
    }
}