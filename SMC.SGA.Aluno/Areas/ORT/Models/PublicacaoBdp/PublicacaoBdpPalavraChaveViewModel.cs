using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Aluno.Areas.ORT.Models
{
    public class PublicacaoBdpPalavraChaveViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid22_24)]
        public string PalavraChave { get; set; }
    }
}