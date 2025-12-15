using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Professor.Areas.APR.Models.EntregaOnline;
using SMC.SGA.Professor.Areas.APR.Views.EntregaOnline.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Professor.Areas.APR.Controllers
{
    public class EntregaOnlineController : SMCControllerBase
    {
        #region Services

        public IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();
        public IEntregaOnlineService EntregaOnlineService => Create<IEntregaOnlineService>();

        #endregion Services

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        public ActionResult Index(SMCEncryptedLong seqAplicacaoAvaliacao, SMCEncryptedLong seqOrigemAvaliacao, OrdenacaoCorrecaoEntregaOnline ordenacaoCorrecaoEntregaOnline = OrdenacaoCorrecaoEntregaOnline.DataEntrega, bool backLancamentoNota = false)
        {
            if (seqAplicacaoAvaliacao == 0 || seqAplicacaoAvaliacao == null || seqOrigemAvaliacao == null || seqOrigemAvaliacao == 0)
                throw new SMCApplicationException("Parâmetros Incorretos.");

            var model = EntregaOnlineService.BuscarEntregasOnline(seqAplicacaoAvaliacao).Transform<AplicacaoAvaliacaoEntregaOnlineViewModel>();
            model.OrdenacaoCorrecaoEntregaOnline = ordenacaoCorrecaoEntregaOnline;
            model.BackLancamentoNota = backLancamentoNota;

            PreencherModelo(model);
            return View(model);
        }

        public ActionResult Salvar(AplicacaoAvaliacaoEntregaOnlineViewModel model)
        {
            // Caso este post venha da index do entrega online, carrega os dados de descrição e sigla que não devem ser alterados.
            // Como não consegui rastrear todos os pontos que fazem esse post, achei mais seguro fazer a validação abaixo para trazer os dados da descricao e sigla do banco novamente
            // Foi necessario pois ao mandar essas informações no post dava problema por causa de segurança do MVC ao receber html no post.
            if (model.PostIndex)
            {
                var dadosNaoAlterados = EntregaOnlineService.BuscarEntregasOnline(model.SeqAplicacaoAvaliacao);
                model.DescricaoOrigemAvaliacao = dadosNaoAlterados.DescricaoOrigemAvaliacao;
                model.Descricao = dadosNaoAlterados.Descricao;
                model.Sigla = dadosNaoAlterados.Sigla;
            }

            EntregaOnlineService.SalvarAvaliacao(model.Transform<AplicacaoAvaliacaoEntregaOnlineData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Avaliacao_Entrega_Online, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqAplicacaoAvaliacao = new SMCEncryptedLong(model.SeqAplicacaoAvaliacao), seqOrigemAvaliacao = new SMCEncryptedLong(model.SeqOrigemAvaliacao) });
        }

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        public FileResult DownloadEntrega(Guid guidFile)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexadoPorGuid(guidFile);
            if (arquivo == null)
                throw new SMCApplicationException("Arquivo não encontrado");

            return File(arquivo.FileData, arquivo.Type, arquivo.Name);
        }

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        public ActionResult DownloadSelecionados(SMCEncryptedLong seqAplicacaoAvaliacao, SMCEncryptedLong seqOrigemAvaliacao, string sigla, List<long> entregasOnline)
        {
            try
            {
                var dadosArquivo = EntregaOnlineService.BuscarArquivosEntregasOnline(entregasOnline);
                return File(dadosArquivo.FullName, "application/zip", $"{seqOrigemAvaliacao.Value}_{sigla}.zip");
            }
            catch (Exception ex)
            {
                return ThrowRedirect(ex, "Index", "EntregaOnline", new RouteValueDictionary(new Dictionary<string, object> { { "seqAplicacaoAvaliacao", new SMCEncryptedLong(seqAplicacaoAvaliacao) }, { "seqOrigemAvaliacao", new SMCEncryptedLong(seqOrigemAvaliacao) }, { "area", "apr" } }));
            }
        }

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        public ActionResult LiberarNovaEntrega(SMCEncryptedLong seqEntregaOnline)
        {
            var model = new LiberarNovaEntregaOnlineViewModel { SeqEntregaOnline = seqEntregaOnline };
            return PartialView(model);
        }

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        [HttpPost]
        public ActionResult SalvarLiberacaoNovaEntrega(LiberarNovaEntregaOnlineViewModel model)
        {
            var seqsRetorno = EntregaOnlineService.SalvarLiberacaoNovaEntrega(model.SeqEntregaOnline, model.Observacao);

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Liberacao_Nova_Entrega, target: SMCMessagePlaceholders.Centro);
            return SMCRedirectToAction("Index", new RouteValueDictionary(new Dictionary<string, object> { { "seqAplicacaoAvaliacao", new SMCEncryptedLong(seqsRetorno.SeqAplicacaoAvaliacao) }, { "seqOrigemAvaliacao", new SMCEncryptedLong(seqsRetorno.SeqOrigemAvaliacao) }, { "area", "apr" } }));
        }

        [SMCAuthorize(UC_APR_001_08_01.CORRECAO_ENTREGA_ONLINE)]
        [HttpPost]
        public ActionResult SalvarNegacaoNovaEntrega(LiberarNovaEntregaOnlineViewModel model)
        {
            var seqsRetorno = EntregaOnlineService.SalvarNegacaoNovaEntrega(model.SeqEntregaOnline, model.Observacao);

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Negacao_Nova_Entrega, target: SMCMessagePlaceholders.Centro);
            return SMCRedirectToAction("Index", new RouteValueDictionary(new Dictionary<string, object> { { "seqAplicacaoAvaliacao", new SMCEncryptedLong(seqsRetorno.SeqAplicacaoAvaliacao) }, { "seqOrigemAvaliacao", new SMCEncryptedLong(seqsRetorno.SeqOrigemAvaliacao) }, { "area", "apr" } }));
        }

        private void PreencherModelo(AplicacaoAvaliacaoEntregaOnlineViewModel model)
        {
            model.EntregasOnline.ForEach(e =>
            {
                // Bloqueia o botão e as alterações na tela..
                e.BloquearBotaoDownload = true;

                /* Comando habilitado caso:
                 *  · Data corrente está entre a data de início e fim da entrega online definido na aplicação da avaliação.
                 *  · A situação da entrega é igual a "Liberado para correção" ou "Solicitada nova entrega" */
                e.BloquearBotaoLiberarNovaEntrega = true;
                if (model.DataInicio <= DateTime.Now &&
                    model.DataFim >= DateTime.Now &&
                    (e.SituacaoEntrega == SituacaoEntregaOnline.LiberadoParaCorrecao || e.SituacaoEntrega == SituacaoEntregaOnline.SolicitadoNovaEntrega))
                    e.BloquearBotaoLiberarNovaEntrega = false;

                e.Participantes.ForEach(p =>
                {
                    p.BloquearAlteracoes = true;

                    // Só libera se for situação de liberado para correção ou data fim já ultrapassada
                    if (e.SituacaoEntrega == SituacaoEntregaOnline.Corrigido || e.SituacaoEntrega == SituacaoEntregaOnline.LiberadoParaCorrecao || (model.DataFim.HasValue && model.DataFim < DateTime.Now))
                        p.BloquearAlteracoes = false;
                });

                // Só libera se for situação de liberado para correção ou data fim já ultrapassada
                if (e.SeqArquivoAnexado.HasValue && (e.SituacaoEntrega == SituacaoEntregaOnline.Corrigido || e.SituacaoEntrega == SituacaoEntregaOnline.LiberadoParaCorrecao || (model.DataFim.HasValue && model.DataFim < DateTime.Now)))
                    e.BloquearBotaoDownload = false;
            });

            // Faz a ordenação para exibição
            if (model.OrdenacaoCorrecaoEntregaOnline == OrdenacaoCorrecaoEntregaOnline.NomeAlunoResponsavel)
                model.EntregasOnline = model.EntregasOnline.OrderBy(e => e.Participantes.FirstOrDefault(p => p.ResponsavelEntrega).NomeAluno).ToList();
            else
                model.EntregasOnline = model.EntregasOnline.OrderBy(e => e.DataEntrega).ToList();
        }
    }
}