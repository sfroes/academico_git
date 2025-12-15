using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.DCT.Controllers
{
    public class ColaboradorVinculoController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IColaboradorVinculoService ColaboradorVinculoService => Create<IColaboradorVinculoService>();

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => Create<ICursoOfertaLocalidadeService>();

        private IFormacaoEspecificaService FormacaoEspecificaService => Create<IFormacaoEspecificaService>();

        private IHierarquiaEntidadeItemService HierarquiaEntidadeItemService => Create<IHierarquiaEntidadeItemService>();

        private IInstituicaoNivelModalidadeService InstituicaoNivelModalidadeService => Create<IInstituicaoNivelModalidadeService>();

        private IInstituicaoTipoEntidadeVinculoColaboradorService InstituicaoTipoEntidadeVinculoColaboradorService => Create<IInstituicaoTipoEntidadeVinculoColaboradorService>();

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>();

        private IProgramaService ProgramaService => Create<IProgramaService>();

        private ITipoVinculoColaboradorService TipoVinculoColaboradorService => Create<ITipoVinculoColaboradorService>();

        #endregion [ Services ]

        public ActionResult CabecalhoColaboradorVinculo(SMCEncryptedLong seqColaborador)
        {
            var model = ExecuteService<PessoaAtuacaoData, PessoaAtuacaoCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacao, seqColaborador);

            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarTiposVinculoColaboradorPorEntidadeSelect(SMCEncryptedLong seqEntidadeVinculo, SMCEncryptedLong seqColaboradorVinculo)
        {
            var tiposVinculo = seqEntidadeVinculo != null
                                ? this.InstituicaoTipoEntidadeVinculoColaboradorService.BuscarTiposVinculoColaboradorPorEntidadeSelect(seqEntidadeVinculo, seqColaboradorVinculo)
                                : TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect();

            return Json(tiposVinculo);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarTiposVinculoColaboradorLookupSelect(SMCEncryptedLong seqEntidadeVinculo, bool? criaVinculoInstitucional = null)
        {
            var tiposVinculo = seqEntidadeVinculo != null
                                ? TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(seqEntidadeVinculo, criaVinculoInstitucional)
                                : TipoVinculoColaboradorService.BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(null, criaVinculoInstitucional);

            return Json(tiposVinculo);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarModalidadesNivelEnsino(SMCEncryptedLong seqNivelEnsino)
        {
            var modalidadesNivel = seqNivelEnsino != null && seqNivelEnsino > 0 ?
                this.InstituicaoNivelModalidadeService.BuscarModalidadesPorNivelEnsinoSelect(seqNivelEnsino) :
                this.InstituicaoNivelModalidadeService.BuscarModalidadesPorInstituicaoSelect();
            return Json(modalidadesNivel);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarLinhasPesquisaGrupoProgramaSelect(SMCEncryptedLong seqEntidadeVinculo)
        {
            var tiposVinculo = this.FormacaoEspecificaService.BuscarLinhasDePesquisaGrupoPrograma(seqEntidadeVinculo);
            return Json(tiposVinculo);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarTiposAtividadeColaborador(SMCEncryptedLong SeqCursoOfertaLocalidade)
        {
            var atividades = this.ColaboradorVinculoService.BuscarTiposAtividadeCursoOfertaLocalidadeSelect(SeqCursoOfertaLocalidade);
            return Json(atividades);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarNomeCursoOfertaLocalidadeVinculo(SMCEncryptedLong SeqCursoOfertaLocalidade)
        {
            var nomeLocalidade = this.CursoOfertaLocalidadeService.BuscarCursoOfertaLocalidade(SeqCursoOfertaLocalidade);
            return Json(nomeLocalidade.DescricaoCursoOferta);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult BuscarNomeLocalidadeVinculo(SMCEncryptedLong SeqCursoOfertaLocalidade)
        {
            var nomeLocalidade = this.CursoOfertaLocalidadeService.BuscarCursoOfertaLocalidade(SeqCursoOfertaLocalidade);
            return Json(nomeLocalidade.NomeLocalidade);
        }

        [SMCAuthorize(UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR)]
        public ActionResult ValidarTipoEntidadeVinculoGrupoPrograma(SMCEncryptedLong seqEntidadeVinculo)
        {
            var vinculoPrograma = this.ColaboradorVinculoService.ValidarVinculoGrupoPrograma(seqEntidadeVinculo);
            return Json(vinculoPrograma);
        }

        [SMCAuthorize(UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR)]
        public ActionResult RetornarTipoVinculoNecessitaAcompanhamento(long seqTipoVinculoColaborador = 0)
        {
            var retorno = this.TipoVinculoColaboradorService.RetornarTipoVinculoNecessitaAcompanhamento(seqTipoVinculoColaborador);
            return Json(retorno);
        }

        [SMCAuthorize(UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR)]
        public ActionResult BuscarEntidadesFilhas(long seqEntidadeVinculo)
        {
            var retorno = this.ProgramaService.BuscarSeqsProgramasGrupo(seqEntidadeVinculo);
                        
            return Json(retorno);
        }

        [SMCAuthorize(UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR)]
        public ActionResult BuscarCursosOfertaLocalidadeVinculo(long seqEntidadeVinculo)
        {
            var result = CursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect(seqEntidadeVinculo);
            return Json(result);
        }

        [SMCAuthorize(UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR)]
        public ActionResult CalcularDataMaxima(DateTime? dataFimVinculo = null, DateTime? datafim = null)
        {
            if (dataFimVinculo == null && datafim == null)
                return Content("");
            var datas = new[] { dataFimVinculo ?? DateTime.MaxValue, datafim ?? DateTime.MaxValue };
            return Content(datas.Min().ToString("dd/MM/yyyy"));
        }

        [SMCAuthorize(UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR)]
        public ActionResult ExibirDataFimSomenteLeitura(long? seqTipoVinculoColaborador, bool permitirAlterarDataFimVinculo)
        {
            bool colaboradorAtivo = false;

            if(!seqTipoVinculoColaborador.HasValue)
            {
                if(permitirAlterarDataFimVinculo)
                    colaboradorAtivo = true;
            }
            else if (seqTipoVinculoColaborador.HasValue)
            {
                if (seqTipoVinculoColaborador.Value == 4) // 4 = Pós Doutorando
                { 
                    colaboradorAtivo = true; 
                }
                else if (permitirAlterarDataFimVinculo)
                {
                    colaboradorAtivo = true;
                }
            }            

            return Json(colaboradorAtivo);
        }
    }
}