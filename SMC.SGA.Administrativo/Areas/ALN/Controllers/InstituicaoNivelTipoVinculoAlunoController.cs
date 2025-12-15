using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class InstituicaoNivelTipoVinculoAlunoController : SMCDynamicControllerBase 
    {
        #region [ Services ]
        
        private IInstituicaoNivelTipoTermoIntercambioService InstituicaoNivelTipoTermoIntercambioService
        {
            get { return this.Create<IInstituicaoNivelTipoTermoIntercambioService>(); }
        }
                
        #endregion [ Services ]

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult VinculoParametros(InstituicaoNivelTipoVinculoAlunoDynamicModel model)
        {
            this.ConfigureDynamic(model); 

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult FormaIngresso(InstituicaoNivelTipoVinculoAlunoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            //O campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) só poderá assumir o"Sim",
            //se o valor do campo "Exige curso?"(Parâmetros Gerais) for "Sim"
            if (!(bool)model.ExigeCurso && (bool)model.ExigeOfertaMatrizCurricular)
            {
                throw new ExigeOfertaMatrizCurricularExigeCursoException();
            }


            //O campo "Concede formação?"(Parâmetros Gerais) só poderá assumir o valor "Sim", se o valor do
            //campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) for "Sim".
            if (!(bool)model.ExigeOfertaMatrizCurricular && (bool)model.ConcedeFormacao)
            {
                throw new ExigeOfertaMatrizCurricularConcedeFormacaoException();
            }

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult TipoTermoIntercambio(InstituicaoNivelTipoVinculoAlunoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult SituacaoMatricula(InstituicaoNivelTipoVinculoAlunoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            if (model.TiposTermoIntercambio != null && model.TiposTermoIntercambio.Count > 0)
            {
                foreach (var item in model.TiposTermoIntercambio)
                {
                    if (!(bool)model.ExigeOfertaMatrizCurricular && (bool)item.ConcedeFormacao)
                    {
                        //O campo "Concede formação?"(Passo 3-Tipo de Termo de Intercâmbio) só poderá assumir o valor "Sim", se o valor
                        //do campo "Exige oferta de matriz curricular?"(Parâmetros Gerais) for "Sim"
                        throw new ConcedeFormacaoTermoIntercambioException();
                    }

                    if (item.ExigePeriodoIntercambioTermo != null)
                    {
                        if ((bool)item.ConcedeFormacao && (bool)item.ExigePeriodoIntercambioTermo)
                        {
                            //Se o valor do campo "Concede formação?" for "Sim", setar o valor "Não" para o campo
                            //"Exige período de intercâmbio no termo ? " e desabilitá-lo para alteração.
                            throw new ConcedeFormacaoExigePeriodoException();
                        }

                        if (!(bool)item.ConcedeFormacao && !(bool)item.ExigePeriodoIntercambioTermo)
                        {
                            //Se o valor do campo "Concede formação?" for "Não", setar o valor "Sim" para o campo
                            //"Exige período de intercâmbio no termo ? " e desabilitá-lo para alteração.
                            throw new NaoConcedeFormacaoNaoExigePeriodoException();
                        }

                        var registroValidacao = InstituicaoNivelTipoTermoIntercambioService.ValidarTermoIntercambioInstituicaoNivel(model.Seq, item.SeqTipoTermoIntercambio, model.SeqInstituicaoNivel, (bool)item.ConcedeFormacao);
                        if (registroValidacao != null && registroValidacao.Count > 0)
                        {
                            string mensagem = $"</br> { string.Join(",</br> ", registroValidacao)} </br>";
                            throw new ConcedeFormacaoInstituicaoValorDiferenteException(mensagem);
                        }
                    }
                }
            }

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult Confirmacao(InstituicaoNivelTipoVinculoAlunoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            //Vinculo 
            IList<SMCDatasourceItem> tiposNiveisEnsino = this.Create<IInstituicaoNivelService>().BuscarNiveisEnsinoDaInstituicaoSelect();
            model.NivelEnsinoConfirmacao = tiposNiveisEnsino.FirstOrDefault(f => f.Seq == model.SeqInstituicaoNivel)?.Descricao;

            IList<SMCDatasourceItem> tiposVinculoAluno = this.Create<ITipoVinculoAlunoService>().BuscarTiposVinculoAlunoSelect(); 
            model.DescricaoVinculoConfirmacao = tiposVinculoAluno.FirstOrDefault(f => f.Seq == model.SeqTipoVinculoAluno)?.Descricao;

            //Parâmetros Gerais  
            model.ExigeParceriaConfirmacao = (model.ExigeParceriaIntercambioIngresso.Value == true) ? "Sim" : "Não";
            model.ExigeCursoConfirmacao = (model.ExigeCurso.Value == true) ? "Sim" : "Não";
            model.ConcedeFormacaoConfirmacao = (model.ConcedeFormacao.Value == true) ? "Sim" : "Não";
            model.ExigeOfertaMatrizCurricularConfirmacao = (model.ExigeOfertaMatrizCurricular.Value == true) ? "Sim" : "Não";
            if(model.QuantidadeOfertaCampanhaIngresso.HasValue)
                model.QuantidadeOfertasConfirmacao = model.QuantidadeOfertaCampanhaIngresso.Value.ToString();
            model.TipoDeCobrancaConfirmacao = model.TipoCobranca.SMCGetDescription();
            model.PossuiValorFixoMatriculaConfirmacao = (model.PossuiValorFixoMatricula.Value == true) ? "Sim" : "Não";

            //Forma de ingresso           
            IList<SMCDatasourceItem> formasIngresso = this.Create<IFormaIngressoService>().BuscarFormasIngressoSelect(new FormaIngressoFiltroData() {SeqTipoVinculoAluno = model.SeqTipoVinculoAluno });

            model.FormasIngressoConfirmacao = new List<InstituicaoNivelFormaIngressoConfirmacaoViewModel>();
            foreach (var item in model.FormasIngresso)
            {
                var objFormaIngresso = new InstituicaoNivelFormaIngressoConfirmacaoViewModel();
                objFormaIngresso.FormaIngressoConfirmacao = formasIngresso.Where(w => w.Seq == item.SeqFormaIngresso).FirstOrDefault().Descricao;
                objFormaIngresso.TipoFormaIngressoConfirmacao = this.Create<IFormaIngressoService>().BuscarFormaIngresso(item.SeqFormaIngresso).TipoFormaIngresso.SMCGetDescription();
                model.FormasIngressoConfirmacao.Add(objFormaIngresso);
            }

            //Tipo de termo de intercambio
            model.TiposTermoIntercambioConfirmacao = new List<InstituicaoNivelTipoTermoIntercambioConfirmacaoViewModel>();
            var tiposTermo = this.Create<ITipoTermoIntercambioService>().BuscarTiposTermosIntercambiosSelect();
            foreach (var item in model.TiposTermoIntercambio)
            {
                var objTipoTermo = new InstituicaoNivelTipoTermoIntercambioConfirmacaoViewModel();
                objTipoTermo.TipoTermoIntercambioConfirmacao = tiposTermo.FirstOrDefault(f => f.Seq == item.SeqTipoTermoIntercambio)?.Descricao;
                objTipoTermo.ConcedeFormacaoTipoTermoConfirmacao = (item.ConcedeFormacao.Value == true) ? "Sim" : "Não";
                objTipoTermo.ExigePeriodoIntercambioTermoConfirmacao = (item.ExigePeriodoIntercambioTermo.Value == true) ? "Sim" : "Não";
                objTipoTermo.PermiteIngressoConfirmacao = (item.PermiteIngresso.Value == true) ? "Sim" : "Não";
                objTipoTermo.PermiteSaidaIntercambioConfirmacao = (item.PermiteSaidaIntercambio.Value == true) ? "Sim" : "Não";
                model.TiposTermoIntercambioConfirmacao.Add(objTipoTermo);
            }

            //Situação de matrícula
            model.SituacoesMatriculaConfirmacao = new List<InstituicaoNivelSituacaoMatriculaConfirmacaoViewModel>();
            IList<SMCDatasourceItem> situacoesMatricula = this.Create<ISituacaoMatriculaService>().BuscarSituacoesMatriculasSelect();
            foreach (var item in model.SituacoesMatricula)
            {
                var objSituacao = new InstituicaoNivelSituacaoMatriculaConfirmacaoViewModel();
                objSituacao.SituacaoMatriculaConfirmacao = situacoesMatricula.Where(w => w.Seq == item.SeqSituacaoMatricula).FirstOrDefault().Descricao;
                model.SituacoesMatriculaConfirmacao.Add(objSituacao);
            } 

            return SMCViewWizard(model, null); 
        } 

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult BuscarTipoFormaIngresso(long SeqFormaIngresso)
        {
            var formaIngresso = this.Create<IFormaIngressoService>().BuscarFormaIngresso(SeqFormaIngresso).TipoFormaIngresso.SMCGetDescription();

            return Json(formaIngresso);
        }

        [SMCAuthorize(UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL)]
        public ActionResult PreencheExigePeriodoIntercambioTermo(bool? concedeFormacao)
        {
            if (concedeFormacao.HasValue)
            {
                bool retorno = !concedeFormacao.Value;
                return Json(retorno.ToString());
            }

            return Json(null);
        } 


    }
}
