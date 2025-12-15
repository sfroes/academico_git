using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoDestinatarioListarDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqNotificacaoEmail { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Assunto { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Remetente { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioEnvio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataEnvio { get; set; }
        
        [SMCHidden]
        public long SeqNotificacaoEmailDestinatario { get; set; }

    }
}