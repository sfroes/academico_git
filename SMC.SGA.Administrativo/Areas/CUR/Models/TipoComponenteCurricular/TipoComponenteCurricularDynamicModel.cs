using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoComponenteCurricularDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCDataSource("Modalidade")]
        [SMCInclude(ignore: true)]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoOrientacaoService), nameof(ITipoOrientacaoService.BuscarTipoOrientacaoSelect))]
        public List<SMCDatasourceItem> TiposOrientacao { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSortable(true)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSortable(true, true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Filter)]        
        [SMCOrder(2)]
        [SMCRequired]
        [SMCMaxLength(100)]        
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        public string DescricaoXSD { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCMaxLength(5)]
        [SMCRegularExpression(REGEX.SIGLA)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid2_24, SMCSize.Grid2_24)]
        public string Sigla { get; set; }

        [SMCDetail(SMCDetailType.Block, min: 1)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCMapForceFromTo]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<TipoDivisaoComponenteViewModel> TiposDivisao { get; set; }

        [SMCHidden]
        [SMCIgnoreMetadata]
        public TipoAtuacao TipoAtuacao { get; set; } = TipoAtuacao.Aluno;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<TipoComponenteCurricularListarDynamicModel>("_DetailList")
                   .Tokens(tokenInsert: UC_CUR_002_02_02.MANTER_TIPO_COMPONENTE_CURRICULAR,
                           tokenEdit: UC_CUR_002_02_02.MANTER_TIPO_COMPONENTE_CURRICULAR,
                           tokenRemove: UC_CUR_002_02_02.MANTER_TIPO_COMPONENTE_CURRICULAR,
                           tokenList: UC_CUR_002_02_01.PESQUISAR_TIPO_COMPONENTE_CURRICULAR);
        }
    }
}