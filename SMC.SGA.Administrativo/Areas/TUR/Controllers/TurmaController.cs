using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using SMC.SGA.Administrativo.Areas.TUR.Views.Turma.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class TurmaController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();
        private ICicloLetivoService CicloLetivoService => Create<ICicloLetivoService>();
        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();
        private IComponenteCurricularService ComponenteCurricularService => Create<IComponenteCurricularService>();
        private ICriterioAprovacaoService CriterioAprovacaoService => Create<ICriterioAprovacaoService>();
        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();
        private IDivisaoComponenteService DivisaoComponenteService => Create<IDivisaoComponenteService>();
        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService => Create<IDivisaoMatrizCurricularComponenteService>();
        private IEscalaApuracaoService EscalaApuracaoService => Create<IEscalaApuracaoService>();
        private IGrupoConfiguracaoComponenteService GrupoConfiguracaoComponenteService => Create<IGrupoConfiguracaoComponenteService>();
        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();
        private IMatrizCurricularOfertaService MatrizCurricularOfertaService => Create<IMatrizCurricularOfertaService>();
        private ITipoTurmaService TipoTurmaService => Create<ITipoTurmaService>();
        private ITurmaService TurmaService => Create<ITurmaService>();
        private ITurmaHistoricoFechamentoDiarioService TurmaHistoricoFechamentoDiarioService => Create<ITurmaHistoricoFechamentoDiarioService>();

        public ISMCApiClient AcompanhamentoReport => SMCApiClient.Create("AcompanhamentoReport");

        #endregion [ Services ]

        #region [ WIZARD ]

        #region [ CONFIGURAÇÃO PRINCIPAL - Step 00 - Aba 01 ]

        // Step 0 - Aba 01
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepConfiguracaoPrincipal(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);
                model.OperacaoFix = model.Operacao;
                if (model.IsNew())
                {
                    switch (model.Operacao)
                    {
                        case OperacaoTurma.Novo:
                            model.SituacaoTurmaAtual = SituacaoTurma.Ofertada;
                            break;

                        case OperacaoTurma.Copiar:
                        case OperacaoTurma.Desdobrar:
                            model.CodigoFormatado = $"{model.Codigo}.{model.Numero}";
                            break;
                    }
                }
                else
                    model.CodigoFormatado = $"{model.Codigo}.{model.Numero}";

                //Preenche o step origem
                model.StepOrigem = 0;

                //Refazer cabeçalho nos próximos passos
                model.Cabecalho = null;

                model.SeqsMatrizCurricular = MatrizCurricularService.BuscarSeqsMatrizCurricularUsuarioLogado();

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public JsonResult PreencherCampoDataInicioPeriodoLetivo(long? seqCicloLetivoInicio)
        {
            return Json(string.Empty);
        }

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public JsonResult PreencherCampoDataFimPeriodoLetivo(long? seqCicloLetivoInicio)
        {
            return Json(string.Empty);
        }

        #endregion [ CONFIGURAÇÃO PRINCIPAL - Step 00 - Aba 01 ]

        #region [ CONFIGURAÇÃO DE COMPARTILHAMENTO - Step 01 - Aba 02 ]

        // Step 1 - Aba 02
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepConfiguracaoCompartilhamento(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                this.TurmaService.ValidarDatasPeriodoLetivo(model.Transform<TurmaData>());

                //Evita validar novamente os mesmos dados
                if (model.StepOrigem < model.Step)
                {
                    //Preenche o Ciclo Letivo Fim de acordo com o Ciclo Letivo Inicio (Provisório até criar a tabela de Ciclo Letivo Turma)
                    model.SeqCicloLetivoFim = model.SeqCicloLetivoInicio;

                    if (model.Cabecalho == null || model.QuantidadeVagas.GetValueOrDefault() != model.Cabecalho.Vagas)
                        model.Cabecalho = PreencherCabecalho(model);

                    //Verifica se a configuração de componente principal exige assunto
                    //if (!TipoTurmaService.VerificarConfiguracaoAssunto(model.ConfiguracaoComponente.Seq.Value))
                    //    throw new Exception(UIResource.MSG_Erro_TipoTurmaExigeAssociacao);

                    //Verifica se a configuração de componente possui grupo componente e matriz curricular
                    ValidarConfiguracaoComponentePossuiGrupoComponenteEMatrizCurricular(model.ConfiguracaoComponente.Seq.Value);

                    if (model.Seq > 0)
                    {
                        var quantidadeVagasOcupadas = TurmaService.BuscarQuantidadeVagasOcupadasTurma(model.Seq);
                        if (model.QuantidadeVagas < quantidadeVagasOcupadas)
                            throw new Exception(string.Format(UIResource.MSG_Erro_QuantidadeVagasOcupadas, quantidadeVagasOcupadas.ToString()));

                        if (model.SituacaoTurmaAtual == SituacaoTurma.Cancelada)
                        {
                            PreencherCompartilhado(model);
                            PreencherOfertaMatriz(model);
                            PreencherParametros(model);
                            PreencherDivisoes(model);
                            PreencherComponenteSubstituto(model);

                            model.Step = 5;
                            return StepDadosTurma(model);
                        }
                    }

                    PreencherCompartilhado(model);
                }
                else
                {
                    if (model.TurmaOfertasMatriz != null)
                    {
                        foreach (var item in model.TurmaOfertasMatriz)
                            item.OfertasMatriz?.UndoClear();
                    }
                }

                //Exibir mensagem se a configuração tiver ofertas associadas
                string retorno = string.Empty;
                if (model.TurmaOfertasMatriz != null && model.TurmaOfertasMatriz.Where(w => w.SeqConfiguracaoComponente != model.SeqConfiguracaoComponentePrincipal && w.OfertasMatriz != null).SMCCount() > 0)
                {
                    model.TurmaOfertasMatriz.Where(w => w.SeqConfiguracaoComponente != model.SeqConfiguracaoComponentePrincipal).SMCForEach(f => retorno += f.DescricaoConfiguracaoComponente + ", <br />");
                    model.MensagemConfiguracaoCompartilhada = string.Format(UIResource.MSG_Informacao_GrupoOferta, retorno);
                }

                //Preenche o step origem
                model.StepOrigem = 1;

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        #endregion [ CONFIGURAÇÃO DE COMPARTILHAMENTO - Step 01 - Aba 02 ]

        #region [ OFERTAS DE MATRIZ - Step 02 - Aba 03 ]

        // Step 2 - Aba 03
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepOfertaMatriz(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                //Evita validar novamente os mesmos dados
                if (model.StepOrigem < model.Step)
                {
                    if (model.Cabecalho == null)
                        model.Cabecalho = PreencherCabecalho(model);

                    if (model.SeqComponenteCurricularPrincipal.HasValue)
                    {
                        var componente = ComponenteCurricularService.BuscarComponenteCurricular(model.SeqComponenteCurricularPrincipal.Value);
                        model.ExigeAssuntoComponente = componente.ExigeAssuntoComponente;
                    }

                    if (Request.Form[nameof(model.GridConfiguracaoCompartilhada)] == null)
                        model.GridConfiguracaoCompartilhada = new List<object>();

                    if (model.GrupoConfiguracoesCompartilhadas == null) { model.GrupoConfiguracoesCompartilhadas = new List<TurmaGrupoConfiguracaoViewModel>(); }

                    if (model.TurmaOfertasMatriz == null && model.Seq == 0)
                        model.TurmaOfertasMatriz = new List<TurmaOfertaMatrizViewModel>();

                    VincularConfiguracaoCompartilhada(model);

                    // Validar Compartilhamentos Removidos
                    var seqsConfiguracoesCompartilhadas = model.GridConfiguracaoCompartilhada.Select(x => long.Parse(x.ToString())).ToList();
                    ValidarConfiguracaoCompartilhdaRemovida(model.Seq, seqsConfiguracoesCompartilhadas);

                    /// Valida se a configuração compartilhada, está associada com as permissões do usuário
                    /// /*Valida apenas quando for Turma Compartilhada*/
                    ValidarUsuarioPermissaoConfiguracaoComponenteSelecionada(model.GridConfiguracaoCompartilhada);

                    //Verifica se a configuração de componente possui grupo componente e matriz curricular
                    ValidarConfiguracaoComponentePossuiGrupoComponenteEMatrizCurricular(model.GrupoConfiguracoesCompartilhadas);

                    model.TurmaOfertasMatriz = model.TurmaOfertasMatriz.Where(w => model.GridConfiguracaoCompartilhada.Contains(w.SeqConfiguracaoComponente.ToString())).ToList();

                    PreencherOfertaMatriz(model);
                }

                if (model.TurmaOfertasMatriz.Count > 0)
                    model.TurmaOfertasMatriz[0].ConfiguracaoPrincipal = LegendaPrincipal.Principal;

                //Exibir mensagem se a oferta tiver um critério associado
                //if (model.OrigemAvaliacao != null && model.OrigemAvaliacao.Seq > 0)
                //    model.MensagemOfertaCriterios = string.Format(UIResource.MSG_Informacao_CriterioAssociado);

                //Preenche o step origem
                model.StepOrigem = 2;

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        private void ValidarConfiguracaoCompartilhdaRemovida(long seqTurma, List<long> seqsConfiguracoesCompartilhadas)
        {
            TurmaService.ValidarConfiguracaoCompartilhdaRemovida(seqTurma, seqsConfiguracoesCompartilhadas);
        }

        private void VincularConfiguracaoCompartilhada(TurmaDynamicModel model)
        {
            model.GridConfiguracaoCompartilhada = new List<object>();
            model.GridConfiguracaoCompartilhada.Add(model.ConfiguracaoComponente.Seq.ToString());
            model.GridConfiguracaoCompartilhada.AddRange(model.GrupoConfiguracoesCompartilhadas.Where(c => c.Selecionado).Select(x => x.SeqConfiguracaoComponente.ToString()));
        }

        #endregion [ OFERTAS DE MATRIZ - Step 02 - Aba 03 ]

        #region [ PARÂMETROS (DESATIVADA) ]

        // Step 03 - Aba 04
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepDivisoesParametros(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                //Evita validar novamente os mesmos dados
                if (model.StepOrigem < model.Step)
                {
                    if (model.Cabecalho == null)
                        model.Cabecalho = PreencherCabecalho(model);

                    if (model.StepOrigem != 0)
                    {
                        RemoverOfertaMatrizVazia(model.TurmaOfertasMatriz);

                        //- Exigir mais de uma oferta de matriz e existir mais de uma configuração de componente associada,
                        //    é obrigatório associar pelo menos uma oferta de matriz para cada configuração associada.
                        ValidarConfiguracaoSemOfertaMatriz(model.TurmaOfertasMatriz);

                        var codigoOfertas = new List<string>();

                        /*A quantidade de vagas reservadas para a matriz, não pode receber um valor menor que a
                         * quantidade de vagas ocupadas na matriz em questão. */

                        //FIX: Bind
                        IEnumerable<MatrizCurricularOfertaData> ofertasVagas = null;
                        var seqsComponentesConsulta = new List<long>();
                        foreach (var ofertaMatriz in model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz))
                        {
                            if (ofertaMatriz.ReservaVagas < ofertaMatriz.QuantidadeVagasOcupadas)
                            {
                                seqsComponentesConsulta.Add(ofertaMatriz.SeqMatrizCurricularOferta.Seq);
                            }
                        }
                        if (seqsComponentesConsulta.SMCAny())
                        {
                            ofertasVagas = MatrizCurricularOfertaService
                                .BuscarMatrizesCurricularLookupOferta(new MatrizCurricularOfertaFiltroData() { Seqs = seqsComponentesConsulta, IgnorarFiltroDados = true });
                        }

                        foreach (var ofertaMatriz in model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz))
                        {
                            if (ofertaMatriz.ReservaVagas < ofertaMatriz.QuantidadeVagasOcupadas)
                            {
                                //codigoOfertas.Add($"{ofertaMatriz.SeqMatrizCurricularOferta.Codigo}: {ofertaMatriz.QuantidadeVagasOcupadas} vagas ocupadas");
                                //FIX: Bind
                                codigoOfertas.Add($"{ofertasVagas.FirstOrDefault(f => f.Seq == ofertaMatriz.SeqMatrizCurricularOferta.Seq)?.Codigo }: {ofertaMatriz.QuantidadeVagasOcupadas} vagas ocupadas");
                            }
                        }
                        if (codigoOfertas.SMCAny())
                        {
                            throw new Exception(string.Format(UIResource.MSG_Erro_OfertaMatrizQtdVagasReservadasMenorVagasOcupadas, string.Join("<br />", codigoOfertas.ToArray())));
                        }

                        var quantidadeTotal = model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Sum(s => s.ReservaVagas);
                        if (quantidadeTotal.HasValue && quantidadeTotal.Value > 0 && model.QuantidadeVagas != quantidadeTotal)
                            throw new Exception(UIResource.MSG_Erro_NumeroVagasOferta);

                        var ofertaComReserva = model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Any(a => a.ReservaVagas > 0);
                        var ofertaSemReserva = model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Any(a => a.ReservaVagas == null || a.ReservaVagas == 0);
                        if (ofertaComReserva && ofertaSemReserva)
                            throw new Exception(UIResource.MSG_Erro_NumeroVagasCadaOferta);

                        if (model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Count(c => c.OfertaMatrizPrincipal) > 1)
                            throw new Exception(UIResource.MSG_Erro_OfertaMatrizPrincipal);

                        if (model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Count(c => c.OfertaMatrizPrincipal) == 0)
                            throw new Exception(UIResource.MSG_Erro_OfertaMatrizPrincipalNenhum);

                        var ofertaMatrizPrincipal = model.TurmaOfertasMatriz.FirstOrDefault(c => c.OfertasMatriz.Any(d => d.OfertaMatrizPrincipal));
                        if (ofertaMatrizPrincipal.ConfiguracaoPrincipal != LegendaPrincipal.Principal)
                        {
                            throw new Exception(UIResource.MSG_Erro_MatrizDevePossuirConfiguracaoComponentePrincipal);
                        }

                        /*  Caso uma oferta de matriz for excluída:
                                1. Verificar se a turma existe no plano de estudo atual de algum aluno:
                                    1.1. Se existir, verificar se a oferta de matriz a ser excluída é a oferta de matriz associada ao plano de estudos atual de pelo menos um aluno matriculado na turma.

                                2. Verificar se a turma existe em alguma solicitação de serviço ativa*:
                                    2.1. Se existir e o solicitante for aluno, verificar se a oferta de matriz a ser excluída é a oferta de matriz associada ao plano de estudos atual de pelo menos um aluno que selecionou a turma na solicitação.

                                2.2. Se existir e o solicitante for ingressante, verificar se a oferta de matriz a ser excluída é a oferta associada a pelo menos um ingressante que selecionou a turma na solicitação.

                                Caso uma destas condições ocorrer, abortar a operação e exibir a seguinte mensagem de erro:
                                    "Operação não permitida. Existem alunos associados às ofertas de matriz:
                                    - <código da oferta de matriz 1>
                                    - <código da oferta de matriz 2>"

                                    Onde, <código da oferta de matriz> deve ser substituído pelo código das ofertas de matriz que foram listadas nas consistências acima.

                                    * Para encontrar solicitações ativas: verificar se a situação atual da solicitação foi parametrizada para ser final de processo. Caso tenha sido, a solicitação está inativa, caso não tenha sido a solicitação está ativa.
                        */

                        var seqsMatrizesCurricularesOfertasExcluidas = model.TurmaOfertasMatriz.SelectMany(t => t.OfertasMatriz?.RemovedItems?.Cast<TurmaOfertaMatrizDetailViewModel>()).Where(e => e.Seq.HasValue && e.Seq.Value > 0).Select(e => e.SeqMatrizCurricularOferta.Seq).ToList();
                        if (seqsMatrizesCurricularesOfertasExcluidas != null && seqsMatrizesCurricularesOfertasExcluidas.Any())
                            ValidarOfertaMatrizExcluidas(model.Seq, seqsMatrizesCurricularesOfertasExcluidas);

                        ValidarOfertaMatrizAssociadaAssunto(model);

                        // Valida as ofertas de matriz com o perfil da hierarquia de entidade do usuário
                        ValidarOfertaMatrizPerfilUsuario(model);

                        ValidarConfiguracaoAssociadaOfertaMatriz(model);

                        ValidarOfertaMatrizAtiva(model);
                    }
                }
                else
                {
                    TempData["DivisoesLocalidades"] = model.DivisoesLocalidades;
                    // Preenche o step origem
                    model.StepOrigem = 3;

                    // Retorna o passo do wizard
                    return SMCViewWizard(model, null);
                }

                PreencherParametros(model);

                if (model.TurmaParametros.Count > 0)
                    model.TurmaParametros[0].ConfiguracaoPrincipal = LegendaPrincipal.Principal;

                ////Preenche o step origem
                //model.StepOrigem = 3;

                return StepDivisoes(model, model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqMatrizCurricularOferta.Seq);
                // Retorna o passo do wizard
                //return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        private void ValidarOfertaMatrizExcluidas(long seqTurma, List<long> seqsMatrizesCurricularesOfertasExcluidas)
        {
            DivisaoTurmaService.ValidarOfertaMatrizExcluidas(seqTurma, seqsMatrizesCurricularesOfertasExcluidas);
        }

        #endregion [ PARÂMETROS (DESATIVADA) ]

        #region [ DIVISÕES Step 03 - Aba 04 ]

        // Step 03 - Aba 04
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepDivisoes(TurmaDynamicModel model, long? ParametrosConteudo)
        {
            try
            {
                this.ConfigureDynamic(model);

                //Evita validar novamente os mesmos dados
                if (model.StepOrigem < model.Step)
                {
                    if (model.Cabecalho == null)
                        model.Cabecalho = PreencherCabecalho(model);

                    if (ParametrosConteudo > 0)
                    {
                        model.TurmaParametros.SelectMany(s => s.ParametrosOfertas).SMCForEach(f => f.Selecionado = false);
                        model.TurmaParametros.SelectMany(s => s.ParametrosOfertas).Where(w => w.SeqOfertaMatriz == ParametrosConteudo).First().Selecionado = true;
                        TempData["ParametrosConteudo"] = ParametrosConteudo;
                    }
                    else
                    {
                        if (model.TurmaParametros.Any(a => a.ExiteOfertasConfiguracao))
                            throw new TurmaParametroObrigatorioInvalidoException();
                    }

                    //Validação da RV025 deve ser realizada mesmo em registro de edição
                    //var criterio = model.CriteriosAprovacao.FirstOrDefault(f => f.Seq == model.TurmaParametros.SelectMany(s => s.ParametrosOfertas).FirstOrDefault().SeqCriterioAprovacao);
                    //if (criterio != null)
                    //{
                    //    if (criterio.DataAttributes.FirstOrDefault(f => f.Key == "apura-frequencia").Value == "True")
                    //    {
                    //        if (!model.TurmaParametros.SelectMany(s => s.ParametrosOfertas).Any(a => a.DivisoesComponente.Any(d => d.ApurarFrequencia)))
                    //            throw new TurmaParametroApurarFrequenciaInvalidoException();
                    //    }
                    //}

                    PreencherDivisoes(model);
                }

                //Preenche o step origem
                model.StepOrigem = 3;

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        #endregion [ DIVISÕES Step 03 - Aba 04 ]

        #region [ ASSUNTO (COMPONENTE SUBSTITUTO) - Step 04 - Aba 05 ]

        // Step 04 - Aba 05
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepComponenteSubstituto(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                this.TurmaService.ValidarExclusaoDivisaoGrupos(model.Transform<TurmaData>());
                this.TurmaService.ValidarQuantidadeVagasOcupadasDivisoes(model.Transform<TurmaData>());
                this.TurmaService.ValidarQuantidadeGrupos(model.Transform<TurmaData>());

                //Evita validar novamente os mesmos dados
                if (model.StepOrigem < model.Step)
                {
                    if (model.Cabecalho == null)
                        model.Cabecalho = PreencherCabecalho(model);

                    //if (model.TurmaDivisoes.SelectMany(s => s.DivisoesComponentes).Sum(t => t.DivisaoVagas) != model.QuantidadeVagas)
                    if (model.TurmaDivisoes.Any(td => td.DivisoesComponentes.Sum(dc => dc.DivisaoVagas) != model.QuantidadeVagas))
                        throw new TurmaDivisaoVagasInvalidoException();

                    if (model.DivisoesTurma.Count > 0)
                    {
                        foreach (var item in model.TurmaDivisoes)
                        {
                            var divisaoTurmaCadastrado = model.DivisoesTurma.FirstOrDefault(f => f.SeqDivisaoComponente == item.SeqDivisaoComponente);
                            if (divisaoTurmaCadastrado != null && item.DivisoesComponentes.Sum(s => s.DivisaoVagas) - divisaoTurmaCadastrado.QuantidadeVagasOcupadas < 0)
                                throw new TurmaDivisaoVagasOcupadasInvalidoException();
                        }
                    }
                }
                if (model.SeqComponenteCurricularPrincipal.HasValue)
                {
                    var componente = ComponenteCurricularService.BuscarComponenteCurricular(model.SeqComponenteCurricularPrincipal.Value);
                    model.ExigeAssuntoComponente = componente.ExigeAssuntoComponente;
                    if (!componente.ExigeAssuntoComponente)
                    {
                        model.Step = 5;
                        StepDadosTurma(model);
                    }
                    else
                    {
                        var seqsMatrizCurricularOferta = model?.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Select(m => m.SeqMatrizCurricularOferta.Seq).ToList();

                        var seqsConfiguracoesComponente = model.GridConfiguracaoCompartilhada.Select(z => long.Parse(z.ToString()));

                        model.ComponentesAssuntos = DivisaoMatrizCurricularComponenteService.BuscarAssuntosComponentesOfertasMatrizesTurma(seqsMatrizCurricularOferta, seqsConfiguracoesComponente.ToList()).ToList();
                    }
                }
                //Preenche o step origem
                model.StepOrigem = 4;

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        #endregion [ ASSUNTO (COMPONENTE SUBSTITUTO) - Step 04 - Aba 05 ]

        #region [ DADOS DA TURMA - Step 05 - Aba 04 ]

        // Step 05 - Aba 06
        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult StepDadosTurma(TurmaDynamicModel model)
        {
            try
            {
                this.ConfigureDynamic(model);

                if (model.Cabecalho == null)
                    model.Cabecalho = PreencherCabecalho(model);

                ValidarComponenteAssunto(model);

                //Preenche as divisões com os detalhes de localidade
                foreach (var item in model.TurmaDivisoes)
                {
                    item.DivisoesComponentesDisplay = item.DivisoesComponentes.TransformMasterDetailList<TurmaDivisoesDetailDisplayViewModel>();
                    foreach (var divisaoLocalidade in item.DivisoesComponentesDisplay)
                        divisaoLocalidade.DescricaoLocalidade = model.DivisoesLocalidades.FirstOrDefault(f => f.Seq == divisaoLocalidade.SeqLocalidade)?.Descricao;
                }

                //Buscar as divisões da configuração de componente na matriz da oferta matriz selecionada
                foreach (var item in model.TurmaOfertasMatriz)
                {
                    if (item.OfertasMatriz.Count > 0)
                        item.OfertasMatrizDisplay = new SMCMasterDetailList<TurmaOfertaMatrizDetailDisplayViewModel>();

                    foreach (var detalhe in item.OfertasMatriz)
                    {
                        var ofertaDisplay = new TurmaOfertaMatrizDetailDisplayViewModel();
                        var specOfertaDispaly = new DivisaoMatrizCurricularComponenteFiltroData()
                        {
                            SeqConfiguracaoComponente = item.SeqConfiguracaoComponente,
                            SeqMatrizCurricularOferta = detalhe.SeqMatrizCurricularOferta.Seq
                        };
                        var divisaoOferta = DivisaoMatrizCurricularComponenteService.BuscarDivisaoMatrizCurricularComponente(specOfertaDispaly);

                        ofertaDisplay.OfertaMatrizPrincipal = detalhe.OfertaMatrizPrincipal;
                        ofertaDisplay.OfertaCompleto = detalhe.SeqMatrizCurricularOferta.OfertaCompleto;
                        ofertaDisplay.ReservaVagas = detalhe.ReservaVagas;
                        item.OfertasMatrizDisplay.Add(ofertaDisplay);
                    }
                }

                if (model.SeqConfiguracaoComponentePrincipal.HasValue)
                {
                    model.CriteriosAprovacao = CriterioAprovacaoService.BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(model.SeqConfiguracaoComponentePrincipal.Value);
                    model.EscalasApuracao = EscalaApuracaoService.BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(model.SeqConfiguracaoComponentePrincipal.Value);
                }

                TempData["CriteriosAprovacao"] = model.CriteriosAprovacao;
                TempData["EscalasApuracao"] = model.EscalasApuracao;

                PreencherComponenteSubstituto(model);

                PreencherTipoTurma(model);

                //Preenche o step origem
                model.StepOrigem = 5;

                // Retorna o passo do wizard
                return SMCViewWizard(model, null);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                throw ex;
            }
        }

        private void PreencherTipoTurma(TurmaDynamicModel model)
        {
            // Busca os tipos de turmas
            var tiposTurma = TipoTurmaService.BuscarTiposTurmasPorComponenteCurricular(model.SeqComponenteCurricularPrincipal.Value, model.TurmaOfertasMatriz.SelectMany(t => t.OfertasMatriz).Select(t => t.SeqMatrizCurricularOferta.Seq).ToList());

            /*
                O campo “Tipo de turma” no cabeçalho do passo 6 deverá ser calculado todas as vezes que a interface for exibida, conforme:
                Verificar se a turma possui mais de uma oferta de matriz associada.
                - Caso existir, associar o tipo de turma que exige mais de uma oferta de matriz e está parametrizado por instituição-nível, de acordo com a instituição do componente da configuração principal e em comum em todos os níveis de ensino dos cursos dos currículos das ofertadas de matriz associadas na turma.
                - Caso não existir, associar o tipo de turma que exige apenas uma oferta de matriz e está parametrizado por instituição-nível, de acordo com a instituição do componente da configuração principal e em comum em todos os níveis de ensino dos cursos dos currículos das ofertadas de matriz associadas na turma.
             */

            model.SeqTipoTurma = null;
            var totalOfertas = model.TurmaOfertasMatriz?.SelectMany(t => t.OfertasMatriz).Count();
            if (totalOfertas.GetValueOrDefault() > 1)
            {
                model.SeqTipoTurma = tiposTurma.FirstOrDefault(t => t.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeMaisDeUma).Seq;
                model.Cabecalho.DescricaoTipoTurma = tiposTurma.FirstOrDefault(t => t.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeMaisDeUma).Descricao;
            }
            else
            {
                model.SeqTipoTurma = tiposTurma.FirstOrDefault(t => t.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeApenasUma).Seq;
                model.Cabecalho.DescricaoTipoTurma = tiposTurma.FirstOrDefault(t => t.AssociacaoOfertaMatriz == AssociacaoOfertaMatriz.ExigeApenasUma).Descricao;
            }
        }

        #endregion [ DADOS DA TURMA - Step 05 - Aba 04 ]

        #endregion [ WIZARD ]

        #region [ Cancelar Turma ]

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult CancelarTurma(SMCEncryptedLong seq, string situacaoJustificativa)
        {
            try
            {
                TurmaService.CancelarTurma(seq, situacaoJustificativa);

                SetSuccessMessage("Turma cancelada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "Turma");
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                ThrowRedirect(ex, "index", "Turma");
                throw ex;
            }
        }

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult ModalCancelarTurma(SMCEncryptedLong seq)
        {
            try
            {
                var model = TurmaService.BuscarTurma(seq).Transform<TurmaDynamicModel>();

                PreencherCompartilhado(model);

                // Verifica se é permitido cancelar a turma
                TurmaService.ValidarCancelarTurma(model.Transform<TurmaData>());

                // Preenche o cabeçaho do modelo
                model.Cabecalho = PreencherCabecalho(model);

                // Preenche as configurações do cabeçalho
                var filtroComponente = new ConfiguracaoComponenteFiltroData()
                {
                    Seq = model.SeqConfiguracaoComponentePrincipal.Value,
                    IgnorarFiltroDados = true
                };
                model.ConfiguracaoComponente = ConfiguracaoComponenteService.BuscarConfiguracoesComponentesLookup(filtroComponente).First().Transform<ConfiguracaoComponenteLookupViewModel>();
                PreencherTurmaConfiguracaoCabecalho(model);

                // Chama a modal de cancelamento
                return PartialView("_CancelarTurma", model);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                ThrowRedirect(ex, "index", "Turma");
                throw ex;
            }
        }

        #endregion [ Cancelar Turma ]

        #region [ Ofertar Turma ]

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult OfertarTurma(SMCEncryptedLong seq)
        {
            try
            {
                TurmaService.OfertarTurma(seq);

                SetSuccessMessage("Turma ofertada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", "Turma");
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                ThrowRedirect(ex, "index", "Turma");
                throw ex;
            }
        }

        #endregion [ Ofertar Turma ]

        #region [ Métodos Privados ]

        private void RemoverOfertaMatrizVazia(List<TurmaOfertaMatrizViewModel> turmaOfertasMatriz)
        {
            if (turmaOfertasMatriz.SMCAny())
            {
                turmaOfertasMatriz.ForEach(o => { o.OfertasMatriz?.RemoveAll(x => x.SeqMatrizCurricularOferta.Seq == 0); });
            }
        }

        /// <summary>
        /// Verificar se todas as ofertas de matriz estão com situação "Ativa" e/ou "Em extinção"
        /// no ciclo letivo selecionado no Passo "Seleção da Oferta de Matriz Principal".
        /// </summary>
        /// <param name="model"></param>
        private void ValidarOfertaMatrizAtiva(TurmaDynamicModel model)
        {
            var seqsMatrizOfertaCurricularOfertas = model.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Select(m => m.SeqMatrizCurricularOferta.Seq)).ToList();
            if (!seqsMatrizOfertaCurricularOfertas.SMCAny()) { return; }

            var ofertasAtivasOuEmExtincao = MatrizCurricularOfertaService.BuscarMatrizesCurricularLookupOferta(new MatrizCurricularOfertaFiltroData() { Seqs = seqsMatrizOfertaCurricularOfertas, SeqCicloLetivo = model.SeqCicloLetivoInicio?.Seq, IgnorarFiltroDados = true });
            var ofertasMatrizInativas = new List<string>();

            //FIX: Bind
            var seqsMatrizCurricualrOferta = new List<long>();

            if (!ofertasAtivasOuEmExtincao.SMCAny())
            {
                //ofertasMatrizInativas = model.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Select(m => m.SeqMatrizCurricularOferta.Codigo)).ToList();
                //FIX: Bind
                seqsMatrizCurricualrOferta.AddRange(model.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Select(m => m.SeqMatrizCurricularOferta.Seq)));
            }
            else
            {
                foreach (var seqOfertaMatriz in seqsMatrizOfertaCurricularOfertas)
                {
                    var oferta = ofertasAtivasOuEmExtincao.FirstOrDefault(x => x.Seq == seqOfertaMatriz);
                    if (oferta == null)
                    {
                        //var ofertaCodigo = model.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Where(z => z.SeqMatrizCurricularOferta.Seq == seqOfertaMatriz).Select(m => m.SeqMatrizCurricularOferta.Codigo)).FirstOrDefault();
                        //ofertasMatrizInativas.Add(ofertaCodigo);
                        //FIX: Bind
                        seqsMatrizCurricualrOferta.Add(model.TurmaOfertasMatriz.SelectMany(o => o.OfertasMatriz.Where(z => z.SeqMatrizCurricularOferta.Seq == seqOfertaMatriz).Select(m => m.SeqMatrizCurricularOferta.Seq)).FirstOrDefault());
                    }
                }
            }
            //if (ofertasMatrizInativas.SMCAny())
            //FIX: Bind
            if (seqsMatrizCurricualrOferta.SMCAny())
            {
                //FIX: Bind
                var ofertasLookup = MatrizCurricularOfertaService.BuscarMatrizesCurricularLookupOferta(new MatrizCurricularOfertaFiltroData() { Seqs = seqsMatrizCurricualrOferta, IgnorarFiltroDados = true });
                ofertasMatrizInativas = ofertasLookup.Select(s => s.Codigo).ToList();
                /*"Não é possível prosseguir. As seguintes ofertas de matriz não estão ativas ou em extinção no período do ciclo letivo selecionado para a turma:
                - < código da oferta de matriz 1 >
                 - < código da oferta de matriz 2 >"*/
                throw new Exception(string.Format(UIResource.MSG_Erro_OfertaMatrizNaoAtiva, string.Join("<br /> - ", ofertasMatrizInativas.ToArray())));
            }
        }

        /// <summary>
        /// Verificar se as configurações de componente estão associadas às suas respectivas ofertas de matriz.
        /// </summary>
        /// <param name="model"></param>
        private void ValidarConfiguracaoAssociadaOfertaMatriz(TurmaDynamicModel model)
        {
            var ofertasSemAssociacao = new List<string>();

            //FIX: Bind
            var ofertas = new List<(string componete, long oferta)>();

            foreach (var turmaOfertaMatriz in model.TurmaOfertasMatriz)
            {
                foreach (var matrizCurricularOferta in turmaOfertaMatriz.OfertasMatriz)
                {
                    if (!MatrizCurricularOfertaService.ValidarMatrizCurricularOfertaConfiguracaoComponente(matrizCurricularOferta.SeqMatrizCurricularOferta.Seq, turmaOfertaMatriz.SeqConfiguracaoComponente))
                    {
                        //ofertasSemAssociacao.Add($"{turmaOfertaMatriz.DescricaoConfiguracaoComponente};{matrizCurricularOferta.SeqMatrizCurricularOferta.Codigo}");
                        //FIX: Bind
                        ofertas.Add((turmaOfertaMatriz.DescricaoConfiguracaoComponente, matrizCurricularOferta.SeqMatrizCurricularOferta.Seq));
                    }
                }
            }

            //if (ofertasSemAssociacao.SMCAny())
            //FIX: Bind
            if (ofertas.SMCAny())
            {
                //FIX: Bind
                var lookupsOferta = MatrizCurricularOfertaService
                    .BuscarMatrizesCurricularLookupOferta(new MatrizCurricularOfertaFiltroData() { Seqs = ofertas.Select(s => s.oferta).ToList(), IgnorarFiltroDados = true });
                ofertasSemAssociacao = ofertas.Select(s => $"{s.componete};{lookupsOferta.FirstOrDefault(f => f.Seq == s.oferta)?.Codigo}").ToList();

                //"Não é possível prosseguir. A configuração <descrição da configuração> não está assoaida à oferta de matriz <código da oferta de matriz>."

                var mensagens = new List<string>();
                mensagens.Add("Não é possível prosseguir.");
                ofertasSemAssociacao.SMCForEach(o =>
                {
                    var info = o.Split(';');
                    mensagens.Add(string.Format(UIResource.MSG_Erro_ConfiguracaoNaoAssociadaOfertaMatriz, info[0], info[1]));
                });

                throw new Exception(string.Join("<br />", mensagens.ToArray()));
            }
        }

        /// <summary>
        /// Valida as ofertas de matriz com o perfil da hierarquia de entidade do usuário
        /// </summary>
        /// <param name="model"></param>
        private void ValidarOfertaMatrizPerfilUsuario(TurmaDynamicModel model)
        {
            var seqsMatrizCurricularOferta = model.TurmaOfertasMatriz.SelectMany(x => x.OfertasMatriz.Select(o => o.SeqMatrizCurricularOferta.Seq)).ToList();
            if (!MatrizCurricularOfertaService.ValidarMatrizCurricularOfertas(seqsMatrizCurricularOferta))
            {
                throw new Exception(UIResource.MSG_Erro_UsuarioSemPermissaoMatrizOfertaException);
            }
        }

        /// <summary>
        /// Método para validar permissão, conforme filtro de dados do usuários
        /// às configurações compartilhadas e a principal.
        /// </summary>
        /// <param name="grupoConfiguracoesCompartilhadas"></param>
        private void ValidarUsuarioPermissaoConfiguracaoComponenteSelecionada(List<object> grupoConfiguracoesCompartilhadas)
        {
            var seqsConfiguracoesComponente = grupoConfiguracoesCompartilhadas.Select(z => long.Parse(z.ToString()));

            if (!seqsConfiguracoesComponente.SMCAny()) { return; }

            var configuracoes = ConfiguracaoComponenteService.BuscarConfiguracoesComponentesLookup(new ConfiguracaoComponenteFiltroData() { SeqConfiguracoesComponentes = ConvertArray(seqsConfiguracoesComponente.ToArray()) })?.FirstOrDefault().Transform<ConfiguracaoComponenteLookupViewModel>();
            if (configuracoes == null
                || !configuracoes.DivisoesComponente.SMCAny()
                || !configuracoes.DivisoesComponente.Any(X => seqsConfiguracoesComponente.Contains(X.SeqConfiguracaoComponente)))
            {
                throw new Exception(UIResource.MSG_Erro_UsuarioSemPermissaoConfiguracaoComponente);
            }
        }

        /// <summary>
        /// Converter Array<T> para Array<T?>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        private static T?[] ConvertArray<T>(T[] array) where T : struct
        {
            T?[] nullableArray = new T?[array.Length];

            for (int i = 0; i < array.Length; i++)

                nullableArray[i] = array[i];

            return nullableArray;
        }

        /// <summary>
        /// Na alteração, se a configuração principal exigir assunto, verificar se a turma está em uma solicitação ativa*
        /// ou em um plano de estudo atual. Se estiver, verificar se o assunto de componente da turma está associado a
        /// todas as ofertas de matriz selecionadas, de acordo com a configuração de componente relacionada a cada oferta de matriz.
        ///</summary>
        /// <param name="model"></param>
        private void ValidarOfertaMatrizAssociadaAssunto(TurmaDynamicModel model)
        {
            if (model.Seq > 0)
            {
                if (model.ExigeAssuntoComponente && model.ExisteSolicitacaoMatriculaOuPlanoEstudo && model.SeqComponenteCurricularAssunto.HasValue)
                {
                    var ofertas = model.TurmaOfertasMatriz.SelectMany(t => t.OfertasMatriz).ToList();

                    var codigosOfertas = new List<string>();

                    //FIX: Bind
                    var seqsOfertas = new List<long>();

                    foreach (var matriz in model.TurmaOfertasMatriz)
                    {
                        foreach (var oferta in matriz.OfertasMatriz)
                        {
                            var componentesAssuntos = DivisaoMatrizCurricularComponenteService.BuscarAssuntosComponentesOfertasMatrizesTurma(new List<long>() { oferta.SeqMatrizCurricularOferta.Seq }, new List<long>() { matriz.SeqConfiguracaoComponente }).ToList();

                            if (!componentesAssuntos.SMCAny() || componentesAssuntos.FirstOrDefault(x => x.Seq == model.SeqComponenteCurricularAssunto) == null)
                            {
                                var ofertaSemAssociacao = ofertas.FirstOrDefault(x => x.SeqMatrizCurricularOferta.Seq == oferta.SeqMatrizCurricularOferta.Seq);
                                //codigosOfertas.Add(ofertaSemAssociacao.SeqMatrizCurricularOferta.Codigo);
                                //FIX: Bind
                                seqsOfertas.Add(ofertaSemAssociacao.SeqMatrizCurricularOferta.Seq);
                            }
                        }
                    }
                    //if (codigosOfertas.SMCAny())
                    //FIX: Bind
                    if (seqsOfertas.SMCAny())
                    {
                        var lookups = MatrizCurricularOfertaService.BuscarMatrizesCurricularLookupOferta(new MatrizCurricularOfertaFiltroData() { Seqs = seqsOfertas, IgnorarFiltroDados = true });
                        codigosOfertas = lookups.Select(s => s.Codigo).ToList();

                        throw new Exception(string.Format(UIResource.MSG_Erro_OfertaMatrizNaoAssociadaAssunto, string.Join("<br /> - ", codigosOfertas.ToArray())));
                    }
                }
            }
        }

        /// <summary>
        /// Associar pelo menos uma oferta de matriz para cada configuração associada
        /// </summary>
        /// <param name="turmaOfertasMatriz"></param>
        private void ValidarConfiguracaoSemOfertaMatriz(List<TurmaOfertaMatrizViewModel> turmaOfertasMatriz)
        {
            if (turmaOfertasMatriz.Count() >= 1 && turmaOfertasMatriz.Where(s => s.OfertasMatriz.Where(o => o.SeqMatrizCurricularOferta.Seq > 0).Count() == 0).Count() > 0)
                throw new Exception(UIResource.MSG_Erro_OfertaMatrizMaisConfiguracaoMaisOfertas);
        }

        /// <summary>
        /// Verifica se a configuração de componente possui grupo componente e matriz curricular
        /// </summary>
        /// <param name="value"></param>
        private void ValidarConfiguracaoComponentePossuiGrupoComponenteEMatrizCurricular(long seqConfiguracaoComponente)
        {
            if (!ConfiguracaoComponenteService.VerificaConfiguracaoComponentePertenceCurriculo(seqConfiguracaoComponente))
                throw new Exception(UIResource.MSG_Erro_ConfiguracaoComponenteCurriculo);
        }

        /// <summary>
        /// Verifica se a configuração de componente possui grupo componente e matriz curricular
        /// </summary>
        /// <param name="value"></param>
        private void ValidarConfiguracaoComponentePossuiGrupoComponenteEMatrizCurricular(List<TurmaGrupoConfiguracaoViewModel> grupoConfiguracoesCompartilhadas)
        {
            var seqsConfiguracoesComponente = grupoConfiguracoesCompartilhadas?.Where(x => x.Selecionado).Select(z => z.SeqConfiguracaoComponente);

            if (!seqsConfiguracoesComponente.SMCAny()) { return; }

            var descricaoConfiguracoes = new List<string>();

            foreach (var seqConfiguracaoComponente in seqsConfiguracoesComponente)
            {
                // Verificar se as configurações de compartilhamento selecionadas estão associadas a uma divisão de oferta de matriz.
                if (!ConfiguracaoComponenteService.VerificaConfiguracaoComponentePertenceCurriculo(seqConfiguracaoComponente))
                {
                    var configuracao = grupoConfiguracoesCompartilhadas.Where(c => c.SeqConfiguracaoComponente == seqConfiguracaoComponente).FirstOrDefault();

                    if (configuracao != null)
                    {
                        descricaoConfiguracoes.Add(configuracao.Descricao);
                    }
                }
            }

            //Caso não estejam, abortar a operação e exibir a mensagem...
            if (descricaoConfiguracoes.SMCAny())
            {
                var descricoes = string.Join(", ", descricaoConfiguracoes.OrderBy(o => o).ToArray());
                throw new Exception(string.Format(UIResource.MSG_Erro_ConfiguracoesComponenteCurriculo, descricoes));
            }
        }

        private void ValidarComponenteAssunto(TurmaDynamicModel model)
        {
            var componente = ComponenteCurricularService.BuscarComponenteCurricular(model.SeqComponenteCurricularPrincipal.Value);
            if (componente.ExigeAssuntoComponente && !model.SeqComponenteCurricularAssunto.HasValue)
            {
                throw new TurmaComponenteAssuntoObrigatorioException();
            }
        }

        #endregion [ Métodos Privados ]

        #region [ Operações ]

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult BuscarCicloLetivoFim(long? SeqCicloLetivoInicioHidden, long? SeqCicloLetivoFimHidden)
        {
            TurmaDynamicModel turma = new TurmaDynamicModel();

            if (!SeqCicloLetivoInicioHidden.HasValue && !SeqCicloLetivoFimHidden.HasValue)
                return PartialView("_DependencyCicloLetivo", turma);

            if (SeqCicloLetivoInicioHidden.HasValue && !SeqCicloLetivoFimHidden.HasValue)
            {
                var retorno = CicloLetivoService.BuscarCicloLetivo(SeqCicloLetivoInicioHidden.Value).Transform<CicloLetivoLookupViewModel>();
                turma.SeqCicloLetivoFim = retorno;
            }
            else if (SeqCicloLetivoFimHidden.HasValue)
            {
                var retorno = CicloLetivoService.BuscarCicloLetivo(SeqCicloLetivoFimHidden.Value).Transform<CicloLetivoLookupViewModel>();
                turma.SeqCicloLetivoFim = retorno;
            }

            return PartialView("_DependencyCicloLetivo", turma);
        }

        [SMCAuthorize(UC_TUR_001_01_01.PESQUISAR_TURMA)]
        public ActionResult CabecalhoLista(TurmaDynamicModel model)
        {
            return PartialView("_CabecalhoLista", model);
        }

        [SMCAuthorize(UC_TUR_001_01_01.PESQUISAR_TURMA)]
        public ActionResult VisualizaTurmaDetalhes(SMCEncryptedLong seqTurma)
        {
            var registro = TurmaService.BuscarTurma(seqTurma, true).Transform<TurmaDynamicModel>();

            if (registro.Cabecalho == null)
                registro.Cabecalho = PreencherCabecalho(registro);

            // Parte de Parâmetros
            //if (registro.TurmaParametros == null)
            //{
            //    //Validação porque as turmas antigas possuem ofertas sem matriz
            //    try
            //    {
            //        PreencherParametros(registro);
            //    }
            //    catch (Exception)
            //    {
            //        ParametrosSemOfertaMatriz(registro);
            //        registro.TurmaOfertasMatriz = null;
            //    }
            //}

            PreencherComponenteSubstituto(registro);

            return PartialView("_StepDadosTurma", registro);
        }

        [SMCAllowAnonymous]
        public ActionResult DesdobrarTurma(SMCEncryptedLong seq)
        {
            var seqTurma = TurmaService.DesdobrarTurma(seq);

            SetSuccessMessage("Turma desdobrada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
            return RedirectToAction("Editar", new { @seq = new SMCEncryptedLong(seqTurma) });
        }

        [SMCAuthorize(UC_TUR_001_01_02.MANTER_TURMA)]
        public ActionResult CopiarTurma(SMCEncryptedLong seq)
        {
            var seqTurma = TurmaService.CopiarTurma(seq);

            SetSuccessMessage("Turma copiada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
            return RedirectToAction("Editar", new { @seq = new SMCEncryptedLong(seqTurma) });
        }

        #endregion [ Operações ]

        #region [ Métodos Privados ]

        private TurmaCabecalhoViewModel PreencherCabecalho(TurmaDynamicModel model)
        {
            TurmaCabecalhoViewModel registro = new TurmaCabecalhoViewModel();
            if (model.Seq == 0)
            {
                /*
                O campo código da turma deverá ser um campo sequencial;
                O campo número da turma deverá ser um sequencial começando sempre de 1 por código da turma.
                 */
                registro = new TurmaCabecalhoViewModel()
                {
                    CodigoFormatado = model.CodigoFormatado,
                    CicloLetivoInicio = model.SeqCicloLetivoInicio?.Descricao,
                    CicloLetivoFim = model.SeqCicloLetivoFim?.Descricao,
                    Vagas = model.QuantidadeVagas.Value,
                    SituacaoTurmaAtual = model.SituacaoTurmaAtual,
                    SituacaoJustificativa = model.SituacaoJustificativa
                };

                if (model.Operacao != OperacaoTurma.Desdobrar)
                {
                    registro.CodigoFormatado = TurmaService.GerarCodigoNumeroTurma();

                    var codigoNumero = registro.CodigoFormatado.Split('.');
                    if (codigoNumero.Length == 2)
                    {
                        model.Codigo = Convert.ToInt32(codigoNumero[0]);
                        model.Numero = Convert.ToInt16(codigoNumero[1]);
                    }
                }
            }
            else
            {
                registro = TurmaService.BuscarTurmaCabecalho(model.Seq).Transform<TurmaCabecalhoViewModel>();

                // Vai calcular no último passo.
                registro.DescricaoTipoTurma = null;

                registro.Vagas = model.QuantidadeVagas.Value;
                registro.SituacaoTurmaAtual = model.SituacaoTurmaAtual;
                registro.SituacaoJustificativa = model.SituacaoJustificativa;
            }

            return registro;
        }

        private string BuscarCriterioDescricao(long seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return criterio.Descricao;
        }

        private string BuscarCriterioNota(long seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return criterio.NotaMaxima.HasValue ? criterio.NotaMaxima.ToString() : string.Empty;
        }

        private string BuscarAprovacaoPercentual(long seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return criterio.PercentualNotaAprovado.HasValue ? criterio.PercentualNotaAprovado.ToString() : string.Empty;
        }

        private string BuscarPresencaPercentual(long seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return criterio.PercentualFrequenciaAprovado.HasValue ? criterio.PercentualFrequenciaAprovado.ToString() : string.Empty;
        }

        private string BuscarEscalaApuracao(long seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return criterio.DescricaoEscalaApuracao ?? string.Empty;
        }

        private void PreencherCompartilhado(TurmaDynamicModel model)
        {
            if (model.GrupoConfiguracoesCompartilhadas == null)
                model.GrupoConfiguracoesCompartilhadas = new List<TurmaGrupoConfiguracaoViewModel>();

            if (model.GridConfiguracaoCompartilhada == null)
                model.GridConfiguracaoCompartilhada = new List<object>();

            var gruposCompartilhados = GrupoConfiguracaoComponenteService.BuscarGrupoConfiguracaoComponentePorConfiguracaoCompartilhado(model.ConfiguracaoComponente.Seq.Value);

            var itemConfiguracao = gruposCompartilhados.SelectMany(s => s.Itens).OrderBy(o => o.ConfiguracaoComponenteDescricao).ToList();

            foreach (var configuracao in itemConfiguracao)
            {
                if (model.GrupoConfiguracoesCompartilhadas.Count(c => c.SeqConfiguracaoComponente == configuracao.SeqConfiguracaoComponente) == 0)
                {
                    var registro = new TurmaGrupoConfiguracaoViewModel();
                    registro.SeqConfiguracaoComponente = configuracao.SeqConfiguracaoComponente;
                    registro.Descricao = configuracao.ConfiguracaoComponenteDescricaoCompleta;
                    registro.SeqComponenteCurricular = configuracao.SeqComponenteCurricular.Value;
                    registro.Principal = false;
                    model.GrupoConfiguracoesCompartilhadas.Add(registro);
                }
                else
                {
                    var configuracaoCompartilhada = model.GrupoConfiguracoesCompartilhadas.FirstOrDefault(c => c.SeqConfiguracaoComponente == configuracao.SeqConfiguracaoComponente);
                    configuracaoCompartilhada.Descricao = configuracao.ConfiguracaoComponenteDescricaoCompleta;
                }
            }

            if (!model.HabilitarGrupoConfiguracoesCompartilhadasDesmarcar)
            {
                if (model.GridConfiguracaoCompartilhadaValoresSelecionadosBanco == null)
                {
                    model.GridConfiguracaoCompartilhadaValoresSelecionadosBanco = new List<object>();
                    model.GridConfiguracaoCompartilhadaValoresSelecionadosBanco.AddRange(model.GrupoConfiguracoesCompartilhadas.Where(c => c.Selecionado).Select(x => x.SeqConfiguracaoComponente.ToString()));

                    foreach (var item in model.GridConfiguracaoCompartilhadaValoresSelecionadosBanco)
                    {
                        var selecionadoBanco = model.GrupoConfiguracoesCompartilhadas.FirstOrDefault(x => x.SeqConfiguracaoComponente.ToString() == item.ToString());
                        selecionadoBanco.SomenteLeitura = true;
                    }
                }
            }
            if (!model.HabilitarGrupoConfiguracoesCompartilhadas)
            {
                foreach (var item in model.GrupoConfiguracoesCompartilhadas)
                {
                    item.SomenteLeitura = !model.HabilitarGrupoConfiguracoesCompartilhadas;
                }
            }
        }

        private void PreencherOfertaMatriz(TurmaDynamicModel model)
        {
            var permitirApenasInserirOfertas = model.TurmaOfertasMatriz.Select(x => x.PermitirApenasInserirOfertas).FirstOrDefault();
            var desabilitarAlteracaoOfertas = model.TurmaOfertasMatriz.Select(x => x.DesabilitarAlteracaoOfertas).FirstOrDefault();
            var desabilitarOfertasMatrizConfiguracaoPrincipal = model.TurmaOfertasMatriz.Select(x => x.DesabilitarOfertasMatrizConfiguracaoPrincipal).FirstOrDefault();

            if (model.TurmaOfertasMatriz.Where(x => x.PermitirApenasInserirOfertas).FirstOrDefault() != null)
            {
                // Desabilito todos os itens da matriz de ofertas
                foreach (var ofertaMatriz in model.TurmaOfertasMatriz.SelectMany(t => t.OfertasMatriz))
                {
                    if (ofertaMatriz.Seq > 0) { ofertaMatriz.DesabilitarMatrizCurricularOferta = true; }
                }
            }

            if (model.TurmaOfertasMatriz.Count(c => c.SeqConfiguracaoComponente == model.ConfiguracaoComponente.Seq.Value) == 0)
            {
                TurmaOfertaMatrizViewModel principal = new TurmaOfertaMatrizViewModel();
                principal.SeqConfiguracaoComponente = model.ConfiguracaoComponente.Seq.Value;
                principal.SeqComponenteCurricular = model.ConfiguracaoComponente.SeqComponenteCurricular.Value;
                principal.DescricaoConfiguracaoComponente = model.ConfiguracaoComponente.ConfiguracaoComponenteDescricaoCompleta;
                principal.OfertasMatriz = new SMCMasterDetailList<TurmaOfertaMatrizDetailViewModel>();
                model.TurmaOfertasMatriz.Add(principal);
            }

            if (model.GrupoConfiguracoesCompartilhadas != null && model.GridConfiguracaoCompartilhada.SMCAny())
            {
                model.GrupoConfiguracoesCompartilhadas.Where(w => model.GridConfiguracaoCompartilhada.Contains(w.SeqConfiguracaoComponente.ToString())).SMCForEach(f =>
                {
                    if (model.TurmaOfertasMatriz.Count(c => c.SeqConfiguracaoComponente == f.SeqConfiguracaoComponente) == 0)
                    {
                        f.Selecionado = true;
                        TurmaOfertaMatrizViewModel grupo = new TurmaOfertaMatrizViewModel();
                        grupo.SeqConfiguracaoComponente = f.SeqConfiguracaoComponente;
                        grupo.SeqComponenteCurricular = f.SeqComponenteCurricular;
                        grupo.DescricaoConfiguracaoComponente = f.Descricao;
                        grupo.OfertasMatriz = new SMCMasterDetailList<TurmaOfertaMatrizDetailViewModel>();
                        model.TurmaOfertasMatriz.Add(grupo);
                    }
                });
            }
            model.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).SMCForEach(f =>
            {
                if (f.SeqMatrizCurricularOferta != null && f.SeqMatrizCurricularOferta.Seq > 0)
                {
                    //FIX: Bind
                    //f.SeqMatrizCurricularOferta = MatrizCurricularService.BuscarMatrizesCurricularLookupOfertaSelecionado(f.SeqMatrizCurricularOferta.Seq).Transform<OfertaMatrizCurricularLookupSelectDescricaoViewModel>();
                    f.ReservaVagas = f.QuantidadeVagasReservadas;
                }
            });

            // Atribuo as propriedades de Habilitar/Desabilitar campos, para os novos registros
            foreach (var turmaOfertaMatriz in model.TurmaOfertasMatriz)
            {
                turmaOfertaMatriz.PermitirApenasInserirOfertas = permitirApenasInserirOfertas;
                turmaOfertaMatriz.DesabilitarAlteracaoOfertas = desabilitarAlteracaoOfertas;
                turmaOfertaMatriz.DesabilitarOfertasMatrizConfiguracaoPrincipal = desabilitarOfertasMatrizConfiguracaoPrincipal;
            }

            if (model.TurmaOfertasMatriz.Where(x => x.DesabilitarMatrizCurricularOferta).FirstOrDefault() != null)
            {
                // Desabilito todos os itens da matriz de ofertas
                foreach (var ofertaMatriz in model.TurmaOfertasMatriz.SelectMany(t => t.OfertasMatriz))
                {
                    ofertaMatriz.DesabilitarMatrizCurricularOferta = true;
                }
            }
            // Atribuição para a passagem de parâmetro do Lookup.
            model.TurmaOfertasMatriz.ForEach(o => o.SeqCicloLetivo = model.SeqCicloLetivoInicio?.Seq);
        }

        private void PreencherParametros(TurmaDynamicModel model)
        {
            if (model.SeqConfiguracaoComponentePrincipal.HasValue)
            {
                model.CriteriosAprovacao = CriterioAprovacaoService.BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(model.SeqConfiguracaoComponentePrincipal.Value);
                model.EscalasApuracao = EscalaApuracaoService.BuscarEscalasApuracaoNivelEnsinoPorConfiguracaoComponenteSelect(model.SeqConfiguracaoComponentePrincipal.Value);
            }

            TempData["CriteriosAprovacao"] = model.CriteriosAprovacao;
            TempData["EscalasApuracao"] = model.EscalasApuracao;

            //Verifica se existe oferta de matriz se existir recuperar os dados e deixar opção de seleção
            if (model.TurmaOfertasMatriz != null && model.TurmaOfertasMatriz.Any(s => s.OfertasMatriz.Count > 0))
            {
                RemoverOfertaMatrizVazia(model.TurmaOfertasMatriz);
                model.TurmaParametros = new List<TurmaParametrosViewModel>();

                foreach (var parametro in model.TurmaOfertasMatriz)
                {
                    TurmaParametrosViewModel registroParametro = new TurmaParametrosViewModel
                    {
                        ParametrosOfertas = new List<TurmaParametrosOfertaViewModel>(),

                        SeqConfiguracaoComponente = parametro.SeqConfiguracaoComponente,
                        SeqComponenteCurricular = parametro.SeqComponenteCurricular,
                        DescricaoConfiguracaoComponente = parametro.DescricaoConfiguracaoComponente,
                        ExiteOfertasConfiguracao = true
                    };

                    foreach (var oferta in parametro.OfertasMatriz.Where(x => x.SeqMatrizCurricularOferta.Seq > 0))
                    {
                        TurmaParametrosOfertaViewModel registroOferta = new TurmaParametrosOfertaViewModel
                        {
                            DescricaoOfertaMatriz = oferta.SeqMatrizCurricularOferta.OfertaCompleto,
                            SeqOfertaMatriz = oferta.SeqMatrizCurricularOferta.Seq
                        };

                        DivisaoMatrizCurricularComponenteFiltroData divisaoFiltroData = new DivisaoMatrizCurricularComponenteFiltroData
                        {
                            SeqConfiguracaoComponente = parametro.SeqConfiguracaoComponente,
                            SeqMatrizCurricularOferta = oferta.SeqMatrizCurricularOferta.Seq
                        };

                        var consultaDivisoes = DivisaoMatrizCurricularComponenteService.BuscarDivisaoMatrizCurricularComponente(divisaoFiltroData);
                        var descricaoDivisoes = DivisaoComponenteService.BuscarDivisoesCompoentePorConfiguracao(parametro.SeqConfiguracaoComponente);

                        registroOferta.DivisaoMatrizCurricularNumero = consultaDivisoes.DivisaoMatrizCurricularNumero;
                        registroOferta.DivisaoMatrizCurricularDescricao = consultaDivisoes.DivisaoMatrizCurricularDescricao;
                        registroOferta.SeqCriterioAprovacao = consultaDivisoes.SeqCriterioAprovacao.GetValueOrDefault();
                        registroOferta.CriterioNotaMaxima = consultaDivisoes.CriterioNotaMaxima;
                        registroOferta.CriterioPercentualNotaAprovado = consultaDivisoes.CriterioPercentualNotaAprovado;
                        registroOferta.CriterioPercentualFrequenciaAprovado = consultaDivisoes.CriterioPercentualFrequenciaAprovado;
                        registroOferta.SeqEscalaApuracao = consultaDivisoes.SeqEscalaApuracao;
                        registroOferta.CriterioDescricaoEscalaApuracao = consultaDivisoes.CriterioDescricaoEscalaApuracao;
                        registroOferta.ApurarFrequencia = consultaDivisoes.ApuracaoFrequencia;

                        if (model.OrigemAvaliacao != null && model.OrigemAvaliacao.Seq > 0)
                        {
                            if (registroOferta.SeqOfertaMatriz == model.OrigemAvaliacao.SeqMatrizCurricularOferta)
                            {
                                registroOferta.Selecionado = true;
                                registroOferta.SeqOrigemAvaliacao = model.OrigemAvaliacao.Seq;
                            }
                        }

                        if (consultaDivisoes.DivisoesComponente != null)
                        {
                            registroOferta.DivisoesComponente = new List<TurmaParametrosDetalheViewModel>();
                            foreach (var item in consultaDivisoes.DivisoesComponente)
                            {
                                registroOferta.DivisoesComponente.Add(new TurmaParametrosDetalheViewModel()
                                {
                                    Seq = item.Seq,
                                    SeqDivisaoComponente = item.SeqDivisaoComponente,
                                    DivisaoComponenteDescricao = descricaoDivisoes.FirstOrDefault(w => w.Seq == item.SeqDivisaoComponente).Descricao,
                                    PermiteGrupo = Convert.ToBoolean(descricaoDivisoes.FirstOrDefault(w => w.Seq == item.SeqDivisaoComponente).DataAttributes.FirstOrDefault(f => f.Key == "permite-grupo").Value),
                                    QuantidadeGrupos = item.QuantidadeGrupos,
                                    QuantidadeProfessores = item.QuantidadeProfessores,
                                    NotaMaxima = item.NotaMaxima,
                                    ApurarFrequencia = item.ApurarFrequencia,
                                    SeqEscalaApuracao = item.SeqEscalaApuracao,
                                    MateriaLecionadaObrigatoria = item.MateriaLecionadaObrigatoria
                                });
                            }
                        }

                        registroParametro.ParametrosOfertas.Add(registroOferta);
                    }
                    model.TurmaParametros.Add(registroParametro);
                }

                //Associar pelo menos uma oferta de matriz para cada configuração associada
                ValidarConfiguracaoSemOfertaMatriz(model.TurmaOfertasMatriz);

                //var ofertasSemParametros = model.TurmaParametros.SelectMany(w => w.ParametrosOfertas).Where(w => w.DivisaoMatrizCurricularNumero == 0).ToList();
                //if (ofertasSemParametros.Count > 0)
                //    throw new TurmaOfertasSemParametrosInvalidoException($"</br> {string.Join(",</br>", ofertasSemParametros.Select(s => s.DescricaoOfertaMatriz))}");

                var parametroSelecionado = (long?)TempData["ParametrosConteudo"];
                if (parametroSelecionado != null)
                    model.TurmaParametros.SelectMany(w => w.ParametrosOfertas).Where(w => w.SeqOfertaMatriz == parametroSelecionado).SMCForEach(f => f.Selecionado = true).ToList();
            }
            else //Se não existir oferta carrega os campos em branco para preenchimento obrigatório
            {
                ParametrosSemOfertaMatriz(model);
            }
        }

        private void ParametrosSemOfertaMatriz(TurmaDynamicModel model)
        {
            TurmaParametrosViewModel registroNovo = new TurmaParametrosViewModel();
            TurmaParametrosOfertaViewModel registroOferta = new TurmaParametrosOfertaViewModel();
            model.TurmaParametros = new List<TurmaParametrosViewModel>();
            registroNovo.ParametrosOfertas = new List<TurmaParametrosOfertaViewModel>();
            registroOferta.DivisoesComponente = new List<TurmaParametrosDetalheViewModel>();

            if (model.SeqComponenteCurricularPrincipal == null && model.SeqConfiguracaoComponentePrincipal != null)
            {
                model.ConfiguracaoComponente = ConfiguracaoComponenteService.BuscarConfiguracoesComponentesLookup(new ConfiguracaoComponenteFiltroData() { Seq = model.SeqConfiguracaoComponentePrincipal }).FirstOrDefault().Transform<ConfiguracaoComponenteLookupViewModel>();
            }

            registroNovo.SeqConfiguracaoComponente = model.SeqConfiguracaoComponentePrincipal.Value;
            registroNovo.SeqComponenteCurricular = model.SeqComponenteCurricularPrincipal.Value;
            registroNovo.DescricaoConfiguracaoComponente = model.DescricaoConfiguracaoPrincipal;
            registroNovo.ExiteOfertasConfiguracao = false;

            if (model.SeqConfiguracaoComponentePrincipal.HasValue)
            {
                var divisoesComponentesNovos = this.DivisaoComponenteService.BuscarDivisoesCompoentePorConfiguracao(model.SeqConfiguracaoComponentePrincipal.Value);
                foreach (var item in divisoesComponentesNovos)
                {
                    var divisaoAtual = new TurmaParametrosDetalheViewModel();
                    divisaoAtual.SeqDivisaoComponente = item.Seq;
                    divisaoAtual.DivisaoComponenteDescricao = item.Descricao;
                    divisaoAtual.PermiteGrupo = Convert.ToBoolean(item.DataAttributes.FirstOrDefault(f => f.Key == "permite-grupo").Value);
                    if (model.DivisoesTurma != null && model.DivisoesTurma.Count > 0)
                    {
                        var divisaoBanco = model.DivisoesTurma.Where(w => w.SeqDivisaoComponente == item.Seq).FirstOrDefault();
                        if (divisaoBanco != null)
                        {
                            divisaoAtual.ApurarFrequencia = divisaoBanco.OrigemAvaliacao.ApurarFrequencia.GetValueOrDefault();
                            divisaoAtual.NotaMaxima = divisaoBanco.OrigemAvaliacao.NotaMaxima.GetValueOrDefault();
                            divisaoAtual.QuantidadeGrupos = divisaoBanco.OrigemAvaliacao.QuantidadeGrupos.GetValueOrDefault();
                            divisaoAtual.QuantidadeProfessores = divisaoBanco.OrigemAvaliacao.QuantidadeProfessores.GetValueOrDefault();
                            divisaoAtual.Seq = divisaoBanco.Seq;
                            divisaoAtual.SeqEscalaApuracao = divisaoBanco.OrigemAvaliacao.SeqEscalaApuracao.GetValueOrDefault();
                            divisaoAtual.SeqOrigemAvaliacao = divisaoBanco.OrigemAvaliacao.Seq;
                        }
                    }

                    registroOferta.DivisoesComponente.Add(divisaoAtual);
                }

                if (model.OrigemAvaliacao != null && model.OrigemAvaliacao.Seq > 0)
                {
                    registroOferta.SeqCriterioAprovacao = model.OrigemAvaliacao.SeqCriterioAprovacao.GetValueOrDefault();

                    if (!model.CriteriosAprovacao.Any(a => a.Seq == registroOferta.SeqCriterioAprovacao))
                        model.CriteriosAprovacao.Add(new SMCDatasourceItem() { Seq = registroOferta.SeqCriterioAprovacao, Descricao = BuscarCriterioDescricao(registroOferta.SeqCriterioAprovacao) });

                    registroOferta.CriterioNotaMaxima = BuscarCriterioNota(registroOferta.SeqCriterioAprovacao);
                    registroOferta.CriterioPercentualNotaAprovado = BuscarAprovacaoPercentual(registroOferta.SeqCriterioAprovacao);
                    registroOferta.CriterioPercentualFrequenciaAprovado = BuscarPresencaPercentual(registroOferta.SeqCriterioAprovacao);
                    registroOferta.SeqEscalaApuracao = model.OrigemAvaliacao.SeqEscalaApuracao.GetValueOrDefault();
                    registroOferta.CriterioDescricaoEscalaApuracao = BuscarEscalaApuracao(registroOferta.SeqCriterioAprovacao);
                    registroOferta.SeqOrigemAvaliacao = model.OrigemAvaliacao.Seq;
                }

                registroNovo.ParametrosOfertas.Add(registroOferta);
            }

            model.TurmaParametros.Add(registroNovo);
        }

        private void PreencherDivisoes(TurmaDynamicModel model)
        {
            PreencherDivisoesLocalidades(model);

            PreencherTurmaConfiguracaoCabecalho(model);

            var filtroComponente = new ConfiguracaoComponenteFiltroData()
            {
                Seq = model.SeqConfiguracaoComponentePrincipal.Value,
                IgnorarFiltroDados = true
            };
            var componentePrincipal = ConfiguracaoComponenteService.BuscarConfiguracoesComponentes(filtroComponente).First();

            // Se não houver alterações entre abas
            if (TurmaDivisoesEDivisoesComponenteIguais(model.TurmaDivisoes, componentePrincipal.DivisoesComponente))
            {
                foreach (var divisao in model.TurmaDivisoes)
                {
                    var item = componentePrincipal.DivisoesComponente.FirstOrDefault(x => x.Seq == divisao.SeqDivisaoComponente);

                    PreencherDivisoesComponentes(model, item, divisao);
                }
            }
            else
            {
                PreencherTurmaDivisoes(model, componentePrincipal.DivisoesComponente);
            }
        }

        private void PreencherTurmaConfiguracaoCabecalho(TurmaDynamicModel model)
        {
            model.TurmaConfiguracoesCabecalho = new List<TurmaCabecalhoConfiguracoesViewModel>();
            TurmaCabecalhoConfiguracoesViewModel principal = new TurmaCabecalhoConfiguracoesViewModel();
            principal.SeqConfiguracaoComponente = model.SeqConfiguracaoComponentePrincipal.Value;
            principal.SeqComponenteCurricular = model.SeqComponenteCurricularPrincipal.Value;
            principal.DescricaoConfiguracaoComponente = model.DescricaoConfiguracaoPrincipal;
            principal.ConfiguracaoPrincipal = LegendaPrincipal.Principal;
            model.TurmaConfiguracoesCabecalho.Add(principal);

            if (model.GrupoConfiguracoesCompartilhadas != null)
            {
                VincularConfiguracaoCompartilhada(model);

                foreach (var item in model.GrupoConfiguracoesCompartilhadas.Where(w => w.Selecionado && model.GridConfiguracaoCompartilhada.Contains(w.SeqConfiguracaoComponente.ToString())))
                {
                    TurmaCabecalhoConfiguracoesViewModel grupo = new TurmaCabecalhoConfiguracoesViewModel();
                    grupo.SeqConfiguracaoComponente = item.SeqConfiguracaoComponente;
                    grupo.SeqComponenteCurricular = item.SeqComponenteCurricular;
                    grupo.DescricaoConfiguracaoComponente = item.Descricao;
                    model.TurmaConfiguracoesCabecalho.Add(grupo);
                }
            }
        }

        private void PreencherDivisoesLocalidades(TurmaDynamicModel model)
        {
            var ofertasMatriz = model?.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Select(s => s.SeqMatrizCurricularOferta).ToList().TransformList<MatrizCurricularOfertaData>();

            //Buscar as localidades das ofertas cadastradas no step 3  com as localidade de exceção
            var localidades = CursoOfertaLocalidadeService.BuscarLocalidadesMatrizTurma(ofertasMatriz);

            model.DivisoesLocalidades = localidades.Distinct().ToList();
            TempData["DivisoesLocalidades"] = model.DivisoesLocalidades;
        }

        private bool TurmaDivisoesEDivisoesComponenteIguais(List<TurmaDivisoesViewModel> turmaDivisoes, List<ConfiguracaoComponenteDivisaoListarData> divisoesComponente)
        {
            if (turmaDivisoes == null) { return false; }

            if (turmaDivisoes.SMCAny() && turmaDivisoes.Count == divisoesComponente.Count)
            {
                foreach (var turmaDivisao in turmaDivisoes)
                {
                    if (!divisoesComponente.SMCAny(x => x.Seq == turmaDivisao.Seq))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void PreencherTurmaDivisoes(TurmaDynamicModel model, List<ConfiguracaoComponenteDivisaoListarData> divisoesComponente)
        {
            model.TurmaDivisoes = new List<TurmaDivisoesViewModel>();
            foreach (var item in divisoesComponente)
            {
                TurmaDivisoesViewModel divisao = new TurmaDivisoesViewModel();
                divisao.SeqDivisaoComponente = item.Seq;
                divisao.DivisaoComponenteDescricao = item.DescricaoFormatada;
                divisao.PermitirGrupo = item.PermiteGrupo;

                PreencherDivisoesComponentes(model, item, divisao);

                model.TurmaDivisoes.Add(divisao);
            }
        }

        private void PreencherDivisoesComponentes(TurmaDynamicModel model, ConfiguracaoComponenteDivisaoListarData divisaoComponenteConfiguracao, TurmaDivisoesViewModel divisao)
        {
            divisao.DivisoesComponentes = new SMCMasterDetailList<TurmaDivisoesDetailViewModel>();
            if (model.DivisoesTurma == null)
                model.DivisoesTurma = new List<TurmaDivisoesCriterioViewModel>();

            var divisoesBanco = model.DivisoesTurma.Where(w => w.SeqDivisaoComponente == divisaoComponenteConfiguracao.Seq).ToList();
            if (divisoesBanco != null && divisoesBanco.SMCAny())
            {
                int count = 1;
                foreach (var divisaoBanco in divisoesBanco)
                {
                    TurmaDivisoesDetailViewModel divisaoDetalhe = new TurmaDivisoesDetailViewModel();
                    divisaoDetalhe.Seq = divisaoBanco.Seq;
                    divisaoDetalhe.Turma = model.Cabecalho.CodigoFormatado;
                    divisaoDetalhe.DivisaoDescricao = divisaoComponenteConfiguracao.Numero.ToString();
                    divisaoDetalhe.GrupoNumero = divisao.PermitirGrupo ? count.ToString().PadLeft(3, '0') : "000";
                    count++;
                    divisaoDetalhe.SeqOrigemAvaliacao = divisaoBanco.SeqOrigemAvaliacao;
                    divisaoDetalhe.SeqLocalidade = divisaoBanco.SeqLocalidade.GetValueOrDefault();
                    divisaoDetalhe.DivisaoVagas = divisaoBanco.QuantidadeVagas;
                    divisaoDetalhe.InformacoesAdicionais = divisaoBanco.InformacoesAdicionais;
                    divisaoDetalhe.QuantidadeVagasOcupadas = divisaoBanco.QuantidadeVagasOcupadas == null ? 0 : divisaoBanco.QuantidadeVagasOcupadas;

                    divisao.DivisoesComponentes.Add(divisaoDetalhe);
                    //O DefaultModel irá inicializar novas divisões de turma como quantidade de vagas ocupadas = 0
                    divisao.DivisoesComponentes.DefaultModel = new TurmaDivisoesDetailViewModel() { QuantidadeVagasOcupadas = 0 };
                }
            }
            else
            {
                TurmaDivisoesDetailViewModel divisaoDetalhe = new TurmaDivisoesDetailViewModel();
                divisaoDetalhe.Turma = model.Cabecalho.CodigoFormatado;
                divisaoDetalhe.DivisaoDescricao = divisaoComponenteConfiguracao.Numero.ToString();
                divisaoDetalhe.GrupoNumero = divisao.PermitirGrupo ? "001" : "000";
                divisaoDetalhe.DivisaoVagas = model.QuantidadeVagas;
                divisaoDetalhe.QuantidadeVagasOcupadas = 0;

                divisao.DivisoesComponentes.Add(divisaoDetalhe);
                //O DefaultModel irá inicializar novas divisões de turma como quantidade de vagas ocupadas = 0
                divisao.DivisoesComponentes.DefaultModel = new TurmaDivisoesDetailViewModel() { QuantidadeVagasOcupadas = 0 };
            }
        }

        private void PreencherComponenteSubstituto(TurmaDynamicModel model)
        {
            if (model.SeqComponenteCurricularPrincipal == null && model.SeqConfiguracaoComponentePrincipal != null)
            {
                var filtroComponente = new ConfiguracaoComponenteFiltroData()
                {
                    Seq = model.SeqConfiguracaoComponentePrincipal,
                    IgnorarFiltroDados = true
                };
                model.ConfiguracaoComponente = ConfiguracaoComponenteService.BuscarConfiguracoesComponentesLookup(filtroComponente).FirstOrDefault().Transform<ConfiguracaoComponenteLookupViewModel>();
            }

            if (model.SeqComponenteCurricularPrincipal.HasValue)
            {
                var componente = ComponenteCurricularService.BuscarComponenteCurricular(model.SeqComponenteCurricularPrincipal.Value);
                if (componente.ExigeAssuntoComponente)
                {
                    var seqsMatrizCurricularOferta = model?.TurmaOfertasMatriz.SelectMany(s => s.OfertasMatriz).Select(m => m.SeqMatrizCurricularOferta.Seq).ToList();

                    var seqsConfiguracoesComponente = model.GridConfiguracaoCompartilhada?.Select(z => long.Parse(z.ToString())) ?? new List<long>();

                    model.ComponentesAssuntos = DivisaoMatrizCurricularComponenteService.BuscarAssuntosComponentesOfertasMatrizesTurma(seqsMatrizCurricularOferta, seqsConfiguracoesComponente.ToList()).ToList();

                    if (model.SeqComponenteCurricularAssunto.HasValue)
                    {
                        model.DescricaoComponenteCurricularAssunto = ComponenteCurricularService.BuscarComponenteCurricularCabecalho(model.SeqComponenteCurricularAssunto.Value).DescricaoCompleta;
                    }
                }
            }
        }

        #endregion [ Métodos Privados ]

        #region [ Reabrir Diário ]

        [SMCAuthorize(UC_TUR_001_01_03.REABRIR_DIARIO)]
        public ActionResult ModalReabrirDiario(SMCEncryptedLong seq)
        {
            var model = CriarHistoricoFechamentoDiario(seq);

            return PartialView("_ReabrirDiario", model);
        }

        [SMCAuthorize(UC_TUR_001_01_03.REABRIR_DIARIO)]
        public ActionResult ReabrirDiario(ReabrirDiarioViewModel model)
        {
            model.DiarioFechado = false;
            TurmaHistoricoFechamentoDiarioService.ReabrirDiario(model.Transform<TurmaHistoricoFechamentoDiarioData>());

            SetSuccessMessage("Diário Reaberto!", "Sucesso", SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "Turma");
        }

        private ReabrirDiarioViewModel CriarHistoricoFechamentoDiario(SMCEncryptedLong seqTurma)
        {
            return new ReabrirDiarioViewModel()
            {
                SeqTurma = seqTurma.Value,
                DataInclusao = DateTime.Now,
                DiarioFechado = true,
                Usuario = SMCContext.User.Identity.Name
            };
        }

        #endregion [ Reabrir Diário ]

        #region [ Configuração Turma ]

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult ConfiguracaoTurma(SMCEncryptedLong seq)
        {
            var retorno = TurmaService.ConfiguracaoTurma(seq);

            var model = retorno.Transform<ConfiguracaoTurmaDynamicModel>();
            model.Cabecalho = new TurmaCabecalhoViewModel()
            {
                CodigoFormatado = retorno.CodigoFormatado,
                CicloLetivoInicio = retorno.DescricaoCicloLetivoInicio,
                Vagas = retorno.QuantidadeVagas.Value,
                DescricaoTipoTurma = retorno.DescricaoTipoTurma,
                SituacaoTurmaAtual = retorno.SituacaoTurmaAtual,
                SituacaoJustificativa = retorno.SituacaoJustificativa
            };

            model.DivisaoComponente.SMCForEach(s => s.ListaEscalaApuracao = retorno.ListaEscalaApuracao);

            this.ConfigureDynamic(model);
            return PartialView("_ConfiguracaoTurma", model);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult SalvarConfiguracaoTurma(ConfiguracaoTurmaDynamicModel model)
        {
            var modelo = model.Transform<ConfiguracaoTurmaData>();
            modelo.OcorreuAlteracaoManual = true;

            TurmaService.SalvarConfiguracaoTurma(modelo);

            SetSuccessMessage("Configuração turma alterada com sucesso.!", "Sucesso", SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "Turma");
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult SalvarConfiguracaoTurmaComComponenteMatriz(ConfiguracaoTurmaDynamicModel model)
        {
            Assert(model, nameof(UIResource.MSG_ConfiguracaoTurma), () => true);

            var modelo = model.Transform<ConfiguracaoTurmaData>();
            modelo.OcorreuAlteracaoManual = false;

            TurmaService.SalvarConfiguracaoTurmaComComponenteMatriz(modelo);

            SetSuccessMessage("Configuração turma alterada com sucesso.!", "Sucesso", SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction("Index", "Turma");
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarCriterioNota(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.NotaMaxima.HasValue ? criterio.NotaMaxima.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarAprovacaoPercentual(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.PercentualNotaAprovado.HasValue ? criterio.PercentualNotaAprovado.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarPresencaPercentual(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.PercentualFrequenciaAprovado.HasValue ? criterio.PercentualFrequenciaAprovado.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarEscalaApuracao(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.DescricaoEscalaApuracao ?? string.Empty);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarSeqEscalaApuracao(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.SeqEscalaApuracao);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarApuracaoFrequencia(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.ApuracaoFrequencia);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        public ActionResult BuscarApuracaoNota(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.ApuracaoNota);
        }

        [SMCAuthorize(UC_TUR_001_08_01.ALTERAR_CONFIGURACAO_TURMA)]
        [HttpPost]
        public ActionResult PreencherConfiguracaoTurmaDivisaoComponente(long seq, long? seqCriterioAprovacao)
        {
            var configuracaoTurma = TurmaService.ConfiguracaoTurma(seq);
            var model = configuracaoTurma.Transform<ConfiguracaoTurmaDynamicModel>();
            model.DivisaoComponente.SMCForEach(s => s.ListaEscalaApuracao = configuracaoTurma.ListaEscalaApuracao);

            if (configuracaoTurma.SeqCriterioAprovacao != seqCriterioAprovacao)
            {
                short? notaMaxima = null;
                bool apuracaoFrequencia = false;
                long? seqEscalaApuracao = null;
                if (seqCriterioAprovacao.HasValue)
                {
                    var criterio = CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao.Value);
                    notaMaxima = criterio.NotaMaxima;
                    apuracaoFrequencia = criterio.ApuracaoFrequencia;
                    if (!criterio.ApuracaoNota)
                        seqEscalaApuracao = criterio.SeqEscalaApuracao;
                }

                foreach (var item in model.DivisaoComponente)
                {
                    // Preenche os valores padrões das divisões
                    item.QuantidadeGrupos = 0;
                    item.QuantidadeProfessores = null;
                    item.ListaEscalaApuracao = configuracaoTurma.ListaEscalaApuracao;
                    item.NotaMaxima = notaMaxima;
                    item.ApurarFrequencia = apuracaoFrequencia;
                    item.SeqEscalaApuracao = seqEscalaApuracao;
                }
            }
            return PartialView("_ConfiguracaoTurmaDivisaoComponenteLista", model);
        }

        #endregion [ Configuração Turma ]

        [SMCAuthorize(UC_APR_001_17_01.EXIBIR_RELATORIO_ACOMPANHAMENTO_NOTA)]
        public ActionResult RelatorioAcompanhamento(SMCEncryptedLong seqOrigemAvaliacao, bool administrativo)
        {
            var arq = AcompanhamentoReport.Download($"GerarRelatorio?seqOrigemAvaliacao={seqOrigemAvaliacao}&administrativo={administrativo}", method: Method.GET);
            return File(arq, "application/pdf");
        }
    }
}