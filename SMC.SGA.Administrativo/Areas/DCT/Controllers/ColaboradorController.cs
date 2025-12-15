using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using SMC.SGA.Administrativo.Areas.DCT.Views.Colaborador.App_LocalResources;
using SMC.SGA.Administrativo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.DCT.Controllers
{
    public class ColaboradorController : PessoaAtuacaoBaseController<ColaboradorDynamicModel>
    {
        #region [ Services ]

        private IFormacaoEspecificaService FormacaoEspecificaService => Create<IFormacaoEspecificaService>();

        private IProgramaService ProgramaService => Create<IProgramaService>();

        private IInstituicaoNivelTipoAtividadeColaboradorService InstituicaoNivelTipoAtividadeColaboradorService => Create<IInstituicaoNivelTipoAtividadeColaboradorService>();

        private ITipoVinculoColaboradorService TipoVinculoColaboradorService => Create<ITipoVinculoColaboradorService>();

        private IColaboradorVinculoService ColaboradorVinculoService => Create<IColaboradorVinculoService>();

        private IColaboradorService ColaboradorService => Create<IColaboradorService>();

        private ITitulacaoDocumentoComprobatorioService TitulacaoDocumentoComprobatorioService => Create<ITitulacaoDocumentoComprobatorioService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public override ActionResult Selecao(ColaboradorDynamicModel model)
        {
            return base.Selecao(model);
        }

        [HttpPost]
        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public override ActionResult BuscarPessoaExistente(ColaboradorDynamicModel model)
        {
            return base.BuscarPessoaExistente(model);
        }

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public override ActionResult DadosPessoais(ColaboradorDynamicModel model)
        {
            var result = base.DadosPessoais(model);
            var seqPessoaAtuacao = model.SelectedValues.GetValueOrDefault();

            if (seqPessoaAtuacao > 0)
            {
                var pessoasExistentes = this.BuscarPessoasExistentes(model, true);
                if (pessoasExistentes.FirstOrDefault(a => a.Seq == seqPessoaAtuacao).Atuacoes.Any(atuacoes => atuacoes == TipoAtuacao.Colaborador))
                    throw new PessoaAtuacaoDuplicadaException(SMCEnumHelper.GetDescription(TipoAtuacao.Colaborador));
            }

            return result;
        }

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public ActionResult Vinculos(ColaboradorDynamicModel model)
        {
            this.ConfigureDynamic(model);
            this.SetViewMode(SMCViewMode.Insert);
            return ViewWizard(model);
        }

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public ActionResult FormacaoAcademica(ColaboradorDynamicModel model)
        {
            this.ConfigureDynamic(model);

            this.ValidaDatasVinculo(model);

            var tipoVinculoExigeFormacao = Convert.ToBoolean(model
                .TiposVinculoColaborador.First(f => Convert.ToInt64(f.Value) == model.SeqTipoVinculoColaborador)
                .DataAttributes.First(f => f.Key == "data-formacao").Value);
            if (!tipoVinculoExigeFormacao)
            {
                model.SeqTitulacao = null;
                model.Descricao = null;
                model.AnoInicio = null;
                model.AnoObtencaoTitulo = null;
                model.TitulacaoMaxima = null;
                model.Curso = null;
                model.Orientador = null;
                model.SeqInstituicaoExterna = null;
                model.SeqClassificacao = null;
                model.SeqDocumentoApresentado = null;
                model.Step++;
            }
            model.TitulacaoMaxima = model.TitulacaoMaxima ?? true;
            return ViewWizard(model);
        }

        private void ValidaDatasVinculo(ColaboradorDynamicModel model)
        {
            // Cria o data
            var data = model.Transform<ColaboradorVinculoData>();

            // Atribui as datas que tem nomes diferentes
            data.DataInicio = model.DataInicioVinculo;
            data.DataFim = model.DataFimVinculo;

            ColaboradorVinculoService.ValidarDatasVinculo(data);
        }

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public ActionResult ConfirmacaoCadastroColaborador(ColaboradorDynamicModel model)
        {
            this.ConfigureDynamic(model);

            if (!model.ExibirColaboradorResponsavel)
                model.ColaboradoresResponsaveis = null;

            // Tratamento de campos opcionais
            model.NomeSocialConfirmacao = string.IsNullOrEmpty(model.NomeSocial) ? App_GlobalResources.UIResource.Label_Campo_Vazio : model.NomeSocial;
            model.NumeroPassaporteConfirmacao = string.IsNullOrEmpty(model.NumeroPassaporte) ? App_GlobalResources.UIResource.Label_Campo_Vazio : model.NumeroPassaporte;

            // Descrição do vínculo
            IList<SMCDatasourceItem> tiposVinculoColaborador = this.Create<ITipoVinculoColaboradorService>().BuscarTipoVinculoColaboradorSelect();
            string entidade = model.EntidadesColaborador?.FirstOrDefault(f => f.Value == model.SeqEntidadeVinculo.ToString())?.Text;
            string tipoVinculoColaborador = tiposVinculoColaborador.FirstOrDefault(f => f.Seq == model.SeqTipoVinculoColaborador)?.Descricao;
            string labelInicio = Views.Colaborador.App_LocalResources.UIResource.Label_Inicio;
            string labelFim = Views.Colaborador.App_LocalResources.UIResource.Label_Fim;
            model.DescricaoVinculoConfirmacao = model.DataFimVinculo.HasValue ?
                $"{entidade} - {tipoVinculoColaborador} - {labelInicio}: {model.DataInicioVinculo.ToShortDateString()} {labelFim}: {model.DataFimVinculo.Value.ToShortDateString()}" :
                $"{entidade} - {tipoVinculoColaborador} - {labelInicio}: {model.DataInicioVinculo.ToShortDateString()}";

            // Colaboradores responsáveis
            model.ColaboradoresResponsaveisConfirmacao = model?.ColaboradoresResponsaveis?.Select(s => s.SeqColaboradorResponsavel.Nome).ToList();

            // Ofertas de curso com suas atividades
            //model.CursosConfirmacao = new List<ColaboradorVinculoCursoConfirmacaoViewModel>();
            //foreach (var curso in model.Cursos)
            //{
            //    var oferta = model.CursoOfertasLocalidades.First(f => f.Value == curso.SeqCursoOfertaLocalidade.ToString());
            //    var descricaoNivelEnsino = oferta.DataAttributes.First(f => f.Key == "DescricaoNivelEnsino").Value;
            //    var nomeLocalidade = oferta.DataAttributes.First(f => f.Key == "NomeLocalidade").Value;
            //    model.CursosConfirmacao.Add(new ColaboradorVinculoCursoConfirmacaoViewModel()
            //    {
            //        NomeCursoOfertaLocalidade = $"{descricaoNivelEnsino} - {nomeLocalidade}",
            //        Atividades = curso.TipoAtividadeColaborador.Select(a => SMCEnumHelper.GetDescription(a)).ToList()
            //    });
            //}

            // Formações específicas
            if (model.FormacoesEspecificas.SMCCount() > 0)
            {
                IList<SMCDatasourceItem> linhasPesquisaGrupoProgramaFormacao = this.FormacaoEspecificaService
                    .BuscarLinhasDePesquisaGrupoPrograma(model.SeqEntidadeVinculo);
                model.FormacoesEspecificasConfirmacao = model.FormacoesEspecificas
                    .Select(m => m.SeqFormacaoEspecifica.DescricaoCompleta).ToList();
            }

            ColaboradorVinculoService.ValidarSobreposicaoPeriodosFormacoesEspecificas(model.FormacoesEspecificas.TransformList<ColaboradorVinculoFormacaoEspecificaData>(), UIResource.Operacao);

            var dataPeriodo = model.Transform<ColaboradorVinculoData>();
            dataPeriodo.DataInicio = model.DataInicioVinculo;
            dataPeriodo.DataFim = model.DataFimVinculo;

            ColaboradorVinculoService.ValidarDatasVinculo(dataPeriodo);

            return ViewWizard(model);
        }

        [SMCAuthorize(UC_DCT_001_06_02.MANTER_COLABORADOR)]
        public override ActionResult Contatos(ColaboradorDynamicModel model)
        {
            return base.Contatos(model);
        }

        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarTiposAtividadeColaboradorSelect(SMCEncryptedLong seqCursoOfertaLocalidade)
        {
            List<SMCDatasourceItem> tiposAtividade = this.InstituicaoNivelTipoAtividadeColaboradorService.BuscarTiposAtividadeColaboradorSelect(new InstituicaoNivelTipoAtividadeColaboradorFiltroData() { SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade, IgnorarFiltros = true });
            return Json(tiposAtividade);
        }

        //FIX: Remover ao corrigir o depency
        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarSituacoesColaborador()
        {
            List<SMCDatasourceItem> situacoesColaborador = new List<SMCDatasourceItem>();
            situacoesColaborador.Add(new SMCDatasourceItem((long)SituacaoColaborador.Ativo, SMCEnumHelper.GetDescription(SituacaoColaborador.Ativo)));
            situacoesColaborador.Add(new SMCDatasourceItem((long)SituacaoColaborador.Inativo, SMCEnumHelper.GetDescription(SituacaoColaborador.Inativo)));
            return Json(situacoesColaborador);
        }

        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarEntidadesFilhas(long seqEntidadeVinculo)
        {
            //var resultado = this.ProgramaService.BuscarSeqsProgramasGrupo(seqEntidadeVinculo);

            return Json(new List<long>() { seqEntidadeVinculo });
        }

        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarTiposVinculoEntidadeColaborador(SMCEncryptedLong seqEntidadeVinculo)
        {
            return Json(TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(seqEntidadeVinculo));
        }

        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarColaboradoresOrientacao(long? seqCursoOferta, long? seqLocalidade, List<long> seqsEntidadeResponsavel)
        {
            if (seqCursoOferta.HasValue || seqLocalidade.HasValue || seqsEntidadeResponsavel.SMCAny())
            {
                var filtro = new ColaboradorOrientadorFiltroData()
                {
                    SeqCursoOferta = seqCursoOferta,
                    SeqLocalidade = seqLocalidade,
                    SeqEntidadeResponsavel = seqsEntidadeResponsavel
                };

                return Json(ColaboradorService.BuscarColaboradoresOrientadores(filtro));
            }
            else
            {
                return Json(new List<SMCDatasourceItem>());
            }
        }

        [SMCAuthorize(UC_DCT_001_06_01.PESQUISAR_COLABORADOR)]
        public ActionResult BuscarDocumentosTitulacao(SMCEncryptedLong seqTitulacao)
        {
            return Json(TitulacaoDocumentoComprobatorioService.BuscarTitulacaoDocumentosComprobatorios(seqTitulacao));
        }

        public override ColaboradorDynamicModel BuscarConfiguracao()
        {
            return ColaboradorService.BuscarConfiguracaoColaborador().Transform<ColaboradorDynamicModel>();
        }
    }
}