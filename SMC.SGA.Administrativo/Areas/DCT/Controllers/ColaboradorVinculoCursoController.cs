using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using SMC.SGA.Administrativo.Areas.DCT.Views.ColaboradorVinculoCurso.App_LocalResources;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.DCT.Controllers
{
    public class ColaboradorVinculoCursoController : SMCControllerBase
    {
        #region [ Services ]

        private IColaboradorVinculoService ColaboradorVinculoService => Create<IColaboradorVinculoService>();

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IColaboradorVinculoCursoService ColaboradorVinculoCursoService => Create<IColaboradorVinculoCursoService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_DCT_001_06_05.ASSOCIAR_OFERTA_CURSO_TIPO_ATIVIDADE)]
        public ActionResult CabecalhoColaboradorVinculoCurso(SMCEncryptedLong seqColaborador, SMCEncryptedLong seqColaboradorVinculo)
        {
            var dadosPessoAtuacao = this.PessoaAtuacaoService.BuscarPessoaAtuacao(seqColaborador);

            var dadosVinculo = this.ColaboradorVinculoService.BuscarColaboradorVinculo(seqColaboradorVinculo);

            ColaboradorVinculoCursoCabecalhoViewModel model = new ColaboradorVinculoCursoCabecalhoViewModel();

            model.Seq = dadosPessoAtuacao.Seq;
            model.Nome = dadosPessoAtuacao.Nome;
            model.NomeSocial = dadosPessoAtuacao.NomeSocial;
            model.Cpf = dadosPessoAtuacao.Cpf;
            model.NumeroPassaporte = dadosPessoAtuacao.NumeroPassaporte;
            model.InseridoPorCarga = dadosVinculo.InseridoPorCarga;
            model.NomeEntidadeVinculo = EntidadeService.BuscarEntidade(dadosVinculo.SeqEntidadeVinculo).Nome;
            model.DescricaoTipoVinculoSelect = dadosVinculo.DescricaoTipoVinculoSelect;
            model.DataInicio = dadosVinculo.DataInicio;
            model.DataFim = dadosVinculo.DataFim;

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_DCT_001_06_05.ASSOCIAR_OFERTA_CURSO_TIPO_ATIVIDADE)]
        public ActionResult Index (SMCEncryptedLong seqColaborador, SMCEncryptedLong seqColaboradorVinculo, SMCEncryptedLong seqEntidadeVinculo)
        {
            ColaboradorVinculoCursoViewModel model = new ColaboradorVinculoCursoViewModel();

            model.Seq = seqColaboradorVinculo;
            model.SeqColaborador = seqColaborador;
            model.SeqColaboradorVinculo = seqColaboradorVinculo;
            model.seqEntidadeVinculo = seqEntidadeVinculo;
            //Preenchimento do Datasource
            model.CursoOfertasLocalidades = this.CursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect(seqEntidadeVinculo);

            model.Cursos = ColaboradorVinculoCursoService.ListarColaboradorVinculoCursos(new ColaboradorVinculoCursoFiltroData() { SeqColaboradorVinculo = seqColaboradorVinculo}).TransformMasterDetailList<CursoViewModel>();

            return View(model);
        }

        [SMCAuthorize(UC_DCT_001_06_05.ASSOCIAR_OFERTA_CURSO_TIPO_ATIVIDADE)]
        public ActionResult Salvar(ColaboradorVinculoCursoViewModel modelo)
        {
            try
            {
                ColaboradorVinculoCursoService.SalvarColaboradorVinculoCurso(modelo.Transform<ColaboradorVinculoData>());

                SetSuccessMessage(UIResource.MSG_ColaboradorVinculoCurso_Sucesso, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", routeValues: new { seqColaborador = new SMCEncryptedLong(modelo.SeqColaborador), seqColaboradorVinculo = new SMCEncryptedLong(modelo.SeqColaboradorVinculo), seqEntidadeVinculo = new SMCEncryptedLong(modelo.seqEntidadeVinculo) });
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
                throw;
            }            
        }
    }
}