using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CNC.Models;
using System;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.CNC.Views.DocumentoConclusao.App_LocalResources;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Framework.Extensions;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Framework;
using SMC.Academico.Common.Areas.CNC.Enums;
using System.Configuration;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using System.Collections.Generic;
using SMC.Framework.Security;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.SGA.Administrativo.Extensions;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class DocumentoConclusaoController : SMCControllerBase
    {
        #region Services

        private ITipoDocumentoAcademicoService TipoDocumentoAcademicoService => Create<ITipoDocumentoAcademicoService>();

        private ISituacaoDocumentoAcademicoService SituacaoDocumentoAcademicoService => Create<ISituacaoDocumentoAcademicoService>();

        private IDocumentoConclusaoService DocumentoConclusaoService => Create<IDocumentoConclusaoService>();

        private ITipoServicoService TipoServicoService => Create<ITipoServicoService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IClassificacaoInvalidadeDocumentoService ClassificacaoInvalidadeDocumentoService => this.Create<IClassificacaoInvalidadeDocumentoService>();

        private IInstituicaoNivelTipoDocumentoAcademicoService InstituicaoNivelTipoDocumentoAcademicoService => this.Create<IInstituicaoNivelTipoDocumentoAcademicoService>();

        #endregion

        [SMCAuthorize(UC_CNC_002_01_01.PESQUISAR_DOCUMENTO_CONCLUSAO)]
        public ActionResult Index(DocumentoConclusaoFiltroViewModel filtro)
        {
            var grupos = new List<GrupoDocumentoAcademico> { GrupoDocumentoAcademico.Diploma, GrupoDocumentoAcademico.HistoricoEscolar };
            long? seqInstituicaoEnsino = HttpContext.GetInstituicaoEnsinoLogada().Seq;

            filtro.EntidadesResponsaveis = this.EntidadeService.BuscarDepartamentosGruposProgramasSelect();
            filtro.TiposDocumento = this.TipoDocumentoAcademicoService.BuscarTiposDocumentoConclusaoSelect(grupos, seqInstituicaoEnsino);
            filtro.Situacoes = this.SituacaoDocumentoAcademicoService.BuscarSituacoesDocumentoAcademicoPorGrupoSelect(grupos);
            filtro.SeqTipoServico = this.TipoServicoService.BuscarTipoServicoPorToken(TOKEN_TIPO_SERVICO.EMISSAO_DOCUMENTO_CONCLUSAO)?.Seq;

            if (filtro.SeqTipoDocumentoAcademico.HasValue && filtro.SeqSituacaoDocumentoAcademico.HasValue)
            {
                var retorno = HabilitaTipoInvalidade(filtro.SeqTipoDocumentoAcademico.Value, filtro.SeqSituacaoDocumentoAcademico.Value);
                filtro.HabilitaTipoInvalidade = (bool)retorno.Data;
            }

            return View(filtro);
        }

        [SMCAuthorize(UC_CNC_002_01_01.PESQUISAR_DOCUMENTO_CONCLUSAO)]
        public ActionResult ListarDocumentosConclusao(DocumentoConclusaoFiltroViewModel filtro)
        {
            SMCPagerModel<DocumentoConclusaoListarViewModel> model = ExecuteService<DocumentoConclusaoFiltroData, DocumentoConclusaoListarData,
                                                                                    DocumentoConclusaoFiltroViewModel, DocumentoConclusaoListarViewModel>
                                                                                   (DocumentoConclusaoService.BuscarDocumentosConclusao, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_CNC_002_01_01.PESQUISAR_DOCUMENTO_CONCLUSAO)]
        public JsonResult HabilitaTipoInvalidade(long? seqTipoDocumentoAcademico, long? seqSituacaoDocumentoAcademico)
        {
            var retorno = false;
            if (seqTipoDocumentoAcademico.HasValue && seqSituacaoDocumentoAcademico.HasValue)
            {
                var temConfiguracao = this.InstituicaoNivelTipoDocumentoAcademicoService.ValidarTipoDocumentoAcademicoArquivoXml(seqTipoDocumentoAcademico.Value);
                if (temConfiguracao)
                {
                    var situacaoDocumentoAcademico = this.SituacaoDocumentoAcademicoService.BuscarSituacaoDocumentoAcademico(seqSituacaoDocumentoAcademico.Value);
                    if (situacaoDocumentoAcademico != null && situacaoDocumentoAcademico.Token == TOKEN_SITUACAO_DOCUMENTO_ACADEMICO.INVALIDO)
                        retorno = true;
                }
            }
            return Json(retorno);
        }

        [SMCAuthorize(UC_CNC_002_01_03.CONSULTAR_DOCUMENTO_CONCLUSAO)]
        public ActionResult Consultar(long seq)
        {
            var modelo = this.DocumentoConclusaoService.BuscarDocumentoConclusaoConsulta(seq).Transform<DocumentoConclusaoConsultaViewModel>();

            if (modelo.SeqDocumentoDiplomaGAD.HasValue && modelo.SeqDocumentoDiplomaGAD != 0)
            {
                var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                modelo.UrlDocumentoGAD = $"{ConfigurationManager.AppSettings["UrlDocumentoGAD"]}?identificador={new SMCEncryptedLong(modelo.SeqDocumentoDiplomaGAD.Value)}&token={modelo.TokenTipoDocumento}&usuario={SMCDESCrypto.EncryptForURL(usuarioInclusao)}";
            }
            return View("ConsultaDocumentoConclusao", modelo);
        }

        [SMCAuthorize(UC_CNC_002_01_03.CONSULTAR_DOCUMENTO_CONCLUSAO)]
        public ActionResult ListarHistoricosDownload(DocumentoConclusaoFiltroHistoricoDownloadViewModel filtro)
        {
            SMCPagerModel<DocumentoConclusaoHistoricoDownloadListarViewModel> model = ExecuteService<DocumentoConclusaoFiltroHistoricoDownloadData, DocumentoConclusaoHistoricoDownloadListarData,
                                                                                      DocumentoConclusaoFiltroHistoricoDownloadViewModel, DocumentoConclusaoHistoricoDownloadListarViewModel>
                                                                                      (DocumentoConclusaoService.BuscarHistoricosDownload, filtro);

            return PartialView("_HistoricosDownload", model);
        }

        [SMCAuthorize(UC_CNC_002_01_07.MANTER_STATUS_DOCUMENTO_DIGITAL)]
        public JsonResult HabilitaCampoClassificacaoInvalidadeDocumento(TipoInvalidade? tipoCancelamento)
        {
            var obrigatoriedade = false;
            if (tipoCancelamento != null)
                obrigatoriedade = true;
            return Json(obrigatoriedade);
        }

        [SMCAuthorize(UC_CNC_002_01_07.MANTER_STATUS_DOCUMENTO_DIGITAL)]
        public ActionResult BuscarDadosSelectClassificacaoInvalidadeDocumento(TipoInvalidade tipoCancelamento)
        {
            return Json(ClassificacaoInvalidadeDocumentoService.BuscarDadosSelectClassificacaoInvalidadeDocumento(tipoCancelamento));
        }

        [SMCAuthorize(UC_CNC_002_01_07.MANTER_STATUS_DOCUMENTO_DIGITAL)]
        public ActionResult AnularDocumentoDigital(long seqDocumentoConclusao, long? seqSolicitacaoServico, string TokenTipoDocumento)
        {
            var modelo = new DocumentoConclusaoStatusDiplomaViewModel()
            {
                SeqDocumentoConclusao = seqDocumentoConclusao,
                TokenAcao = TOKEN_ACAO_DOCUMENTO_DIGITAL.ANULAR_DOCUMENTO_DIGITAL,
                MensagemInformativa = UIResource.Mensagem_Informativa_Anular_Documento,
                ExigeTipoCancelamento = TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL ||
                                        TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_PARCIAL ||
                                        TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_FINAL ||
                                        TokenTipoDocumento == TOKEN_TIPO_DOCUMENTO_ACADEMICO.HISTORICO_ESCOLAR_2VIA,
                MotivosCancelamento = new List<SMCDatasourceItem>(),
                TiposCancelamento = new List<SMCDatasourceItem>(),
                ClassificacoesInvalidadeDocumento = new List<SMCSelectListItem>()
            };

            modelo.MotivosCancelamento.Add(MotivoInvalidadeDocumento.DadosInconsistentes.SMCToSelectItem());

            if (modelo.ExigeTipoCancelamento)
            {
                var documentoInvalidoTemporario = DocumentoConclusaoService.DocumentoInvalidoTemporario(seqDocumentoConclusao);
                if (documentoInvalidoTemporario)
                {
                    modelo.TiposCancelamento.Add(TipoInvalidade.Permanente.SMCToSelectItem());
                }
                else
                {
                    modelo.TiposCancelamento.Add(TipoInvalidade.Permanente.SMCToSelectItem());
                    modelo.TiposCancelamento.Add(TipoInvalidade.Temporario.SMCToSelectItem());
                }
            }

            return PartialView("_AlterarStatusDocumentoDigital", modelo);
        }

        [SMCAuthorize(UC_CNC_002_01_07.MANTER_STATUS_DOCUMENTO_DIGITAL)]
        public ActionResult RestaurarDocumentoDigital(long seqDocumentoConclusao)
        {
            var modelo = new DocumentoConclusaoStatusDiplomaViewModel()
            {
                SeqDocumentoConclusao = seqDocumentoConclusao,
                TokenAcao = TOKEN_ACAO_DOCUMENTO_DIGITAL.RESTAURAR_DOCUMENTO_DIGITAL,
                MensagemInformativa = UIResource.Mensagem_Informativa_Restaurar_Documento,
                MotivosCancelamento = new List<SMCDatasourceItem>()
            };

            modelo.MotivosCancelamento.Add(MotivoInvalidadeDocumento.DadosInconsistentes.SMCToSelectItem());

            return PartialView("_AlterarStatusDocumentoDigital", modelo);
        }

        [SMCAuthorize(UC_CNC_002_01_07.MANTER_STATUS_DOCUMENTO_DIGITAL)]
        public ActionResult SalvarStatusDocumentoDigital(DocumentoConclusaoStatusDiplomaViewModel modelo)
        {
            try
            {
                var retorno = DocumentoConclusaoService.ValidarDocumentoConclusaoParaAlterarStatus(modelo.SeqDocumentoConclusao, modelo.TokenAcao);
                if (!string.IsNullOrEmpty(retorno.listaDocumentosAssociados))
                    if (modelo.TokenAcao == TOKEN_ACAO_DOCUMENTO_DIGITAL.ANULAR_DOCUMENTO_DIGITAL)
                    {
                        Assert(modelo, string.Format(UIResource.MSG_Assert_Confirma_AnularDocumentosAssociados, retorno.listaDocumentosAssociados), () => true);

                        foreach (var seqDocumentoAssociado in retorno.listaSeqsDocumentosAssociados)
                            DocumentoConclusaoService.SalvarStatusDocumentoDigital(seqDocumentoAssociado, modelo.TokenAcao, modelo.Observacao, modelo.MotivoInvalidadeDocumento, modelo.TipoCancelamento, modelo.SeqClassificacaoInvalidadeDocumento);
                    }
                    else
                    {
                        Assert(modelo, string.Format(UIResource.MSG_Assert_Confirma_RestaurarDocumentosAssociados, retorno.listaDocumentosAssociados), () => true);

                        foreach (var seqDocumentoAssociado in retorno.listaSeqsDocumentosAssociados)
                            DocumentoConclusaoService.SalvarStatusDocumentoDigital(seqDocumentoAssociado, modelo.TokenAcao, modelo.Observacao, modelo.MotivoInvalidadeDocumento, modelo.TipoCancelamento, modelo.SeqClassificacaoInvalidadeDocumento);
                    }

                DocumentoConclusaoService.SalvarStatusDocumentoDigital(modelo.SeqDocumentoConclusao, modelo.TokenAcao, modelo.Observacao, modelo.MotivoInvalidadeDocumento, modelo.TipoCancelamento, modelo.SeqClassificacaoInvalidadeDocumento);
                SetSuccessMessage("Status do documento alterado com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SMCRedirectToAction("Consultar", routeValues: new { Seq = new SMCEncryptedLong(modelo.SeqDocumentoConclusao) });
        }

        [SMCAuthorize(UC_CNC_002_01_03.CONSULTAR_DOCUMENTO_CONCLUSAO)]
        public ActionResult VisualizarDadosPessoais(SMCEncryptedLong seqPessoaDadosPessoais)
        {
            var model = this.DocumentoConclusaoService.VisualizarDadosPessoais(seqPessoaDadosPessoais).Transform<DocumentoConclusaoDadosPessoaisViewModel>();
            return PartialView("_VisualizarDadosPessoais", model);
        }

        [SMCAuthorize(UC_CNC_002_01_02.MANTER_DOCUMENTO_CONCLUSAO_ANTIGO)]
        public ActionResult Excluir(long seq)
        {
            try
            {
                this.DocumentoConclusaoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Documento_Conclusao, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index));
        }
    }
}