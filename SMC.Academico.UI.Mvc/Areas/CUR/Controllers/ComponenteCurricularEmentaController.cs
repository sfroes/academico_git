using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Models;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Controllers
{
    public class ComponenteCurricularEmentaController : SMCControllerBase
    {
        #region [ Services ]

        private IComponenteCurricularEmentaService ComponenteCurricularEmentaService => Create<IComponenteCurricularEmentaService>();

        private IComponenteCurricularService ComponenteCurricularService => Create<IComponenteCurricularService>();

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IPlanoEstudoService PlanoEstudoService => Create<IPlanoEstudoService>();
                
        #endregion [ Services ]

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult ConsultaComponenteCurricularEmenta(long seqComponenteCurricular, long?seqPessoaAtuacao, long? seqHistoricoEscolarUltimo, long? seqPlanoEstudo)
        {
            long seqCicloLetivo = 0;
            long seqCursoOfertaLocalidadeTurno = 0;

            if (seqPlanoEstudo.HasValue)
            {
                var registroPlano = PlanoEstudoService.BuscarCicloLetivoLocalidadeTurnoPlanoEstudo(seqPlanoEstudo.Value);
                seqCicloLetivo = registroPlano.SeqCicloLetivo;
                seqCursoOfertaLocalidadeTurno = registroPlano.SeqCursoOfertaLocalidadeTurno;
            }
            else if (seqHistoricoEscolarUltimo.HasValue)
            {
                var registroHistorico = HistoricoEscolarService.BuscarCicloLetivoLocalidadeTurnoHistoricoEscolar(seqHistoricoEscolarUltimo.Value);
                seqCicloLetivo = registroHistorico.SeqCicloLetivo;
                seqCursoOfertaLocalidadeTurno = registroHistorico.SeqCursoOfertaLocalidadeTurno;
            }
            else
            {
                var dadosOrigem = PessoaAtuacaoService.RecuperaDadosOrigem(seqPessoaAtuacao.Value);
                seqCicloLetivo = dadosOrigem.SeqCicloLetivo.GetValueOrDefault();
                seqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno;
            }


            var registro = ComponenteCurricularEmentaService.BuscarComponenteCurricularEmentaPorComponenteCiclo(seqComponenteCurricular, seqCicloLetivo, seqCursoOfertaLocalidadeTurno);

            var model = new ComponenteCurricularEmentaViewModel();
            if (registro != null)
            {
                model.CodigoComponente = registro.ComponenteCurricular.Codigo;
                model.SiglaComponente = registro.ComponenteCurricular.SiglaTipoComponenteCurricular;
                model.DescricaoComponente = registro.ComponenteCurricular.Descricao;
                model.DescricaoEmenta = registro.Ementa;
            }
            else
            {
                var componente = ComponenteCurricularService.BuscarComponenteCurricular(seqComponenteCurricular);
                model.CodigoComponente = componente?.Codigo;
                model.SiglaComponente = componente?.SiglaTipoComponenteCurricular;
                model.DescricaoComponente = componente?.Descricao;               
            }

            var viewModal = GetExternalView(AcademicoExternalViews.COMPONENTE_CURRICULAR_EMENTA_MODAL);
            return PartialView(viewModal, model);
        }
    }
}
