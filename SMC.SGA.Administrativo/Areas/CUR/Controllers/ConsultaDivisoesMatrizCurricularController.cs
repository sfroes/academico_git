using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class ConsultaDivisoesMatrizCurricularController : SMCControllerBase
    {
        #region [ Service ]

        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();

        private IConsultaDivisoesMatrizCurricularService ConsultaDivisoesMatrizCurricularService => Create<IConsultaDivisoesMatrizCurricularService>();
        
        private IGrupoCurricularService GrupoCurricularService => Create<IGrupoCurricularService>();

        #endregion [ Service ]

        [SMCAuthorize(UC_CUR_001_06_01.CONSULTA_DIVISOES_MATRIZ_CURRICULAR)]
        public ActionResult CabecalhoConsultaDivisoesMatrizCurricular(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = ExecuteService<MatrizCurricularCabecalhoData, MatrizCurricularCabecalhoViewModel>(MatrizCurricularService.BuscarMatrizCurricularCabecalho, seqMatrizCurricular);
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_001_06_01.CONSULTA_DIVISOES_MATRIZ_CURRICULAR)]
        public ActionResult Index(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = PreencherModeloConsultaDivisoesMatrizCurricular(seqMatrizCurricular);
            return View(model);
        }

        [SMCAuthorize(UC_CUR_001_06_01.CONSULTA_DIVISOES_MATRIZ_CURRICULAR)]
        public ActionResult VisualizarDadosGrupoCurricular(SMCEncryptedLong seqGrupoCurricular)
        {
            var model = PreencherModeloDadosGrupoCurricular(seqGrupoCurricular);
            return PartialView("_VisualizarDadosGrupoCurricular", model);
        }

        [NonAction]
        private ConsultaDivisoesMatrizCurricularViewModel PreencherModeloConsultaDivisoesMatrizCurricular(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = this.ConsultaDivisoesMatrizCurricularService.BuscarConsultaDivisoesMatrizCurricular(seqMatrizCurricular)
                .Transform<ConsultaDivisoesMatrizCurricularViewModel>();

            foreach (var grupo in model.ComponentesACursar)
            {
                ConfigurarSituacaoGrupo(grupo);
            }
            foreach (var divisao in model.DivisoesMatrizCurricular)
            {
                foreach (var grupo in divisao.ComponentesGrupos)
                {
                    ConfigurarSituacaoGrupo(grupo);
                }
                foreach (var grupo in divisao.ConfiguracoesGrupos)
                {
                    ConfigurarSituacaoGrupo(grupo);
                }
            }

            return model;
        }

        private static void ConfigurarSituacaoGrupo(GrupoCurricularListarDynamicModel grupo)
        {
            grupo.SituacaoConfiguracaoGrupoCurricular = SituacaoConfiguracaoGrupoCurricular.Nenhum;
            if (grupo.ContemFormacaoEspecifica.GetValueOrDefault())
            {
                grupo.SituacaoConfiguracaoGrupoCurricular |= SituacaoConfiguracaoGrupoCurricular.FormacaoEspecificaAssociada;
            }
            if (grupo.ContemBeneficios.GetValueOrDefault())
            {
                grupo.SituacaoConfiguracaoGrupoCurricular |= SituacaoConfiguracaoGrupoCurricular.BeneficiosAssociados;
            }
            if (grupo.ContemCondicoesObrigatoriedade.GetValueOrDefault())
            {
                grupo.SituacaoConfiguracaoGrupoCurricular |= SituacaoConfiguracaoGrupoCurricular.CondicoesObrigatoriedadeAssociadas;
            }
        }

        [NonAction]
        private ConsultaDivisoesMatrizCurricularGrupoViewModel PreencherModeloDadosGrupoCurricular(long seqGrupoCurricular)
        {
            return GrupoCurricularService.BuscarGrupoCurricularDescricao(seqGrupoCurricular).Transform<ConsultaDivisoesMatrizCurricularGrupoViewModel>();
        }
    }
}