using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CNC.Models;
using System;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.CNC.Views.DocumentoConclusaoApostilamento.App_LocalResources;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class DocumentoConclusaoApostilamentoController : SMCControllerBase
    {
        #region Serviços

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IDocumentoConclusaoService DocumentoConclusaoService => Create<IDocumentoConclusaoService>();

        private ISituacaoDocumentoAcademicoService SituacaoDocumentoAcademicoService => Create<ISituacaoDocumentoAcademicoService>();

        private IDocumentoConclusaoApostilamentoService DocumentoConclusaoApostilamentoService => Create<IDocumentoConclusaoApostilamentoService>();

        private IFormacaoEspecificaService FormacaoEspecificaService => Create<IFormacaoEspecificaService>();

        private ITipoFormacaoEspecificaService TipoFormacaoEspecificaService => Create<ITipoFormacaoEspecificaService>();

        private ITipoApostilamentoService TipoApostilamentoService => Create<ITipoApostilamentoService>();

        private IAlunoFormacaoService AlunoFormacaoService => Create<IAlunoFormacaoService>();

        #endregion Serviços

        [SMCAuthorize(UC_CNC_002_01_04.PESQUISAR_APOSTILAMENTO)]
        public ActionResult Index(DocumentoConclusaoApostilamentoFiltroViewModel filtro)
        {
            var documentoConclusao = this.DocumentoConclusaoService.BuscarDocumentoConclusao(filtro.SeqDocumentoConclusao);

            if (documentoConclusao.SeqDocumentoAcademicoHistoricoSituacaoAtual.HasValue)
            {
                var situacaoDocumentoAcademicoAtual = this.SituacaoDocumentoAcademicoService.BuscarSituacaoDocumentoAcademicoPorSituacaoAtual(documentoConclusao.SeqDocumentoAcademicoHistoricoSituacaoAtual.Value);
                filtro.ClasseSituacaoDocumentoAtual = situacaoDocumentoAcademicoAtual.ClasseSituacaoDocumento;
            }

            return View(filtro);
        }

        [SMCAuthorize(UC_CNC_002_01_04.PESQUISAR_APOSTILAMENTO)]
        public ActionResult CabecalhoDocumentoConclusaoApostilamentoAluno(long seqDocumentoConclusao)
        {
            var documentoConclusao = this.DocumentoConclusaoService.BuscarDocumentoConclusao(seqDocumentoConclusao);

            var modelCabecalho = ExecuteService<PessoaAtuacaoCabecalhoData, CabecalhoDocumentoConclusaoApostilamentoAlunoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho, documentoConclusao.SeqAtuacaoAluno);

            return PartialView("_CabecalhoDocumentoConclusaoApostilamentoAluno", modelCabecalho);
        }

        [SMCAuthorize(UC_CNC_002_01_04.PESQUISAR_APOSTILAMENTO)]
        public ActionResult CabecalhoDocumentoConclusaoApostilamentoDocumento(long seqDocumentoConclusao)
        {
            var modelCabecalho = ExecuteService<DocumentoConclusaoCabecalhoData, CabecalhoDocumentoConclusaoApostilamentoDocumentoViewModel>(DocumentoConclusaoService.BuscarDocumentoConclusaoCabecalho, seqDocumentoConclusao);

            return PartialView("_CabecalhoDocumentoConclusaoApostilamentoDocumento", modelCabecalho);
        }

        [SMCAuthorize(UC_CNC_002_01_04.PESQUISAR_APOSTILAMENTO)]
        public ActionResult ListarDocumentosConclusaoApostilamento(DocumentoConclusaoApostilamentoFiltroViewModel filtro)
        {
            SMCPagerModel<DocumentoConclusaoApostilamentoListarViewModel> model = ExecuteService<DocumentoConclusaoApostilamentoFiltroData, DocumentoConclusaoApostilamentoListarData,
                                                                                  DocumentoConclusaoApostilamentoFiltroViewModel, DocumentoConclusaoApostilamentoListarViewModel>
                                                                                 (DocumentoConclusaoApostilamentoService.BuscarDocumentosConclusaoApostilamento, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult Incluir(long seqDocumentoConclusao)
        {
            var modelo = new DocumentoConclusaoApostilamentoViewModel()
            {
                SeqDocumentoConclusao = seqDocumentoConclusao
            };

            var formacoesEspecificas = this.FormacaoEspecificaService.BuscarFormacoesEspecificasPorDocumentoConclusao(modelo.SeqDocumentoConclusao);

            if (formacoesEspecificas.Any())
            {
                var seqTipoFormacaoEspecifica = formacoesEspecificas.First().SeqTipoFormacaoEspecifica;
                var documentoConclusao = this.DocumentoConclusaoService.BuscarPessoaCursoGrauDocumentoConclusao(seqDocumentoConclusao);

                if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.GeraCarimbo.HasValue && a.TipoFormacaoEspecifica.GeraCarimbo.Value))
                    modelo.TiposApostilamento = this.TipoApostilamentoService.BuscarTiposApostilamentoSelect();
                else if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.GeraCarimbo.HasValue && !a.TipoFormacaoEspecifica.GeraCarimbo.Value))
                {
                    modelo.TiposApostilamento = this.TipoApostilamentoService.BuscarTiposApostilamentoSemTokenFormacaoSelect();
                    modelo.MensagemTipoFormacaoEspecifica = UIResource.Mensagem_TipoFormacaoEspecifica;
                }

                if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.ExigeGrau.HasValue && a.TipoFormacaoEspecifica.ExigeGrau.Value))
                    modelo.FormacoesAluno = this.AlunoFormacaoService.BuscarFormacoesAlunoTipoExigeGrauAcademico(seqDocumentoConclusao, documentoConclusao.SeqPessoa, documentoConclusao.SeqCurso, documentoConclusao.SeqGrauAcademico);
                else if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.ExigeGrau.HasValue && !a.TipoFormacaoEspecifica.ExigeGrau.Value))
                    modelo.FormacoesAluno = this.AlunoFormacaoService.BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(seqDocumentoConclusao, documentoConclusao.SeqPessoa, documentoConclusao.SeqCurso, seqTipoFormacaoEspecifica);
            }

            return View(modelo);
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.DocumentoConclusaoApostilamentoService.BuscarDocumentoConclusaoApostilamento(seq).Transform<DocumentoConclusaoApostilamentoViewModel>();
            modelo.Mensagem = modelo.CamposReadyOnly ? UIResource.Mensagem_Informativa : string.Empty;

            var formacoesEspecificas = this.FormacaoEspecificaService.BuscarFormacoesEspecificasPorDocumentoConclusao(modelo.SeqDocumentoConclusao);

            if (formacoesEspecificas.Any())
            {
                var seqTipoFormacaoEspecifica = formacoesEspecificas.First().SeqTipoFormacaoEspecifica;
                var documentoConclusao = this.DocumentoConclusaoService.BuscarPessoaCursoGrauDocumentoConclusao(modelo.SeqDocumentoConclusao);

                if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.GeraCarimbo.HasValue && a.TipoFormacaoEspecifica.GeraCarimbo.Value))
                    modelo.TiposApostilamento = this.TipoApostilamentoService.BuscarTiposApostilamentoSelect();
                else if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.GeraCarimbo.HasValue && !a.TipoFormacaoEspecifica.GeraCarimbo.Value))
                {
                    modelo.TiposApostilamento = this.TipoApostilamentoService.BuscarTiposApostilamentoSemTokenFormacaoSelect();
                    modelo.MensagemTipoFormacaoEspecifica = UIResource.Mensagem_TipoFormacaoEspecifica;
                }

                if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.ExigeGrau.HasValue && a.TipoFormacaoEspecifica.ExigeGrau.Value))
                    modelo.FormacoesAluno = this.AlunoFormacaoService.BuscarFormacoesAlunoTipoExigeGrauAcademico(modelo.SeqDocumentoConclusao, documentoConclusao.SeqPessoa, documentoConclusao.SeqCurso, documentoConclusao.SeqGrauAcademico);
                else if (formacoesEspecificas.Any(a => a.TipoFormacaoEspecifica.ExigeGrau.HasValue && !a.TipoFormacaoEspecifica.ExigeGrau.Value))
                    modelo.FormacoesAluno = this.AlunoFormacaoService.BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(modelo.SeqDocumentoConclusao, documentoConclusao.SeqPessoa, documentoConclusao.SeqCurso, seqTipoFormacaoEspecifica);
            }

            return View(modelo);
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public JsonResult BuscarTokenTipoApostilamento(long? seqTipoApostilamento)
        {
            var tipoApostilamento = TipoApostilamentoService.BuscarTipoApostilamento(seqTipoApostilamento.Value);
            return Json(tipoApostilamento.Token);
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult BuscarMensagemTokenApostilamento(long? seqTipoApostilamento)
        {
            var tipoApostilamento = TipoApostilamentoService.BuscarTipoApostilamento(seqTipoApostilamento.Value);

            if (tipoApostilamento.Token == TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO)
                return PartialView("_MensagemTokenTipoApostilamento", UIResource.Mensagem_TokenFormacao);
            else if (tipoApostilamento.Token == TOKEN_TIPO_APOSTILAMENTO.DADOS_PESSOAIS)
                return PartialView("_MensagemTokenTipoApostilamento", UIResource.Mensagem_TokenDadosPessoais);
            else
                return Content("");
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult BuscarBotaoTokenApostilamento(long? seqTipoApostilamento, long? seqDocumentoConclusao)
        {
            var tipoApostilamento = TipoApostilamentoService.BuscarTipoApostilamento(seqTipoApostilamento.Value);
            var documentoConclusao = this.DocumentoConclusaoService.BuscarDocumentoConclusao(seqDocumentoConclusao.Value);

            if (tipoApostilamento.Token == TOKEN_TIPO_APOSTILAMENTO.DADOS_PESSOAIS)
                return PartialView("_BotaoTokenDadosPessoais", documentoConclusao.SeqAtuacaoAluno);
            else
                return Content("");
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult Salvar(DocumentoConclusaoApostilamentoViewModel modelo)
        {
            long retorno = this.DocumentoConclusaoApostilamentoService.Salvar(modelo.Transform<DocumentoConclusaoApostilamentoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Documento_Conclusao_Apostilamento, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_CNC_002_01_05.MANTER_APOSTILAMENTO)]
        public ActionResult Excluir(long seq, long seqDocumentoConclusao)
        {
            try
            {
                this.DocumentoConclusaoApostilamentoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Documento_Conclusao_Apostilamento, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqDocumentoConclusao = new SMCEncryptedLong(seqDocumentoConclusao) });
        }
    }
}