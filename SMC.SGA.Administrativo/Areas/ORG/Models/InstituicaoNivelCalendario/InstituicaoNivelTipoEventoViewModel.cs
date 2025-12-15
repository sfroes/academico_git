using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelTipoEventoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelCalendario { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCRequired]
        [SMCSelect("TiposEventos")]
        [SMCDependency("SeqCalendarioAgd", nameof(InstituicaoNivelCalendarioController.BuscarTiposEventosCalendario), "InstituicaoNivelCalendario", false)]
        public long SeqTipoEventoAgd { get; set; }

        [SMCHidden]
        public bool BloquearToken { get { return true; } }

        [SMCRequired]
        [SMCConditionalReadonly(nameof(BloquearToken), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqTipoEventoAgd), nameof(InstituicaoNivelCalendarioController.BuscarTokenTipoEventoAGD), "InstituicaoNivelCalendario", true)]
        [SMCSize(SMCSize.Grid7_24)]
        public string TokenTipoEventoAgd { get; set; }

        [SMCSize(SMCSize.Grid7_24)]
        [SMCSelect("ListaTipoAvaliacao")]
        [SMCDependency("UsoCalendario", nameof(InstituicaoNivelCalendarioController.BuscarTiposAvaliacao), "InstituicaoNivelCalendario", false)]
        public TipoAvaliacao? TipoAvaliacao { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelCalendarioService), nameof(IInstituicaoNivelCalendarioService.BuscarTiposAvaliacao), values: new string[] { "UsoCalendario" })]
        public List<SMCDatasourceItem> ListaTipoAvaliacao { get; set; }
    }
}