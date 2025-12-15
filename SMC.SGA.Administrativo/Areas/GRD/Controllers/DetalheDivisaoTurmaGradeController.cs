using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.GRD.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.GRD.Controllers
{
    public class DetalheDivisaoTurmaGradeController : SMCControllerBase
    {
        #region [ Services ]   

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return this.Create<IDivisaoTurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_GRD_001_01_03.DETALHE_DIVISAO_TURMA_GRADE)]
        public ActionResult Index()
        {
            return View();
        }

        [SMCAuthorize(UC_GRD_001_01_03.DETALHE_DIVISAO_TURMA_GRADE)]
        public ActionResult ConsultaDetalheDivisaoTurmaGradeAngular(SMCEncryptedLong seqDivisaoTurma)
        {
            try
            {
                var model = this.DivisaoTurmaService.BuscarDetalhesDivisaoTurmaGrade(seqDivisaoTurma).Transform<DetalheDivisaoTurmaGradeViewModel>();

                return PartialView("_ConsultaDetalheDivisaoTurmaGrade", model);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
        }
    }
}