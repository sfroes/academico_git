using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using SMC.SGA.Administrativo.Areas.ORT.Views.PublicacaoBdp.App_LocalResources;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class PublicacaoBdpController : SMCDynamicControllerBase
    {
        #region Services

        private ITrabalhoAcademicoService TrabalhoAcademicoService { get => Create<ITrabalhoAcademicoService>(); }

        private IPublicacaoBdpService PublicacaoBdpService { get => Create<IPublicacaoBdpService>(); }

        private IFuncionarioService FuncionarioService { get => Create<IFuncionarioService>(); }

        private IInstituicaoNivelModeloRelatorioService InstituicaoNivelModeloRelatorioService { get => Create<IInstituicaoNivelModeloRelatorioService>(); }

        #endregion Services

        public ActionResult LegendaLista()
        {
            return PartialView("_LegendaLista");
        }

        public ActionResult EditarCabecalho(SMCEncryptedLong seq)
        {
            var data = TrabalhoAcademicoService.BuscarCabecalhoPublicacaoBdp(seq);
            return PartialView("_EditarCabecalho", data.Transform<PublicacaoBdpCabecalhoViewModel>());
        }

        [SMCAuthorize(UC_ORT_003_02_02.LIBERAR_PARA_BIBLIOTECA)]
        public ActionResult RetornarAlunoPublicacao(SMCEncryptedLong seqPublicacaoBdp, SMCEncryptedLong seqTrabalhoAcademico)
        {
            // Altera a situação da publicação do bdp para liberada para biblioteca
            PublicacaoBdpService.RetornarSituacaoAlunoBdp(seqPublicacaoBdp);

            return SMCRedirectToAction("Editar", "PublicacaoBdp", new { seq = seqTrabalhoAcademico });
        }

        [SMCAuthorize(UC_ORT_003_02_02.LIBERAR_PARA_BIBLIOTECA)]
        public ActionResult LiberarConferenciaBiblioteca(SMCEncryptedLong seqPublicacaoBdp, SMCEncryptedLong seqTrabalhoAcademico)
        {
            Assert(null, UIResource.MensagemConfirmacaoLiberarBiblioteca_Mensagem, () =>
            {
                return PublicacaoBdpService.ValidarLiberacaoConferenciaBiblioteca(seqPublicacaoBdp);
            });

            PublicacaoBdpService.LiberarConferenciaBiblioteca(seqPublicacaoBdp);

            return SMCRedirectToAction("Editar", "PublicacaoBdp", new { seq = seqTrabalhoAcademico });
        }

        [SMCAuthorize(UC_ORT_003_02_02.LIBERAR_PARA_CONSULTA)]
        public ActionResult LiberarConsulta(SMCEncryptedLong seqPublicacaoBdp, SMCEncryptedLong seqTrabalhoAcademico)
        {
            //PublicacaoBdpService.AlterarSituacao(seqPublicacaoBdp, SituacaoTrabalhoAcademico.LiberadaConsulta);
            PublicacaoBdpService.LiberarConsulta(seqPublicacaoBdp);
            return SMCRedirectToAction("Editar", "PublicacaoBdp", new { seq = seqTrabalhoAcademico });
        }

        [SMCAllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Download(string guidFile, string name, string type, SMCEncryptedLong seqEntity = null)
        {
            Guid guidParsed = Guid.Empty;

            if (Guid.TryParse(guidFile, out guidParsed))
            {
                var data = SMCUploadHelper.GetFileData(new SMCUploadFile { GuidFile = guidFile });
                if (data != null)
                {
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();
                    return File(data, type, name);
                }
            }

            if (!Int64.TryParse(guidFile, out long seq))
            {
                seq = new SMCEncryptedLong(guidFile);
            }
            else if (seqEntity != null)
            {
                seq = seqEntity.Value;
            }

            return Redirect(TrabalhoAcademicoService.UrlPublicacao(seq, name));
        }

        [SMCAllowAnonymous]
        public ActionResult ImprimirAutorizacao(SMCEncryptedLong seq, SMCEncryptedLong seqInstituicaoNivel)
        {
            return RedirectToAction(nameof(PublicacaoBdpReportController.ImprimirAutorizacao), "PublicacaoBdpReport", new { seq, seqInstituicaoNivel });
        }

        [SMCAuthorize(UC_ORT_003_02_05.EMITIR_FICHA_CATALOGRAFICA)]
        public ActionResult PrepararFichaCatalografica(SMCEncryptedLong seqPublicacaoBdp)
        {
            FichaCatalograficaViewModel model = new FichaCatalograficaViewModel();

            // Busca os dados da publicação BDP para gerar a ficha
            var publicacaoBD = this.PublicacaoBdpService.BuscarPubicacaoBdp(seqPublicacaoBdp);

            // Busca os bibliotecários ativos para seleção
            model.BibliotecariosAtivos = this.FuncionarioService.ListarFuncionariosVinculoAtivo(TOKEN_TIPO_FUNCIONARIO.BIBLIOTECARIO);

            // Prepara o modelo para apresentação
            model.Assunto = new SMCMasterDetailList<AssuntoIdiomaTrabalhoViewModel>();
            model.Titulo = publicacaoBD.InformacoesIdioma
                .Where(x => x.IdiomaTrabalho == true)
                .Select(x => x.Titulo)
                .FirstOrDefault();
            model.Aluno = publicacaoBD.TrabalhoAcademico.Autores.FirstOrDefault().NomeAutor;
            model.SeqPublicacaoBdp = publicacaoBD.Seq;

            return PartialView("_EmitirFichaCatalografica", model);
        }
    }
}