using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class InstituicaoNivelTipoMensagemController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ITipoMensagemService TipoMensagemService
        {
            get { return Create<ITipoMensagemService>(); }
        }

        private IInstituicaoNivelTipoMensagemService InstituicaoNivelTipoMensagemService
        {
            get { return Create<IInstituicaoNivelTipoMensagemService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposAtuacao(long seqTipoMensagem)
        {
            return PartialView("_TiposAtuacao", TipoMensagemService.BuscarTiposAtuacao(seqTipoMensagem));
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposUso(long seqTipoMensagem)
        {
            return PartialView("_TiposUso", TipoMensagemService.BuscarTiposUso(seqTipoMensagem));
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTags(long? seqTipoMensagem, long seq)
        {
            InstituicaoNivelTipoMensagemViewModel model = new InstituicaoNivelTipoMensagemViewModel();
            if (seq > 0)
            {
                model.MensagemPadrao = InstituicaoNivelTipoMensagemService.BuscarMensagem(seq);
            }
            if (seqTipoMensagem.HasValue && seqTipoMensagem.Value > 0)
            {                
                model.MensagemObrigatoria = !TipoMensagemService.PermiteCadastroManual(seqTipoMensagem.Value);
                model.Tags = TipoMensagemService.BuscarTags(seqTipoMensagem.Value);
            }
            return PartialView("_Mensagem", model);
        }
    }
}