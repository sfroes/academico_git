using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.SRC.Views.TipoServico.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoServicoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoServicoService), nameof(ITipoServicoService.BuscarClassesTemplateProcessoSgfSelect))]
        public List<SMCDatasourceItem> ClassesTemplateProcessoSgfDataSource { get; set; }

        #endregion [ DataSources ]

        [SMCOrder(0)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.All)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid15_24)]
        [SMCRequired]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid7_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public bool? ExigeEscalonamento { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid9_24)]
        [SMCSelect(nameof(ClassesTemplateProcessoSgfDataSource), AutoSelectSingleItem = true)]
        public long SeqClasseTemplateProcessoSgf { get; set; } 

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((TipoServicoListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_SRC_001_02_01.MANTER_TIPO_SERVICO,
                           tokenInsert: UC_SRC_001_02_01.MANTER_TIPO_SERVICO,
                           tokenEdit: UC_SRC_001_02_01.MANTER_TIPO_SERVICO,
                           tokenRemove: UC_SRC_001_02_01.MANTER_TIPO_SERVICO);
        }

        #endregion
    }
}