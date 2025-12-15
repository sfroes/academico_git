using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class TipoProcessoSeletivoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoOfertaService), nameof(ITipoOfertaService.BuscarTiposOfertaSelect))]
        public List<SMCDatasourceItem> TiposOfertaDataSource { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoProcessoService), nameof(ITipoProcessoService.BuscarTiposProcessoKeyValue))]
        public List<SMCDatasourceItem> TiposProcessoSeletivoGPIDataSource { get; set; }

        #endregion [ DataSources ]

        #region [ SMCHiden ]

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        #endregion [ SMCHiden ]

        [SMCKey]
        [SMCReadOnly]
        [SMCSortable]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24,SMCSize.Grid4_24,SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid12_24,SMCSize.Grid18_24,SMCSize.Grid12_24,SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24,SMCSize.Grid24_24,SMCSize.Grid8_24,SMCSize.Grid8_24)]
        public bool IngressoDireto { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24,SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public TipoCalculoDataAdmissao TipoCalculoDataAdmissao { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCInclude(nameof(TiposOferta))]
        public SMCMasterDetailList<TipoProcessoSeletivoTipoOfertaViewModel> TiposOferta { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCInclude(nameof(TiposProcessosGpi))]
        public SMCMasterDetailList<TipoProcessoSeletivoGpiViewModel> TiposProcessosGpi { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CAM_001_07_02.MANTER_TIPO_PROCESSO_SELETIVO,
                           tokenInsert: UC_CAM_001_07_02.MANTER_TIPO_PROCESSO_SELETIVO,
                           tokenRemove: UC_CAM_001_07_02.MANTER_TIPO_PROCESSO_SELETIVO,
                           tokenList: UC_CAM_001_07_01.PESQUISAR_TIPO_PROCESSO_SELETIVO);
        }

        #endregion [ Configuração ]
    }
}