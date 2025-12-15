using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using SMC.SGA.Administrativo.Areas.ALN.Views.Ingressante.App_LocalResources;
using SMC.SGA.Administrativo.Areas.PES.Models;
using SMC.SGA.Administrativo.Controllers;
using SMC.SGA.Administrativo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class IngressanteController : PessoaAtuacaoBaseController<IngressanteDynamicModel>
    {
        #region [ Servicos ]

        private ICampanhaCicloLetivoService CampanhaCicloLetivoService { get => Create<ICampanhaCicloLetivoService>(); }

        private ICicloLetivoService CicloLetivoService { get => Create<ICicloLetivoService>(); }

        private IColaboradorService ColaboradorService { get => Create<IColaboradorService>(); }

        private ICondicaoObrigatoriedadeService CondicaoObrigatoriedadeService { get => Create<ICondicaoObrigatoriedadeService>(); }

        private IFormaIngressoService FormaIngressoService { get => Create<IFormaIngressoService>(); }

        private IIngressanteService IngressanteService { get => Create<IIngressanteService>(); }

        private IInstituicaoExternaService InstituicaoExternaService { get => Create<IInstituicaoExternaService>(); }

        private IInstituicaoNivelService InstituicaoNivelService { get => Create<IInstituicaoNivelService>(); }

        private IInstituicaoNivelTipoOrientacaoParticipacaoService InstituicaoNivelTipoOrientacaoParticipacaoService { get => Create<IInstituicaoNivelTipoOrientacaoParticipacaoService>(); }

        private IInstituicaoNivelTipoOrientacaoService InstituicaoNivelTipoOrientacaoService { get => Create<IInstituicaoNivelTipoOrientacaoService>(); }

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService { get => Create<IInstituicaoNivelTipoVinculoAlunoService>(); }

        private IMatrizCurricularOfertaService MatrizCurricularOfertaService { get => Create<IMatrizCurricularOfertaService>(); }

        private IPessoaAtuacaoCondicaoObrigatoriedadeService PessoaAtuacaoCondicaoObrigatoriedadeService { get => Create<IPessoaAtuacaoCondicaoObrigatoriedadeService>(); }

        private IProcessoSeletivoService ProcessoSeletivoService { get => Create<IProcessoSeletivoService>(); }

        private IProcessoService ProcessoService { get => Create<IProcessoService>(); }

        private ITermoIntercambioService TermoIntercambioService { get => Create<ITermoIntercambioService>(); }

        private ITipoVinculoAlunoService TipoVinculoAlunoService { get => Create<ITipoVinculoAlunoService>(); }

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        #endregion [ Servicos ]

        #region [ Steps Wizard Ingressante ]

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public override ActionResult Selecao(IngressanteDynamicModel model)
        {
            return base.Selecao(model);
        }

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public override ActionResult DadosPessoais(IngressanteDynamicModel model)
        {
            return base.DadosPessoais(model);
        }

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public override ActionResult Contatos(IngressanteDynamicModel model)
        {
            if (model.Campanha?.Seq == null)
            {
                model.SeqProcessoSeletivo = 0;
            }

            if (model.SeqProcessoSeletivo == 0)
            {
                model.SeqNivelEnsino = 0;
                model.SeqTipoVinculoAluno = 0;
            }

            if (model.SeqNivelEnsino == 0)
            {
                model.SeqCicloLetivo = 0;
                model.ExigeOfertaMatrizCurricular = null;
                model.QuantidadeOfertaCampanhaIngresso = 0;
                model.ExigeParceriaIntercambioIngresso = null;
                model.PermiteOrientacaoPessoaAtuacao = null;
                model.RequerOrientacaoPessoaAtuacao = null;
                model.SeqTipoOrientacao = null;
            }

            if (model.SeqTipoVinculoAluno == 0)
            {
                model.SeqFormaIngresso = 0;
                model.ExigeOfertaMatrizCurricular = null;
                model.QuantidadeOfertaCampanhaIngresso = 0;
                model.ExigeParceriaIntercambioIngresso = null;
                model.PermiteOrientacaoPessoaAtuacao = null;
                model.RequerOrientacaoPessoaAtuacao = null;
                model.SeqTipoOrientacao = null;
            }

            if (model.SeqFormaIngresso == 0)
            {
                model.SeqInstituicaoTransferenciaExterna = null;
            }

            if (model.QuantidadeOfertaCampanhaIngresso == 0)
            {
                model.Ofertas = null;
            }

            if (!model.ExigeParceriaIntercambioIngresso.GetValueOrDefault())
            {
                model.TermosIntercambio = null;
            }

            if (!model.Ofertas.SMCAny())
            {
                model.SeqMatrizCurricularOferta = null;
                model.OfertasMatrizDependencyOfertas = null;
            }

            if (!model.TermosIntercambio.SMCAny())
            {
                model.SeqMatrizCurricularOferta = null;
                model.OfertasMatrizDependencyTermos = null;
            }

            if (!model.PermiteOrientacaoPessoaAtuacao.GetValueOrDefault())
            {
                model.OrientacaoParticipacoesColaboradores = null;
            }

            if (!model.SeqTipoOrientacao.HasValue)
            {
                model.OrientacaoParticipacoesColaboradores = null;
            }

            IngressanteService.ValidarBloqueioPessoa(model.Transform<IngressanteData>());
            return base.Contatos(model);
        }

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult DadosAcademicos(IngressanteDynamicModel model)
        {
            this.ConfigureDynamic(model);

            if (model.SeqTipoOrientacao.HasValue && model.OrientacaoParticipacoesColaboradores.SMCAny())
            {
                // Preenche os DataSource de orientação direta
                var colaboradores = BuscarColaboradoresDataSource(seqCampanhaOferta: model.Ofertas?.FirstOrDefault()?.SeqCampanhaOferta.Seq);
                var tiposParticipacao = BuscarTiposParticipacaoDataSource(model.SeqTipoOrientacao, model.SeqNivelEnsino, null, model.SeqTipoVinculoAluno);
                foreach (var orientacao in model.OrientacaoParticipacoesColaboradores)
                {
                    orientacao.Colaboradores = colaboradores;
                    orientacao.TiposParticipacaoOrientacao = tiposParticipacao;
                    orientacao.InstituicoesExternas = BuscarInstituicoesExternasColaboradorDataSource(orientacao.SeqColaborador);
                }
            }
            if (model.TermosIntercambio.SMCAny())
            {
                // Preeche os DataSource de orientação em intercâmbio
                var colaboradores = BuscarColaboradoresDataSource(seqCampanhaOferta: model.Ofertas?.FirstOrDefault()?.SeqCampanhaOferta.Seq);
                foreach (var termo in model.TermosIntercambio)
                {
                    if (termo.SeqTipoOrientacao.HasValue && termo.OrientacaoParticipacoesColaboradores.SMCAny())
                    {
                        var tiposParticipacao = BuscarTiposParticipacaoDataSource(termo.SeqTipoOrientacao, model.SeqNivelEnsino, termo.SeqTermoIntercambio?.Seq, model.SeqTipoVinculoAluno);
                        foreach (var orientacao in termo.OrientacaoParticipacoesColaboradores)
                        {
                            orientacao.Colaboradores = colaboradores;
                            orientacao.TiposParticipacaoOrientacao = tiposParticipacao;
                            orientacao.InstituicoesExternas = BuscarInstituicoesExternasColaboradorDataSource(orientacao.SeqColaborador, termo.SeqTermoIntercambio?.Seq, termo.SeqInstituicaoEnsinoExterna, orientacao.TipoParticipacaoOrientacao, model.SeqNivelEnsino, model.SeqTipoVinculoAluno);
                        }
                    }
                }
                model.ExigeParceriaIntercambioIngresso = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(model.SeqNivelEnsino, model.SeqTipoVinculoAluno)?.ExigeParceriaIntercambioIngresso.GetValueOrDefault();
            }

            IngressanteService.ValidarContatosIngressante(model.Transform<IngressanteData>());

            this.SetViewMode(SMCViewMode.Insert);

            return ViewWizard(model);
        }

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult Escalonamento(IngressanteDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Recupera a campanha ciclo
            var filtroCampanhaCiclo = new CampanhaCicloLetivoFiltroData()
            {
                SeqCampanha = model.SeqCampanha,
                SeqCicloLetivo = model.SeqCicloLetivo
            };
            model.SeqCampanhaCicloLetivo = CampanhaCicloLetivoService.BuscarCampanhaCicloLetivo(filtroCampanhaCiclo)?.Seq ?? 0;

            // Recupera o processo referente a campanha ciclo e processo seletivo, utilizado como parâmetro no lookup de grupo de escalonamento
            var filtroCampanha = new ProcessoFiltroData() { SeqCampanhaCicloLetivo = model.SeqCampanhaCicloLetivo, SeqProcessoSeletivo = model.SeqProcessoSeletivo };
            model.SeqProcesso = ProcessoService.BuscarProcesso(filtroCampanha)?.Seq ?? 0;

            // Valida a quantidade de ofertas por nível tipo vínculo UC_ALN_002_01_02.NV15
            var configVinculo = this.InstituicaoNivelTipoVinculoAlunoService
                .BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(model.SeqNivelEnsino, model.SeqTipoVinculoAluno);
            if (model.Ofertas.SMCCount() > configVinculo.QuantidadeOfertaCampanhaIngresso.GetValueOrDefault())
            {
                var tipoVinculo = model.TiposVinculoAluno.FirstOrDefault(f => f.Seq == model.SeqTipoVinculoAluno)?.Descricao;
                throw new QuantidadeOfertasIngressanteException(tipoVinculo);
            }

            // Caso não seja selecionada nenhuma oferta
            if (model.Ofertas.SMCCount() == 0)
                throw new AssociacaoIngressanteOfertaObrigatoriaException();

            // Limpa os dados de ingercâmbio caso este não seja permitido
            if (!configVinculo.ExigeParceriaIntercambioIngresso.GetValueOrDefault())
            {
                model.TermosIntercambio = null;
            }

            // Limpa a orientação caso esta não seja permitida
            if (!ValidarOrientacaoPermitidaBool(model.SeqNivelEnsino, model.SeqTipoVinculoAluno, model.TermosIntercambio?.FirstOrDefault()?.SeqTermoIntercambio?.Seq))
            {
                model.OrientacaoParticipacoesColaboradores = null;
            }

            var ingressanteData = model.Transform<IngressanteData>();
            IngressanteService.ValidarTermosIntercambioIngressante(ingressanteData);
            IngressanteService.ValidarOrientacaoIngressante(ingressanteData);
            IngressanteService.ValidarVagasOfertasDisciplinaIsoladaIngressante(ingressanteData);

            this.SetViewMode(SMCViewMode.Insert);
            return ViewWizard(model);
        }

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult Confirmacao(IngressanteDynamicModel model)
        {
            this.ConfigureDynamic(model);

            IngressanteService.ValidarGrupoEscalonamentoIngressante(model.SeqGrupoEscalonamento.Seq);

            // Formatação dados pessoais

            // Nome social formatado conforme a regra RN_PES_023 - Nome e Nome Social - Visão Administrativo
            model.NomeConfirmacao = !string.IsNullOrEmpty(model.NomeSocial) ?
                $"{model.NomeSocial} ({model.Nome})" : model.Nome;

            model.NumeroPassaporteConfirmacao = string.IsNullOrEmpty(model.NumeroPassaporte) ? "-" : model.NumeroPassaporte;

            model.EmailConfirmacao = model.EnderecosEletronicos?.FirstOrDefault(f => f.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)?.Descricao ?? "-";

            // Formatação vínculos
            bool exibirVinculoComTermo = IngressanteService.ConsistenciaVinculoTipoTermoIntercambio(model.Transform<IngressanteData>());

            model.VinculoConfirmacao = exibirVinculoComTermo ?
                $"{model.TiposVinculoAluno.FirstOrDefault(f => f.Seq == model.SeqTipoVinculoAluno)?.Descricao} - {model.TermosIntercambio?.FirstOrDefault()?.DescricaoTipoIntercambio}" :
                $"{model.TiposVinculoAluno.FirstOrDefault(f => f.Seq == model.SeqTipoVinculoAluno)?.Descricao}";

            var tiposParticipacao = InstituicaoNivelTipoOrientacaoParticipacaoService.BuscarInstituicaoNivelTipoOrientacaoParticipacoes(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData());

            if (model.CondicoesObrigatoriedade.SMCCount() > 0)
            {
                // FIX: Remover ao corrigir o HiddenFor dentro de uma lista submetível
                var condicoes = this.CondicaoObrigatoriedadeService.BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(model.SeqMatrizCurricularOferta.GetValueOrDefault());
                model.CondicoesObrigatoriedadeConfirmacao = new List<IngressanteCondicaoObrigatoriedadeConfirmacaoViewModel>();
                foreach (var codicao in model.CondicoesObrigatoriedade)
                {
                    model.CondicoesObrigatoriedadeConfirmacao.Add(new IngressanteCondicaoObrigatoriedadeConfirmacaoViewModel()
                    {
                        CondicaoObrigatoriedade = condicoes.FirstOrDefault(f => f.Seq == codicao.SeqCondicaoObrigatoriedade)?.Descricao,
                        Ativa = codicao.Ativo
                    });
                }
                //model.CondicoesObrigatoriedadeConfirmacao = model.CondicoesObrigatoriedade.Select(s => $"{s.DescricaoCondicaoObrigatoriedade}: {s.Ativo}").ToList();
            }

            model.MatrizCurricularOfertaConfirmacao = model.SeqMatrizCurricularOferta.HasValue ?
                MatrizCurricularOfertaService.BuscarMatrizCurricularOferta(model.SeqMatrizCurricularOferta.GetValueOrDefault())?.DescricaoMatrizCurricular : "-";

            if (model.TermosIntercambio.SMCAny())
            {
                // Carrega os datasources de orientação
                var seqsColaboradores = model.TermosIntercambio.SelectMany(s => s.OrientacaoParticipacoesColaboradores?.Select(so => so.SeqColaborador)).Distinct().ToArray();
                var colaboradores = ColaboradorService.BuscarColaboradoresSelect(new ColaboradorFiltroData() { Seqs = seqsColaboradores });
                var instituicoesExternas = InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroData() { SeqsColaboradores = seqsColaboradores });

                foreach (var termo in model.TermosIntercambio)
                {
                    if (termo.OrientacaoParticipacoesColaboradores != null && termo.OrientacaoParticipacoesColaboradores.Count > 0)
                    {
                        foreach (var orientacao in termo.OrientacaoParticipacoesColaboradores)
                        {
                            orientacao.NomeColaborador = colaboradores.FirstOrDefault(f => f.Seq == orientacao.SeqColaborador)?.Descricao;
                            orientacao.NomeInstituicaoExterna = instituicoesExternas.FirstOrDefault(f => f.Seq == orientacao.SeqInstituicaoExterna)?.Descricao;
                            orientacao.ColaboradorParticipacaoConfirmacao = $"{orientacao.NomeColaborador} - {orientacao.TipoParticipacaoOrientacao.SMCGetDescription()}";
                        }
                    }
                }
            }
            this.SetViewMode(SMCViewMode.ReadOnly);
            return ViewWizard(model);
        }

        #endregion [ Steps Wizard Ingressante ]

        #region [ Dependencies Ingressante ]

        [HttpPost]
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public override ActionResult BuscarPessoaExistente(IngressanteDynamicModel model)
        {
            return base.BuscarPessoaExistente(model);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarProcessosSeletivos(long? campanha = null, long? seqCampanha = null)
        {
            return Json(this.ProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaSelect(campanha ?? seqCampanha ?? 0));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarProcessosSeletivosIngressoDireto(long? campanha = null, long? seqCampanha = null)
        {
            return Json(this.ProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect(campanha ?? seqCampanha ?? 0));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarNiveisEnsino(long seqProcessoSeletivo)
        {
            return Json(this.InstituicaoNivelService.BuscarNiveisEnsinoPorProcessoSeletivoSelect(seqProcessoSeletivo));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarCiclosLetivos(long seqCampanha, long seqNivelEnsino)
        {
            return Json(this.CicloLetivoService.BuscarCiclosLetivosPorCampanhaNivelSelect(seqCampanha, seqNivelEnsino));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarTiposVinculoAluno(long? seqProcessoSeletivo)
        {
            if (!seqProcessoSeletivo.HasValue)
            {
                return Json(TipoVinculoAlunoService.BuscarTipoVinculoAlunoNaInstituicaoSelect(null));
            }
            return Json(this.TipoVinculoAlunoService.BuscarTiposVinculoAlunoPorProcessoSeletivo(seqProcessoSeletivo.Value));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarFormasIngresso(long? seqTipoVinculoAluno, long? seqProcessoseletivo)
        {
            return Json(this.FormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect(new FormaIngressoFiltroData() { SeqTipoVinculoAluno = seqTipoVinculoAluno, SeqProcessoSeletivo = seqProcessoseletivo }));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult TipoVinculoAlunoExigeOfertaMatrizCurricular(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            var exigeOfertaMatrizCurricular = this.InstituicaoNivelTipoVinculoAlunoService
                .BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino, seqTipoVinculoAluno)
                ?.ExigeOfertaMatrizCurricular ?? false;
            return Json(exigeOfertaMatrizCurricular);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarQuantidadeOfertaCampanhaIngresso(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            var quantidadeOfertaCampanhaIngresso = this.InstituicaoNivelTipoVinculoAlunoService
                .BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino, seqTipoVinculoAluno)
                ?.QuantidadeOfertaCampanhaIngresso ?? 0;
            return Json(quantidadeOfertaCampanhaIngresso);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarOfertas(int quantidadeOfertaCampanhaIngresso)
        {
            var model = new IngressanteDynamicModel()
            {
                QuantidadeOfertaCampanhaIngresso = quantidadeOfertaCampanhaIngresso,
                Ofertas = new SMCMasterDetailList<IngressanteOfertaDetailViewModel>()
                {
                    new IngressanteOfertaDetailViewModel()
                }
            };
            return PartialView("_Ofertas", model);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarTermosIntercambio(bool exigeParceriaIntercambioIngresso)
        {
            var model = new IngressanteDynamicModel() { ExigeParceriaIntercambioIngresso = exigeParceriaIntercambioIngresso };
            return PartialView("_TermosIntercambio", model);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarOfertasMatrizCurricular(long seqNivelEnsino, long seqTipoVinculoAluno, long seqCicloLetivo, List<long> ofertasMatrizDependencyOfertas, List<long> ofertasMatrizDependencyTermos)
        {
            return Json(this.MatrizCurricularOfertaService.BuscarMatrizesCurricularesOfertasPorCampanhaSelect(new MatrizCurricularOfertaFiltroData()
            {
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno,
                SeqCicloLetivo = seqCicloLetivo,
                SeqsCampanhaOferta = ofertasMatrizDependencyOfertas,
                SeqsTermoIntercambio = ofertasMatrizDependencyTermos
            }));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarCondicoesObrigatoriedadeMatrizCurricular(long SeqMatrizCurricularOferta, long? seq = null, bool dadosAcademicosSomenteLeitura = false)
        {
            var model = new IngressanteDynamicModel();

            // Caso seja uma edição, recupera as condições de obrigatoriedade da pessoa
            if (seq.HasValue)
            {
                var codicoes = this.PessoaAtuacaoCondicaoObrigatoriedadeService.AlterarPessoaAtuacaoCondicaoObrigatoriedade(seq.Value);
                model.CondicoesObrigatoriedade = codicoes.CondicoesObrigatoriedade.TransformList<PessoaAtuacaoCondicaoObrigatoriedadeViewModel>();
            }
            else
            {
                // Caso contrário, busca as condições de obrigatoriedade da oferta de matriz selecionada
                var condicoes = this.CondicaoObrigatoriedadeService.BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(SeqMatrizCurricularOferta);
                model.CondicoesObrigatoriedade = condicoes.Select(s => new PessoaAtuacaoCondicaoObrigatoriedadeViewModel()
                {
                    SeqCondicaoObrigatoriedade = s.Seq,
                    DescricaoCondicaoObrigatoriedade = s.Descricao
                }).ToList();
            }

            // Caso seja para manter como somente leitura, força a situação e origem para valores que definem os dados academicos como somente leitura
            if (dadosAcademicosSomenteLeitura)
            {
                model.SituacaoIngressante = SituacaoIngressante.AguardandoLiberacaMatricula;
                model.OrigemIngressante = OrigemIngressante.Convocacao;
            }

            return PartialView("_CondicoesObrigatoriedade", model);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult TipoVinculoAlunoExigeParceriaIntercambioIngresso(long seqNivelEnsino, long seqTipoVinculoAluno)
        {
            return Json((InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino, seqTipoVinculoAluno)?.ExigeParceriaIntercambioIngresso ?? false));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarTipoTermoIntercambio(long seqTermoIntercambio)
        {
            var tipoTermoIntercambio = this.TermoIntercambioService
                .ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio })
                .FirstOrDefault()
                ?.TipoTermoIntercambio ?? "";
            return Json(tipoTermoIntercambio);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarDependenciasTermoIntercambio(long[] seqTermoIntercambio, long[] seqNivelEnsino, long[] seqTipoVinculoAluno)
        {
            //Fix: Voltar para parâmetros long ao corrigir o dependency
            var tipoTermoIntercambio = this.TermoIntercambioService.ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio[0] }).FirstOrDefault();

            var seqTipoTermo = TermoIntercambioService.PreencherModeloTermoIntercambio(seqTermoIntercambio[0])?.SeqTipoTermoIntercambio ?? 0;
            var existePeriodo = InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino[0], seqTipoVinculoAluno[0])?.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == seqTipoTermo).ExigePeriodoIntercambioTermo ?? false;
            var permiteOrientacao = ValidarOrientacaoPermitidaBool(seqNivelEnsino[0], seqTipoVinculoAluno[0], seqTermoIntercambio[0]);
            var requerOrientacao = BuscarTiposOrientacaoSelect(seqNivelEnsino[0], seqTipoVinculoAluno[0], seqTermoIntercambio[0], new[] { CadastroOrientacao.Exige }).Count > 0;
            var seqTipoOrientacao = BuscarTiposOrientacaoSelect(seqNivelEnsino[0], seqTipoVinculoAluno[0], seqTermoIntercambio[0], new[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite });

            var ret = new
            {
                DescricaoTipoIntercambio = tipoTermoIntercambio?.TipoTermoIntercambio ?? "",
                SeqInstituicaoEnsinoExterna = tipoTermoIntercambio?.SeqInstituicaoEnsinoExterna ?? 0,
                InstituicaoExterna = tipoTermoIntercambio?.InstituicaoEnsinoExterna ?? "",
                ExistePeriodo = existePeriodo,
                PermiteOrientacao = permiteOrientacao,
                RequerOrientacao = requerOrientacao,
                //DataInicio = tipoTermoIntercambio.DataInicio,
                //DataFim = tipoTermoIntercambio.DataFim,
                SeqTipoOrientacao = seqTipoOrientacao,
            };

            return Json(ret);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarDependenciasTermoIntercambioDatas(long[] seqTermoIntercambio)
        {
            //Fix: Voltar para parâmetros long ao corrigir o dependency
            var tipoTermoIntercambio = this.TermoIntercambioService.ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio[0] }).FirstOrDefault();

            var ret = new
            {
                tipoTermoIntercambio.DataInicio,
                tipoTermoIntercambio.DataFim,
            };

            return Json(ret);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarSeqInstituicaoEnsinoExterna(long seqTermoIntercambio)
        {
            var tipoTermoIntercambio = this.TermoIntercambioService
                .ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio })
                .FirstOrDefault()
                ?.SeqInstituicaoEnsinoExterna ?? 0;
            return Json(tipoTermoIntercambio);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarDescricaoInstituicaoEnsinoExterna(long seqTermoIntercambio)
        {
            var tipoTermoIntercambio = this.TermoIntercambioService
                .ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio })
                .FirstOrDefault()
                ?.InstituicaoEnsinoExterna ?? "";
            return Json(tipoTermoIntercambio);
        }

        //FIX: Verificar por que o lookup está passando array quando é feita a busca normal (lupinha)
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult VerificarExistenciaDePeriodoTermosIntercambio(long[] seqNivelEnsino, long[] seqTipoVinculoAluno, long seqTermoIntercambio)
        {
            var seqTipoTermo = TermoIntercambioService.PreencherModeloTermoIntercambio(seqTermoIntercambio)?.SeqTipoTermoIntercambio ?? 0;
            return Json((InstituicaoNivelTipoVinculoAlunoService.BuscarInstituicaoNivelTipoVinculoAlunoPorNivelTipo(seqNivelEnsino.FirstOrDefault(), seqTipoVinculoAluno.FirstOrDefault())?.TiposTermoIntercambio.FirstOrDefault(f => f.SeqTipoTermoIntercambio == seqTipoTermo).ExigePeriodoIntercambioTermo ?? false));
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarDataInicioTermoIntercambio(long seqTermoIntercambio)
        {
            var dataInicio = this.TermoIntercambioService
                .ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio })
                .FirstOrDefault()
                ?.DataInicio;
            return Json(dataInicio);
        }

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult BuscarDataFimTermoIntercambio(long seqTermoIntercambio)
        {
            var dataFim = this.TermoIntercambioService
                .ListarTermoIntercambio(new TermoIntercambioFiltroData() { Seq = seqTermoIntercambio })
                .FirstOrDefault()
                ?.DataFim;
            return Json(dataFim);
        }

        //FIX: Verificar por que o lookup está passando array quando é feita a busca normal (lupinha)
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE, UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult ValidarOrientacaoPermitida(long[] seqNivelEnsino, long[] seqTipoVinculoAluno, long? seqTermoIntercambio)
        {
            return Json(ValidarOrientacaoPermitidaBool(seqNivelEnsino.FirstOrDefault(),
                                                    seqTipoVinculoAluno.FirstOrDefault(),
                                                    seqTermoIntercambio));
        }

        private bool ValidarOrientacaoPermitidaBool(long seqNivelEnsino, long seqTipoVinculoAluno, long? seqTermoIntercambio)
        {
            return BuscarTiposOrientacaoSelect(seqNivelEnsino,
                                               seqTipoVinculoAluno,
                                               seqTermoIntercambio,
                                               new[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite }).Count > 0;
        }

        //FIX: Verificar por que o lookup está passando array quando é feita a busca normal (lupinha)
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE, UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult BuscarTiposOrientacao(long[] seqNivelEnsino, long[] seqTipoVinculoAluno, long? seqTermoIntercambio)
        {
            return Json(BuscarTiposOrientacaoSelect(seqNivelEnsino.FirstOrDefault(),
                                                    seqTipoVinculoAluno.FirstOrDefault(),
                                                    seqTermoIntercambio,
                                                    new[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite }));
        }

        //FIX: Verificar por que o lookup está passando array quando é feita a busca normal (lupinha)
        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE, UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult ValidarOrientacaoRequerida(long[] seqNivelEnsino, long[] seqTipoVinculoAluno, long? seqTermoIntercambio)
        {
            return Json(BuscarTiposOrientacaoSelect(seqNivelEnsino.FirstOrDefault(),
                                                    seqTipoVinculoAluno.FirstOrDefault(),
                                                    seqTermoIntercambio,
                                                    new[] { CadastroOrientacao.Exige }).Count > 0);
        }

        private List<SMCDatasourceItem> BuscarTiposOrientacaoSelect(long seqNivelEnsino, long seqTipoVinculoAluno, long? seqTermoIntercambio, CadastroOrientacao[] cadastroOrientacao)
        {
            return this.InstituicaoNivelTipoOrientacaoService
                            .BuscarTiposOrientacaoSelect(new InstituicaoNivelTipoOrientacaoFiltroData()
                            {
                                SeqNivelEnsino = seqNivelEnsino,
                                SeqTipoVinculoAluno = seqTipoVinculoAluno,
                                SeqTermoIntercambio = seqTermoIntercambio,
                                PossuiTipoIntercambio = seqTermoIntercambio.HasValue,
                                CadastroOrientacoesIngressante = cadastroOrientacao
                            });
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposParticipacao(long? seqTipoOrientacao, long? seqNivelEnsino, long? seqTermoIntercambio, long? seqTipoVinculoAluno)
        {
            return Json(BuscarTiposParticipacaoDataSource(seqTipoOrientacao, seqNivelEnsino, seqTermoIntercambio, seqTipoVinculoAluno));
        }

        private List<SMCSelectListItem> BuscarTiposParticipacaoDataSource(long? seqTipoOrientacao, long? seqNivelEnsino, long? seqTermoIntercambio, long? seqTipoVinculoAluno)
        {
            return InstituicaoNivelTipoOrientacaoParticipacaoService
                .BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData()
                {
                    SeqTipoOrientacao = seqTipoOrientacao,
                    SeqNivelEnsino = seqNivelEnsino,
                    SeqTermoIntercambio = seqTermoIntercambio,
                    SeqTipoVinculo = seqTipoVinculoAluno
                }).TransformList<SMCSelectListItem>();
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarOrigemColaborador(TipoParticipacaoOrientacao TipoParticipacaoOrientacao, long seqTipoOrientacao, long seqNivelEnsino, long seqTermoIntercambio, long seqTipoVinculoAluno)
        {
            return Json(InstituicaoNivelTipoOrientacaoParticipacaoService
                .BuscarInstituicaoNivelTipoOrientacaoParticipacaoOrigemSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData()
                {
                    TipoParticipacaoOrientacao = TipoParticipacaoOrientacao,
                    SeqTipoOrientacao = seqTipoOrientacao,
                    SeqNivelEnsino = seqNivelEnsino,
                    SeqTermoIntercambio = seqTermoIntercambio,
                    SeqTipoVinculo = seqTipoVinculoAluno
                }));
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarColaboradores(long? seqEntidadeResponsavel = null, long? seqInstituicaoEnsinoExterna = null, long? seqCampanhaOferta = null)
        {
            return Json(BuscarColaboradoresDataSource(seqEntidadeResponsavel, seqInstituicaoEnsinoExterna, seqCampanhaOferta));
        }

        private List<SMCSelectListItem> BuscarColaboradoresDataSource(long? seqEntidadeResponsavel = null, long? seqInstituicaoEnsinoExterna = null, long? seqCampanhaOferta = null)
        {
            var filtros = new ColaboradorFiltroData()
            {
                TipoAtividade = TipoAtividadeColaborador.Orientacao,
                VinculoAtivo = true,
                SeqEntidadeVinculo = seqEntidadeResponsavel,
                SeqInstituicaoExterna = seqInstituicaoEnsinoExterna,
                SeqCampanhaOferta = seqCampanhaOferta,
            };
            return ColaboradorService.BuscarColaboradoresSelect(filtros).TransformList<SMCSelectListItem>();
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarInstituicoesExternasColaborador(long seqColaborador, long? seqTermoIntercambio = null, long? seqInstituicaoEnsinoExterna = null, TipoParticipacaoOrientacao? tipoParticipacaoOrientacao = null, long? seqNivelEnsino = null, long? seqTipoVinculoAluno = null)
        {
            return Json(BuscarInstituicoesExternasColaboradorDataSource(seqColaborador, seqTermoIntercambio, seqInstituicaoEnsinoExterna, tipoParticipacaoOrientacao, seqNivelEnsino, seqTipoVinculoAluno));
        }

        private List<SMCSelectListItem> BuscarInstituicoesExternasColaboradorDataSource(long seqColaborador, long? seqTermoIntercambio = null, long? seqInstituicaoEnsinoExterna = null, TipoParticipacaoOrientacao? tipoParticipacaoOrientacao = null, long? seqNivelEnsino = null, long? seqTipoVinculoAluno = null)
        {
            var filtros = new InstituicaoExternaFiltroData()
            {
                SeqColaborador = seqColaborador,
                SeqTermoIntercambio = seqTermoIntercambio,
                SeqInstituicaoEnsinoExterna = seqInstituicaoEnsinoExterna,
                TipoParticipacaoOrientacao = tipoParticipacaoOrientacao,
                SeqNivelEnsino = seqNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno,
                SeqInstituicaoEnsino = HttpContext.GetInstituicaoEnsinoLogada().Seq,
                Ativo = true
            };
            return InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(filtros).TransformList<SMCSelectListItem>();
        }

        #endregion [ Dependencies Ingressante ]

        #region [ Cabeçalho Ingressante ]

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE)]
        public ActionResult CabecalhoIngressante(SMCEncryptedLong seqIngressante)
        {
            var modelHeader = ExecuteService<PessoaAtuacaoCabecalhoData, AssociacaoIngressanteLoteCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho, seqIngressante);
            return PartialView("_Cabecalho", modelHeader);
        }

        #endregion [ Cabeçalho Ingressante ]

        #region [ Liberação de Matrícula ]

        [SMCAuthorize(UC_ALN_002_01_02.MANTER_INGRESSANTE)]
        public ActionResult LiberarMatricula(SMCEncryptedLong seq)
        {
            try
            {
                IngressanteService.LiberarIngressanteMatricula(seq);
                SetSuccessMessage(UIResource.MSG_LiberacaoMatriculaSucesso, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }
            return SMCRedirectToAction("Index");
        }

        #endregion [ Liberação de Matrícula ]

        #region [ Associação Formação Específica ]

        [SMCAuthorize(UC_ALN_002_03_02.ASSOCIAR_FORMACAO_ESPECIFICA)]
        public ActionResult AssociacaoFormacaoEspecificaIngressante(SMCEncryptedLong seqIngressante)
        {
            var result = this.IngressanteService.BuscarAssociacaoFormacoesEspecificasIngressante(seqIngressante);

            var modelo = result.Transform<AssociacaoFormacaoEspecificaIngressanteViewModel>();

            if (IngressanteService.ValidarSituacaoImpeditivaIngressante(seqIngressante))
            {
                this.SetViewMode(SMCViewMode.ReadOnly);
                ViewBag.Title = UIResource.Title_Associacao_Formacao_Especifica_Readonly;
            }

            return View(modelo);
        }

        [SMCAuthorize(UC_ALN_002_03_02.ASSOCIAR_FORMACAO_ESPECIFICA)]
        public ActionResult SalvarAssociacaoFormacaoEspecificaIngressante(AssociacaoFormacaoEspecificaIngressanteViewModel model)
        {
            try
            {
                var seqInstituicao = HttpContext.GetInstituicaoEnsinoLogada().Seq;

                this.IngressanteService.SalvarAssociacaoFormacaoEspecíficaIngressante(seqInstituicao, model.Transform<AssociacaoFormacaoEspecificaIngressanteData>());

                string msg = string.Format(UIResource.MSG_AssociacaoFormacaoEspecificaIngressante_Sucesso, "Associação de formação específica de ingressante");

                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(IngressanteController.AssociacaoFormacaoEspecificaIngressante), new RouteValueDictionary(new { seqIngressante = new SMCEncryptedLong(model.SeqIngressante) }));
        }

        #endregion [ Associação Formação Específica ]

        #region [ Associação de Orientador ]

        [SMCAuthorize(UC_ALN_002_03_03.ASSOCIAR_ORIENTADOR)]
        public ActionResult AssociacaoOrientadorIngressante(SMCEncryptedLong seqIngressante, SMCEncryptedLong seqNivelEnsino)
        {
            var result = this.IngressanteService.BuscarAssociacaoOrientadorIngressante(seqIngressante);

            var modelo = result.Transform<AssociacaoOrientadorIngressanteViewModel>();

            PrepararModeloAssociacaoOrientadorIngressante(modelo, seqNivelEnsino);

            if (IngressanteService.ValidarSituacaoImpeditivaIngressante(seqIngressante))
            {
                this.SetViewMode(SMCViewMode.ReadOnly);
                ViewBag.Title = UIResource.AssociacaoOrientadorIngressante_Title;
            }

            return View(modelo);
        }

        private void PrepararModeloAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteViewModel modelo, long? seqNivelEnsino)
        {
            modelo.Colaboradores = this.ColaboradorService.BuscarColaboradoresPorIngressanteSelect(modelo.SeqIngressante);
            modelo.TiposOrientacao = this.InstituicaoNivelTipoOrientacaoService.BuscarTiposOrientacaoSelect(new InstituicaoNivelTipoOrientacaoFiltroData() { SeqNivelEnsino = seqNivelEnsino, SeqTipoIntercambio = modelo.SeqTipoIntercambio, PossuiTipoIntercambio = modelo.SeqTipoIntercambio.HasValue });
            modelo.TiposParticipacaoOrientacao = this.InstituicaoNivelTipoOrientacaoParticipacaoService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData() { SeqTipoOrientacao = modelo.SeqTipoOrientacao, SeqNivelEnsino = seqNivelEnsino, SeqTipoIntercambio = modelo.SeqTipoIntercambio });
        }

        [SMCAuthorize(UC_ALN_002_03_03.ASSOCIAR_ORIENTADOR)]
        public ActionResult SalvarAssociacaoOrientadorIngressante(AssociacaoOrientadorIngressanteViewModel model)
        {
            try
            {
                this.IngressanteService.SalvarAssociacaoOrientadorIngressante(model.Transform<AssociacaoOrientadorIngressanteData>());

                string msg = string.Format(UIResource.MSG_AssociacaoOrientadorIngressante_Sucesso, "Associação de orientador");

                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(AssociacaoOrientadorIngressante), "Ingressante", new { SeqIngressante = new SMCEncryptedLong(model.SeqIngressante), SeqNivelEnsino = new SMCEncryptedLong(model.SeqNivelEnsino) });
        }

        [SMCAuthorize(UC_ALN_002_03_03.ASSOCIAR_ORIENTADOR)]
        public ActionResult BuscarOrientacoesIngressante(AssociacaoOrientadorIngressanteViewModel model)
        {
            model.TiposParticipacaoOrientacao = this.InstituicaoNivelTipoOrientacaoParticipacaoService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroData() { SeqTipoOrientacao = model.SeqTipoOrientacao, SeqNivelEnsino = model.SeqNivelEnsino, SeqTipoIntercambio = model.SeqTipoIntercambio });

            model.Orientacoes = this.IngressanteService.BuscarOrientacoesIngressante(model.Transform<AssociacaoOrientadorIngressanteData>()).TransformMasterDetailList<AssociacaoOrientadorIngressanteItemViewModel>();

            model.Colaboradores = this.ColaboradorService.BuscarColaboradoresPorIngressanteSelect(model.SeqIngressante);

            if (IngressanteService.ValidarSituacaoImpeditivaIngressante(model.SeqIngressante))
                this.SetViewMode(SMCViewMode.ReadOnly);

            return PartialView("_OrientacoesIngressante", model);
        }

        #endregion [ Associação de Orientador ]

        #region [ Visualizar Dados Acadêmicos Ingressante ]

        [SMCAuthorize(UC_ALN_002_01_01.PESQUISAR_INGRESSANTE, UC_SRC_004_01_02.CONSULTAR_SOLICITACAO)]
        public ActionResult VisualizarDadosAcademicosIngressante(SMCEncryptedLong seq)
        {
            var model = IngressanteService.BuscarDadosAcademicosIngressante(seq).Transform<IngressanteListarDynamicModel>();
            return PartialView("_VisualizarDadosAcademicos", model);
        }

        #endregion [ Visualizar Dados Acadêmicos Ingressante ]

        #region [ Configuracao ]

        public override IngressanteDynamicModel BuscarConfiguracao()
        {
            return IngressanteService.BuscarConfiguracaoIngressante().Transform<IngressanteDynamicModel>();
        }

        #endregion [ Configuracao ]
    }
}