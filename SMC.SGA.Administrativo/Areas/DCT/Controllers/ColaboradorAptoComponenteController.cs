using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Service.Areas.DCT.Services;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.DCT.Views.ColaboradorAptoComponente.App_LocalResources;
using SMC.Framework.Extensions;
using SMC.Academico.ServiceContract.Areas.DCT.Data;

namespace SMC.SGA.Administrativo.Areas.DCT.Controllers
{
    public class ColaboradorAptoComponenteController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IColaboradorAptoComponenteService ColaboradorAptoComponenteService => Create<IColaboradorAptoComponenteService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_DCT_001_08_01.PESQUISAR_COMPONENTE_APTO_LECIONAR)]
        public ActionResult CabecalhoColaboradorAptoComponente(SMCEncryptedLong seqAtuacaoColaborador)
        {
            var model = ExecuteService<PessoaAtuacaoData, PessoaAtuacaoCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacao, seqAtuacaoColaborador);

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_DCT_001_08_02.ASSOCIAR_COMPONENTE_APTO_LECIONAR)]
        public ActionResult BuscarNomeColaborador(SMCEncryptedLong seqAtuacaoColaborador)
        {
            var pessoaAtuacao = PessoaAtuacaoService.BuscarPessoaAtuacao(seqAtuacaoColaborador);

            var nomeCompleto = string.Empty;

            if (!string.IsNullOrEmpty(pessoaAtuacao.NomeSocial))
            {
                if (!string.IsNullOrEmpty(pessoaAtuacao.Nome))
                    nomeCompleto += $"{pessoaAtuacao.NomeSocial} ({pessoaAtuacao.Nome})";
                else
                    nomeCompleto += $"{pessoaAtuacao.NomeSocial}";
            }
            else
            {
                if (!string.IsNullOrEmpty(pessoaAtuacao.Nome))
                    nomeCompleto += $"{pessoaAtuacao.Nome}";
            }

            return Content(nomeCompleto);
        }
              

        [SMCAuthorize(UC_DCT_001_08_02.ASSOCIAR_COMPONENTE_APTO_LECIONAR)]
        public ActionResult ValidarFormacaoAcademica(SMCEncryptedLong seqAtuacaoColaborador)
        {
            var possuiFormacaoAcademica = ColaboradorAptoComponenteService.ValidarFormacaoAcademica(seqAtuacaoColaborador);

            return Content(possuiFormacaoAcademica.ToString());
        }

    }
}