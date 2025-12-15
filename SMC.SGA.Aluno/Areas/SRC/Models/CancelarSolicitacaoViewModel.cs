using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class CancelarSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoServico { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string NumeroProtocolo { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Servico { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Situacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataSolicitacao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }
    }
}