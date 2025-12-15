using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarAtividadeComplementarListarViewModel : SMCViewModelBase
    {
        [SMCValueEmpty("-")]
        public string Codigo { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataInicio { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataFim { get; set; }

        [SMCValueEmpty("-")]
        public string Descricao { get; set; }

        [SMCValueEmpty("-")]
        public string CargaHoraria { get; set; }

        [SMCValueEmpty("-")]
        public string Etiqueta { get; set; }

        [SMCValueEmpty("-")]
        public string NomeDocente { get; set; }

        [SMCValueEmpty("-")]
        public string TitulacaoDocente { get; set; }
    }
}