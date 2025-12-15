using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models.EnvioNotificacaoDestinatario
{
    public class EnvioNotificacaoDestinatarioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid6_24)]
        public string Assunto { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid6_24)]
        public string Remetente { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioEnvio { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataEnvio { get; set; }
    }
}