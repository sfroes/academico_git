using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "configuracaoDuracaoAula", Size = SMCSize.Grid12_24,  CssClass = "smc-sga-fieldset-agrupamentos-internos smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-size-lg-12")]
    [SMCGroupedPropertyConfiguration(GroupId = "PrazoFrequencia", Size = SMCSize.Grid12_24, CssClass = "smc-sga-fieldset-agrupamentos-internos smc-size-md-24 smc-size-xs-24 smc-size-sm-24 smc-size-lg-12")]
    public class InstituicaoNivelDynamicModel : SMCDynamicViewModel
    {
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCKey]
        [SMCOrder(0)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCOrder(1)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [NivelEnsinoLookup]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid21_24)]
        public NivelEnsinoLookupViewModel NivelEnsino { get; set; }

        /// <summary>
        /// Quando o Lookup for uma classe então deve criar uma outra propriedade para representar o Seq.
        /// Exemplo: NivelEnsino -> SeqNivelEnsino
        /// Isso para que o Dynamic consiga mapear corretamente a classe de Dominio
        /// </summary>
        [SMCIgnoreMetadata]
        public long? SeqNivelEnsino
        {
            get { return NivelEnsino?.Seq; }
        }

        [SMCOrder(1)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCSortable(true, true, "NivelEnsino.Descricao")]
        [SMCDescription]
        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        [SMCRequired]
        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool OfertaTemporal { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(3)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        [SMCRequired]
        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool VerificaComponenteAptoLecionar { get; set; }

        [SMCRequired]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(5)]
        [SMCMinValue(0)]
        [SMCMaxValue(100)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public int PercentualEquivalencia { get; set; }

        [SMCRequired]
        [SMCOrder(6)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public bool PermiteCreditoComponenteCurricular { get; set; }

        [SMCGroupedProperty("PrazoFrequencia")]
        [SMCRequired]
        [SMCOrder(7)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public short QuantidadeDiasPrazoApuracaoFrequencia { get; set; }


        [SMCGroupedProperty("PrazoFrequencia")]
        [SMCRequired]
        [SMCOrder(8)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        public TimeSpan? MinutosPrazoAlteracaoFrequencia { get; set; }

        [SMCGroupedProperty("configuracaoDuracaoAula")]
        [SMCOrder(9)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCRequired]
        public TimeSpan? MinutosMinimoAula { get; set; }

        [SMCGroupedProperty("configuracaoDuracaoAula")]
        [SMCOrder(10)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCRequired]
        public TimeSpan? MinutosMaximoAula { get; set; }

        [SMCIgnoreProp]
        public short QuantidadeMinutosPrazoAlteracaoFrequencia
        {
            get
            {
                if (MinutosPrazoAlteracaoFrequencia.HasValue)
                    return (short)(MinutosPrazoAlteracaoFrequencia.Value.Hours * 60 + MinutosPrazoAlteracaoFrequencia.Value.Minutes);
                return 0;
            }
            set
            { MinutosPrazoAlteracaoFrequencia = TimeSpan.FromMinutes(value); }
        }

        [SMCIgnoreProp]
        public short QuantidadeMinutosMinimoAula
        {
            get
            {
                if (MinutosMinimoAula.HasValue)
                    return (short)(MinutosMinimoAula.Value.Hours * 60 + MinutosMinimoAula.Value.Minutes);
                return 0;
            }

            set
            {
                MinutosMinimoAula = TimeSpan.FromMinutes(value);
            }
        }

        [SMCIgnoreProp]
        public short QuantidadeMinutosMaximoAula
        {
            get
            {
                if (MinutosMaximoAula.HasValue)
                    return (short)(MinutosMaximoAula.Value.Hours * 60 + MinutosMaximoAula.Value.Minutes);
                return 0;
            }

            set
            {
                MinutosMaximoAula = TimeSpan.FromMinutes(value);
            }
        }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .ModalSize(SMCModalWindowSize.Auto)
                   .IgnoreFilterGeneration()
                   .RegisterControls(RegisterHelperControls.Lookup, RegisterHelperControls.Fields, RegisterHelperControls.DataSelector)
                   .Tokens(tokenInsert: UC_ORG_002_01_02.MANTER_ASSOCIACAO_NIVEL_ENSINO,
                           tokenEdit: UC_ORG_002_01_02.MANTER_ASSOCIACAO_NIVEL_ENSINO,
                           tokenRemove: UC_ORG_002_01_02.MANTER_ASSOCIACAO_NIVEL_ENSINO,
                           tokenList: UC_ORG_002_01_01.PESQUISAR_PARAMETROS_INSTITUICAO_NIVEL_ENSINO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                this.VerificaComponenteAptoLecionar = true;
        }
    }
}