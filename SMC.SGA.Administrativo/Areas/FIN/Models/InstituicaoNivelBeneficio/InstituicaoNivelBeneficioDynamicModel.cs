using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using System;
using System.Collections.Generic;
using SMC.SGA.Administrativo.Areas.FIN.Views.InstituicaoNivelBeneficio.App_LocalResources;
using SMC.Framework.Security;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Framework.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Configuracao", Size = SMCSize.Grid24_24)]
    public class InstituicaoNivelBeneficioDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource(dataSource: "Beneficio")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IFINDynamicService))]
        public List<SMCDatasourceItem> Beneficios { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        [SMCHidden]
        [SMCOrder(0)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect(nameof(Beneficios), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public long SeqBeneficio { get; set; }

        [SMCHidden]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCOrder(3)]
        public bool DeducaoValorParcelaTitular { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(6)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public bool ObrigatorioAdesaoContrato { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid17_24)]
        public bool ObrigatorioCondicaoPagamento { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCConditionalRequired(nameof(ObrigatorioCondicaoPagamento), SMCConditionalOperation.Equals, "False")]
        [SMCMask("99999")]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        public short? NumeroParcelasPadraoCondicaoPagamento { get; set; }

        /// <summary>
        /// Removido por mudança na regra de negocio
        /// </summary>
        #region Agrupamento Configuração Beneficio

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(7)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoDeducao TipoDeducao { get; set; }

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(8)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public FormaDeducao FormaDeducao { get; set; }

        [SMCIgnoreProp]
        public decimal? ValorDeducao
        {
            get { return this.ValorDeducaoMoeda ?? this.ValorDeducaoPercentual; }
            set
            {
                if (this.FormaDeducao == FormaDeducao.PercentualBolsa)
                {
                    this.ValorDeducaoPercentual = value;
                }
                else
                {
                    this.ValorDeducaoMoeda = value;
                }
            }
        }

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True", RuleName = "RP1")]
        [SMCConditionalDisplay(nameof(FormaDeducao), SMCConditionalOperation.Equals, FormaDeducao.PercentualBolsa, RuleName = "RP2")]
        [SMCConditionalRule("RP1 && RP2")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCConditionalRequired(nameof(TipoDeducao), SMCConditionalOperation.Equals, TipoDeducao.Fixo)]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(9)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCMaxValue(100)]
        [SMCCurrency]
        [SMCSize(SMCSize.Grid8_24)]
        public decimal? ValorDeducaoPercentual { get; set; }

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True", RuleName = "RM1")]
        [SMCConditionalDisplay(nameof(FormaDeducao), SMCConditionalOperation.NotEqual, FormaDeducao.PercentualBolsa, RuleName = "RM2")]
        [SMCConditionalRule("RM1 && RM2")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCConditionalRequired(nameof(TipoDeducao), SMCConditionalOperation.Equals, TipoDeducao.Fixo)]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(9)]
        [SMCMapForceFromTo]
        [SMCMinValue(0)]
        [SMCCurrency]
        [SMCSize(SMCSize.Grid8_24)]
        public decimal? ValorDeducaoMoeda { get; set; }

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCConditionalRequired(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime? DataInicioValidade { get; set; }

        [SMCConditionalDisplay(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "True")]
        [SMCConditionalReadonly(nameof(DeducaoValorParcelaTitular), SMCConditionalOperation.Equals, "False")]
        [SMCIgnoreProp(SMCViewMode.Edit)]
        [SMCGroupedProperty("Configuracao")]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter | SMCViewMode.Edit)]
        [SMCOrder(12)]
        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCMinDate(nameof(DataInicioValidade))]
        public DateTime? DataFimValidade { get; set; }

        #endregion Agrupamento Configuração Beneficio

        [SMCInclude("ConfiguracoesBeneficio")]
        [SMCIgnoreProp]
        public List<ConfiguracaoBeneficioDynamicModel> ConfiguracaoBeneficio { get; set; }

        [SMCInclude("BeneficiosHistoricosValoresAuxilio")]
        [SMCIgnoreProp]
        public List<BeneficioHistoricoValorAuxilioDynamicModel> BeneficioHistoricoValorAuxilio { get; set; }

        #region Campos para mensagem

        [SMCIgnoreProp]
        public string DescricaoBeneficio { get; set; }

        [SMCIgnoreProp]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion Campos para mensagem

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IInstituicaoNivelBeneficioService>(save: nameof(IInstituicaoNivelBeneficioService.SalvarInstituicaoNivelBeneficio),
                                                               index: nameof(IInstituicaoNivelBeneficioService.BuscarInstituicoesNiveisBeneficios),
                                                               delete: nameof(IInstituicaoNivelBeneficioService.ExcluirInstituicoesNiveisBeneficios))
                   .Tokens(tokenList: UC_FIN_003_01_01.PESQUISAR_BENEFICIO_INSTITUICAO_NIVEL,
                           tokenInsert: UC_FIN_003_01_02.MANTER_BENEFICIO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_FIN_003_01_02.MANTER_BENEFICIO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_FIN_003_01_02.MANTER_BENEFICIO_INSTITUICAO_NIVEL)
                   .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                             ((InstituicaoNivelBeneficioListarDynamicModel)x).DescricaoBeneficio,
                             ((InstituicaoNivelBeneficioListarDynamicModel)x).DescricaoInstituicaoNivel))
                  .Detail<InstituicaoNivelBeneficioListarDynamicModel>("_DetailList")
                  .DisableInitialListing(true)
                  .Button("BeneficioHistoricoValorAuxilio", "Index", "BeneficioHistoricoValorAuxilio",
                           UC_FIN_003_01_03.PESQUISAR_VALOR_AUXILIO_BENEFICIO,
                            i => new
                            {
                                seqInstituicaoNivelBeneficio = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                                seqInstituicaoNivel = SMCDESCrypto.EncryptNumberForURL(((InstituicaoNivelBeneficioListarDynamicModel)i).SeqInstituicaoNivel),
                                seqBeneficio = SMCDESCrypto.EncryptNumberForURL(((InstituicaoNivelBeneficioListarDynamicModel)i).SeqBeneficio)
                            })
                   .Button("ConfiguracaoBeneficio", nameof(ConfiguracaoBeneficioController.VerificarDeducaoBeneficio), "ConfiguracaoBeneficio",
                           UC_FIN_003_01_05.PESQUISAR_CONFIGURACAO_BENEFICIO,
                           i => new
                           {
                               seqInstituicaoNivelBeneficio = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq),
                               seqInstituicaoNivel = SMCDESCrypto.EncryptNumberForURL(((InstituicaoNivelBeneficioListarDynamicModel)i).SeqInstituicaoNivel),
                               seqBeneficio = SMCDESCrypto.EncryptNumberForURL(((InstituicaoNivelBeneficioListarDynamicModel)i).SeqBeneficio)
                           });

        }

        #endregion [ Configurações ]
    }
}