using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.CAM.Views.TipoOferta.App_LocalResources;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.Common.Constants;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class TipoOfertaController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ITipoOfertaService TipoOfertaService
        {
            get { return this.Create<ITipoOfertaService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarTipoOfertaSelecaoOfertaCampanha(long seqTipoOferta, long seqCampanha)
        {
            var retorno = TipoOfertaService.BuscarTipoOfertaSelecaoOfertaCampanha(seqTipoOferta, seqCampanha);

            var mensagem = string.Empty;

            if (retorno.ExigeCursoOfertaLocalidadeTurno)
            {
                mensagem += string.Format(UIResource.MSG_TipoOfertaExigeCursoOfertaLocalidadeTurno, retorno.DescricaoCicloLetivo);

                switch (retorno.Token)
                {
                    case TOKEN_TIPO_OFERTA.AREA_CONCENTRACAO:
                        mensagem += UIResource.MSG_TipoOfertaAreaConcentracao;
                        break;
                    case TOKEN_TIPO_OFERTA.LINHA_PESQUISA:
                        mensagem += string.Format(UIResource.MSG_TipoOfertaLinhaPesquisa, retorno.DescricaoCicloLetivo);
                        break;
                    case TOKEN_TIPO_OFERTA.EIXO_TEMATICO:
                        mensagem += string.Format( UIResource.MSG_TipoOfertaEixoTematico, retorno.DescricaoCicloLetivo);
                        break;
                    case TOKEN_TIPO_OFERTA.AREA_TEMATICA:
                        mensagem += string.Format(UIResource.MSG_TipoOfertaAreaTematica,retorno.DescricaoCicloLetivo);
                        break;
                    case TOKEN_TIPO_OFERTA.ORIENTADOR:
                        mensagem += string.Format(UIResource.MSG_TipoOfertaOrientador, retorno.DescricaoCicloLetivo);
                        break;
                    default:
                        break;
                }
            }
            else            
                if (retorno.Token == TOKEN_TIPO_OFERTA.TURMA)
                    mensagem += UIResource.MSG_TipoOfertaNaoExigeCursoOfertaLocalidadeTurnoTipoTurma;

            if (!string.IsNullOrEmpty(mensagem))
                return Content($"<div class='smc-sga-mensagem-informativa smc-sga-mensagem smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-size-lg-24'><p>{mensagem}</p></div>");

            return Content(mensagem);
        }
    }
}