using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoCurricularDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCIgnoreProp]
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> NiveisEnsinos { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("TipoDivisaoCurricular", storageType: SMCStorageType.TempData)]
        [SMCServiceReference(typeof(ITipoDivisaoCurricularService), nameof(ITipoDivisaoCurricularService.BuscarTiposDivisaoCurricularNivelEnsinoSelect), values: new string[1] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> TipoDivisaoCurricularDataSource { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("RegimeLetivo")]
        [SMCServiceReference(typeof(IRegimeLetivoService), nameof(IRegimeLetivoService.BuscarRegimesLetivoPorNivelEnsinoSelect), values: new string[1] { nameof(SeqNivelEnsino) })]
        public List<SMCDatasourceItem> RegimeLetivoDataSource { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCDescription]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsinos), NameDescriptionField = nameof(DescricaoNivelEnsino))]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqNivelEnsino { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("NivelEnsino")]
        [SMCMapProperty("NivelEnsino.Descricao")]
        [SMCOrder(1)]
        [SMCSortable(true, true, "NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(nameof(TipoDivisaoCurricularDataSource), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoTipoDivisaoCurricular))]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCDependency(nameof(SeqNivelEnsino), "BuscarTiposDivisaoCurricularNivelEnsinoSelect", "TipoDivisaoCurricular", "CUR", true)]
        public long SeqTipoDivisaoCurricular { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoDivisaoCurricular")]
        [SMCMapProperty("TipoDivisaoCurricular.Descricao")]
        [SMCOrder(3)]
        public string DescricaoTipoDivisaoCurricular { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect(nameof(RegimeLetivoDataSource), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoRegimeLetivo))]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCDependency(nameof(SeqNivelEnsino), "BuscarRegimesLetivoPorNivelEnsinoSelect", "RegimeLetivo", "CAM", true)]
        public long SeqRegimeLetivo { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCOrder(4)]
        [SMCInclude("RegimeLetivo")]
        [SMCMapProperty("RegimeLetivo.Descricao")]
        public string DescricaoRegimeLetivo { get; set; }

        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(min: 1, DisplayCounter = true, CounterProperty = nameof(DivisaoCurricularItemViewModel.Numero))]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        public SMCMasterDetailList<DivisaoCurricularItemViewModel> Itens { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IDivisaoCurricularService>(save: nameof(IDivisaoCurricularService.SalvarDivisaoCurricular),
                                                       edit: nameof(IDivisaoCurricularService.BuscarDivisaoCurricular))
                   .Tokens(tokenInsert: UC_CUR_001_02_02.MANTER_DIVISAO_CURRICULAR,
                           tokenEdit: UC_CUR_001_02_02.MANTER_DIVISAO_CURRICULAR,
                           tokenList: UC_CUR_001_02_01.PESQUISAR_DIVISAO_CURRICULAR,
                           tokenRemove: UC_CUR_001_02_02.MANTER_DIVISAO_CURRICULAR);
        }

        #endregion [ Configurações ]
    }
}