using System;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.PES.Models.EnvioNotificacao;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class VisualizarNotificacaoViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCSize(SMCSize.Grid8_24)]
        public string NomeRemetente { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string EmailRemetente { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string EmailResposta { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Assunto { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public string Mensagem { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string UsuarioEnvio { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataEnvio { get; set; }

        [SMCHidden]
        public TipoAtuacao TipoAtuacao { get; set; }

        [SMCHidden]
        public List<long> SeqsDestinatarios { get; set; }

        [SMCHidden]
        public virtual Nullable<long> SeqLayoutMensagemEmail { get; set; }

        public VisualizarDestinatariosNotificacaoViewModel VisualizarDestinatariosNotificacaoViewModel { get; set; }

    }
}