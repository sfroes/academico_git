using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.FIN.Models.InstituicaoNivelBeneficio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class BeneficioHistoricoValorAuxilioDynamicModel : SMCDynamicViewModel
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
        public long SeqInstituicaoNivelBeneficio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCMinValue(0)]
        [SMCCurrency(AllowZero = false)]
        public decimal? ValorAuxilio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable(true, true, sort: SMCSortDirection.Descending)]
        public DateTime DataInicioValidade { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCMinDate(nameof(DataInicioValidade))]
        public DateTime? DataFimValidade { get; set; }

        [SMCIgnoreProp]
        public bool FlagUltimoValorAuxilio { get; set; }

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
            options.HeaderIndexPartial("_CabecalhoHIstoricoValorAuxilio")
                   .EditInModal()
                   .ButtonBackIndex("Index", "InstituicaoNivelBeneficio")
                   .IgnoreFilterGeneration()
                   .ConfigureButton((button, model, action) =>
                   {
                       if (action == SMCDynamicButtonAction.Remove || action == SMCDynamicButtonAction.Edit)
                       {
                           var modelo = (BeneficioHistoricoValorAuxilioDynamicModel)model;
                           button.Hide(!modelo.FlagUltimoValorAuxilio);
                       }
                       else
                       {
                           button.Hide(false);
                       }
                   })
                  .Tokens(tokenInsert: UC_FIN_003_01_04.MANTER_VALOR_AUXILIO_BENEFICIO,
                          tokenEdit: UC_FIN_003_01_04.MANTER_VALOR_AUXILIO_BENEFICIO,
                          tokenRemove: UC_FIN_003_01_04.MANTER_VALOR_AUXILIO_BENEFICIO,
                          tokenList: UC_FIN_003_01_03.PESQUISAR_VALOR_AUXILIO_BENEFICIO)
                  .Service<IBeneficioHistoticoValorAuxilioService>(index: nameof(IBeneficioHistoticoValorAuxilioService.BuscarDadosValoresAuxilio),
                                                                   insert: nameof(IBeneficioHistoticoValorAuxilioService.BuscarDadosValorAuxilio),
                                                                   save: nameof(IBeneficioHistoticoValorAuxilioService.SalvarBeneficioHistoricoValorAuxilio));
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new InstituicaoNivelBeneficioNavigationGroup(this);
        }

        #endregion Configurações
    }
}