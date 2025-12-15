using System.Collections.Generic;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Models.EnvioNotificacao
{
    public class VisualizarDestinatariosNotificacaoViewModel : SMCPagerViewModel
    {
        public List<long> SeqsDestinatarios { get; set; }
        public long SeqNotificacao { get; set; }
        public TipoAtuacao TipoAtuacao { get; set; }


    }
}