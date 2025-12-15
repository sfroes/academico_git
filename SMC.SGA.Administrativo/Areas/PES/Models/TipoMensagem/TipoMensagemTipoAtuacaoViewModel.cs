using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoMensagemTipoAtuacaoViewModel : SMCViewModelBase
    { 
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoMensagem { get; set; }

        [SMCSize(SMCSize.Grid14_24)]
        [SMCRequired]
        [SMCSelect]
        public TipoAtuacao TipoAtuacao { get; set; }
    }
}