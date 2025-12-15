using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Aluno.Areas.MAT.Models;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.MAT.Controllers
{
    public class HomeController : SMCControllerBase
    {
        public ISolicitacaoServicoService SolicitacaoServicoService { get { return this.Create<ISolicitacaoServicoService>(); } }

        public ActionResult Index(SMCEncryptedLong seqSolicitacaoServico = null)
        {
            // Verifica se já selecionou o processo para continuar a inscrição
            var pessoaAtuacaoSelecionada = Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] as SolicitacaoMatriculaSelecionadaViewModel;

			if (seqSolicitacaoServico != null && pessoaAtuacaoSelecionada != null && pessoaAtuacaoSelecionada.SeqSolicitacaoServico != seqSolicitacaoServico)
			{
				// Limpa a solicitação que já está selecionada na seção pois foi passada uma solicitação de serviço nova.
				pessoaAtuacaoSelecionada = null;
				Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] = null;
			}

            if (pessoaAtuacaoSelecionada == null)
            {
                // Verifica se veio pessoa atuação informado. Caso sim, não exibe a tela de seleção de ingressante.
                if (seqSolicitacaoServico != null && seqSolicitacaoServico.Value != 0)
                {
                    pessoaAtuacaoSelecionada = new SolicitacaoMatriculaSelecionadaViewModel
                    {
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        DescricaoProcesso = "PREENCHER"
                    };
                    Session[SolicitacaoMatriculaConstants.KEY_SESSION_PESSOA_ATUACAO_SELECIONADA] = pessoaAtuacaoSelecionada;
                    Session[SolicitacaoMatriculaConstants.KEY_SESSION_DESABILITAR_SELECAO_SERVICO] = true;
                }
                else
                {
                    // Retorna a view de seleção do ingressante
                    return View();
                }
            }
            // Retorna a view de etapas da solicitação
            return SMCRedirectToAction("Index", "Solicitacao");
        }
    }
}