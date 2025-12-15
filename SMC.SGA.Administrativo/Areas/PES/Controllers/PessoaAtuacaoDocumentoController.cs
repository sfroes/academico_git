using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.FIN.Models;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaAtuacaoDocumentoController : SMCDynamicControllerBase
    {
        #region [Services]

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();
        private IPessoaAtuacaoBeneficio PessoaAtuacaoBeneficio => Create<IPessoaAtuacaoBeneficio>();
        private IPessoaAtuacaoDocumentoService PessoaAtuacaoDocumentoService => Create<IPessoaAtuacaoDocumentoService>();

        #endregion [Services]

        /// <summary>
        /// BI_PES_004 - Pessoa - Cabeçalho
        /// </summary>
        /// <param name="SeqPessoaAtuacao">Sequencial do ingressante</param>
        /// <returns></returns>
        public ActionResult CabecalhoPessoaAtuacaoDocumento(long SeqPessoaAtuacao)
        {
            var modelo = PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho(SeqPessoaAtuacao).Transform<PessoaAtuacaoDocumentoCabecalhoViewModel>();
            var dadosVinculo = ExecuteService<PessoaAtuacaoBeneficioData, PessoaAtuacaoBeneficioCabecalhoViewModel>(PessoaAtuacaoBeneficio.BuscarPessoaAtuacaoDocumentoCabecalho, SeqPessoaAtuacao);

            modelo.DadosVinculo = dadosVinculo.DadosVinculo;

            return PartialView("_Cabecalho", modelo);            
        }

        public ActionResult ApresentarMensagemInformativa(long seqPessoaAtuacao)
        {
            return PartialView("_MensagemInformativa", PessoaAtuacaoDocumentoService.BuscarItensBloqueio(seqPessoaAtuacao));
        }

        public bool VerificaDocumentoObrigatorio(long seqTipoDocumento, long seqPessoaAtuacao)
        {
            var existeDocumentoObrigatorio = PessoaAtuacaoDocumentoService.VerificaDocumentoObrigatorio(seqTipoDocumento, seqPessoaAtuacao);

            return existeDocumentoObrigatorio;
        }

        public void GerarBloqueio(long seqPessoaAtuacao, long seqTipoDocumento)
        {
            PessoaAtuacaoDocumentoService.GerarBloqueio(seqPessoaAtuacao, seqTipoDocumento);
        }
    }
}