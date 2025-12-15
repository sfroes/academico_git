using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class DadosModalSolicitacaoHistoricoViewModel : SMCViewModelBase
    {
        public List<DadosModalSolicitacaoHistoricoItemViewModel> Historicos { get; set; }
    }
}