using SMC.Academico.Common.Helper;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Mobile.Models;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace SMC.SGA.Mobile.Controllers
{
    public class QrCodeController : SMCControllerBase
    {
        [SMCAllowAnonymous]
        public ActionResult Index(string texto, int tamanho)
        {

            try
            {
                // QrCodeHelper.CreateQrCode(texto) retorna uma imagem
                using (var qrCodeImage = QrCodeHelper.CreateQrCode(texto))
                {
                    using (var ms = new MemoryStream())
                    {
                        // Salva a imagem no MemoryStream no formato PNG
                        qrCodeImage.Save(ms, ImageFormat.Png);

                        // Converte o conteúdo do MemoryStream para um array de bytes
                        var imageBytes = ms.ToArray();

                        // Retorna a imagem como um FileResult com o tipo MIME image/png
                        return File(imageBytes, "image/png");
                    }
                }
            }
            catch (Exception ex)
            {
                // LogError(ex);
                return new HttpStatusCodeResult(500, "Ocorreu um erro ao gerar a imagem.");
            }
        }
    }
}