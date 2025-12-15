using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.Framework.Util;
using SMC.SGA.Aluno.Areas.ORT.Models;
using SMC.SGA.Aluno.Areas.ORT.Views.PublicacaoBdp.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Aluno.Areas.ORT.Controllers
{
    public class PublicacaoBdpController : SMCReportingControllerBase
    {
        #region Serviços

        private IPublicacaoBdpService PublicacaoBdpService { get => Create<IPublicacaoBdpService>(); }

        private ITrabalhoAcademicoService TrabalhoAcademicoService { get => Create<ITrabalhoAcademicoService>(); }

        private ISMCReportMergeService SMCReportMergeService { get => Create<ISMCReportMergeService>(); }

        private IInstituicaoNivelModeloRelatorioService InstituicaoNivelModeloRelatorioService { get => Create<IInstituicaoNivelModeloRelatorioService>(); }

        private IProgramaService ProgramaService { get => Create<IProgramaService>(); }

        private IProgramaTipoAutorizacaoBdpService ProgramaTipoAutorizacaoBdpService { get => Create<IProgramaTipoAutorizacaoBdpService>(); }

        #endregion Serviços

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult Edit(long seq)
        {
            this.SetViewMode(SMCViewMode.Edit);

            // Caso tenha alguma exception será necessário montar a tela com o estado inicial
            // verifica se o modelo foi passado, pelo TempData
            var publicacao = (PublicacaoBdpViewModel)TempData["publicacaoBdp_" + seq];
            if (publicacao == null)
            {
                publicacao = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq).Transform<PublicacaoBdpViewModel>();

                // Informa a data de autorização como hoje
                publicacao.DataAutorizacao = DateTime.Now.Date;
            }
            else
            {
                // Pelo fato de se ter campos somente leitura, desta forma recuperamos os dados
                var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq).Transform<PublicacaoBdpViewModel>();

                publicacao.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial = publicacaoBD.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial;
                publicacao.TrabalhoAcademico.MembrosBancaExaminadora = publicacaoBD.TrabalhoAcademico.MembrosBancaExaminadora;
            }

            var seqAluno = publicacao.TrabalhoAcademico.Autores.Select(x => x.SeqAluno).FirstOrDefault();
            var diasAutorizacaoParcial = publicacao.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial;

            publicacao.TiposAutorizacoes = PublicacaoBdpService.BuscarTiposAutorizacao(seqAluno, diasAutorizacaoParcial);

            if (!publicacao.Arquivos.Any(a => a.Arquivo.FileData == null))
                TempData["publicacaoBdp_Arquivo" + seq] = publicacao.Arquivos.Select(f => System.Text.Encoding.Default.GetString(f.Arquivo.FileData)).ToList();

            return View(publicacao);
        }

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult Insert(long seq)
        {
            this.SetViewMode(SMCViewMode.Insert);

            ///Caso tenha alguma exception será necessário montar a tela com o estado inicial
            ///verifica se o modelo foi passado, pelo TempData
            var publicacao = (PublicacaoBdpViewModel)TempData["publicacaoBdp_" + seq];
            if (publicacao == null)
            {
                publicacao = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq).Transform<PublicacaoBdpViewModel>();

                // Informa a data de autorização como hoje
                publicacao.DataAutorizacao = DateTime.Now.Date;
            }
            else
            {
                ///Pelo fato de se ter campos somente leitura, desta forma recuperamos os dados
                var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq).Transform<PublicacaoBdpViewModel>();

                publicacao.TrabalhoAcademico.MembrosBancaExaminadora = publicacaoBD.TrabalhoAcademico.MembrosBancaExaminadora;
            }


            var seqAluno = publicacao.TrabalhoAcademico.Autores.Select(x => x.SeqAluno).FirstOrDefault();
            var diasAutorizacaoParcial = publicacao.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial;

            publicacao.TiposAutorizacoes = PublicacaoBdpService.BuscarTiposAutorizacao(seqAluno, diasAutorizacaoParcial);

            if (!publicacao.Arquivos.Any(a => a.Arquivo.FileData == null))
                TempData["publicacaoBdp_Arquivo" + seq] = publicacao.Arquivos.Select(f => System.Text.Encoding.Default.GetString(f.Arquivo.FileData)).ToList();

            ViewBag.Title = UIResource.Incluir_Title;
            return View(publicacao);
        }

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult Details(long seq)
        {
            this.SetViewMode(SMCViewMode.ReadOnly);

            var publicacao = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq).Transform<PublicacaoBdpViewModel>();

            // Valida se é para exibir o cabeçalho secundario da autorização
            if (publicacao.UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.AguardandoCadastroAluno && publicacao.UltimaSituacaoTrabalho != SituacaoTrabalhoAcademico.CadastradaAluno)
            {
                publicacao.ExibirAutorizacao = true;
            }

            ViewBag.Title = UIResource.Details_Title;

            return View(publicacao);
        }

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult CabecalhoPublicacaoBdp(long seqTrabalhoAcademico)
        {
            var cabecalho = this.TrabalhoAcademicoService.BuscarCabecalhoPublicacaoBdp(seqTrabalhoAcademico).Transform<CabecalhoPublicacaoBdpViewModel>();

            return PartialView("_Cabecalho", cabecalho);
        }

        /// <summary>
        /// Salvar somente não efetuando a autorização da publicação bdp
        /// </summary>
        /// <param name="model">Modelo de dados da publicação</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult SalvarAutorizarPosteriormente(PublicacaoBdpViewModel model)
        {
            try
            {
                // Caso o usuario tenha preenchido os dados de autorização ele irá resetar os campo
                model.DataAutorizacao = null;
                model.TipoAutorizacao = null;

                if (model.Arquivos.GroupBy(g => g.TipoAutorizacao).Select(s => new { Tipo = s.Key, Count = s.Count() }).Any(w => w.Count > 1))
                    throw new PublicacaoBdpArquivoTipoException();

                // Valida Arquivo Mensagem Erro 
                if (model.Arquivos.Any(a => a.Arquivo.FileData == null && a.Seq == 0))
                {
                    List<string> filedataErro = (List<string>)TempData["publicacaoBdp_Arquivo" + model.Seq];
                    for (int indice = 0; indice < model.Arquivos.Count(); indice++)
                    {
                        if (model.Arquivos[indice].Arquivo.FileData == null && filedataErro.Any())
                        {
                            model.Arquivos[indice].Arquivo.FileData = System.Text.Encoding.Default.GetBytes(filedataErro[indice]);
                        }
                    }
                }

                // Salva a publicação e envia mensagem de sucesso
                this.PublicacaoBdpService.SalvarPublicacaoBdp(model.Transform<PublicacaoBdpData>());
                SetSuccessMessage(UIResource.Mensagem_Salvar_Publicacao, target: SMCMessagePlaceholders.Centro);
                TempData["SeqPublicacaoBdp"] = model.Seq;

                // REtorna para a página de index do aluno
                return SMCRedirectToAction("Index", "Home", new RouteValueDictionary(new { Area = "" }));
            }
            catch (System.Exception ex)
            {
                if (model.InformacoesIdioma.SMCAny())
                {
                    model.InformacoesIdioma.UndoClear();
                }

                TempData["publicacaoBdp_" + model.Seq] = model;

                //Verifica baseado na ultima situação do trabalho se ele esta no modo de inserção ou edição
                return ThrowRedirect(ex, (model.UltimaSituacaoTrabalho == SituacaoTrabalhoAcademico.AguardandoCadastroAluno ? nameof(this.Insert) : nameof(this.Edit)),
                                          routeValues: new RouteValueDictionary(new { Seq = new SMCEncryptedLong(model.Seq) }));
            }
        }

        /// <summary>
        /// Autorizar uma publicação
        /// </summary>
        /// <param name="model">Modelo de dados da publicação</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_003_01_02.AUTORIZAR_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult Autorizar(PublicacaoBdpAutorizacaoViewModel model)
        {
            try
            {
                // Monta o modelo para autorização com o tipo de autorização selecionado na modal
                PublicacaoBdpData modelo = new PublicacaoBdpData();
                modelo = model.Transform<PublicacaoBdpData>();
                modelo.TipoAutorizacao = SMCEnumHelper.GetEnum<TipoAutorizacao>(model.SeqTipoAutorizacao.ToString());

                // Chama o serviço para autorização da publicação
                this.PublicacaoBdpService.AutorizarPublicacaoBdp(modelo);
                SetSuccessMessage(UIResource.Mensagem_Salvar_Autorizacao, target: SMCMessagePlaceholders.Centro);
                return RedirectToAction("Index", "Home", new RouteValueDictionary(new { Area = "" }));
            }
            catch (System.Exception ex)
            {
                return ThrowRedirect(ex, "Index", "Home", new RouteValueDictionary(new { Area = "" }));
            }
        }

        [SMCAllowAnonymous]
        public ActionResult Download(string guidFile, string name, string type, string seqEntity = null)
        {
            Guid guidParsed = Guid.Empty;

            if (Guid.TryParse(guidFile, out guidParsed))
            {
                var data = SMCUploadHelper.GetFileData(new SMCUploadFile { GuidFile = guidFile });
                if (data != null)
                {
                    return File(data, type, name);
                }
            }
            long seq = 0;

            if (!Int64.TryParse(guidFile, out seq))
            {
                seq = new SMCEncryptedLong(guidFile);
            }
            else if (seqEntity != null)
            {
                SMCEncryptedLong seqEntityParsed = null;
                try
                {
                    seqEntityParsed = new SMCEncryptedLong(seqEntity);
                    seq = seqEntityParsed.Value;
                }
                catch (Exception)
                {
                    seq = 0;
                }
            }

            return Redirect(TrabalhoAcademicoService.UrlPublicacao(seq, name));
        }

        /// <summary>
        /// Modal de somente autorizar
        /// </summary>
        /// <param name="seq">Sequencial da publicação</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_003_01_02.AUTORIZAR_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult ModalAutorizarPublicacao(long seq)
        {
            var model = new PublicacaoBdpAutorizacaoViewModel();
            var publicacao = this.PublicacaoBdpService.BuscarPubicacaoBdp(seq);
            var dataAutorizacao = DateTime.Now.Date;

            var diasAutorizacaoParcial = publicacao.TrabalhoAcademico.NumeroDiasDuracaoAutorizacaoParcial;

             if (diasAutorizacaoParcial > 0)
            {
                var dataFinalAutorizacao = dataAutorizacao.AddDays(diasAutorizacaoParcial.Value);
                model.TextoAutorizacao = string.Format(UIResource.TXT_AutorizacaoParcial, dataFinalAutorizacao.ToString("dd/MM/yyyy"));
            }
            else
            {
                model.TextoAutorizacao = string.Format(UIResource.TXT_AutorizacaoTextoCompleto);
            }

            model.DataAutorizacao = dataAutorizacao;
            model.Seq = seq;

            return PartialView("_Autorizacao", model);
        }

        [SMCAuthorize(UC_ORT_003_01_03.EXIBIR_RELATORIO_AUTORIZACAO_PUBLICACAO)]
        public ActionResult ImprimirAutorizacao(long seq, long seqInstituicaoNivel)
        {
            var dadosPublicacao = this.PublicacaoBdpService.DadosAutorizacaoPublicacaoBdp(seq);
            if (dadosPublicacao != null)
            {

                // Recupera o template do relatório
                var template = InstituicaoNivelModeloRelatorioService.VerificarTemplateModeloRelatorio(dadosPublicacao.NumDiasAutorizacaoParcial, seqInstituicaoNivel);
                if (template == null)
                    throw new TemplatePublicacaoBdpNaoEncontradoException();

                return SMCDocumentMergeInline(string.Format("{0}.pdf", Guid.NewGuid().ToString()), template.ArquivoModelo.FileData, new object[] { dadosPublicacao });
            }

            return null;
        }
    
        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult WizardInformacoesGerais(PublicacaoBdpViewModel model)
        {
            this.SetViewMode(SMCViewMode.Edit);

            //Se por ventura navergar entre os passos e não tiver banca examinadora, isso acontece pois o sistema não da bild
            if (model.TrabalhoAcademico.MembrosBancaExaminadora == null)
            {
                // Pelo fato de se ter campos somente leitura, desta forma recuperamos os dados
                var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(model.Seq).Transform<PublicacaoBdpViewModel>();

                model.TrabalhoAcademico.MembrosBancaExaminadora = publicacaoBD.TrabalhoAcademico.MembrosBancaExaminadora;
            }

            return PartialView("_StepInformacoesGerais", model);
        }

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult WizardInformacoesIdioma(PublicacaoBdpViewModel model)
        {
            if (model.Arquivos.Count() == 0 || !model.Arquivos.Any(a => a.TipoAutorizacao == TipoAutorizacao.TextoCompleto))
                throw new PublicacaoBdpTipoAutorizacaoObrigatorioException(TipoAutorizacao.TextoCompleto);

            if (model.Arquivos.GroupBy(g => g.TipoAutorizacao).Select(s => new { Tipo = s.Key, Count = s.Count() }).Any(w => w.Count > 1))
                throw new PublicacaoBdpArquivoTipoException();

            return PartialView("_StepInformacoesIdiomas", model);
        }

        [SMCAuthorize(UC_ORT_003_01_01.MANTER_PUBLICACAO_TRABALHO_BIBLIOTECA)]
        public ActionResult WizardConfirmacao(PublicacaoBdpViewModel model)
        {
            // RN_ORT_004 -Consistir quantidade de informações por Idioma
            // É necessário que as informações do trabalho sejam cadastradas em pelo menos 2 idiomas.
            // Sendo o português obrigatório.
            // Verificar se um (e somente um) dos idiomas cadastrados possui a marcação de idioma do trabalho
            bool existeIdiomaPortugues = false;
            int countIdiomaTrabalho = 0;
            if (model.InformacoesIdioma.SMCAny())
            {
                foreach (var item in model.InformacoesIdioma)
                {
                    if (item.Idioma == Linguagem.Portuguese)
                        existeIdiomaPortugues = true;
                    if (item.IdiomaTrabalho.HasValue && item.IdiomaTrabalho.Value)
                        countIdiomaTrabalho++;
                }
            }
            if (!existeIdiomaPortugues)
                throw new PublicacaoBdpSemIdiomaPortuguesException();
            if (countIdiomaTrabalho != 1)
                throw new PublicacaoBdpIdiomaTrabalhoException();

            // Ordena os idiomas para apresentação
            // Primeiro o idioma do trabalho e depois pelo idioma 
            var listaOrdenada = model.InformacoesIdioma.OrderByDescending(i => i.IdiomaTrabalho).ThenBy(i => i.Idioma.SMCGetDescription()).ToList();
            model.InformacoesIdioma = new SMCMasterDetailList<PublicacaoBdpIdiomaViewModel>();
            model.InformacoesIdioma.AddRange(listaOrdenada);

            // Recupera novamente os dados dos membros da banca
            if (model.TrabalhoAcademico.MembrosBancaExaminadora == null)
            {
                // Pelo fato de se ter campos somente leitura, desta forma recuperamos os dados
                var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(model.Seq).Transform<PublicacaoBdpViewModel>();
                model.TrabalhoAcademico.MembrosBancaExaminadora = publicacaoBD.TrabalhoAcademico.MembrosBancaExaminadora;
            }

            return PartialView("_Details", model);
        }

        ///// <summary>
        ///// Valida se arquivo foi trocado
        ///// </summary>
        ///// <param name="modelo">Modelo de dados</param>
        ///// <returns>View model da publicação BDP</returns>
        //private PublicacaoBdpViewModel ValidaModelo(PublicacaoBdpViewModel modelo)
        //{
        //    /*Valida se houve troca de arquivo, e não houve nenhuma exception no meio do caminho
        //    Caso tenha acontecido ele informa ao modelo que o arquivo e novo
        //    - Cenario: Se acontece uma excessão e não temos iremos retorna a tela e iremos montada antes da exception e
        //     o arquivo não terá o state como novo assim ele não irá salvar o novo arquivo mantendo o antigo*/
        //    var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(modelo.Seq).Transform<PublicacaoBdpViewModel>();                        

        //    if (publicacaoBD.Arquivos != null && publicacaoBD.Arquivos.Count() > 0 &&
        //        modelo.Arquivos.Any(a => a.Arquivo.FileData == null))
        //    {
        //        modelo.Arquivos = publicacaoBD.Arquivos;
        //    }

        //    return modelo;
        //}
    }
}