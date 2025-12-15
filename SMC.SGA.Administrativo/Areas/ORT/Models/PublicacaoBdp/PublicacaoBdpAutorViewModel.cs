using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpAutorViewModel
    {
        [SMCHidden]
        public long SeqTrabalhoAcademicoAutoria { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        public string NomeAutor { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        public string NomeAutorFormatado { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCEmail]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public string EmailAutor { get; set; }
    }
}