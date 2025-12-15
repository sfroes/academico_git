using SMC.Formularios.UI.Mvc.Models;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class FormularioPadraoDadoFormularioViewModel : DadoFormularioViewModel
    {
        public virtual SMCEncryptedLong SeqSolicitacaoServico { get; set; }

        public virtual SMCEncryptedLong SeqConfiguracaoEtapaPagina { get; set; }
    }
}