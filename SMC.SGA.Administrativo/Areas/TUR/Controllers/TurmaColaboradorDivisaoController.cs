using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.TUR.Views.TurmaColaboradorDivisao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class TurmaColaboradorDivisaoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return this.Create<IDivisaoTurmaService>(); }
        }

        private IDivisaoTurmaColaboradorService DivisaoTurmaColaboradorService
        {
            get { return this.Create<IDivisaoTurmaColaboradorService>(); }
        }

        private ITurmaService TurmaService
        {
            get { return this.Create<ITurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult CabecalhoDivisao(long seqTurma, long seqDivisao)
        {
            var cabecalho = TurmaService.BuscarTurmaCabecalho(seqTurma).Transform<TurmaCabecalhoViewModel>();

            var divisao = DivisaoTurmaService.BuscarDivisaoTurmaCabecalho(seqDivisao);

            if (divisao != null && divisao.Seq > 0)
            {
                cabecalho.TurmaDivisoesCabecalho = new List<TurmaCabecalhoDivisoesViewModel>();
                var divisaoView = new TurmaCabecalhoDivisoesViewModel();
                divisaoView.SeqDivisao = divisao.Seq;
                divisaoView.DivisaoComponenteDescricao = divisao.DescricaoFormatada;
                divisaoView.DetalhesDivisao = new List<DivisaoTurmaColaboradorDivisoesViewModel>();
                divisaoView.DetalhesDivisao.Add(divisao.Transform<DivisaoTurmaColaboradorDivisoesViewModel>());
                cabecalho.TurmaDivisoesCabecalho.Add(divisaoView);
            }

            return PartialView("_Cabecalho", cabecalho);
        }

        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult RecuperarTipoOrganizacaoComponente(long seqDivisao)
        {
            var tipoOrganizacao = DivisaoTurmaColaboradorService.BuscarTipoComponenteDivisaoTurma(seqDivisao);

            return Json(tipoOrganizacao);
        }

        [SMCAuthorize(UC_TUR_001_02_01.PESQUISAR_ASSOCIACAO_PROFESSOR_TURMA)]
        public ActionResult Desassociar(SMCEncryptedLong seq, SMCEncryptedLong seqTurma)
        {
            try
            {
                DivisaoTurmaColaboradorService.ExcluirDivisaoTurmaColaborador(seq);

                SetSuccessMessage(UIResource.Mensagem_Desassociar_Sucesso, UIResource.Titulo_Desassociar_Sucesso, SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
            }           
            
            return RedirectToAction("Index", "DivisaoTurmaColaborador", new { seqTurma = seqTurma});
        }
    }
}