using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.ORT.Exceptions.Orientacao;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using SMC.SGA.Administrativo.Areas.ORT.Views.Orientacao.App_LocalResources;
using SMC.SGA.Administrativo.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class OrientacaoController : SMCDynamicControllerBase
    {
        #region Serviços

        private IInstituicaoNivelTipoVinculoAlunoService InstituicaoNivelTipoVinculoAlunoService
        {
            get { return this.Create<IInstituicaoNivelTipoVinculoAlunoService>(); }
        }

        private IInstituicaoNivelTipoOrientacaoParticipacaoService InstituicaoNivelTipoOrientacaoParticipacaoService
        {
            get { return this.Create<IInstituicaoNivelTipoOrientacaoParticipacaoService>(); }
        }

        private IInstituicaoNivelTipoOrientacaoService InstituicaoNivelTipoOrientacaoService
        {
            get { return this.Create<IInstituicaoNivelTipoOrientacaoService>(); }
        }

        private IAlunoService AlunosService
        {
            get { return this.Create<IAlunoService>(); }
        }

        private IInstituicaoExternaService InstituicaoExternaService
        {
            get { return this.Create<IInstituicaoExternaService>(); }
        }

        private ISituacaoMatriculaService SituacaoMatricula
        {
            get { return this.Create<ISituacaoMatriculaService>(); }
        }

        private ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        private IOrientacaoService OrientacaoService
        {
            get { return this.Create<IOrientacaoService>(); }
        }

        private ITipoTermoIntercambioService TipoTermoIntercambioService
        {
            get { return this.Create<ITipoTermoIntercambioService>(); }
        }

        private ITipoOrientacaoService TipoOrientacaoService
        {
            get { return this.Create<ITipoOrientacaoService>(); }
        }

        private ITipoSituacaoMatriculaService TipoSituacaoMatriculaService => Create<ITipoSituacaoMatriculaService>();

        #endregion Serviços

        #region Constantes

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Constantes

        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public JsonResult BuscarTipoVinculoSelect(long SeqNivelEnsino)
        {
            List<SMCDatasourceItem> itens = InstituicaoNivelTipoVinculoAlunoService.BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(SeqNivelEnsino);

            return Json(itens);
        }

        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public JsonResult BuscarTipoIntecambioSelect(long? seq, long SeqTipoVinculoAluno, long SeqNivelEnsino)
        {
            List<SMCDatasourceItem> itens = InstituicaoNivelTipoVinculoAlunoService.BuscarTermoIntercambioPorNivelEnsinoPermiteManutencaoManual(SeqNivelEnsino, SeqTipoVinculoAluno);

            return Json(itens);
        }

        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public JsonResult BuscarTipoOrientacaoSelect(long SeqNivelEnsino, long SeqTipoVinculoAluno, long? SeqTipoTermoIntercambio)
        {
            List<SMCDatasourceItem> itens = InstituicaoNivelTipoVinculoAlunoService.BuscarTipoOrientacaoPorNivelEnsinoPermiteInclusaoManual(SeqNivelEnsino, SeqTipoVinculoAluno, SeqTipoTermoIntercambio);

            return Json(itens);
        }

        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public JsonResult BuscarInstituicaoExternaPorColaboradoroSelect(long SeqColaborador, bool? Ativo)
        {
            List<SMCDatasourceItem> itens = InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroData { Ativo = Ativo, SeqColaborador = SeqColaborador });

            return Json(itens);
        }

        /// <summary>
        /// Step Dados Orientação
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public ActionResult Passo1(OrientacaoDynamicModel model)
        {
            ///Faz a verificação de inserção/edição pelo fato do wizard perder em qual viewmode ele se encontra de forma automatica
            if (model.Seq == 0)
            {
                SMCHtmlHelperExtension.SetViewMode(this, SMCViewMode.Insert);
            }
            else
            {
                SMCHtmlHelperExtension.SetViewMode(this, SMCViewMode.Edit);
            }

            this.ConfigureDynamic(model);

            model.SeqEntidadeInstituicao = HttpContext.GetInstituicaoEnsinoLogada().Seq;

            if (model.Seq != 0)
            {
                model.SeqNivelEnsinoComparacao = model.SeqNivelEnsino;
                model.SeqTipoIntercambioComparacao = model.SeqTipoTermoIntercambio;
                model.SeqTipoOrientacaoComparacao = model.SeqTipoOrientacao;
                model.SeqTipoVinculoComparacao = model.SeqTipoVinculoAluno;
            }

            if (model.Seq == 0)
            {
                ///Toda vez que passa no passo um ele irá receber um incremento para assim exibir uma mensagem na tela
                model.StepOrigem += 1;
            }

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Alunos
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public ActionResult Passo2(OrientacaoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            ///Caso haja alguma alteração no passo1 ele removerá limpará as listas
            if (model.SeqNivelEnsinoComparacao != model.SeqNivelEnsino
                || model.SeqTipoIntercambioComparacao != model.SeqTipoTermoIntercambio
                || model.SeqTipoOrientacaoComparacao != model.SeqTipoOrientacao
                || model.SeqTipoVinculoComparacao != model.SeqTipoVinculoAluno)
            {
                model.OrientacoesColaborador = null;
                model.OrientacoesPessoaAtuacao = null;
                model.StepOrigemAluno = 0;
            }

            model.SeqNivelEnsinoComparacao = model.SeqNivelEnsino;
            model.SeqTipoIntercambioComparacao = model.SeqTipoTermoIntercambio;
            model.SeqTipoOrientacaoComparacao = model.SeqTipoOrientacao;
            model.SeqTipoVinculoComparacao = model.SeqTipoVinculoAluno;

            model.NumeroMaximoAlunos = (int)InstituicaoNivelTipoOrientacaoService.BuscarNumeroMaximoAlunosOrientacao(new InstituicaoNivelTipoOrientacaoFiltroData
            {
                SeqNivelEnsino = model.SeqNivelEnsino,
                SeqTipoIntercambio = model.SeqTipoTermoIntercambio,
                SeqTipoOrientacao = model.SeqTipoOrientacao,
                SeqTipoVinculoAluno = model.SeqTipoVinculoAluno
            });

            ///Se for edição o step de origem do aluno já vem setado para sempre exibir
            if (model.Seq != 0)
            {
                model.StepOrigemAluno += 2;

                ///Se for edição sempre que entrar no passo de alunos os alunos de comparação serão setados
                model.OrientacoesPessoaAtuacaoComparacao = new List<long>();
                foreach (var item in model.OrientacoesPessoaAtuacao)
                {
                    model.OrientacoesPessoaAtuacaoComparacao.Add((long)item.SeqPessoaAtuacao.Seq);
                }
            }

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Professores
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public ActionResult Passo3(OrientacaoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            ///Toda vez que passa no passo um ele irá receber um incremento para assim exibir uma mensagem na tela
            model.StepOrigemAluno += 2;

            if (model.OrientacoesPessoaAtuacaoComparacao != null)
            {
                ///Cria a lista de seqs de pessoa atuação
                List<long> listaSeqPessoaAtuacao = new List<long>();
                foreach (var item in model.OrientacoesPessoaAtuacao)
                {
                    listaSeqPessoaAtuacao.Add((long)item.SeqPessoaAtuacao.Seq);
                }

                ///Cria a comparação entre as duas listas tanto a exceção de um lado quanto de outro
                var listaDiferentesComparacao = model.OrientacoesPessoaAtuacaoComparacao.Except(listaSeqPessoaAtuacao);

                ///Cria a comparação entre as duas listas tanto a exceção de um lado quanto de outro
                var listaDiferentesOriginal = listaSeqPessoaAtuacao.Except(model.OrientacoesPessoaAtuacaoComparacao);

                if (listaDiferentesOriginal.Any() || listaDiferentesComparacao.Any())
                {
                    model.OrientacoesColaborador = null;
                }
            }

            ///Monta a lista de comparação novamente, para casos o usuario tente novamente ir no passo anterior
            model.OrientacoesPessoaAtuacaoComparacao = new List<long>();
            foreach (var item in model.OrientacoesPessoaAtuacao)
            {
                model.OrientacoesPessoaAtuacaoComparacao.Add((long)item.SeqPessoaAtuacao.Seq);
            }

            var filtroOrientacoes = new InstituicaoNivelTipoOrientacaoFiltroData
            {
                SeqNivelEnsino = model.SeqNivelEnsino,
                SeqTipoIntercambio = model.SeqTipoTermoIntercambio,
                SeqTipoOrientacao = model.SeqTipoOrientacao,
                SeqTipoVinculoAluno = model.SeqTipoVinculoAluno,
            };

            if (!model.SeqTipoIntercambio.HasValue)
            {
                filtroOrientacoes.ExcetoParceriaIntercambio = true;
            }

            var listaOrietnacoes = InstituicaoNivelTipoOrientacaoService.BuscarTiposOritencoes(filtroOrientacoes);

            if (model.OrientacoesColaborador != null)
            {
                foreach (var item in model.OrientacoesColaborador)
                {
                    if (item.InstituicoesExternas == null || item.InstituicoesExternas.Count() == 0)
                    {
                        item.InstituicoesExternas = InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroData { Ativo = true, SeqColaborador = item.SeqColaborador });
                    }
                }
            }

            string tiposParticipacao = string.Empty;

            foreach (var item in listaOrietnacoes.SelectMany(x => x.TiposParticipacao.Where(w => w.ObrigatorioOrientacao == true).ToList()).ToList())
            {
                tiposParticipacao += "<br />" + item.TipoParticipacaoOrientacao.ToString() + " com a Instituição sendo " + SMCEnumHelper.GetDescription(item.OrigemColaborador);
            }

            model.MensagemIformativa = string.Format(UIResource.MensagemIformativa, tiposParticipacao);

            ///1.Verificar se a pessoa - atuação tem tipo de orientação que é configurada como trabalho de conclusão de curso.
            ///Levando em cosideração que a orientação selecionada seja de conclusão de curso pois não será permitido somente orientção de conclusão por aluno no mesmo nível de ensino
            foreach (var item in model.SeqsAlunos)
            {
                var orientacoesAluno = this.OrientacaoService.BuscarOrientacoesPorAluno(item);
                foreach (var orientacao in orientacoesAluno)
                {
                    var tipoOrientacao = this.TipoOrientacaoService.BuscarTipoOrientacao(orientacao.SeqTipoOrientacao);
                    if (tipoOrientacao.TrabalhoConclusaoCurso)
                    {
                        ///Verifica se a orientação atual e de conclusão de curso e é o mesmo nivel de ensino, desta forma esta duplicando a orientação
                        var tipoOrientacaoAtual = this.TipoOrientacaoService.BuscarTipoOrientacao(model.SeqTipoOrientacao);
                        if (tipoOrientacaoAtual.TrabalhoConclusaoCurso && orientacao.SeqNivelEnsino == model.SeqNivelEnsino)
                        {
                            model.Step = 1;
                            throw new OrientacaoJaExisteEmVigenciaException(this.AlunosService.BuscarAluno(item).Nome, tipoOrientacao.Descricao);
                        }
                    }
                }
            }

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Step Confirmação de Dados
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORT_001_02_02.MANTER_ORIENTACAO)]
        public ActionResult Passo4(OrientacaoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            model.DescricaoNivelEnsino = model.NiveisEnsino.Where(w => w.Seq == model.SeqNivelEnsino).FirstOrDefault().Descricao;

            if (model.SeqTipoTermoIntercambio != null)
            {
                model.DescricaoTipoIntercambio = this.TipoTermoIntercambioService.BuscarTiposTermosIntercambiosSelect().Where(w => w.Seq == model.SeqTipoTermoIntercambio).FirstOrDefault().Descricao;
            }
            else
            {
                model.DescricaoTipoIntercambio = " - ";
            }

            model.DescricaoTipoOrientacao = this.TipoOrientacaoService.BuscarTipoOrientacaoSelect().Where(w => w.Seq == model.SeqTipoOrientacao).FirstOrDefault().Descricao;

            model.DescricaoTipoVinculoAluno = this.InstituicaoNivelTipoVinculoAlunoService.BuscarTipoVinculoPorNivelEnsinoPermiteManutencaoManual(model.SeqNivelEnsino).Where(w => w.Seq == model.SeqTipoVinculoAluno).FirstOrDefault().Descricao;

            model.ListaNomeProfessores = new List<string>();

            foreach (var item in model.OrientacoesColaborador)
            {
                model.ListaNomeProfessores.Add(item.ColaboradorNameDescriptionField);
            }

            var retornoValidacoesOrientacao = OrientacaoService.ValidarOrientacoes(model.Transform<OrientacaoData>());

            if (!string.IsNullOrEmpty(retornoValidacoesOrientacao.ParticipacaoOrientacaoObrigatoria))
            {
                model.Step = 2;
                throw new OrientacaoObrigatoriaException((model.Seq == 0 ? INCLUSAO : ALTERAR), retornoValidacoesOrientacao.ParticipacaoOrientacaoObrigatoria);
            }
            else if (!string.IsNullOrEmpty(retornoValidacoesOrientacao.OrientadoresSemVinculosAtivos))
            {
                model.Step = 2;
                throw new ProfessorNaoTemVinculoValidoException(retornoValidacoesOrientacao.OrientadoresSemVinculosAtivos);
            }

            //FIX: Esta regra esta comentada pois irá ser reavalida no futuro

            /////2.É necessário a associação de pelo menos um professor com um período vigente.
            /////  Caso não ocorra, exibir mensagem de erro abaixo e abortar a operação:
            //int existeColaboradorVigente = 0;
            //foreach (var item in model.OrientacoesColaborador)
            //{
            //    if (item.DataInicioOrientacao <= DateTime.Now && (item.DataFimOrientacao == null
            //                                                     || item.DataFimOrientacao >= DateTime.Now))
            //    {
            //        existeColaboradorVigente++;
            //    }

            //    if (existeColaboradorVigente == 0)
            //    {
            //        model.Step = 2;
            //        throw new OrientacaoAoMenosUmColaboradorVigenteException(model.Seq == 0 ? INCLUSAO : ALTERAR);
            //    }
            //}

            ///Não é possível associar o mesmo colaborador com o mesmo período de datas ou com períodos sobrepostos.Caso ocorra, abortar a operação e exibir e seguinte mensagem de erro:
            ///Professor 1 – De 01/01/2018 a 31/12/2018  -- Professor 1 – Início 03 / 05 / 2018
            var colaboradoresMesmos = model.OrientacoesColaborador.GroupBy(g => g.SeqColaborador).Where(w => w.Count() > 1).SelectMany(s => s).OrderBy(o => o.DataInicioOrientacao).ToList();

            if (colaboradoresMesmos.SMCAny())
            {
                for (int i = 0; i < colaboradoresMesmos.Count(); i++)
                {
                    if (i + 1 < colaboradoresMesmos.Count())///Verificar se existe mais itens na lista para serem comparados e não estourar a lista
                    {
                        if (colaboradoresMesmos[i + 1].DataInicioOrientacao >= colaboradoresMesmos[i].DataInicioOrientacao
                           && (colaboradoresMesmos[i + 1].DataInicioOrientacao <= colaboradoresMesmos[i].DataFimOrientacao || colaboradoresMesmos[i].DataFimOrientacao == null))
                        {
                            throw new OrientacaoComColaboradoresPeriodoSobrepostoException(model.Seq == 0 ? INCLUSAO : ALTERAR);
                        }
                    }
                }
            }

            var colaboradoresOrdenados = model.OrientacoesColaborador.OrderBy(o => o.DataInicioOrientacao).ToList();

            if (colaboradoresOrdenados.SMCAny())
            {
                int colaboradoresCoutuela = 0;

                for (int i = 0; i < colaboradoresOrdenados.Count(); i++)
                {
                    if (i + 1 < colaboradoresOrdenados.Count())///Verificar se existe mais itens na lista para serem comparados e não estourar a lista
                    {
                        if (colaboradoresOrdenados[i + 1].DataInicioOrientacao >= colaboradoresOrdenados[i].DataInicioOrientacao
                           && (colaboradoresOrdenados[i + 1].DataInicioOrientacao <= colaboradoresOrdenados[i].DataFimOrientacao || colaboradoresOrdenados[i].DataFimOrientacao == null))
                        {
                            ///Para orientações do tipo de TCC:
                            if (this.TipoOrientacaoService.ValidarTipoOrientacaoConclucaoCurso(model.SeqTipoOrientacao))
                            {
                                ///Se o tipo de termo de intercâmbio não tiver sido informado, só poderá existir um colaborador com o
                                ///tipo de participação “Orientador” no período. Ou seja, não permitir o registro de mais que um
                                ///“Orientador” em períodos coincidentes.
                                if (model.SeqTipoTermoIntercambio == null)
                                {
                                    if (colaboradoresOrdenados[i].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador
                                        && colaboradoresOrdenados[i + 1].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador)
                                    {
                                        throw new OrientacaoColaboradorMesmoTipoParticipacaoException(model.Seq == 0 ? INCLUSAO : ALTERAR);
                                    }
                                }
                                else
                                {
                                    ///Se o tipo de termo de intercâmbio for igual a “Cotutela, só poderá existir dois colaboradores com o tipo de
                                    ///participação “Orientador” no período.Ou seja, não permitir o registro de mais que dois “Orientadores” em períodos coincidentes.
                                    if (this.TipoTermoIntercambioService.ValidarTipoTermoIntercambioCoutela((long)model.SeqTipoTermoIntercambio))
                                    {
                                        if (colaboradoresOrdenados[i].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador
                                        && colaboradoresOrdenados[i + 1].TipoParticipacaoOrientacao == TipoParticipacaoOrientacao.Orientador)
                                        {
                                            colaboradoresCoutuela++;
                                        }
                                    }

                                    if (colaboradoresCoutuela > 1)
                                    {
                                        throw new OrientacaoColaboradorMesmoTipoParticipacaoCotutelaException(model.Seq == 0 ? INCLUSAO : ALTERAR);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ///Dados do Aluno para página de confirmação
            foreach (var item in model.OrientacoesPessoaAtuacao)
            {
                item.NomeAlunoConfirmacao = item.SeqPessoaAtuacao.Nome;
                item.RAConfirmacao = (long)item.SeqPessoaAtuacao.NumeroRegistroAcademico;
            }

            ///Dados do Orientador para página de confirmação
            foreach (var item in model.OrientacoesColaborador)
            {
                item.NomeInstiuicaoFinanceiraConfirmacao = InstituicaoExternaService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroData { SeqColaborador = item.SeqColaborador })
                                                           .First(f => f.Seq == item.SeqInstituicaoExterna).Descricao;
                item.NomeOrientadorConfirmacao = item.ColaboradorNameDescriptionField;
            }

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ORT_001_02_01.PESQUISAR_ORIENTACAO)]
        public JsonResult BuscarSituacaoMatriculaSelect(long SeqTipoSituacaoMatricula)
        {
            var retorno = this.SituacaoMatricula.BuscarSituacoesMatriculaPorTipo(SeqTipoSituacaoMatricula);

            return Json(retorno);
        }

        [SMCAuthorize(UC_ORT_001_02_01.PESQUISAR_ORIENTACAO)]
        public JsonResult BuscarTurnosCursoOfertaLocalidadeSelect(long? SeqCursoOfertaLocalidade)
        {
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();

            if (SeqCursoOfertaLocalidade.HasValue)
            {
                retorno = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(SeqCursoOfertaLocalidade.GetValueOrDefault());
            }
            else
            {
                retorno = this.TurnoService.BuscarTunos();
            }
            return Json(retorno);
        }

        [SMCAuthorize(UC_ORT_001_02_01.PESQUISAR_ORIENTACAO)]
        public JsonResult BuscarTiposSituacoesMatriculasTokenMatriculadoSelect(long seqCicloLetivo, long? SeqTipoSituacaoMatricula)
        {
            List<SMCDatasourceItem> retorno;

            if (SeqTipoSituacaoMatricula.HasValue)
            {
                retorno = this.TipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasSelect();

                foreach (var item in retorno)
                {
                    if (item.Seq == SeqTipoSituacaoMatricula)
                    {
                        item.Selected = true;
                    }
                }
            }
            else
            {
                retorno = this.TipoSituacaoMatriculaService.BuscarTiposSituacoesMatriculasTokenMatriculadoSelect();
            }

            return Json(retorno);
        }
    }
}