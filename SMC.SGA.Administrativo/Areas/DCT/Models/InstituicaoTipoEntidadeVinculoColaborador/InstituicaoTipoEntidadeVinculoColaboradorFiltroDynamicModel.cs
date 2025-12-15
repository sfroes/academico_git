using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoTipoEntidadeVinculoColaboradorFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicoesTipoEntidade")]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqInstituicaoTipoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidadeSelect))]
        public List<SMCDatasourceItem> InstituicoesTipoEntidade { get; set; }

        [SMCFilter(true, true)]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("TiposVinculoColaborador", "Seq", "Descricao", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqTipoVinculoColaborador { get; set; }

        [SMCDataSource("TipoVinculoColaborador")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        public List<SMCDatasourceItem> TiposVinculoColaborador { get; set; }
    }
}