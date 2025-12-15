using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.Service;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Reporting;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORT.Controllers
{
    public class RelatorioEmitirComprovanteEntregaController : SMCReportingControllerBase
    {
        #region Services

        private ISMCReportMergeService SMCReportMergeService
        {
            get { return this.Create<ISMCReportMergeService>(); }
        }

        private ITrabalhoAcademicoService TrabalhoAcademicoService
        {
            get { return this.Create<ITrabalhoAcademicoService>(); }
        }

        private IInstituicaoNivelModeloRelatorioService InstituicaoNivelModeloRelatorioService
        {
            get { return this.Create<IInstituicaoNivelModeloRelatorioService>(); }
        }

        #endregion 

        [SMCAuthorize(UC_ORT_002_02_06.EMITIR_COMPROVANTE_ENTREGA)]
        public ActionResult Index(long seqTrabalho)
        {

            var comprovantes = TrabalhoAcademicoService.BuscarComprovantesEntregaTrabalhoAcademico(seqTrabalho);
            var nomeUsuarioLogado = SMCContext.User.SMCGetNome();

            if (comprovantes.Any())
            {
                ValidarDataEntrega(comprovantes);

                // Recupera o template do relatório
                var template = InstituicaoNivelModeloRelatorioService.BuscarTemplateModeloRelatorio(comprovantes.FirstOrDefault().SeqInstituicaoNivel, ModeloRelatorio.ComprovanteEntregaTrabalhoAcademico);

                if (template == null)
                {
                    LancarExceptionTemplateNaoExiste();
                    return RedirectToAction("Index", "TrabalhoAcademico", new { @area = "ORT" });
                }
                // Chama o serviço para gerar o relatório
                List<Dictionary<string, object>> modelDic = new List<Dictionary<string, object>>();

                comprovantes.SMCForEach(c => c.NomeUsuarioLogado = nomeUsuarioLogado);

                return SMCDocumentMergeInline(string.Format("{0}.pdf", Guid.NewGuid().ToString()), template.ArquivoModelo.FileData,  comprovantes.ToArray() );
            }
            return null;
        }

        /// <summary>
        /// NV08 Este comando estará disponível somente quando a data de entrega do trabalho na 
        /// secretaria tiver sido informada.
        /// Caso não esteja disponível, deverá conter uma mensagem informativa: "A emissão do 
        /// comprovante de entrega não pode ser realizada, pois a data de entrega do Trabalho ainda não foi registrada".
        /// </summary>
        /// <param name="comprovantes"></param>
        private void ValidarDataEntrega(List<ComprovanteEntregaTrabalhoAcademicoData> comprovantes)
        {
            foreach (var comprovante in comprovantes)
            {
                if (comprovante.DataDepositoSecretaria == null || !comprovante.DataDepositoSecretaria.HasValue)
                {
                    //Lançar exceção de sem data de entrega 
                    throw new TrabalhoAcademicoComprovanteSemDataException();
                }
            }

        }

        private void LancarExceptionTemplateNaoExiste()
        {
            string tituloErro = Views.TrabalhoAcademico.App_LocalResources.UIResource.MSG_Titulo_Relatorio_Erro;
            try
            {
                //Lançar exceção de template
                throw new TemplateComprovanteNaoEncontradoException();
            }
            catch (Exception ex)
            {
                //Lançar exceção de template
                SetErrorMessage(ex.Message, tituloErro, SMCMessagePlaceholders.Centro);
            }
        }
    }
}