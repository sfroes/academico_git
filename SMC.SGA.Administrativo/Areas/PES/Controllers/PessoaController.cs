using SMC.Academico.Common.Areas.PES.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Controllers
{
    public class PessoaController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ILocalidadeService LocalidadeService
        {
            get { return this.Create<ILocalidadeService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult BuscarPaisesPorTipoNacionalidade(TipoNacionalidade tipoNacionalidade, bool multipleRequest = false)
        {
            List<SMCSelectListItem> paises;
            paises = this.LocalidadeService.BuscarPaisesValidosCorreios().Select(s => new SMCSelectListItem(s.Codigo, s.Nome)).ToList();

            if (tipoNacionalidade == TipoNacionalidade.Brasileira)
            {
                var valor = LocalidadesDefaultValues.SEQ_PAIS_BRASIL.ToString();
                paises.FirstOrDefault(f => f.Value == valor).Selected = true;
            }

            if (multipleRequest)
            {
                return Json(new
                {
                    CodigoPaisNacionalidade = paises,
                    CodigoPaisNacionalidadeReadOnly = paises
                });
            }
            else
                return Json(paises);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarCidadesPorUF(string ufNaturalidade)
        {
            var localidades = this.LocalidadeService.BuscarCidadesPorUF(ufNaturalidade)
                .Select(s => new SMCSelectListItem(s.Codigo, s.Nome));

            return Json(localidades);
        }

        [SMCAllowAnonymous]
        public ActionResult ValidarObrigatoriedadePassaporte(TipoNacionalidade tipoNacionalidade, string NumeroPassaporte, DateTime? DataValidadePassaporte, int? CodigoPaisEmissaoPassaporte, TipoAtuacao? tipoAtuacao, TipoAtuacao? tipoAtuacaoAuxiliar)
        {
            bool obrigatorio = RecuperarObrigatoriedadePassaporte(tipoNacionalidade, NumeroPassaporte, DataValidadePassaporte, CodigoPaisEmissaoPassaporte, tipoAtuacao, tipoAtuacaoAuxiliar);

            return Content(obrigatorio.ToString().ToLower());
        }

        public static bool RecuperarObrigatoriedadePassaporte(TipoNacionalidade tipoNacionalidade, string NumeroPassaporte, DateTime? DataValidadePassaporte, int? CodigoPaisEmissaoPassaporte, TipoAtuacao? tipoAtuacao, TipoAtuacao? tipoAtuacaoAuxiliar)
        {
            var tipoAtuacaoNaoEhColaborador = (tipoAtuacao.HasValue && tipoAtuacao != TipoAtuacao.Colaborador) || (tipoAtuacaoAuxiliar.HasValue && tipoAtuacaoAuxiliar != TipoAtuacao.Nenhum && tipoAtuacaoAuxiliar != TipoAtuacao.Colaborador);

            if (tipoAtuacaoNaoEhColaborador)
            {
                return tipoNacionalidade == TipoNacionalidade.Estrangeira
                    || !string.IsNullOrEmpty(NumeroPassaporte)
                    || DataValidadePassaporte.HasValue
                    || CodigoPaisEmissaoPassaporte.HasValue;
            }
            else
            {
                return false;
            }
        }

        [SMCAllowAnonymous]
        public ActionResult RetirarObrigatoridadeParaEstrangeiro(TipoNacionalidade tipoNacionalidade, TipoAtuacao? tipoAtuacao, TipoAtuacao? tipoAtuacaoAuxiliar)
        {
            var obrigatoriedade = RecuperarObrigatoridadeParaEstrangeiro(tipoNacionalidade, tipoAtuacao, tipoAtuacaoAuxiliar);

            return Content(obrigatoriedade.ToString().ToLower());
        }

        public static bool RecuperarObrigatoridadeParaEstrangeiro(TipoNacionalidade tipoNacionalidade, TipoAtuacao? tipoAtuacao, TipoAtuacao? tipoAtuacaoAuxiliar)
        {
            var tipoAtuacaoColaborador = (tipoAtuacao.HasValue && tipoAtuacao == TipoAtuacao.Colaborador) || (tipoAtuacaoAuxiliar.HasValue && tipoAtuacaoAuxiliar != TipoAtuacao.Nenhum && tipoAtuacaoAuxiliar == TipoAtuacao.Colaborador);

            if (tipoAtuacaoColaborador)
            {
                return tipoNacionalidade != TipoNacionalidade.Estrangeira;
            }
            else
            {
                return true;
            }
        }
    }
}