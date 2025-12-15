using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class TurmaOrientacaoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService { get => this.Create<ITurmaService>(); }

        private IAlunoService AlunoService { get => Create<IAlunoService>(); }

        private IDivisaoTurmaService DivisaoTurmaService { get => Create<IDivisaoTurmaService>(); }

        #endregion [ Services ]

        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult CabecalhoTurma(long seqDivisaoTurma)
        {
            var cabecalho = new TurmaCabecalhoViewModel();

            var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(seqDivisaoTurma).FirstOrDefault(d => d.Seq == seqDivisaoTurma);
            cabecalho.DescricaoTurma = dadosDivisao.TurmaDescricaoFormatado;
            cabecalho.DescricoesCursoOfertaLocalidadeTurno = dadosDivisao.DescricoesCursoOfertaLocalidadeTurno;
            cabecalho.SeqComponenteCurricular = dadosDivisao.SeqComponenteCurricular;

            //var cabecalho = TurmaService.BuscarTurmaCabecalho(seqTurma).Transform<TurmaCabecalhoViewModel>();

            return PartialView("_Cabecalho", cabecalho);
        }

        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult CabecalhoTurmaCompleto(long seqTurma, long seq)
        {
            var cabecalho = TurmaService.BuscarTurmaCabecalho(seqTurma).Transform<TurmaOrientacaoCabecalhoViewModel>();

            var aluno = this.AlunoService.BuscarAluno(seq);

            foreach (var item in cabecalho.TurmaConfiguracoesCabecalho)
            {
                item.RaNome = string.IsNullOrEmpty(aluno.NomeSocial) ? $"{aluno.NumeroRegistroAcademico} - {aluno.Nome}" : $"{aluno.NumeroRegistroAcademico} - {aluno.NomeSocial} ({aluno.Nome})";
            }

            return PartialView("_CabecalhoCompleto", cabecalho);
        }

        [SMCAuthorize(UC_TUR_001_02_01.PESQUISAR_ASSOCIACAO_PROFESSOR_TURMA)]
        public ActionResult CabecalhoListar(TurmaOrientacaoDynamicModel modelo)
        {
            return PartialView("_CabecalhoListar", modelo);
        }
    }
}