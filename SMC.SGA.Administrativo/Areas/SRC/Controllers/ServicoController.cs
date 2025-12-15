using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using SMC.SGA.Administrativo.Areas.SRC.Views.Servico.App_LocalResources;
using SMC.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Extensions;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Util;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Academico.Common.Areas.SRC.Exceptions;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ServicoController : SMCControllerBase
    {
        #region Services

        private IServicoService ServicoService => Create<IServicoService>();

        private ITipoServicoService TipoServicoService => Create<ITipoServicoService>();

        private IInstituicaoNivelService InstituicaoNivelService => Create<IInstituicaoNivelService>();

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService => Create<IInstituicaoNivelTipoVinculoAlunoService>();

        private ISituacaoMatriculaService SituacaoMatriculaService => Create<ISituacaoMatriculaService>();

        private IProcessoService ProcessoService => Create<IProcessoService>();

        private IMotivoBloqueioService MotivoBloqueioService => Create<IMotivoBloqueioService>();

        private ITipoNotificacaoService TipoNotificacaoService => Create<ITipoNotificacaoService>();

        private IDocumentoRequeridoService DocumentoRequeridoService => Create<IDocumentoRequeridoService>();

        private ITipoTemplateProcessoService ITipoTemplateProcessoService => Create<ITipoTemplateProcessoService>();

        private IEtapaService EtapaService => Create<IEtapaService>();
        private IProcessoEtapaConfiguracaoNotificacaoService IProcessoEtapaConfiguracaoNotificacaoService => Create<IProcessoEtapaConfiguracaoNotificacaoService>();

        #endregion

        [SMCAuthorize(UC_SRC_001_01_01.PESQUISAR_SERVICO)]
        public ActionResult Index(ServicoFiltroViewModel filtro)
        {
            filtro.TiposServico = this.TipoServicoService.BuscarTiposServicosSelect();

            return View(filtro);
        }

        [SMCAuthorize(UC_SRC_001_01_01.PESQUISAR_SERVICO)]
        public ActionResult ListarServicos(ServicoFiltroViewModel filtro)
        {
            SMCPagerModel<ServicoListarViewModel> model = ExecuteService<ServicoFiltroData, ServicoListarData,
                                                                         ServicoFiltroViewModel, ServicoListarViewModel>
                                                                         (ServicoService.BuscarServicos, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_SRC_001_01_01.PESQUISAR_SERVICO)]
        public ActionResult ConsultarTaxasPorNucleo(long seq)
        {
            var listaTaxasValores = this.ServicoService.ConsultarTaxasPorNucleo(seq).TransformList<ConsultarTaxasPorNucleoListarViewModel>();
            var retorno = new SMCPagerModel<ConsultarTaxasPorNucleoListarViewModel>(listaTaxasValores);
            retorno.PageSettings.Total = listaTaxasValores.Count();

            return PartialView("_ConsultarTaxasPorNucleo", retorno);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult Incluir()
        {
            var modelo = new ServicoViewModel()
            {
                TiposServico = this.TipoServicoService.BuscarTiposServicosSelect(),
                NiveisEnsino = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect(),
                TiposTransacao = this.ServicoService.BuscarTiposTransacao(),
                ServicosRestricao = this.ServicoService.BuscarServicosSelect(new ServicoFiltroData()),
                MotivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioFinanceiroSelect(),
                TiposNotificacaoSGA = this.TipoNotificacaoService.BuscarTiposNotificacaoNoAcademicoSelect(),
                TaxasGRA = this.ServicoService.BuscarTaxasAcademicas()
            };

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.ServicoService.BuscarServico(seq).Transform<ServicoViewModel>();

            modelo.TiposServico = this.TipoServicoService.BuscarTiposServicosSelect();
            modelo.NiveisEnsino = this.InstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect();
            modelo.TiposTransacao = this.ServicoService.BuscarTiposTransacao();
            modelo.ServicosRestricao = this.ServicoService.BuscarServicosSelect(new ServicoFiltroData());
            modelo.MotivosBloqueio = this.MotivoBloqueioService.BuscarMotivosBloqueioFinanceiroSelect();
            modelo.TiposNotificacaoSGA = this.TipoNotificacaoService.BuscarTiposNotificacaoNoAcademicoSelect();
            modelo.TaxasGRA = this.ServicoService.BuscarTaxasAcademicas();

            var processosPorServico = this.ProcessoService.BuscarProcessosPorServicoSelect(seq);
            modelo.CamposReadyOnly = processosPorServico.Any();

            return View(modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarTemplatesSGFPorTipoServicoSelect(long seqTipoServico)
        {
            return Json(ServicoService.BuscarTemplatesSGFPorTipoServicoSelect(seqTipoServico));
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarValidarSituacaoFutura(TipoAtuacao tipoAtuacao)
        {
            if (tipoAtuacao == TipoAtuacao.Ingressante)
            {

                return Json(new List<SMCDatasourceItem<string>>()
                {
                        new SMCDatasourceItem<string>() { Seq = false.ToString(), Descricao = "Não", Selected = true},
                });
            }
            else
            {
                return Json(new List<SMCDatasourceItem<string>>()
                {
                        new SMCDatasourceItem<string>() { Seq = false.ToString(), Descricao = "Não"},
                        new SMCDatasourceItem<string>() { Seq = true.ToString(), Descricao = "Sim"},
                });
            }

        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarTiposVinculoPorNivelEnsinoSelect(long seqNivelEnsino)
        {
            return Json(InstituicaoNivelTipoVinculoAlunoService.BuscarTipoVinculoPorNivelEnsino(seqNivelEnsino));
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarTiposEmissaoTaxa(OrigemSolicitacaoServico origemSolicitacaoServicoAuxiliarTaxas, TipoEmissaoTaxa tipoEmissaoTaxaAuxiliar)
        {
            var listaTiposEmissao = ServicoService.BuscarTiposEmissaoTaxa(origemSolicitacaoServicoAuxiliarTaxas);

            if (tipoEmissaoTaxaAuxiliar != TipoEmissaoTaxa.Nenhum)
            {
                foreach (var item in listaTiposEmissao)
                {
                    //SETANDO O VALOR DO TIPO DE EMISSÃO NA EDIÇÃO
                    if (item.Seq == (short)tipoEmissaoTaxaAuxiliar)
                        item.Selected = true;
                }
            }

            return Json(listaTiposEmissao);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarBancosAgencias(OrigemSolicitacaoServico origemSolicitacaoServicoAuxiliarTaxas, long? seqBancoAgenciaContaCarteiraAuxiliar)
        {
            var listaBancosAgencias = ServicoService.BuscarBancosAgencias();

            if (seqBancoAgenciaContaCarteiraAuxiliar.HasValue)
            {
                foreach (var item in listaBancosAgencias)
                {
                    //SETANDO O VALOR DO BANCO AGÊNCIA DE EMISSÃO NA EDIÇÃO
                    if (item.Seq == seqBancoAgenciaContaCarteiraAuxiliar.Value)
                        item.Selected = true;
                }
            }

            return Json(listaBancosAgencias);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarCodigoBanco(int? seqBancoAgenciaContaCarteira)
        {
            var listaBancosAgencias = ServicoService.BuscarBancosAgencias();
            short codigoBanco = 0;

            if (seqBancoAgenciaContaCarteira.HasValue)
            {
                var itemBancoAgencia = listaBancosAgencias.FirstOrDefault(a => a.Seq == seqBancoAgenciaContaCarteira);
                codigoBanco = Convert.ToInt16(itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-banco").Value.Trim());
            }

            return Json(codigoBanco);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarCodigoAgencia(int? seqBancoAgenciaContaCarteira)
        {
            var listaBancosAgencias = ServicoService.BuscarBancosAgencias();
            string codigoAgencia = "";

            if (seqBancoAgenciaContaCarteira.HasValue)
            {
                var itemBancoAgencia = listaBancosAgencias.FirstOrDefault(a => a.Seq == seqBancoAgenciaContaCarteira);
                codigoAgencia = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-agencia").Value.Trim();
            }

            return Json(codigoAgencia);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarCodigoConta(int? seqBancoAgenciaContaCarteira)
        {
            var listaBancosAgencias = ServicoService.BuscarBancosAgencias();
            string codigoConta = "";

            if (seqBancoAgenciaContaCarteira.HasValue)
            {
                var itemBancoAgencia = listaBancosAgencias.FirstOrDefault(a => a.Seq == seqBancoAgenciaContaCarteira);
                codigoConta = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-conta").Value.Trim();
            }

            return Json(codigoConta);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult BuscarCodigoCarteira(int? seqBancoAgenciaContaCarteira)
        {
            var listaBancosAgencias = ServicoService.BuscarBancosAgencias();
            string codigoCarteira = "";

            if (seqBancoAgenciaContaCarteira.HasValue)
            {
                var itemBancoAgencia = listaBancosAgencias.FirstOrDefault(a => a.Seq == seqBancoAgenciaContaCarteira);
                codigoCarteira = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-carteira").Value.Trim();
            }

            return Json(codigoCarteira);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult Salvar(ServicoViewModel modelo)
        {
            long retorno = SalvarServico(modelo);

            return SMCRedirectToAction(nameof(Editar), routeValues: new { seq = new SMCEncryptedLong(retorno) });
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult SalvarNovo(ServicoViewModel modelo)
        {
            SalvarServico(modelo);

            return SMCRedirectToAction(nameof(Incluir));
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult SalvarSair(ServicoViewModel modelo)
        {
            SalvarServico(modelo);

            return SMCRedirectToAction(nameof(Index));
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult Excluir(long seq)
        {
            try
            {
                this.ServicoService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Servico, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index));
        }

        private long SalvarServico(ServicoViewModel modelo)
        {
            modelo.SeqInstituicaoEnsino = HttpContext.GetInstituicaoEnsinoLogada().Seq;

            long retorno = this.ServicoService.Salvar(modelo.Transform<ServicoData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Servico, target: SMCMessagePlaceholders.Centro);

            return retorno;
        }

        private bool ExisteConfiguracaoNotificacao(List<long> excluidos, long seqServico)
        {
            return IProcessoEtapaConfiguracaoNotificacaoService.ExisteConfiguracaoNotificacao(excluidos, seqServico);
        }

        #region [ Wizard Steps Serviço ]

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepCadastroGeral(ServicoViewModel modelo)
        {
            if (modelo.SeqTipoServico != 0)
                modelo.TemplatesSGF = ServicoService.BuscarTemplatesSGFPorTipoServicoSelect(modelo.SeqTipoServico);

            modelo.CampoOrigemReadOnly = modelo.ParametrosEmissaoTaxa != null && modelo.ParametrosEmissaoTaxa.Any();

            return PartialView("_WizardStepCadastroGeral", modelo);
        }

        [ValidateInput(false)]
        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepNivelEnsinoTipoVinculo(ServicoViewModel modelo)
        {
            this.ServicoService.ValidarCampoLiberarTrabalhoAcademico(modelo.Transform<ServicoData>());

            modelo.ExibirCampoTipoTransacao = modelo.IntegracaoFinanceira == IntegracaoFinanceira.TransacaoFinanceira;
            
            return PartialView("_WizardStepNivelEnsinoTipoVinculo", modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepSituacoesRestricoes(ServicoViewModel modelo)
        {
            modelo.ExibirSecaoReabrir = modelo.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteTodas || modelo.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.PermiteExcetoFinalizadaComSucesso ? true : false;
            modelo.ExibirSecaoMotivosBloqueioParcelas = this.TipoServicoService.BuscarTipoServico(modelo.SeqTipoServico).ExigeEscalonamento;
            modelo.ServicosRestricao = this.ServicoService.BuscarServicosSelect(new ServicoFiltroData() { TipoAtuacao = modelo.TipoAtuacao });
            modelo.TiposDocumentoSelect = DocumentoRequeridoService.BuscarTiposDocumentoSelect();

            switch (modelo.TipoAtuacao)
            {
                case TipoAtuacao.Aluno:

                    modelo.Situacoes = SituacaoMatriculaService.BuscarSituacoesMatriculasDaInstiuicaoSelect(new SituacaoMatriculaFiltroData()
                    {
                        SeqsNivelEnsino = modelo.InstituicaoNivelServicos.Select(a => a.SeqNivelEnsino).ToList(),
                        SeqsTipoVinculoAluno = modelo.InstituicaoNivelServicos.Select(a => a.SeqTipoVinculoAluno).ToList()
                    });

                    break;

                case TipoAtuacao.Ingressante:
                    modelo.Situacoes = Enum.GetValues(typeof(SituacaoIngressante)).Cast<SituacaoIngressante>().Where(a => (short)a != 0).Select(a => new SMCDatasourceItem() { Seq = (short)a, Descricao = SMCEnumHelper.GetDescription(a) }).ToList();
                    break;

                default:
                    modelo.Situacoes = new List<SMCDatasourceItem>();
                    break;
            }

            return PartialView("_WizardStepSituacoesRestricoes", modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepTiposNotificacaoParaEtapas(ServicoViewModel modelo)
        {
            var etapasTemplateSGF = EtapaService.BuscarEtapasDoTemplate(modelo.SeqTemplateProcessoSgf);

            if (etapasTemplateSGF.Count() > 0)
            {
                var templatesList = etapasTemplateSGF.OrderBy(c => c.Ordem).ToList();
                modelo.EtapasTemplateProcessoSgf = new List<SMCDatasourceItem>();

                foreach (var etapa in templatesList)
                {
                    modelo.EtapasTemplateProcessoSgf.Add(new SMCDatasourceItem()
                    {
                        Seq = etapa.Seq,
                        Descricao = etapa.Descricao
                    });
                }
            }

            // Ordenar por etapa, e depois alfabeticamente pela notificação
            if (modelo.TiposNotificacao != null)
            {
                var seqsTiposDocumentos = modelo.TiposNotificacaoSGA.Select(f => f.Seq).ToList();
                modelo.TiposNotificacao = modelo.TiposNotificacao.OrderBy(c => c.SeqEtapaSgf).ThenBy(c => seqsTiposDocumentos.IndexOf(c.SeqTipoNotificacao.Value)).TransformMasterDetailList<ServicoTipoNotificacaoViewModel>();

                //Cria nova listagem de tipo notificação,recebendo o mesmo valor da busca acima. Será usada para comparação, em caso de exclusão de tipo de notificação.
                modelo.ListaHistoricoTipoNotificacao = new SMCMasterDetailList<ServicoTipoNotificacaoViewModel>();
                modelo.ListaHistoricoTipoNotificacao.AddRange(modelo.TiposNotificacao);
            }

            return PartialView("_WizardStepTiposNotificacaoParaEtapas", modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepTaxas(ServicoViewModel modelo)
        {
            //TASK 58696 - Caso um tipo de notificação seja excluído, verificar se já existe configuração desse tipo em algum processo do serviço em questão.
            //Caso exista, abortar a operação e exibir a seguinte mensagem:

            //Valida se existiu exclusão, comparando a listagem atual de tipo notificação (modelo.TipoNotificacao)
            //com a listagem de histórico preenchida no método acima (modelo.ListaHistoricoTipoNotificacao)
            if (modelo.TiposNotificacao != null && modelo.ListaHistoricoTipoNotificacao != null &&
                modelo.TiposNotificacao.Count() != modelo.ListaHistoricoTipoNotificacao.Count())
            {
                var listaTipoNotificacaoExcluidos = new List<long>();               
                var ListaSeqsTiposNotificacao = modelo.TiposNotificacao.Select(s=>s.SeqTipoNotificacao).ToList();
                var ListaSeqsHistoricoTiposNotificacao = modelo.ListaHistoricoTipoNotificacao.Select(s => s.SeqTipoNotificacao).ToList();
                foreach (var seqTipoNotificacao in ListaSeqsHistoricoTiposNotificacao)
                {
                    //Verifica no histórico e, caso não exista algum tipo de notificação na listagem atual, adiciona o SeqTipoNotificacao na lista de excluídos
                    if (!ListaSeqsTiposNotificacao.Contains(seqTipoNotificacao))
                        listaTipoNotificacaoExcluidos.Add(seqTipoNotificacao.Value);
                }

                if (listaTipoNotificacaoExcluidos.Count() > 0)
                {
                    //Verifica se existe alguma configuração de tipo notificação com o(s) tipo(s) excluído(s) de acordo com o serviço
                    var existeConfiguracaoNotificacao = ExisteConfiguracaoNotificacao(listaTipoNotificacaoExcluidos, modelo.Seq);
                    if (existeConfiguracaoNotificacao)
                        throw new ExisteConfiguracaoComTipoNotificacaoException();
                }
            }

            
            modelo.CamposTaxaReadOnly = modelo.IntegracaoFinanceira != IntegracaoFinanceira.CobrancaDeTaxa;
            modelo.MensagemInformativaConfiguracaoTaxaNaoPermitida = modelo.CamposTaxaReadOnly ? UIResource.MSG_ConfiguracaoTaxaNaoPermitida : string.Empty;
            modelo.OrigemSolicitacaoServicoAuxiliarTaxas = modelo.OrigemSolicitacaoServico;

            //DATA SOURCES PARA O MESTRE DETALHE EM MODAL DE PARAMETROS DE EMISSÃO
            modelo.TiposEmissaoTaxas = this.ServicoService.BuscarTiposEmissaoTaxa(modelo.OrigemSolicitacaoServico);
            modelo.BancosAgenciasContasCarteiras = this.ServicoService.BuscarBancosAgencias();

            //SETANDO O VALOR DO CAMPO SeqBancoAgenciaContaCarteira, PORQUE ESSE CAMPO NÃO É SALVO NO BANCO
            if (modelo.ParametrosEmissaoTaxa != null)
            {
                foreach (var item in modelo.ParametrosEmissaoTaxa)
                {
                    if (item.CodigoBancoEmissaoTitulo.HasValue)
                    {
                        var itensBancoAgencia = modelo.BancosAgenciasContasCarteiras.Where(a => a.DataAttributes.Any(b => b.Key == "codigo-banco" && b.Value.Trim() == item.CodigoBancoEmissaoTitulo.ToString())).ToList();

                        foreach (var itemBancoAgencia in itensBancoAgencia)
                        {
                            var dataAtributeCodigoAgencia = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-agencia").Value.Trim();
                            var dataAtributeCodigoConta = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-conta").Value.Trim();
                            var dataAtributeCodigoCarteira = itemBancoAgencia.DataAttributes.FirstOrDefault(a => a.Key == "codigo-carteira").Value.Trim();

                            if (dataAtributeCodigoAgencia == item.CodigoAgenciaEmissaoTitulo && dataAtributeCodigoConta == item.CodigoContaEmissaoTitulo && dataAtributeCodigoCarteira == item.CodigoCarteiraEmissaoTitulo)
                            {
                                item.SeqBancoAgenciaContaCarteira = itemBancoAgencia.Seq;
                                break;
                            }
                        }
                    }
                }
            }

            if (modelo.CamposTaxaReadOnly)
            {
                modelo.Taxas = new SMCMasterDetailList<ServicoTaxaViewModel>();
                modelo.ParametrosEmissaoTaxa = new SMCMasterDetailList<ServicoParametroEmissaoTaxaViewModel>();
            }

            return PartialView("_WizardStepTaxas", modelo);
        }

        [SMCAuthorize(UC_SRC_001_01_02.MANTER_SERVICO)]
        public ActionResult WizardStepConfirmacao(ServicoViewModel modelo)
        {
            //FIX: LIMPANDO OS VALORES DO MESTRE DETALHES SE ELE NÃO É EXIBIDO NA TELA (CORREÇÃO WIZZARD)
            if (modelo.ExigeJustificativaSolicitacao.HasValue && !modelo.ExigeJustificativaSolicitacao.Value)
                modelo.Justificativas = new SMCMasterDetailList<JustificativaSolicitacaoServicoViewModel>();

            if (modelo.PermiteReabrirSolicitacao == PermiteReabrirSolicitacao.NaoPermite)
            {
                modelo.SituacoesReabrir = new SMCMasterDetailList<ServicoSituacaoReabrirViewModel>();
                modelo.TipoPrazoReabertura = null;
                modelo.NumeroDiasPrazoReabertura = null;
            }

            if (!modelo.ExibirSecaoMotivosBloqueioParcelas)
                modelo.MotivosBloqueioParcela = new SMCMasterDetailList<ServicoMotivoBloqueioParcelaViewModel>();

            if (!modelo.ExibirCampoTipoTransacao)
            {
                foreach (var instituicaoNivelServico in modelo.InstituicaoNivelServicos)
                {
                    instituicaoNivelServico.SeqTipoTransacaoFinanceira = null;
                }
            }

            modelo.MensagemInformativaTaxasNaoParametrizadas = "";

            this.ServicoService.ValidarModelo(modelo.Transform<ServicoData>());

            if (modelo.Taxas != null && modelo.Taxas.Any())
            {
                var validarAssertProximo = this.ServicoService.ValidarAssertProximo(modelo.Transform<ServicoData>());

                if (validarAssertProximo.ExibirAssert)
                {
                    modelo.MensagemInformativaTaxasNaoParametrizadas = string.Format(UIResource.MSG_ProximoPassoTaxa, validarAssertProximo.MensagemAssertTaxasNaoParametrizadas);                    
                }
            }

            if (modelo.ParametrosEmissaoTaxa != null)
            {
                foreach (var item in modelo.ParametrosEmissaoTaxa)
                {
                    if (item.SeqBancoAgenciaContaCarteira.HasValue)
                    {
                        var listaBancosAgencias = ServicoService.BuscarBancosAgencias();
                        item.DescricaoBancoAgenciaContaCarteira = listaBancosAgencias.FirstOrDefault(a => a.Seq == item.SeqBancoAgenciaContaCarteira).Descricao;
                    }
                }
            }

            return PartialView("_WizardStepConfirmacao", modelo);
        }

        #endregion
    }
}