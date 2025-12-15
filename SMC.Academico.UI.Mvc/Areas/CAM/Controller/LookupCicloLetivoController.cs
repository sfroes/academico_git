using SMC.Academico.Common.Enums;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ALN.Models;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.CAM.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Controllers
{
    public class LookupCicloLetivoController : SMCControllerBase
    {
        #region [ Services ]

        private ICicloLetivoService CicloLetivoService =>  this.Create<ICicloLetivoService>();

        private IRegimeLetivoService RegimeLetivoService => this.Create<IRegimeLetivoService>();

        #endregion [ Services ]

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCiclosLetivosLookupSelect(LookupCicloLetivoFiltroViewModel filtro)
        {
           SMCPagerData<CicloLetivoData> result =  CicloLetivoService.BuscarCiclosLetivosLookup(filtro.Transform<CicloLetivoFiltroData>());

            var retorno = result.Select(s => new
            {
               Key = s.Seq,
               Value = s.Descricao
            });

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarCiclosLetivosLookup(LookupCicloLetivoFiltroViewModel filtro)
        {
            SMCPagerData<CicloLetivoData> result = CicloLetivoService.BuscarCiclosLetivosLookup(filtro.Transform<CicloLetivoFiltroData>());

            var retorno = new
            {
                itens = result.Select(s => new
                {
                    s.Seq,
                    s.Descricao,
                    s.DescricaoRegimeLetivo,
                    DescricaoNiveisEnsino = s.NiveisEnsino?.Select(sn => sn.Descricao).ToList()
                }),
                total = result.Total
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarDataSourceRegimeLetivo()
        {
            var result = RegimeLetivoService.BuscarRegimesLetivosInstituicaoSelect();

            var retorno = result.Select(s => new
            {
                Value = s.Seq,
                Label = s.Descricao
            });

            return SMCJsonResultAngular(retorno);
        }
    }
}
