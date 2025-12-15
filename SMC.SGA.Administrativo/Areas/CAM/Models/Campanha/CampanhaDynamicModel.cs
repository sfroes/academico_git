using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCSelectListItem> UnidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(UnidadesResponsaveis))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqEntidadeResponsavel { get; set; }

        [CicloLetivoLookup]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public List<CicloLetivoLookupViewModel> CiclosLetivos { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CAM_001_01_02.MANTER_CAMPANHA,
                           tokenInsert: UC_CAM_001_01_02.MANTER_CAMPANHA,
                           tokenRemove: UC_CAM_001_01_02.MANTER_CAMPANHA,
                           tokenList: UC_CAM_001_01_01.PESQUISAR_CAMPANHA)
                   .DisableInitialListing(true)
                   .Button("OfertasCampanha", nameof(CampanhaOfertaController.Index), "CampanhaOferta",
                            (model) => new { seqCampanha = SMCDESCrypto.EncryptNumberForURL((model as CampanhaListarDynamicModel).Seq) })
                   .Button("ProcessoSeletivo", "Index", "ProcessoSeletivo",
                            (model) => new { seqCampanha = SMCDESCrypto.EncryptNumberForURL((model as CampanhaListarDynamicModel).Seq) })
                   .Button("CopiarCampanha", nameof(CampanhaCopiaController.CopiarCampanha), "CampanhaCopia",
                            (model) => new { seqCampanha = SMCDESCrypto.EncryptNumberForURL((model as CampanhaListarDynamicModel).Seq) })
                   .Service<ICampanhaService>(index: nameof(ICampanhaService.BuscarCampanhas),
                                              edit: nameof(ICampanhaService.BuscarCampanha),
                                              save: nameof(ICampanhaService.SalvarCampanha));
        }

        #endregion [ Configuração ]
    }
}