using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoOfertaDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoFormacaoEspecificaService), nameof(ITipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaSelect))]
        public List<SMCDatasourceItem> TiposFormacaoEspecificaDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPISelect))]
        public List<SMCDatasourceItem> UnidadeResponsavelDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoItemHierarquiaOfertaService), nameof(ITipoItemHierarquiaOfertaService.BuscarTiposItemHierarquiaOfertaSelect))]
        public List<SMCDatasourceItem> TiposItemHierarquiaOfertaDataSource { get; set; }

        #endregion [ DataSources ]

        #region [ SMCHiden ]

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #endregion [ SMCHiden ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSortable]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid12_24,SMCSize.Grid18_24, SMCSize.Grid20_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24,SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public bool ExigeCursoOfertaLocalidadeTurno { get; set; }

        [SMCSize(SMCSize.Grid7_24,SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCSortable(true, true)]
        [SMCSelect(nameof(TiposFormacaoEspecificaDataSource), AutoSelectSingleItem = true)]
        public long? SeqTipoFormacaoEspecifica { get; set; }

        [SMCSize(SMCSize.Grid10_24,SMCSize.Grid12_24, SMCSize.Grid16_24, SMCSize.Grid8_24)]
        [SMCSortable(true, true)]
        [SMCSelect(nameof(TiposItemHierarquiaOfertaDataSource), AutoSelectSingleItem = true)]
        public long? SeqTipoItemHierarquiaOfertaGpi { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid12_24, SMCSize.Grid8_24,SMCSize.Grid8_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCInclude(nameof(UnidadesResponsaveis))]
        public SMCMasterDetailList<TipoOfertaUnidadeResponsavelViewModel> UnidadesResponsaveis { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CAM_001_06_02.MANTER_TIPO_OFERTA,
                           tokenInsert: UC_CAM_001_06_02.MANTER_TIPO_OFERTA,
                           tokenRemove: UC_CAM_001_06_02.MANTER_TIPO_OFERTA,
                           tokenList: UC_CAM_001_06_01.PESQUISAR_TIPO_OFERTA)
                   .Service<ITipoOfertaService>(save: nameof(ITipoOfertaService.SalvarTipoOferta));
        }

        #endregion [ Configuração ]
    }
}