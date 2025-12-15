using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelSistemaOrigemFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelSistemaOrigemService), nameof(IInstituicaoNivelSistemaOrigemService.BuscarSistemaOrigemGADSelect))]
        public List<SMCDatasourceItem<string>> SistemaOrigemGAD { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }
        #endregion
                
        [SMCSelect(nameof(InstituicaoNiveisEnsino))]
        [SMCSize(SMCSize.Grid7_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCSelect(nameof(SistemaOrigemGAD))]
        [SMCSize(SMCSize.Grid6_24)]
        public string TokenSistemaOrigemGAD { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public UsoSistemaOrigem? UsoSistemaOrigem { get; set; }
    }
}