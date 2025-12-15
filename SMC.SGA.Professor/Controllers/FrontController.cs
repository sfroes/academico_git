using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Exceptions;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Caching;
using SMC.Framework.DataFilters;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.SGA.Professor.Extensions;
using SMC.SGA.Professor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SMC.SGA.Professor.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [SMCAllowAnonymous]
    public class FrontController : SMCControllerBase
    {
        public ActionResult PartialHeader()
        {
            return PartialView("_Header");
        }

        public ActionResult PartialMenu()
        {
            return PartialView("_Menu");
        }

        public ActionResult PartialFooter()
        {
            return PartialView("_Footer");
        }
    }
}