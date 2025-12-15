using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.FIN.Views.ConfiguracaoBeneficio.App_LocalResources;
using SMC.SGA.Administrativo.Areas.FIN.Models.InstituicaoNivelBeneficio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ConfiguracaoBeneficioDynamicModel : SMCDynamicViewModel
    {

        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCInclude(Ignore = true)]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource(dataSource: "Beneficio")]
        [SMCIgnoreProp]
        [SMCInclude(Ignore = true)]
        [SMCServiceReference(typeof(IFINDynamicService))]
        public List<SMCDatasourceItem> Beneficios { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqInstituicaoNivelBeneficio { get; set; }

        [SMCConditionalReadonly(nameof(AssociacaoPessoaBeneficio), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public TipoDeducao TipoDeducao { get; set; }

        //[SMCConditionalRequired(nameof(TipoDeducao), SMCConditionalOperation.NotEqual, TipoDeducao.Variavel)]
        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public FormaDeducao? FormaDeducao { get; set; }

        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public decimal? ValorDeducao
        {
            get { return this.ValorDeducaoMoeda ?? this.ValorDeducaoPercentual; }
            set
            {
                if (this.FormaDeducao == SMC.Academico.Common.Areas.FIN.Enums.FormaDeducao.PercentualBolsa)
                {
                    this.ValorDeducaoPercentual = value;
                }
                else
                {
                    this.ValorDeducaoMoeda = value;
                }
            }
        }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCMaxValue(100)]
        [SMCCurrency(AllowZero = false)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public decimal? ValorDeducaoPercentual { get; set; }

        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCCurrency(AllowZero = false)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public decimal? ValorDeducaoMoeda { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCSortable(true, true, sort: SMCSortDirection.Descending)]
        public DateTime DataInicioValidade { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCMinDate(nameof(DataInicioValidade))]
        public DateTime? DataFimValidade { get; set; }

        [SMCIgnoreProp]
        public bool FlagUltimaConfiguracao { get; set; }

        [SMCHidden]
        [SMCIgnoreProp(SMCViewMode.Insert)]
        public bool AssociacaoPessoaBeneficio { get; set; }

        [SMCHidden]
        public DateTime DataBanco { get; set; }

        [SMCDisplay]
        [SMCConditionalDisplay(nameof(AssociacaoPessoaBeneficio), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.List)]
        public string MensagemInformativa { get; set; } = UIResource.Texto_MensagemInformativa;

        #region Campos de Cabeçalho

        [SMCIgnoreProp]
        [SMCSelect(nameof(InstituicaoNiveis))]
        public long SeqInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCSelect(nameof(Beneficios))]
        public long SeqBeneficio { get; set; }

        #endregion Campos de Cabeçalho

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal(refreshIndexPageOnSubmit: true)
                   .ViewPartialEdit("_EditarConfiguracaoBeneficio")
                   .ViewPartialInsert("_EditarConfiguracaoBeneficio")
                   .HeaderIndexPartial("_CabecalhoConfiguracaoBeneficio")
                   .ButtonBackIndex("Index", "InstituicaoNivelBeneficio")
                   .IgnoreFilterGeneration()
                   .ConfigureButton((button, model, action) =>
                   {
                       if (action == SMCDynamicButtonAction.Remove || action == SMCDynamicButtonAction.Edit)
                       {
                           var modelo = (ConfiguracaoBeneficioDynamicModel)model;
                           button.Hide(!modelo.FlagUltimaConfiguracao);
                       }
                       else
                       {
                           button.Hide(false);
                       }
                   })
                  .Service<IConfiguracaoBeneficioService>(index: nameof(IConfiguracaoBeneficioService.BuscarConfiguracoesBeneficios),
                                                          insert: nameof(IConfiguracaoBeneficioService.BuscarDadosConfiguracaoBeneficio),
                                                          save: nameof(IConfiguracaoBeneficioService.SalvarConfiguracaoBeneficio),
                                                          delete: nameof(IConfiguracaoBeneficioService.ExcluirConfiguracaoBeneficio),
                                                          edit: nameof(IConfiguracaoBeneficioService.AlterarConfiguracoesBeneficios))
                  .Assert("MSG_Alteracao_Data", x =>
                  {
                      var model = (x as ConfiguracaoBeneficioDynamicModel);
                      return model.DataFimValidade != model.DataBanco && model.Seq > 0 && model.AssociacaoPessoaBeneficio;
                  })
                  .Tokens(tokenInsert: UC_FIN_003_01_06.MANTER_CONFIGURACAO_BENEFICIO,
                          tokenEdit: UC_FIN_003_01_06.MANTER_CONFIGURACAO_BENEFICIO,
                          tokenRemove: UC_FIN_003_01_06.MANTER_CONFIGURACAO_BENEFICIO,
                          tokenList: UC_FIN_003_01_05.PESQUISAR_CONFIGURACAO_BENEFICIO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new InstituicaoNivelBeneficioNavigationGroup(this);
        }

        #endregion Configurações
    }
}