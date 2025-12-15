using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class InstituicaoNivelCalendarioController : SMCDynamicControllerBase
    {
        private IInstituicaoNivelService InstituicaoNivelService
        {
            get { return this.Create<IInstituicaoNivelService>(); }
        }

        private ICalendarioService CalendarioService
        {
            get { return this.Create<ICalendarioService>(); }
        }

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        private IInstituicaoNivelCalendarioService InstituicaoNivelCalendarioService
        {
            get { return this.Create<IInstituicaoNivelCalendarioService>(); }
        }

        private IEntidadeService EntidadeService
        {
            get { return this.Create<IEntidadeService>(); }
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarSeqUnidadeResponsavelAgd(SMCEncryptedLong seqInstituicaoLogada)
        {
            var seqUnidadeResponsavelAgd = EntidadeService.BuscarEntidade(seqInstituicaoLogada).SeqUnidadeResponsavelAgd.GetValueOrDefault();

            return Json(seqUnidadeResponsavelAgd);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarCalendariosAgd(long seqUnidadeResponsavelAgd)
        {
            var calendarios = CalendarioService.BuscarCalendariosUnidadeResponsavelSelect(seqUnidadeResponsavelAgd);
            return Json(calendarios);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposEventosCalendario(long seqCalendarioAgd)
        {
            var tiposEventos = TipoEventoService.BuscarTiposEventosAGDSelect(new Calendarios.ServiceContract.Areas.CLD.Data.TipoEventoFiltroData { SeqCalendario = seqCalendarioAgd });
            return Json(tiposEventos);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposAvaliacao(UsoCalendario usoCalendario)
        {
            return Json(InstituicaoNivelCalendarioService.BuscarTiposAvaliacao(usoCalendario));
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTokenTipoEventoAGD(long seqTipoEventoAgd)
        {
            return Json(TipoEventoService.BuscarTokenTipoEventoAGD(seqTipoEventoAgd));
        }
    }
}