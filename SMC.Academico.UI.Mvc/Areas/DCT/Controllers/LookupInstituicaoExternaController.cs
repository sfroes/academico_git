using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.DCT.Controllers
{
    public class LookupInstituicaoExternaController : SMCControllerBase
    {
        #region [ Services ]

        private IInstituicaoExternaService InstituicaoExternaService => this.Create<IInstituicaoExternaService>();

        private ILocalidadeService LocalidadeService => this.Create<ILocalidadeService>();

        private ICategoriaInstituicaoEnsinoService CategoriaInstituicaoEnsinoService => this.Create<ICategoriaInstituicaoEnsinoService>();

        #endregion [ Services ]


        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarSelectInstituicoesExternas(LookupInstituicaoExternaFiltroViewModel filtro)
        {
            if (!filtro.RetornarInstituicaoEnsinoLogada)
            {
                filtro.SeqInstituicaoEnsino = HttpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Value;
            }

            SMCPagerData<InstituicaoExternaListaData> result = InstituicaoExternaService.BuscarInstituicoesExternas(filtro.Transform<InstituicaoExternaFiltroData>());

            var retorno = result.Select(s => new
            {
                Key = s.Seq,
                Value = s.Nome
            });

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarInstituicoesExternas(LookupInstituicaoExternaFiltroViewModel filtro)
        {
            if (!filtro.RetornarInstituicaoEnsinoLogada)
            {
                filtro.SeqInstituicaoEnsino = HttpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Value;
            }

            SMCPagerData<InstituicaoExternaListaData> result = InstituicaoExternaService.BuscarInstituicoesExternas(filtro.Transform<InstituicaoExternaFiltroData>());

            var retorno = new
            {
                itens = result.Select(s => new
                {
                    s.Seq,
                    s.Sigla,
                    s.Nome,
                    s.DescricaoPais,
                    s.Ativo,
                    TipoInstituicaoEnsino = s.TipoInstituicaoEnsino.SMCGetDescription(),
                    s.DescricaoCategoria

                }),
                total = result.Total
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceCategoriasInstituicaoEnsino()
        {
            List<SMCDatasourceItem> result = CategoriaInstituicaoEnsinoService.BuscarCategoriasInstituicaoEnsinoSelect();

            return SMCDataSourceAngular(result, keyValue: true);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourcePaisesValidosCorreios()
        {
            List<SMCDatasourceItem> result = LocalidadeService.BuscarPaisesValidosCorreios().Select(s => new SMCDatasourceItem(s.Codigo, s.Nome)).ToList();

            return SMCDataSourceAngular(result, keyValue: true);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceTipoInstituicaoEnsino()
        {
            return SMCDataSourceAngular<TipoInstituicaoEnsino>(keyValue: true);
        }
    }
}

