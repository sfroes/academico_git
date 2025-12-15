using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class DivisaoMatrizCurricularComponenteController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ICriterioAprovacaoService CriterioAprovacaoService
        {
            get { return this.Create<ICriterioAprovacaoService>(); }
        }

        private IDivisaoComponenteService DivisaoComponenteService
        {
            get { return this.Create<IDivisaoComponenteService>(); }
        }

        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService
        {
            get { return this.Create<IDivisaoMatrizCurricularComponenteService>(); }
        }

        private IMatrizCurricularDivisaoComponenteService MatrizCurricularDivisaoComponenteService
        {
            get { return this.Create<IMatrizCurricularDivisaoComponenteService>(); }
        }

        private IMatrizCurricularService MatrizCurricularService
        {
            get { return this.Create<IMatrizCurricularService>(); }
        }

        private ITipoDivisaoComponenteService TipoDivisaoComponenteService
        {
            get { return this.Create<ITipoDivisaoComponenteService>(); }
        }

        #endregion [ Services ]

        public ActionResult DivisaoMatrizCurricularComponenteCabecalho(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = ExecuteService<MatrizCurricularCabecalhoData, MatrizCurricularCabecalhoViewModel>(MatrizCurricularService.BuscarMatrizCurricularCabecalho, seqMatrizCurricular);
            return PartialView("_Cabecalho", model);
        }

        public ActionResult DivisaoMatrizCurricularComponenteEdicaoCabecalho(DivisaoMatrizCurricularComponenteDynamicModel model)
        {
            var cabecalho = this.DivisaoMatrizCurricularComponenteService
                .DivisaoMatrizCurricularComponenteCabecalho(model.SeqMatrizCurricular, model.SeqGrupoCurricularComponente)
                .Transform<DivisaoMatrizCurricularComponenteCabecalhoViewModel>();
            return PartialView("_CabecalhoComponente", cabecalho);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult BuscarDivisoesComponenteSelect(SMCEncryptedLong seqConfiguracaoComponente)
        {
            var divisoes = this.DivisaoComponenteService.BuscarDivisoesCompoentePorConfiguracao(seqConfiguracaoComponente);
            return Json(divisoes);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult BuscarCriterioNota(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.NotaMaxima.HasValue ? criterio.NotaMaxima.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult BuscarAprovacaoPercentual(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.PercentualNotaAprovado.HasValue ? criterio.PercentualNotaAprovado.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult BuscarPresencaPercentual(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.PercentualFrequenciaAprovado.HasValue ? criterio.PercentualFrequenciaAprovado.ToString() : string.Empty);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult BuscarEscalaApuracao(SMCEncryptedLong seqCriterioAprovacao)
        {
            var criterio = this.CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao);
            return Json(criterio.DescricaoEscalaApuracao ?? string.Empty);
        }

        private bool BuscarTipoComponenteDivisaoTurma(long seqDivisaoComponente)
        {
            var tipoDivisao = this.TipoDivisaoComponenteService.BuscarTipoDivisaoComponentePorDivisaoComponente(seqDivisaoComponente);
            var retorno = tipoDivisao.TipoGestaoDivisaoComponente == TipoGestaoDivisaoComponente.Turma;

            return retorno;
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        [HttpPost]
        public ActionResult PreencherDivisoesConfiguracaoComponente(long? seqConfiguracaoComponente, long seq, long seqCurriculoCursoOferta, long? seqCriterioAprovacao)
        {
            var dynamic = new DivisaoMatrizCurricularComponenteDynamicModel
            {
                DivisoesComponente = new List<DivisaoMatrizCurricularComponenteDetailViewModel>()
            };

            if (!seqConfiguracaoComponente.HasValue)
                return PartialView("_EditarComponentesDivisoesListarView", dynamic);

            // Recupera as divisões configuradas para o componente
            var divisoesComponenteConfiguracao = this.DivisaoComponenteService.BuscarDivisoesCompoentePorConfiguracao(seqConfiguracaoComponente.Value);

            var escalaApuracaoService = this.Create<IEscalaApuracaoService>();
            dynamic.EscalasApuracao = escalaApuracaoService.BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(seqCurriculoCursoOferta);

            // Caso seja uma edição
            if (seq > 0)
            {
                // Recupera as divisões configuradas na matriz
                var divisoesComponente = MatrizCurricularDivisaoComponenteService.BuscarDivisaoMatrizCurricularComponenteDivisoes(seq);

                // Atualiza os flags das configurações do banco apenas caso as duas coleções de divisões sejam iguais
                if (divisoesComponente.Select(s => s.SeqDivisaoComponente).Intersect(divisoesComponenteConfiguracao.Select(s => s.Seq)).Count() == divisoesComponenteConfiguracao.Count())
                {
                    divisoesComponente.ForEach(f =>
                            {
                                var config = divisoesComponenteConfiguracao.Where(w => w.Seq == f.SeqDivisaoComponente).FirstOrDefault();
                                f.DivisaoComponenteDescricao = config?.Descricao;
                                f.TipoComponenteCurricularTurma = BuscarTipoComponenteDivisaoTurma(f.SeqDivisaoComponente);
                                f.TipoDivisaoComponenteArtigo = config?.DataAttributes?.Any(k => k.Key == "tipo-artigo" && k.Value == true.ToString()) ?? false;
                            }
                        );

                    dynamic.DivisoesComponente = divisoesComponente.TransformList<DivisaoMatrizCurricularComponenteDetailViewModel>();
                    dynamic.DivisoesComponente.ForEach(f =>
                    {
                        f.EscalasApuracao = dynamic.EscalasApuracao;

                        // NV14
                        f.ListaTipoPagamentoAula = RecuperarTiposPagamentoAula(f.TipoDistribuicaoAula ?? TipoDistribuicaoAula.Nenhum);
                        f.ComprovacaoArtigoSelect = DivisaoMatrizCurricularComponenteService.BuscarComprovacaoArtigoOrdenada();
                    });
                    return PartialView("_EditarComponentesDivisoesListarView", dynamic);
                }
            }

            // Busca as configurações do critério de aprovação selecionado
            short? notaMaxima = null;
            bool apuracaoFrequencia = false;
            long? seqEscalaApuracao = null;
            if (seqCriterioAprovacao.HasValue)
            {
                var criterioAprovacaoService = this.Create<ICriterioAprovacaoService>();
                var criterio = criterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao.Value);
                notaMaxima = criterio.NotaMaxima;
                apuracaoFrequencia = criterio.ApuracaoFrequencia;
                if (!criterio.ApuracaoNota)
                    seqEscalaApuracao = criterio.SeqEscalaApuracao;
            }

            foreach (var item in divisoesComponenteConfiguracao)
            {
                int? cargaHorariaGrade = null;
                if (item.DataAttributes.FirstOrDefault(i => i.Key == "carga-horaria-grade")?.Value != null)
                    cargaHorariaGrade = Convert.ToInt32(item.DataAttributes.FirstOrDefault(i => i.Key == "carga-horaria-grade")?.Value);

                dynamic.DivisoesComponente.Add(new DivisaoMatrizCurricularComponenteDetailViewModel()
                {
                    SeqDivisaoComponente = item.Seq,
                    DivisaoComponenteDescricao = item.Descricao,
                    TipoComponenteCurricularTurma = BuscarTipoComponenteDivisaoTurma(item.Seq),
                    // Preenche os valores padrões das divisões
                    QuantidadeGrupos = 0,
                    QuantidadeProfessores = null,
                    EscalasApuracao = dynamic.EscalasApuracao,
                    NotaMaxima = notaMaxima,
                    ApurarFrequencia = apuracaoFrequencia,
                    SeqEscalaApuracao = seqEscalaApuracao,
                    DivisaoComponenteCargaHorariaGrade = cargaHorariaGrade,
                    ComprovacaoArtigoSelect = DivisaoMatrizCurricularComponenteService.BuscarComprovacaoArtigoOrdenada()
                });
            }
            return PartialView("_EditarComponentesDivisoesListarView", dynamic);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public JsonResult BuscarDadosTipoPagamentoAula(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            List<SMCDatasourceItem> ret = RecuperarTiposPagamentoAula(tipoDistribuicaoAula);
            return Json(ret);
        }

        private List<SMCDatasourceItem> RecuperarTiposPagamentoAula(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            List<SMCDatasourceItem> ret = new List<SMCDatasourceItem>();

            /*
             NV14:  Este campo somente será exibido se o campo de distribuição de horas estiver preenchido, e nesse caso será
                    origatório. As opções de seleção serão exibidas seguindo a regra: Quando o tipo de distribuição de aula for ‘Semanal’ todas as
                    opções listadas acima serão listadas. Quando o tipo de distribuição for ‘Quinzenal’ ou ‘Concentrada’ somente serão
                    listados os tipos de pagamento ‘Aula Semanal’ e ‘Aula Fracionada’. Quando o tipo de distribuição de aula for ‘Livre’ somente a opção ‘Aula executada’ será listada.*/

            if (tipoDistribuicaoAula == TipoDistribuicaoAula.Semanal)
            {
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaSemanal, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaSemanal)));
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaFracionada, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaFracionada)));
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaExecutada, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaExecutada)));
            }
            else if (tipoDistribuicaoAula == TipoDistribuicaoAula.Quinzenal || tipoDistribuicaoAula == TipoDistribuicaoAula.Concentrada)
            {
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaSemanal, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaSemanal)));
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaFracionada, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaFracionada)));
            }
            else if (tipoDistribuicaoAula == TipoDistribuicaoAula.Livre)
            {
                ret.Add(new SMCDatasourceItem((long)TipoPagamentoAula.AulaExecutada, SMCEnumHelper.GetDescription(TipoPagamentoAula.AulaExecutada)));
            }

            return ret;
        }
    }
}