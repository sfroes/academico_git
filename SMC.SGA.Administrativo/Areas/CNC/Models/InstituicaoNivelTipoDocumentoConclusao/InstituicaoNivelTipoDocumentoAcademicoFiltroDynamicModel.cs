using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class InstituicaoNivelTipoDocumentoAcademicoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoComSequencialInstituicaoNivelSelect))]
        public List<SMCDatasourceItem> NiveisEnsinoDataSource { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoDocumentoAcademicoService), nameof(ITipoDocumentoAcademicoService.BuscarTiposDocumentoAcademicoSelect))]
        public List<SMCDatasourceItem> TiposDocumentoAcademicoDataSource { get; set; }

        #endregion DataSources  

        [SMCFilter(true, true)]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect(nameof(NiveisEnsinoDataSource))]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSortable(true, true, "TipoDocumentoAcademico.Descricao")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposDocumentoAcademicoDataSource))]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public UsoSistemaOrigem? UsoSistemaOrigem { get; set; }
    }
}