using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class EscalonamentoController : SMCDynamicControllerBase
    {
        #region Services

        private IEscalonamentoService EscalonamentoService { get => Create<IEscalonamentoService>(); }

        private IProcessoEtapaService ProcessoEtapaService { get => Create<IProcessoEtapaService>(); }

        private IProcessoService ProcessoService { get => Create<IProcessoService>(); }

        #endregion

        [SMCAuthorize(UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA)]
        public ActionResult CabecalhoEscalonamento(SMCEncryptedLong seqProcesso)
        {
            var modelCabecalho = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<EscalonamentoCabecalhoViewModel>();

            var resultEtapas = EscalonamentoService.BuscarEscalonamentosPorProcesso(new EscalonamentoFiltroData() { SeqProcesso = seqProcesso });

            modelCabecalho.HabilitarGrupoEscalonamento = true;

            ///A opção Grupo de escalonamento deverá ser habilitada somente se há cadastro de escalonamentos por etapa, caso
            ///contrário o botão deverá ser desabilitado
            foreach (var item in resultEtapas.Itens)
            {
                foreach (var etapa in item.Etapas)
                {
                    if(etapa.Escalonamentos.Count == 0 || etapa.Escalonamentos == null)
                    {
                        modelCabecalho.HabilitarGrupoEscalonamento = false;
                    }
                }
            }

            return PartialView("_Cabecalho", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_05_01.PESQUISAR_ESCALONAMENTO_ETAPA)]
        public ActionResult CabecalhoEscalonamentoCompleto(SMCEncryptedLong seq, SMCEncryptedLong seqProcesso, SMCEncryptedLong seqProcessoEtapa)
        {
            var modelCabecalhoProcesso = ProcessoService.BuscarCabecalhoProcesso(seqProcesso, false).Transform<ProcessoCabecalhoViewModel>();

            var modelCabecalhoProcessoEtapa = ProcessoEtapaService.BuscarProcessoEtapa(seqProcessoEtapa);

            var modelCabecalho = modelCabecalhoProcesso.Transform<EscalonamentoCabecalhoViewModel>();

            modelCabecalho.SeqEscalonamento = seq;
            modelCabecalho.DescricaoEtapa = modelCabecalhoProcessoEtapa.DescricaoEtapa;
            modelCabecalho.SituacaoEtapa = modelCabecalhoProcessoEtapa.SituacaoEtapa;

            return PartialView("_CabecalhoEscalonamento", modelCabecalho);
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult ColocarManutencaoEtapa(SMCEncryptedLong seqEtapa)
        {
            var retorno = this.ProcessoEtapaService.ColocarProcessoEtapaManutencao(seqEtapa);

            return SMCRedirectToAction("Index", "Escalonamento", routeValues: new { SeqProcesso = new SMCEncryptedLong(retorno.SeqProcesso) });
        }

        [SMCAuthorize(UC_SRC_002_05_02.MANTER_ESCALONAMENTO_ETAPA)]
        public ActionResult LiberarEtapa(SMCEncryptedLong seqEtapa)
        {
            var retorno = this.ProcessoEtapaService.LiberarProcessoEtapa(seqEtapa);

            return SMCRedirectToAction("Index", "Escalonamento", routeValues: new { SeqProcesso = new SMCEncryptedLong(retorno.SeqProcesso) });
        }
    }
}