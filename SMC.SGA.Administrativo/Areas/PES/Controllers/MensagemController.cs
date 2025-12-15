using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class MensagemController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService
        {
            get { return Create<IPessoaAtuacaoService>(); }
        }

        private ITipoMensagemService TipoMensagemService
        {
            get { return Create<ITipoMensagemService>(); }
        }

        private IInstituicaoNivelTipoMensagemService InstituicaoNivelTipoMensagemService
        {
            get { return Create<IInstituicaoNivelTipoMensagemService>(); }
        }

        private IMensagemPessoaAtuacaoService MensagemPessoaAtuacaoService
        {
            get { return Create<IMensagemPessoaAtuacaoService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarCabecalho(SMCEncryptedLong seqPessoaAtuacao)
        {
            PessoaAtuacaoMensagemCabecalhoViewModel model = ExecuteService<PessoaAtuacaoMensagemHeaderData, PessoaAtuacaoMensagemCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoHeaderMensagem, seqPessoaAtuacao);
            return PartialView("_Cabecalho", model);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTiposUso(long seqTipoMensagem)
        {
            return PartialView("_TiposUso", TipoMensagemService.BuscarTiposUso(seqTipoMensagem));
        }

        [SMCAllowAnonymous]
        public JsonResult ArquivoObrigatorio(long seqTipoMensagem, long seqPessoaAtuacao)
        {
            var filtro = new InstituicaoNivelTipoMensagemFiltroData();
            filtro.SeqTipoMensagem = seqTipoMensagem;
            filtro.PermiteCadastroManual = true;
            filtro.SeqPessoaAtuacao = seqPessoaAtuacao;

            var lista = InstituicaoNivelTipoMensagemService.BuscarInstituicaoNivelTipoMensagens(filtro);
            return (lista != null && lista.Count > 0) ? Json(lista[0].ExigeArquivo) : Json(false);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarMensagemPadrao(long? seqTipoMensagem, long seqPessoaAtuacao, long seq)
        {
            MensagemViewModel model = new MensagemViewModel();
            if (seq > 0)
            {
                model.Descricao = MensagemPessoaAtuacaoService.BuscarMensagem(seqPessoaAtuacao, seq).Descricao;
            }
            else
            {
                var filtro = new InstituicaoNivelTipoMensagemFiltroData();
                filtro.SeqTipoMensagem = seqTipoMensagem;
                filtro.SeqPessoaAtuacao = seqPessoaAtuacao;

                InstituicaoNivelTipoMensagemData data = InstituicaoNivelTipoMensagemService.BuscarInstituicaoNivelTipoMensagem(filtro);
                model.Descricao = seqTipoMensagem.HasValue ? InstituicaoNivelTipoMensagemService.BuscarMensagem(data.Seq) : string.Empty;
            }
            return Content(model.Descricao);
        }

        [SMCAllowAnonymous]
        public bool DataFimObigratorio(long seqTipoMensagem)
        {
            TipoMensagemData tipoMensagem = TipoMensagemService.BuscarTipoMensagem(seqTipoMensagem);

            return tipoMensagem.CategoriaMensagem == CategoriaMensagem.Ocorrencia ? true : false;
        }
    }
}