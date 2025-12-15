using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoViewModel
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        public string NumeroProtocolo { get; set; }

        public string Processo { get; set; }
        
        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public string TokenTipoServico { get; set; }

        public long? SeqProcessoEtapaAtual { get; set; }

    }
}