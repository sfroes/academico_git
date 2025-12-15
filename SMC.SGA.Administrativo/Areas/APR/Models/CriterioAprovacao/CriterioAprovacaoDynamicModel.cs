using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class CriterioAprovacaoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEscalaApuracaoService), "BuscarEscalaApuracaoFinalSelect")]
        public List<SMCDatasourceItem> EscalasApuracao { get; set; }

        #endregion [ DataSources ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)] //Tamanho para linha inteira: 13
        [SMCRequired]
        [SMCMaxLength(255)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)] //Tamanho para linha inteira: 5
        [SMCRequired]
        public bool ApuracaoFrequencia { get; set; }

        [SMCConditionalReadonly("ApuracaoFrequencia", SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired("ApuracaoFrequencia", SMCConditionalOperation.Equals, true)]
        [SMCMask("999")]
        [SMCMaxValue(100)]
        [SMCMinValue(0)]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)] //Tamanho para linha inteira: 6
        public short? PercentualFrequenciaAprovado { get; set; }

        [SMCConditionalReadonly("ApuracaoFrequencia", SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired("ApuracaoFrequencia", SMCConditionalOperation.Equals, true)]
        [SMCOrder(3)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public TipoArredondamento? TipoArredondamento { get; set; }

        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)] //Tamanho para linha inteira: 4
        [SMCRequired]
        public bool ApuracaoNota { get; set; }

        [SMCConditionalReadonly("ApuracaoNota", SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired("ApuracaoNota", SMCConditionalOperation.Equals, true)]
        [SMCMask("9999")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid2_24)] //Tamanho para linha inteira: 3
        public short? NotaMaxima { get; set; }

        [SMCConditionalReadonly("ApuracaoNota", SMCConditionalOperation.NotEqual, true)]
        [SMCConditionalRequired("ApuracaoNota", SMCConditionalOperation.Equals, true)]
        [SMCMask("999")]
        [SMCMaxValue(100)]
        [SMCMinValue(0)]
        [SMCOrder(6)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)] //Tamanho para linha inteira: 6
        public short? PercentualNotaAprovado { get; set; }

        [SMCConditionalRequired("ApuracaoNota", SMCConditionalOperation.Equals, false)]
        [SMCOrder(7)]
        [SMCSelect("EscalasApuracao")]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)] //Tamanho para linha inteira: 11
        [SMCHidden(SMCViewMode.List)]
        public long? SeqEscalaApuracao { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_APR_001_04_02.MANTER_CRITERIO_APROVACAO,
                           tokenEdit: UC_APR_001_04_02.MANTER_CRITERIO_APROVACAO,
                           tokenRemove: UC_APR_001_04_02.MANTER_CRITERIO_APROVACAO,
                           tokenList: UC_APR_001_04_01.PESQUISAR_CRITERIO_APROVACAO)
                   .DisableInitialListing(true)
                   .Service<ICriterioAprovacaoService>(save: "SalvarCriterioAprovacao");
        }

        #endregion [ Configuração ]
    }
}