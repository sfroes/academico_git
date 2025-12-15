using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class EnvioNotificacaoDestinatarioController : SMCDynamicControllerBase
    {
        #region Services

        private IEnvioNotificacaoDestinatarioService EnvioNotificacaoDestinatarioService => Create<IEnvioNotificacaoDestinatarioService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        #endregion Services

        [SMCAuthorize(UC_PES_008_02_01.VISUALIZAR_NOTIFICACAO_POR_PESSOA_ATUACAO)]
        public ActionResult BuscarCabecalho(long seqPessoaAtuacao)
        {
            var tipoAtuacao = PessoaAtuacaoService.BuscarPessoaAtuacao(seqPessoaAtuacao).TipoAtuacao;

            if (tipoAtuacao == TipoAtuacao.Aluno)
            {
                var modelHeader = ExecuteService<EnvioNotificacaoDestinatarioAlunoCabecalhoData,
                                                 EnvioNotificacaoDestinatarioAlunoCabecalhoViewModel>(EnvioNotificacaoDestinatarioService
                                                             .BuscaDadosEnvioNotificacaoDestinatarioAlunoCabecalho, seqPessoaAtuacao);

                return PartialView("_CabecalhoAluno", modelHeader);
            }
            else if (tipoAtuacao == TipoAtuacao.Colaborador)
            {
                var modelHeader = ExecuteService<EnvioNotificacaoDestinatarioColaboradorCabecalhoData,
                                                 EnvioNotificacaoDestinatarioColaboradorCabecalhoViewModel>(EnvioNotificacaoDestinatarioService
                                                             .BuscaDadosEnvioNotificacaoDestinatarioColaboradorCabecalho, seqPessoaAtuacao);

                return PartialView("_CabecalhoColaborador", modelHeader);
            }

            return new EmptyResult();
        }
    }
}