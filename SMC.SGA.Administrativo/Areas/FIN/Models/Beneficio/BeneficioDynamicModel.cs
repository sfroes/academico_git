using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.FIN.Lookups;
using SMC.Academico.UI.Mvc.Areas.FIN.Lookups.LK_FIN_001_Pessoa_Juridica;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Views.Beneficio.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class BeneficioDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IBeneficioService), nameof(IBeneficioService.BuscarTipoBeneficioSelect))]
        public List<SMCDatasourceItem> TiposBeneficioDataSource { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IBeneficioService), nameof(IBeneficioService.BuscarBeneficiosGRASelect), values: new[] { nameof(SeqTipoBeneficio) })]
        public List<SMCDatasourceItem> BeneficiosGRADataSource { get; set; }

        #endregion DataSource

        [SMCKey]
        [SMCOrder(1)]
        [SMCSortable(true)]
        [SMCReadOnly(SMCViewMode.All)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCOrder(2)]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid13_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid8_24)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect(nameof(TiposBeneficioDataSource), autoSelectSingleItem: true)]
        public long SeqTipoBeneficio { get; set; }

        [SMCOrder(4)]
        [SMCInclude("TipoBeneficio")]
        [SMCMapProperty("TipoBeneficio.Descricao")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSortable(true, true, "TipoBeneficio.Descricao")]
        public string DescricaoTipoBeneficio { get; set; }

        [SMCRequired]
        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        public bool BeneficioIntercambio { get; set; }

        [SMCRequired]
        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        public bool ConcessaoAteFinalCurso { get; set; }

        [SMCRequired]
        [SMCOrder(7)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        public bool DeducaoValorParcelaTitular { get; set; }

        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid6_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCSelect(nameof(BeneficiosGRADataSource), autoSelectSingleItem: true)]
        [SMCConditionalRequired(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, true)]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, false)]
        [SMCDependency(nameof(SeqTipoBeneficio), nameof(ConfiguracaoBeneficioController.BuscarBeneficiosGRASelect), "ConfiguracaoBeneficio", true)]
        public int? SeqBeneficioFinanceiro { get; set; }

        [SMCRequired]
        [SMCOrder(9)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        public AssociarResponsavelFinanceiro AssociarResponsavelFinanceiro { get; set; }

        [SMCConditionalReadonly(nameof(AssociarResponsavelFinanceiro), SMCConditionalOperation.Equals, AssociarResponsavelFinanceiro.NaoPermite)]
        [SMCConditionalRequired(nameof(AssociarResponsavelFinanceiro), SMCConditionalOperation.NotEqual, AssociarResponsavelFinanceiro.NaoPermite)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(10)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        public TipoResponsavelFinanceiro? TipoResponsavelFinanceiro { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(11)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid6_24)]
        public bool IncluirDesbloqueioTemporario { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(12)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid6_24)]
        public bool RecebeCobranca { get; set; }

        [SMCConditionalReadonly(nameof(RecebeCobranca), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(RecebeCobranca), SMCConditionalOperation.Equals, false)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMaxLength(1000)]
        [SMCOrder(13)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        public string JustificativaNaoRecebeCobranca { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(14)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public bool ExigeCodigoPessoaAssociado { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(15)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public bool ExigeIdentificacaoParentesco { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCOrder(16)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public bool ExigeControleConcessaoBolsa { get; set; }

        [SMCConditionalReadonly(nameof(AssociarResponsavelFinanceiro), SMCConditionalOperation.NotEqual, AssociarResponsavelFinanceiro.Exige)]
        [SMCOrder(17)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCIgnoreProp(SMCViewMode.List)]
        [PessoaJuridicaLookup]
        public List<PessoaJuridicaLookupViewModel> ResponsaveisFinanceiros { get; set; }

        [SMCHidden]
        public bool ExibirMensagem
        {
            get { return this.Seq > 0 && !this.DeducaoValorParcelaTitular; }
        }

        [SMCDisplay]
        [SMCOrder(18)]
        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, true, RuleName = "R1")]
        [SMCConditionalDisplay(nameof(ExibirMensagem), SMCConditionalOperation.Equals, true, RuleName = "R2")]
        [SMCConditionalRule("R1 && R2")]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.List)]
        public string MensagemInformativa { get; set; } = UIResource.Texto_MensagemInformativa;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Tokens(tokenInsert: UC_FIN_001_02_02.MANTER_BENEFICIO,
                           tokenEdit: UC_FIN_001_02_02.MANTER_BENEFICIO,
                           tokenRemove: UC_FIN_001_02_02.MANTER_BENEFICIO,
                           tokenList: UC_FIN_001_02_01.PESQUISAR_BENEFICIO)
            .Service<IBeneficioService>(save: nameof(IBeneficioService.SalvarBeneficio));
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
            {
                this.RecebeCobranca = true;
            }

        }
    }
}