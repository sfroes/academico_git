using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Aluno.Areas.MAT.Models;
using SMC.SGA.Aluno.Areas.SRC.Models;
using SMC.SGA.Aluno.Extensions;
using System.Web.Mvc;
using SGA.Aluno.Areas.SRC.SolicitacaoServico;

namespace SMC.SGA.Aluno.Areas.SRC.Controllers
{
    public class SolicitacaoServicoController : SMCControllerBase
    {
        #region Serviços

        public ISolicitacaoServicoService SolicitacaoServicoService { get { return this.Create<ISolicitacaoServicoService>(); } }

        public IServicoService ServicoService { get { return this.Create<IServicoService>(); } }

        public ITipoServicoService TipoServicoService { get { return this.Create<ITipoServicoService>(); } }

        private IProcessoService ProcessoService { get { return this.Create<IProcessoService>(); } }

        private IContratoService ContratoService { get { return this.Create<IContratoService>(); } }

        #endregion Serviços

        // GET: SRC/SolicitacaoServico
        [SMCAuthorize(UC_ALN_005_02_01.PESQUISAR_SOLICITACAO_ALUNO)]
        public ActionResult Index(SolicitacaoServicoFiltroViewModel filtro)
        {
            PreencherModelo(filtro);

            return View(filtro);
        }

        [SMCAuthorize(UC_ALN_005_02_01.PESQUISAR_SOLICITACAO_ALUNO)]
        public ActionResult Listar(SolicitacaoServicoFiltroViewModel filtro)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            var filtroData = filtro.Transform<SolicitacaoServicoPessoaAtuacaoFiltroData>();
            filtroData.SeqPessoaAtuacao = alunoLogado.Seq;

            var dados = SolicitacaoServicoService.BuscarSolicitacoesPessoaAtuacao(filtroData).Transform<SMCPagerData<SolicitacaoServicoListaViewModel>>();

            var model = new SMCPagerModel<SolicitacaoServicoListaViewModel>(dados, filtroData.PageSettings);
            return PartialView("_Listar", model);
        }

        private void PreencherModelo(SolicitacaoServicoFiltroViewModel filtro)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            ServicoPorAlunoFiltroData filtroData = new ServicoPorAlunoFiltroData()
            {
                SeqAluno = alunoLogado.Seq
            };

            // Busca os tipos de serviços para select de filtro
            filtro.TiposServico = TipoServicoService.BuscarTiposServicosPorAlunoSelect(filtroData);

            // Busca os serviços para o select de filtro de acordo com o tipo selecionado
            if (filtro.SeqTipoServico.GetValueOrDefault() > 0)
            {
                filtroData.SeqTipoServico = filtro.SeqTipoServico.GetValueOrDefault();
                filtro.Servicos = filtro.Servicos = ServicoService.BuscarServicosPorAlunoSelect(filtroData);
            }
        }

        private void PreencherModeloNovaSolicitacao(NovaSolicitacaoViewModel filtro)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            ServicoPorAlunoFiltroData filtroData = new ServicoPorAlunoFiltroData()
            {
                SeqAluno = alunoLogado.Seq,
                PermissaoServico = PermissaoServico.CriarSolicitacao,
                OrigemSolicitacaoServico = OrigemSolicitacaoServico.PortalAluno,
                Com1EtapaAtiva = true,
                ConsiderarSituacaoAluno = true,
                TipoAtuacao = TipoAtuacao.Aluno
            };

            // Busca os tipos de serviço para select de criação de uma nova solicitação
            filtro.TiposServico = TipoServicoService.BuscarTiposServicosPorAlunoSelect(filtroData);
        }

        [SMCAuthorize(UC_ALN_005_02_02.MANTER_SOLICITACAO_ALUNO)]
        public ActionResult NovaSolicitacao()
        {
            var model = new NovaSolicitacaoViewModel();
            PreencherModeloNovaSolicitacao(model);
            return PartialView("_NovaSolicitacao", model);
        }

        [SMCAuthorize(UC_ALN_005_02_03.CANCELAR_SOLICITACAO_ALUNO)]
        public ActionResult CancelarSolicitacao(SMCEncryptedLong seqSolicitacaoServico)
        {
            var result = this.SolicitacaoServicoService.BuscarDadosCabecalhoCancelamentoSolicitacao(seqSolicitacaoServico);

            var model = new CancelarSolicitacaoViewModel()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                NumeroProtocolo = result.Protocolo,
                Servico = result.DescricaoServico,
                DataSolicitacao = result.DataSolicitacao,
                Situacao = result.SituacaoAtual,
            };
            return PartialView("_CancelarSolicitacao", model);
        }

        [SMCAuthorize(UC_ALN_005_02_03.CANCELAR_SOLICITACAO_ALUNO)]
        public ActionResult SalvarCancelamentoSolicitacao(CancelarSolicitacaoViewModel model)
        {
            var tokenEtapaAtualSolicitacao = SolicitacaoServicoService.BuscarTokenEtapaAtualSolicitacao(model.SeqSolicitacaoServico);

            if (tokenEtapaAtualSolicitacao == TOKEN_ETAPA_SOLICITACAO.ASSINATURA_DOCUMENTO_DIGITAL)
                Assert(model, string.Format(UIResource.MSG_Confirmacao_Fluxo_Assinatura));

            // Transforma em Data e adiciona o token de cancelamento pelo aluno
            var modelData = model.Transform<CancelamentoSolicitacaoData>();
            modelData.TokenMotivoCancelamento = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.SOLICITACAO_CANCELADA_PORTAL_ALUNO;

            SolicitacaoServicoService.SalvarCancelamentoSolicitacao(modelData);
            return SMCRedirectToAction("Index");
        }

        [SMCAuthorize(UC_ALN_005_02_01.PESQUISAR_SOLICITACAO_ALUNO)]
        public JsonResult BuscarServicos(long seqTipoServico)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            ServicoPorAlunoFiltroData filtroData = new ServicoPorAlunoFiltroData()
            {
                SeqAluno = alunoLogado.Seq,
                SeqTipoServico = seqTipoServico
            };
            var dados = ServicoService.BuscarServicosPorAlunoSelect(filtroData);
            return Json(dados);
        }

        [SMCAuthorize(UC_ALN_005_02_02.MANTER_SOLICITACAO_ALUNO)]
        public JsonResult BuscarProcessos(long seqTipoServico)
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            ServicoPorAlunoFiltroData filtroData = new ServicoPorAlunoFiltroData()
            {
                SeqAluno = alunoLogado.Seq,
                PermissaoServico = PermissaoServico.CriarSolicitacao,
                OrigemSolicitacaoServico = OrigemSolicitacaoServico.PortalAluno,
                Com1EtapaAtiva = true,
                ConsiderarSituacaoAluno = true,
                SeqTipoServico = seqTipoServico,
                TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel
            };
            var dados = ProcessoService.BuscarProcessosPorAlunoSelect(filtroData);
            return Json(dados);
        }

        /// <summary>
        /// redireciona o aluno para a ultima etapa da solicitação de serviço quando a solicitação
        /// não pode ser exibida no portal do aluno. OrigemSolicitacaoServico != PortalAluno
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequancial da configuração da etapa</param>
        /// <returns>Redireciona o usurário para a ultima etapa da solicitação</returns>
        [SMCAuthorize(UC_ALN_005_02_01.PESQUISAR_SOLICITACAO_ALUNO)]
        public ActionResult RedirecionarEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa)
        {
            var pessoaAtuacaoSelecionada = new SolicitacaoMatriculaSelecionadaViewModel
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                DescricaoProcesso = "PREENCHER"
            };
            Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] = pessoaAtuacaoSelecionada;

            var backUrl = Url.Action("Index", "SolicitacaoServico", new { area = "SRC" });

            return SMCRedirectToAction("EntrarEtapa", "Matricula", new { @backUrl = backUrl, @seqSolicitacaoMatricula = seqSolicitacaoServico, @seqConfiguracaoEtapa = seqConfiguracaoEtapa, @area = "MAT" });
        }
    }
}