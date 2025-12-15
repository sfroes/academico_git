using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConsultaTurmaAmostraFiltroViewModel: SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        public long? SeqConfiguracaoAvaliacaoPpa { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoAvaliacaoPpaTurma { get; set; }
    }
}