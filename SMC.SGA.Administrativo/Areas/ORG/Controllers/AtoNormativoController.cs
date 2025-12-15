using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class AtoNormativoController : SMCDynamicControllerBase
    {
        #region [Services]

        private ITipoAtoNormativoService TipoAtoNormativoService => this.Create<ITipoAtoNormativoService>();
        private IAssuntoNormativoService AssuntoNormativoService => this.Create<IAssuntoNormativoService>();
        private ITipoEntidadeService TipoEntidadeService => this.Create<ITipoEntidadeService>();

        #endregion

        [SMCAuthorize(UC_ORG_003_03_02.MANTER_ATO_NORMATIVO)]
        public ActionResult ManipularDescricao(long seq, long? SeqAssuntoNormativo, long? seqTipoAtoNormativo, string numeroDocumento, DateTime? dataDocumento,
            int? NumeroPublicacao, int? NumeroSecaoPublicacao, int? NumeroPaginaPublicacao, DateTime? DataPublicacao, VeiculoPublicacao? veiculoPublicacao, string descricao)
        {
            string retorno = string.Empty;

            if (seq == 0)
            {
                TipoAtoNormativoData tipoAtoNormativoData = TipoAtoNormativoService.BuscarTipoAtoNormativo(seqTipoAtoNormativo.GetValueOrDefault());
                AssuntoNormativoData AssuntoNormativo = AssuntoNormativoService.BuscarAssuntoNormativo(SeqAssuntoNormativo.GetValueOrDefault());

                if (AssuntoNormativo != null && tipoAtoNormativoData != null && !string.IsNullOrEmpty(numeroDocumento) && dataDocumento.HasValue)
                {
                    if (veiculoPublicacao.HasValue)
                    {
                        retorno = $"{AssuntoNormativo.Descricao}: {tipoAtoNormativoData?.Descricao} nº{numeroDocumento}, de {dataDocumento?.ToShortDateString()}, " +
                                  $"{veiculoPublicacao} nº{NumeroPublicacao}, seção {NumeroSecaoPublicacao}, página {NumeroPaginaPublicacao}, de {DataPublicacao?.ToShortDateString()}";
                    }
                    else
                        retorno = $"{AssuntoNormativo.Descricao}: {tipoAtoNormativoData?.Descricao} nº{numeroDocumento}, de {dataDocumento?.ToShortDateString()}";
                }
            }
            else
                retorno = descricao;

            return Json(retorno);
        }


        [SMCAuthorize(UC_ORG_003_03_01.PESQUISAR_ATO_NORMATIVO)]
        public JsonResult BuscarTokenTipoEntidade(long SeqTipoEntidade)
        {
            var retorno = TipoEntidadeService.BuscarTokenTipoEntidade(SeqTipoEntidade);
            return Json(retorno);
        }

        [SMCAuthorize(UC_ORG_003_03_02.MANTER_ATO_NORMATIVO)]
        public JsonResult HabilitaCampos(VeiculoPublicacao? VeiculoPublicacao)
        {
            var habilita = VeiculoPublicacao != null;
            return Json(habilita);
        }
    }
}