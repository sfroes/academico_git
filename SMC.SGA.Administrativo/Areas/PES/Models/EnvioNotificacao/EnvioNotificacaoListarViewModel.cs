using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCIgnoreProp]
        public long Seq { get; set; }

        public string Assunto { get; set; }

        public string Remetente { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public string UsuarioEnvio { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataEnvio { get; set; }

        [SMCHidden]
        public List<long> SeqsDestinatarios { get; set; }

        [SMCIgnoreProp]
        public long SeqNotificacaoEmail { get; set; }
    }
}