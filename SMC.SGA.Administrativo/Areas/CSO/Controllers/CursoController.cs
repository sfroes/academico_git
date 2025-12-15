using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using SMC.SGA.Administrativo.Areas.CSO.Views.Curso.App_LocalResources;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class CursoController : SMCDynamicControllerBase
    {
        #region Serviços

        private ICursoService CursoService => Create<ICursoService>();

        private ICursoOfertaService CursoOfertaService => Create<ICursoOfertaService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private ICursoFormacaoEspecificaService CursoFormacaoEspecificaService => Create<ICursoFormacaoEspecificaService>();

        private IHierarquiaEntidadeItemService HierarquiaEntidadeItemService => Create<IHierarquiaEntidadeItemService>();

        #endregion Serviços

        /// <summary>
        /// Incluir uma Oferta de Curso direto da tela de Curso
        /// Verifica se existe pelo menos uma formação específica para o curso.
        /// </summary>
        /// <param name="seq">Sequencial da Oferta de Curso</param>
        [SMCAuthorize(UC_CSO_001_01_03.MANTER_OFERTA_CURSO)]
        public ActionResult IncluirCursoOferta(SMCEncryptedLong seq)
        {
            try
            {
                if (CursoFormacaoEspecificaService.VerificarExisteCursoFormacaoEspecifica(seq))
                {
                    return View(seq);
                }
                else
                {
                    var curso = CursoService.BuscarCurso(seq);
                    string msg = string.Format(UIResource.MSG_Erro_CursoSemFormacao, curso.NivelEnsino.Descricao, curso.Nome);
                    throw new Exception(msg);
                }
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de excluir, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }

        public ActionResult Legenda()
        {
            return PartialView("_Legenda");
        }

        /// <summary>
        /// Excluir uma Oferta de Curso direto da tela de Curso
        /// </summary>
        /// <param name="seq">Sequencial da Oferta de Curso</param>
        [SMCAuthorize(UC_CSO_001_01_03.MANTER_OFERTA_CURSO)]
        public ActionResult ExcluirCursoOferta(SMCEncryptedLong seq)
        {
            try
            {
                CursoOfertaService.ExcluirCursoOferta(seq);

                string msg = string.Format(UIResource.MSG_Excluido_Sucesso, "Registro");

                // Seta mensagem de sucesso
                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Default);

                // Renderiza a lista novamente
                return SMCRedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de excluir, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }

        /// <summary>
        /// Passo 1 - Nível Ensino
        /// </summary>
        /// <param name="model">Modelo de curso</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult NivelEnsino(CursoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            model.NivelEnsinoComClassificacao = model.Hierarquias.SMCCount() > 0 && model.Hierarquias.Any(s => s.Classificacoes.SMCCount() > 0);

            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 2 - Dados gerais
        /// </summary>
        /// <param name="model">Modelo de curso</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult DadosGerais(CursoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Caso não tenha definido os tipos de curso para o nível de ensino selecionado, dispara exception e redireciona para index
            if (model.TiposCurso.SMCCount() == 0)
            {
                var nivelEnsinoSelecionado = model.NiveisEnsino.Single(s => s.Seq == model.SeqNivelEnsino).Descricao;
                ThrowRedirect(new InstituicaoNivelTipoCursoNaoAssociadoException(nivelEnsinoSelecionado), "Index");
            }

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 2 - Dados gerais
        /// </summary>
        /// <param name="model">Modelo de curso</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult Confirmacao(CursoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            SetInformationMessage(string.Format(UIResource.MSG_Confirmacao, model.DescricaoNivelEnsino, model.Nome), target: SMCMessagePlaceholders.Default);

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 3 - Contatos
        /// </summary>
        /// <param name="model">Modelo de curso</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult VerificarPasso(CursoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Caso não tenha dados de contato, pula para hierarquia de classificação
            while (PularPasso(model))
            {
                model.Step++;
            }

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 4 - Hierarquia de classificação
        /// </summary>
        /// <param name="model">Modelo de curso</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult Classificacoes(CursoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Caso não tenha dados de contato, pula para hierarquia de classificação
            while (PularPasso(model))
                model.Step++;

            model.Hierarquias = this.EntidadeService.BuscarHierarquiaClassificacaoPorNivelEnsino(model.SeqNivelEnsino).TransformList<EntidadeClassificacoesViewModel>();

            //model.SeqsHierarquiaEntidadeItem = model.HierarquiasEntidades?.Select(s => s.Seq).ToArray();
            model.SeqsHierarquiaEntidadeItem = model.HierarquiasEntidades?.Select(i => i.SeqItemSuperior).ToArray();
     
            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Aplica a regra UC_CSO_001_01_02.NV14
        /// </summary>
        /// <param name="tipoCurso">Tipo do curso selecionado</param>
        /// <param name="nome">Nome atual do curso</param>
        /// <returns>Retorna a máscara segundo a regra 14</returns>
        [SMCAuthorize(UC_CSO_001_01_02.MANTER_CURSO)]
        public ActionResult RecuperarNomeCurso(TipoCurso? tipoCurso, string nome)
        {
            if (string.IsNullOrEmpty(nome) && tipoCurso == TipoCurso.ABI)
                return Json(UIResource.MascaraCurso);
            return Json(nome);
        }

        /// <summary>
        /// Verifica se deve pular o passo
        /// </summary>
        /// <param name="model">Modelo configurado</param>
        /// <returns>Setado caso deva pular o passo</returns>
        private bool PularPasso(CursoDynamicModel model)
        {
            switch (model.Step)
            {
                // Verifica se têm contatos ativos no passo de contato
                case 2: return !(model.HabilitaEnderecos || model.HabilitaEnderecosEletronicos || model.HabilitaTelefones);
                // Verifica se têm hierarquias de classificação no passo de classificação
                case 3:
                    {
                        if (model.Hierarquias.SMCCount() == 0)
                            model.Hierarquias = this.EntidadeService.BuscarHierarquiaClassificacaoPorNivelEnsino(model.SeqNivelEnsino).TransformList<EntidadeClassificacoesViewModel>();
                        return model.Hierarquias.SMCCount() == 0;
                    }

                default: return false;
            }
        }
    }
}