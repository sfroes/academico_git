using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models.AvaliacaoTrabalhoAcademico
{
    public class AlterarAtaDefesaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? SeqArquivoAnexadoAtaDefesa { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCHidden]
        public long? SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        public long? SeqTrabalhoAcademico { get; set; }
    }
}