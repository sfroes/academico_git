using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Aluno.Areas.FIN.Models;
using SMC.SGA.Aluno.Extensions;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Controllers
{
    public class NegociacaoParcelaCartaoController : SMCControllerBase
    {
        #region [ Services ]

        internal IPessoaService PessoaService
        {
            get { return Create<IPessoaService>(); }
        }

        internal IPessoaAtuacaoService PessoaAtuacaoService
        {
            get { return Create<IPessoaAtuacaoService>(); }
        }

        internal IAlunoService AlunoService
        {
            get { return Create<IAlunoService>(); }
        }

        #endregion [ Services ]

        public ActionResult Index()
        {
            NegociacaoParcelaCartaoViewModel model = new NegociacaoParcelaCartaoViewModel();
            try
            {
                var alunoLogado = this.GetAlunoLogado();
                if (alunoLogado == null || alunoLogado.Seq == 0)
                    throw new AlunoLogadoSemVinculoException();

                PessoaAtuacaoData pessoaAtuacao = PessoaAtuacaoService.BuscarPessoaAtuacao(alunoLogado.Seq);
                AlunoData aluno = AlunoService.BuscarAluno(alunoLogado.Seq);
                long codigoPessoa = PessoaService.BuscarCodigoDePessoaNosDadosMestres(pessoaAtuacao.SeqPessoa, TipoPessoa.Fisica, alunoLogado.Seq);

                //Preenchendo o modelo cujos dados serão passados para o componente do financeiro.
                model.CodigoAluno = aluno.CodigoAlunoMigracao.GetValueOrDefault();
                model.CodigoPessoa = codigoPessoa;
                model.SituacaoAluno = 1;    //TODO: Saber de onde vem isto.
                model.TipoAluno = 1;        //TODO: Saber de onde vem isto.
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, "", SMCMessagePlaceholders.Centro);
            }

            return View("Index", model);
        }
    }
}