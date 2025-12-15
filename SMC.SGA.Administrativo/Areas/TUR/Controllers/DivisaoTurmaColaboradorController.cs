using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class DivisaoTurmaColaboradorController : SMCControllerBase
    {
        #region [ Services ]

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return this.Create<IDivisaoTurmaService>(); }
        }

        private ITurmaService TurmaService
        {
            get { return this.Create<ITurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_TUR_001_02_01.PESQUISAR_ASSOCIACAO_PROFESSOR_TURMA)]
        public ActionResult CabecalhoTurma(long seqTurma)
        {
            var cabecalho = TurmaService.BuscarTurmaCabecalho(seqTurma).Transform<TurmaCabecalhoViewModel>();
            return PartialView("_Cabecalho", cabecalho);
        }

        [SMCAuthorize(UC_TUR_001_02_01.PESQUISAR_ASSOCIACAO_PROFESSOR_TURMA)]
        public ActionResult Index(SMCEncryptedLong seqTurma)
        {
            var model = DivisaoTurmaService.BuscarDivisaoTurmaLista(seqTurma);
            var turma = new DivisaoTurmaColaboradorViewModel();

            if (model != null && model.Count > 0)
            {
                turma.Divisoes = new List<DivisaoTurmaColaboradorListaViewModel>();
                turma.SeqTurma = seqTurma;

                foreach (var item in model)
                {
                    var divisaoView = new DivisaoTurmaColaboradorListaViewModel();
                    divisaoView.SeqDivisao = item.Seq;
                    divisaoView.SeqTurma = seqTurma;
                    divisaoView.DivisaoComponenteDescricao = item.DescricaoFormatada;
                    divisaoView.GeraOrientacao = item.GeraOrientacao;
                    divisaoView.DiarioFechado = item.DiarioFechado;
                    divisaoView.TurmaPossuiAgenda = item.TurmaPossuiAgenda;
                    divisaoView.CargaHorariaGrade = item.CargaHorariaGrade;
                    divisaoView.DetalhesDivisao = new List<DivisaoTurmaColaboradorDivisoesViewModel>();
                    divisaoView.DetalhesDivisao.Add(item.Transform<DivisaoTurmaColaboradorDivisoesViewModel>());
                    divisaoView.ColaboradoresDivisao = new List<DivisaoTurmaColaboradorProfessorViewModel>();
                    foreach (var itemColaborador in item.Colaboradores)
                    {
                        var colaborador = new DivisaoTurmaColaboradorProfessorViewModel();
                        colaborador.Seq = itemColaborador.Seq;
                        colaborador.SeqTurma = turma.SeqTurma;
                        colaborador.QuantidadeCargaHoraria = itemColaborador.QuantidadeCargaHoraria;
                        colaborador.SeqColaborador = itemColaborador.SeqColaborador;
                        colaborador.NomeColaborador = itemColaborador.NomeFormatado;
                        colaborador.GeraOrientacao = item.GeraOrientacao;
                        colaborador.DiarioFechado = item.DiarioFechado;
                        colaborador.TurmaPossuiAgenda = item.TurmaPossuiAgenda;
                        colaborador.CargaHorariaGrade = item.CargaHorariaGrade;

                        divisaoView.ColaboradoresDivisao.Add(colaborador);
                    }

                    turma.Divisoes.Add(divisaoView);
                }
            }

            return View(turma);
        }
    }
}