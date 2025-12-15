using SMC.Academico.WebApi.Models;
using SMC.Framework;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Util;
using SMC.SGA.Aluno.Extensions;
using SMC.SGA.Aluno.Models;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Controllers
{
    public class SharepointController : SMCControllerBase
    {
        #region APIS

        public SMCApiClient APIArquivoSharepoint => SMCApiClient.Create("ArquivoSharepoint");

        #endregion APIS

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var model = new HomeAlunoViewModel();
            return View(model);
        }

        [SMCAllowAnonymous]
        [HttpPost]
        public ActionResult SalvarArquivo(HomeAlunoViewModel model)
        {
            try
            {
                var alunoLogado = this.GetAlunoLogado();

                var registro = new FileSharepointModel();
                registro.biblioteca = "Aluno";
                registro.metadado = new MetadadoSharepointModel();
                registro.metadado.codigo = User.SMCGetCodigoPessoa().ToString();
                registro.metadado.nome = User.SMCGetNome();
                registro.metadado.sistema = "SGA.Aluno";
                registro.arquivo = new ArquivoSharepointModel();
                var separador = model.Arquivo.Name.Split('.');
                registro.arquivo.nome = separador[0];
                registro.arquivo.extensao = "." + separador[1];
                registro.arquivo.conteudo = SMCUploadHelper.GetFileData(model.Arquivo);

                var retorno = APIArquivoSharepoint.Execute<object>("UploadFile", registro);

                model.LinkUrl = retorno.ToString();
                return PartialView("_SharepointArquivo", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}