using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IRegimeLetivoService), nameof(IRegimeLetivoService.BuscarRegimesLetivosInstituicaoSelect))]
        public List<SMCDatasourceItem> RegimesLetivosDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelRegimeLetivoService), nameof(IInstituicaoNivelRegimeLetivoService.BuscarNiveisEnsinoDoRegimeSelect),
            values: new string[] { nameof(SeqRegimeLetivo) })]
        public List<SMCDatasourceItem> NiveisEnsinoDataSource { get; set; }

        #endregion [ DataSources ]

        #region [ SMCHiden ]

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #endregion [ SMCHiden ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSortable]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCSelect(nameof(RegimesLetivosDataSource), AutoSelectSingleItem = true)]
        [SMCSortable(true, true, "RegimeLetivo.Descricao")] //TODO: Ordem 2
        public long SeqRegimeLetivo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMask("0000")]
        [SMCSortable(true, true)] //TODO: Ordem 0
        public short? Ano { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMask("99")]
        [SMCSortable(true, true)] //TODO: Ordem 1
        public short? Numero { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCInclude(nameof(NiveisEnsino))]
        public SMCMasterDetailList<CicloLetivoNivelEnsinoViewModel> NiveisEnsino { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CAM_002_01_02.MANTER_CICLO_LETIVO,
                           tokenInsert: UC_CAM_002_01_02.MANTER_CICLO_LETIVO,
                           tokenRemove: UC_CAM_002_01_02.MANTER_CICLO_LETIVO,
                           tokenList: UC_CAM_002_01_01.PESQUISAR_CICLO_LETIVO)
                   .EditInModal()
                   .Grid(allowSelect: true)
                   .DisableInitialListing(true)
                   .ModalSize(SMCModalWindowSize.Auto, SMCModalWindowSize.Auto, SMCModalWindowSize.Large)
                   .Button("ParametrizarTipoEventoLetivo", "Index", "CicloLetivoTipoEvento",
                           UC_CAM_002_01_02.MANTER_CICLO_LETIVO,
                           i => new { seqCicloLetivo = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })
                   .HeaderIndexList(nameof(CicloLetivoController.CopiaCicloLetivo))
                   .Service<ICicloLetivoService>(save: nameof(ICicloLetivoService.SalvarCicloLetivo));
        }

        #endregion [ Configuração ]
    }
}