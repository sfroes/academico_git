using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.MAT.Controllers;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.MAT.Views.RelatorioAlunosPorComponente.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class RelatorioAlunosPorComponenteCabecalhoViewModel : ISMCMappable
    {
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get => UIResource.MSG_MensagemInformativa; }
    }
}