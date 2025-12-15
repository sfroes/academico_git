using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IRegimeLetivoService), nameof(IRegimeLetivoService.BuscarRegimesLetivosInstituicaoSelect))]
        public List<SMCDatasourceItem> RegimesLetivosDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> NiveisEnsinoDataSource { get; set; }

        #endregion [ DataSources ]

        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCMask("0000")]
        public short? Ano { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCMask("99")]
        public short? Numero { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect("RegimesLetivosDataSource", AutoSelectSingleItem = true)]
        public long? SeqRegimeLetivo { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect("NiveisEnsinoDataSource", AutoSelectSingleItem = true)]
        [SMCInclude("NiveisEnsino")]
        [SMCMapProperty("NiveisEnsino.Seq")]
        public long? SeqNivel { get; set; }
    }
}