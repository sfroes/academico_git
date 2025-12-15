using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.FIN.Lookups;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.FIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.FIN.Views.PessoaAtuacaoBeneficio.App_LocalResources;
using System.Web.Routing;
using SMC.SGA.Administrativo.Extensions;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{
    public class PessoaAtuacaoBeneficioController : SMCDynamicControllerBase
    {
        #region Serviços

        private IPessoaAtuacaoBeneficio PessoaAtuacaoBeneficio => Create<IPessoaAtuacaoBeneficio>();

        private IBeneficioService Beneficio => Create<IBeneficioService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IAlunoService AlunoService => Create<IAlunoService>();

        private IMotivoAlteracaoBeneficio MotivoAlteracaoBeneficio => Create<IMotivoAlteracaoBeneficio>();

        #endregion Serviços

        [SMCAuthorize(UC_FIN_001_03_01.PESQUISAR_ASSOCIACAO_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult CabecalhoPessoaAtuacaoBeneficio(SMCEncryptedLong seqPessoaAtuacao)
        {
            var model = ExecuteService<PessoaAtuacaoBeneficioData, PessoaAtuacaoBeneficioCabecalhoViewModel>(PessoaAtuacaoBeneficio.BuscarPesssoaAtuacaoBeneficioCabecalho, seqPessoaAtuacao);

            if (model.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var aluno = this.AlunoService.BuscarAluno(seqPessoaAtuacao);
                model.RA = aluno.NumeroRegistroAcademico;
                model.CodigoMigracaoAluno = aluno.CodigoAlunoMigracao;
            }
            else
            {
                model.SeqIngressante = seqPessoaAtuacao;
            }

            return PartialView("_CabecalhoPessoaAtuacaoBeneficio", model);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult BuscarConfiguracoesBeneficiosSelect(long seqBeneficio, long seqPessoaAtuacao)
        {
            List<SMCDatasourceItem> itens = PessoaAtuacaoBeneficio.BuscarConfiguracoesBeneficiosSelect(seqBeneficio, seqPessoaAtuacao);

            return Json(itens);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarDescricaoTipoDeducao(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarTipoDeducao(seqConfiguracaoBeneficio);

            return Json(SMCEnumHelper.GetDescription(selecionado));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarIdTipoDeducao(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarTipoDeducao(seqConfiguracaoBeneficio);

            return Json((int)selecionado);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarTipoDeducaoEnum(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarTipoDeducao(seqConfiguracaoBeneficio);

            return Json((selecionado));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarDescricaoFormaDeducao(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarFormaDeducao(seqConfiguracaoBeneficio);

            return Json(SMCEnumHelper.GetDescription(selecionado));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarIdFormaDeducao(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarFormaDeducao(seqConfiguracaoBeneficio);

            return Json(((int)selecionado));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarFormaDeducaoEnum(long seqConfiguracaoBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarFormaDeducao(seqConfiguracaoBeneficio);

            return Json((selecionado));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarValorConfiguracaoBeneficio(long? seqConfiguracaoBeneficio, long seq, bool multipleRequest = false)
        {
            decimal selecionado;

            if (seqConfiguracaoBeneficio.HasValue)
            {
                selecionado = PessoaAtuacaoBeneficio.BuscarValorConfiguracaoBeneficio(seqConfiguracaoBeneficio.GetValueOrDefault());

                if (seq > 0)
                {
                    var pessoaAtuacaoBenefico = this.PessoaAtuacaoBeneficio.AlterarPessoaAtuacaoBeneficio(seq);

                    if (selecionado != pessoaAtuacaoBenefico.ValorBeneficio)
                    {
                        selecionado = (decimal)pessoaAtuacaoBenefico.ValorBeneficio;
                    }
                }

                if (multipleRequest)
                {
                    return Json(new
                    {
                        ValorBeneficioPercentual = selecionado.ToString("n2"),
                        ValorBeneficioMoeda = selecionado.ToString("n2")
                    });
                }
            }
            else
            {
                return Json(new
                {
                    ValorBeneficioPercentual = "",
                    ValorBeneficioMoeda = ""
                });
            }

            return Json(selecionado.ToString("n2"));
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarIdAssociarResponsavelFinanceiro(long seqBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarIdAssociarResponsavelFinanceiro(seqBeneficio);

            return Json((int)selecionado);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarIdDeducaoValorParcelaTitular(long seqBeneficio)
        {
            var selecionado = PessoaAtuacaoBeneficio.BuscarIdDeducaoValorParcelaTitular(seqBeneficio);

            return Json(selecionado);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarDataAdmissaoIngressante(long SeqPessoaAtuacao, bool IncideParcelaMatricula)
        {
            if (IncideParcelaMatricula)
            {
                return Json(PessoaAtuacaoBeneficio.BuscarDataAdmissaoIngressante(SeqPessoaAtuacao));
            }
            else
            {
                return Json(string.Empty);
            }
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarResponsaveisFinanceiroBeneficio(long seqBeneficio, long Seq)
        {
            var beneficio = Beneficio.BuscarBeneficio(seqBeneficio);

            var pessoaAtuacaoBeneficio = PessoaAtuacaoBeneficio.BuscarPessoaAtuacaoBeneficio(Seq);

            PessoaAtuacaoBeneficioDynamicModel model = new PessoaAtuacaoBeneficioDynamicModel();

            List<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel> pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel = new List<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel>();

            List<SMCDatasourceItem> listaTipoResponsavelFinanceiro = new List<SMCDatasourceItem>();

            if (beneficio.TipoResponsavelFinanceiro != null)
            {
                //Preenche todos os dados do datasourse
                if (beneficio.TipoResponsavelFinanceiro == TipoResponsavelFinanceiro.Todos)
                {
                    foreach (var item in Enum.GetValues(typeof(TipoResponsavelFinanceiro)))
                    {
                        //Verifica se o item é o nenhum ou todos e desta forma não retorna-lo
                        if (Convert.ToUInt32(item) != (int)TipoResponsavelFinanceiro.Nenhum && Convert.ToUInt32(item) != (int)TipoResponsavelFinanceiro.Todos)
                        {
                            listaTipoResponsavelFinanceiro.Add(new SMCDatasourceItem()
                            {
                                Seq = Convert.ToUInt32(item),
                                Descricao = SMCEnumHelper.GetDescription(SMCEnumHelper.GetEnum<TipoResponsavelFinanceiro>(item.ToString()))
                            });
                        }
                    }
                }
                else
                {
                    listaTipoResponsavelFinanceiro.Add(new SMCDatasourceItem()
                    {
                        Seq = (int)beneficio.TipoResponsavelFinanceiro,
                        Descricao = beneficio.TipoResponsavelFinanceiro.SMCGetDescription()
                    });
                }
            }

            if (Seq == 0)
            {
                foreach (var item in beneficio.ResponsaveisFinanceiros)
                {
                    pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel.Add(new PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel() { ResponsaveisFinanceiro = item.Transform<PessoaJuridicaLookupViewModel>() });
                }

                var responsaveis = pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel.TransformMasterDetailList<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel>();


                model = new PessoaAtuacaoBeneficioDynamicModel()
                {
                    ResponsaveisFinanceiro = responsaveis,
                    IdExisteResponsaveisFinanceiros = (beneficio.ResponsaveisFinanceiros != null ? beneficio.ResponsaveisFinanceiros.Count() > 0 : false),
                    IdAssociarResponsavelFinanceiro = (int)beneficio.AssociarResponsavelFinanceiro
                };
            }
            else
            {
                if (beneficio.Seq == pessoaAtuacaoBeneficio.SeqBeneficio)
                {
                    foreach (var item in pessoaAtuacaoBeneficio.ResponsaveisFinanceiro)
                    {
                        pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel.Add(new PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel() { ResponsaveisFinanceiro = item.PessoaJuridica.Transform<PessoaJuridicaLookupViewModel>(), ValorPercentual = item.ValorPercentual });
                    }
                }
                else
                {
                    foreach (var item in beneficio.ResponsaveisFinanceiros)
                    {
                        pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel.Add(new PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel() { ResponsaveisFinanceiro = item.Transform<PessoaJuridicaLookupViewModel>() });
                    }
                }

                var responsaveis = pessoaAtuacaoBeneficioResponsavelFinanceiroViewModel.TransformMasterDetailList<PessoaAtuacaoBeneficioResponsavelFinanceiroViewModel>();
                model = new PessoaAtuacaoBeneficioDynamicModel()
                {
                    ResponsaveisFinanceiro = responsaveis,
                    IdExisteResponsaveisFinanceiros = (beneficio.ResponsaveisFinanceiros != null ? beneficio.ResponsaveisFinanceiros.Count() > 0 : false),
                    IdAssociarResponsavelFinanceiro = (int)beneficio.AssociarResponsavelFinanceiro
                };
            }

            model.ListaTipoResponsavelFinanceiro = listaTipoResponsavelFinanceiro.TransformList<SMCSelectListItem>();
            return PartialView("_ResponsaveisFinanceiros", model);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult VerificarExisteResponsaveisFinanceiros(long seqBeneficio)
        {
            var beneficio = Beneficio.BuscarBeneficio(seqBeneficio);

            var retorno = false;

            if (beneficio.ResponsaveisFinanceiros.Count() > 0)
            {
                retorno = true;
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult VerificarExisteConfiguracaoBeneficio(long seqBeneficio, long seqPessoaAtuacao, bool multipleRequest = false)
        {
            /*NV11 Se a pessoa-atuação for um Ingressante:
            Se o benefício em questão não possui configurações, o campo deverá ser preenchido com o valor Não e deverá
            permanecer desabilitado. Senão, se o benefício possui configurações, o campo deverá ser preenchido com o valor 
            Sim e deverá ser ///habitado.O usuário poderá alterar o valor do campo, sendo que: Se preenchido com o valor Sim, 
            a data de início deverá ser desabilitada e preenchida com o primeiro dia do mês / ano da data de admissão do ingressante.
            Senão, exibir a seguinte mensagem: "Ao desmarcar o campo "Incide na parcela de matrícula ? ", o
            benefício não será incidido na parcela de matrícula do ingressante.E o mês / ano da data de início
            deverá ser maior que o mês/ ano da data de admissão do ingressante."
            Se a pessoa - atuação for um Aluno:
            O campo não deverá ser exibido na tela e não deverá ser ser preenchido com nenhum valor default*/

            var itens = PessoaAtuacaoBeneficio.BuscarConfiguracoesBeneficiosSelect(seqBeneficio, seqPessoaAtuacao);

            var retorno = false;

            if (itens.Count() > 0)
                retorno = true;

            if (multipleRequest)
                return Json(new
                {
                    IncideParcelaMatriculaBanco = retorno,
                    ExisteConfiguracaoBeneficio = retorno
                }, JsonRequestBehavior.AllowGet);

            return Content(retorno.ToString().ToLower());

        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult BuscarIncideParcelaSelect(long seqBeneficio, long seqPessoaAtuacao, bool aluno)
        {
            /*NV11 Se a pessoa-atuação for um Ingressante:
            Se o benefício em questão não possui configurações, o campo deverá ser preenchido com o valor Não e deverá
            permanecer desabilitado. Senão, se o benefício possui configurações, o campo deverá ser preenchido com o valor 
            Sim e deverá ser ///habitado.O usuário poderá alterar o valor do campo, sendo que: Se preenchido com o valor Sim, 
            a data de início deverá ser desabilitada e preenchida com o primeiro dia do mês / ano da data de admissão do ingressante.
            Senão, exibir a seguinte mensagem: "Ao desmarcar o campo "Incide na parcela de matrícula ? ", o
            benefício não será incidido na parcela de matrícula do ingressante.E o mês / ano da data de início
            deverá ser maior que o mês/ ano da data de admissão do ingressante."
            Se a pessoa - atuação for um Aluno:
            O campo não deverá ser exibido na tela e não deverá ser ser preenchido com nenhum valor default*/

            List<SMCSelectListItem> lista = new List<SMCSelectListItem>();
            if (!aluno)
            {
                var itens = PessoaAtuacaoBeneficio.BuscarConfiguracoesBeneficiosSelect(seqBeneficio, seqPessoaAtuacao);
                if (itens.Count() > 0)
                {
                    lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim", Selected = true });
                    lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não" });
                }
                else
                {
                    lista.Add(new SMCSelectListItem() { Value = false.ToString(), Text = "Não", Selected = true });
                    lista.Add(new SMCSelectListItem() { Value = true.ToString(), Text = "Sim" });
                }
            }

            return Json(lista);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult ExibirMensagemInformativa(bool incideParcelaMatricula, bool incideParcelaMatriculaBanco, long seq, bool aluno, long seqBeneficio, bool existeConfiguracaoBeneficio)
        {
            var retorno = false;

            if (!aluno)
            {
                if (seq > 0)
                {
                    if (incideParcelaMatriculaBanco && !incideParcelaMatricula)
                        retorno = true;
                }
                else
                {
                    if (!existeConfiguracaoBeneficio || !incideParcelaMatricula)
                    {
                        retorno = true;
                    }
                }
            }

            return Json(retorno);
        }

        /// <summary>
        /// Redireciona para para aluno ou ingressante conforme a atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da paessoa atuação</param>
        /// <returns>Redirecionamento para o index da controller de aluno ou ingressante conforme o tipo da atuação informada</returns>
        [SMCAllowAnonymous]
        public ActionResult Voltar(SMCEncryptedLong seqPessoaAtuacao)
        {
            var tipoAtuacao = PessoaAtuacaoService.BuscarPessoaAtuacao(seqPessoaAtuacao)?.TipoAtuacao ?? TipoAtuacao.Ingressante;
            return SMCRedirectToAction("Index", tipoAtuacao.ToString(), new { Area = "ALN" });
        }

        /// <summary>
        /// Verifica se o beneficio é de sequencial é 146 ou 68 no financieiro e de forma fixa, conforme regra NV28 
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>Retorna a data predeterminada 31/12/2099</returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarDataFimPorBeneficio(long seqBeneficio)
        {
            var beneficio = this.Beneficio.BuscarBeneficio(seqBeneficio);

            if (beneficio.SeqBeneficioFinanceiro == 146 || beneficio.SeqBeneficioFinanceiro == 68)
            {
                return Json(new DateTime(2079, 5, 31));
            }

            return Json("");
        }

        /// <summary>
        /// Verifica se o beneficio é de sequencial 146 e 68 no financieiro e de forma fixa, conforme regra NV28 
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>True para desabilitar o campo data fim do beneficio</returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult DesabilitarDataFimBeneficio(long seqBeneficio)
        {
            var retorno = false;

            var beneficio = this.Beneficio.BuscarBeneficio(seqBeneficio);

            if (beneficio.SeqBeneficioFinanceiro == 146 || beneficio.SeqBeneficioFinanceiro == 68)
            {
                retorno = true;
            }

            return Json(retorno);
        }

        /// <summary>
        /// Desabilitar o Campo valor se ele tiver configuração de beneficio
        /// </summary>
        /// <param name="SeqBeneficio">Sequencial de beneficio</param>
        /// <param name="SeqConfiguracaoBenefico">Sequencial de configuração de beneficio</param>
        /// <returns></returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult DesabilitaCampoValorPorConfiguracaoBeneficio(long? SeqConfiguracaoBeneficio)
        {
            return Json(!SeqConfiguracaoBeneficio.HasValue);
        }

        /// <summary>
        /// NV23
        /// Quando o campo Situação (exceto se igual a Deferido) for alterado, exibir o campo Justificativa.
        /// E o mesmo deverá ser preenchido obrigatoriamente.
        /// A descrição do campo Justificativa será utilizado para preencher o campo Observações ao criar o registro no Histórico
        /// de Situação do Benefício.
        /// </summary>
        /// <param name="SeqSituacaoChancelaBeneficioAtualBanco"></param>
        /// <param name="SeqSituacaoChancelaBeneficioAtual"></param>
        /// <returns></returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult DesabilitarJustificativa(int SeqSituacaoChancelaBeneficioAtualBanco, int SeqSituacaoChancelaBeneficioAtual)
        {
            bool retorno = true;

            if (SeqSituacaoChancelaBeneficioAtualBanco != SeqSituacaoChancelaBeneficioAtual)
            {
                retorno = false;
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult ConteudoJustificativa(bool? desabilitarJustificativa, string justificativaBanco)
        {
            string retorno = string.Empty;

            if (desabilitarJustificativa.GetValueOrDefault())
            {
                retorno = justificativaBanco;
            }
            
            return Json(retorno);
        }

        /// <summary>
        /// Abrir modal excluir pessoa atuação beneficio
        /// </summary>
        /// <param name="descricaoBeneficio">Descrição do Beneficio</param>
        /// <param name="nomePessoaAtuacao">Descrição da Pessoa Atuação</param>
        /// <returns></returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult ModalExcluir(long seqPessoaAtuacaoBeneficio, long seqPessoaAtuacao, string descricaoBeneficio, string nomePessoaAtuacao)
        {
            PessoaAtuacaoBeneficioExcluirViewModel modelo = new PessoaAtuacaoBeneficioExcluirViewModel();

            modelo.DescricaoBeneficio = descricaoBeneficio;
            modelo.Nome = nomePessoaAtuacao;
            modelo.SeqPessoaAtuacaoBeneficio = seqPessoaAtuacaoBeneficio;
            modelo.SeqPessoaAtuacao = seqPessoaAtuacao;

            return PartialView("_ModalExcluir", modelo);
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult ExcluirAssociacaoBeneficio(PessoaAtuacaoBeneficioExcluirViewModel modelo)
        {
            try
            {
                PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio = new PessoaAtuacaoBeneficioData();
                pessoaAtuacaoBeneficio.Seq = modelo.SeqPessoaAtuacaoBeneficio;
                pessoaAtuacaoBeneficio.Justificativa = modelo.Justificativa;

                this.PessoaAtuacaoBeneficio.ExcluirPesssoaAtuacaoBeneficio(pessoaAtuacaoBeneficio);

                SetSuccessMessage(UIResource.MensagemExcluiPessoaAtuacaoSucesso, target: SMCMessagePlaceholders.Centro);

                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public ActionResult BuscarTipoResponsavelFinanceiroSelect(long seqBeneficio)
        {
            var listaTipoResponsavelFinanceiro = this.PessoaAtuacaoBeneficio.BuscarTipoResponsavelFinanceiroSelect(seqBeneficio).TransformList<SMCSelectListItem>();

            return Json(listaTipoResponsavelFinanceiro);
        }

        [SMCAuthorize(UC_FIN_001_03_03.CONSULTAR_BENEFÍCIO_PESSOA_ATUACAO)]
        public ActionResult ConsultaBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            ViewBag.Title = UIResource.Title_Consulta_Beneficio;

            var modelo = this.PessoaAtuacaoBeneficio.ConsultarPessoaAtuacaoBeneficio(seqPessoaAtuacaoBeneficio).Transform<PessoaAtaucaoBeneficioConsultaViewModel>();

            return View("ConsultaBeneficio", modelo);
        }

        [SMCAuthorize(UC_FIN_001_03_03.CONSULTAR_BENEFÍCIO_PESSOA_ATUACAO)]
        public ActionResult SalvarPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioDynamicModel pessoaAtuacaoBeneficio)
        {
            long seqPessoaAtuacaoBeneficio = this.PessoaAtuacaoBeneficio.SalvarPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficioData>());

            SetSuccessMessage(UIResource.MensagmAlteracaoSucesso, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToUrl(Request.UrlReferrer.ToString());
        }

        [SMCAuthorize(UC_FIN_001_03_04.ALTERAR_VIGENCIA_BENEFÍCIO)]
        public ActionResult AlterarVigencia(long seqPessoaAtuacaoBeneficio)
        {
            var modelo = this.PessoaAtuacaoBeneficio.ConsultarPessoaAtuacaoBeneficio(seqPessoaAtuacaoBeneficio).Transform<PessoaAtaucaoBeneficioAlterarVigenciaViewModel>();

            /* Nav 10 - Se a pessoa - atuação for um Ingressante:
            O campo Incide na Parcela de Matrícula deverá ser exibido e de preenchimento obrigatório.
              O campo será preenchido automaticamente de acordo com o benefício selecionado, conforme as seguintes
              condições abaixo.E em todos os casos o usuário poderá alterar o valor do campo:
            Se o benefício não possui configuração, o campo deverá ser preenchido com o valor Não.
            Senão, se o benefício possui configuração, o campo deverá ser preenchido com o valor Sim.
            Se a pessoa - atuação for um Aluno:
            O campo não deverá ser exibido na tela e não deverá ser preenchido com nenhum valor default. */
            //if (!modelo.Aluno)
            //{
            //    modelo.IncideParcelaMatricula = this.PessoaAtuacaoBeneficio.BuscarConfiguracoesBeneficiosSelect(modelo.SeqBeneficio, modelo.SeqPessoaAtuacao).SMCAny();

            //    if (modelo.IncideParcelaMatricula.GetValueOrDefault())
            //    {
            //        modelo.DataInicioVigencia = this.PessoaAtuacaoBeneficio.BuscarDataAdmissaoIngressante(modelo.SeqPessoaAtuacao);
            //    }
            //}

            /*NV04
            Se o benefício selecionado possui o código financeiro igual a 146(Bolsa assistencial) ou 68(Bolsa auxílio
            pós - graduação).O campo Data Fim deverá ser exibido como somente leitura.*/
            //var beneficio = this.Beneficio.BuscarBeneficio(modelo.SeqBeneficio);
            //if (beneficio.SeqBeneficioFinanceiro == 146 || beneficio.SeqBeneficioFinanceiro == 68)
            //{
            //    modelo.DataFimVigencia = new DateTime(2079, 5, 31);
            //    modelo.DesablilitarDataFim = true;
            //}
            //else
            //{
            //    modelo.DataFimVigencia = null;
            //}

            //NV06 Listar em ordem alfabética os motivos de alteração do benefício associados à instituição de ensino logada.
            long seqIntituicao = HttpContext.GetInstituicaoEnsinoLogada().Seq;
            modelo.MotivosAlteracoes = MotivoAlteracaoBeneficio.BuscarMotivoAlteracaoBeneficioInstituicaoEnsino(seqIntituicao).Select(s => new SMCDatasourceItem()
            {
                Seq = s.Seq,
                Descricao = s.Descricao
            }).ToList();

            return PartialView("_AlterarVigencia", modelo);
        }

        [SMCAuthorize(UC_FIN_001_03_04.ALTERAR_VIGENCIA_BENEFÍCIO)]
        public ActionResult SalvarAlterarVigencia(PessoaAtaucaoBeneficioAlterarVigenciaViewModel modelo)
        {
            long seqPessoaAtuacaoBeneficio = this.PessoaAtuacaoBeneficio.SalvarAlterarVigenciaBeneficio(modelo.Transform<PessoaAtuacaoBeneficioData>());

            SetSuccessMessage(UIResource.MensagmAlteracaoSucesso, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToUrl(Request.UrlReferrer.ToString());
        }

        /// <summary>
        /// Buscar o valor percentual conforme NV30
        /// </summary>
        /// <param name="tipoResponsavelFinanceiro">Enum tipo responsavel financeiro</param>
        /// <returns>True para desabilitar o campo data fim do beneficio</returns>
        [SMCAuthorize(UC_FIN_001_03_02.ASSOCIAR_BENEFICIO_PESSOA_ATUACAO)]
        public JsonResult BuscarValorPercentual(TipoResponsavelFinanceiro tipoResponsavelFinanceiro)
        {
            string retorno = string.Empty;

            if (tipoResponsavelFinanceiro == TipoResponsavelFinanceiro.ConvenioParceiro)
            {
                retorno = "0";
            }

            return Json(retorno);
        }

        [SMCAuthorize(UC_FIN_001_03_03.CONSULTAR_BENEFÍCIO_PESSOA_ATUACAO)]
        public ActionResult BuscarNotificacoesPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            PessoaAtaucaoBeneficioConsultaViewModel modelo = PessoaAtuacaoBeneficio.BuscarNotificacoesPessoaAtuacaoBeneficio(seqPessoaAtuacaoBeneficio).Transform<PessoaAtaucaoBeneficioConsultaViewModel>();

            return PartialView("_TabNotificacoes", modelo);
        }
    }
}