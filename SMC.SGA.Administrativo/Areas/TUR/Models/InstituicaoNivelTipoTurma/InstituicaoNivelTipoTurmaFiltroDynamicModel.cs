using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class InstituicaoNivelTipoTurmaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("TipoTurma")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITURDynamicService))]
        public List<SMCDatasourceItem> TiposTurma { get; set; }

        #endregion DataSource

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSortable(false, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCRequired]
        [SMCFilter(true, true)]
        [SMCSortable(false, true, "TipoTurma.Descricao")]
        [SMCSelect("TipoTurma", autoSelectSingleItem: true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqTipoTurma { get; set; }

    }
}