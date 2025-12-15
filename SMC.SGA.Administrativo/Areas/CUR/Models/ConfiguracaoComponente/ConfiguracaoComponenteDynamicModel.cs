using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSource ]

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(ITipoDivisaoComponenteService), nameof(ITipoDivisaoComponenteService.BuscarTipoDivisaoComponentePorComponenteSelect))]
        public List<SMCDatasourceItem> TiposDivisao { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IConfiguracaoComponenteService), nameof(IConfiguracaoComponenteService.BuscarComponenteOrganizacoesSelect))]
        public List<SMCDatasourceItem> ComponenteOrganizacoes { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IComponenteCurricularService), nameof(IComponenteCurricularService.BuscarOrganizacoesComponenteCurricularSelect), values: new string[] { nameof(SeqComponenteCurricular) })]
        public List<SMCDatasourceItem> ComponentesCurricularesOrganizacoes { get; set; }

        #endregion [ DataSource ]

        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCOrder(0)]
        [SMCRequired]
        public long SeqComponenteCurricular { get; set; }

        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        public string Codigo { get; set; }

        [SMCOrder(2)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid8_24)]
        public string DescricaoComplementar { get; set; }

        [SMCOrder(3)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public bool Ativo { get; set; }

        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid8_24)]
        public bool PermiteAlunoSemNota { get; set; }

        [SMCDetail(SMCDetailType.Modal, min: 1, windowSize: SMCModalWindowSize.Large)]
        [SMCMapForceFromTo]
        [SMCOrder(5)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ConfiguracaoComponenteDivisaoViewModel> DivisoesComponente { get; set; }

        [SMCHidden]
        public bool ExibirItensOrganizacao { get; set; }
        
        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<ConfiguracaoComponenteListarDynamicModel>("_DetailList")
                   .ButtonBackIndex("Index", "ComponenteCurricular")
                   .HeaderIndex("CabecalhoComponenteCurricular")
                   .Header("CabecalhoComponenteCurricular")
                   .IgnoreFilterGeneration()
                   .Assert("_Confirmacao",
                        assert =>
                        {
                            // Conta apenas as remoções de itens gravados anteriormente
                            return (assert as ConfiguracaoComponenteDynamicModel)
                                .DivisoesComponente
                                .RemovedItems
                                .OfType<ConfiguracaoComponenteDivisaoViewModel>()
                                .Where(w => w.Seq != 0).SMCCount() > 0;
                        },
                        viewModel =>
                           {
                               // Considera apenas os itens gravados anteriormente
                               var configuracao = (ConfiguracaoComponenteDynamicModel)viewModel;
                               return new ConfiguracaoComponenteConfirmacaoViewModel()
                               {
                                   DivisoesExcluidas = configuracao
                                       .DivisoesComponente
                                       .RemovedItems
                                       .OfType<ConfiguracaoComponenteDivisaoViewModel>()
                                       .Where(w => w.Seq != 0)
                               };
                           })
                   .Service<IConfiguracaoComponenteService>(
                        index: nameof(IConfiguracaoComponenteService.BuscarConfiguracoesComponentes),
                       insert: nameof(IConfiguracaoComponenteService.BuscarConfiguracaoComponenteCurricular),
                         edit: nameof(IConfiguracaoComponenteService.BuscarConfiguracaoComponente),
                         save: nameof(IConfiguracaoComponenteService.SalvarConfiguracaoComponente))
                   .Tokens(
                        tokenList: UC_CUR_002_01_03.PESQUISAR_CONFIGURACAO_COMPONENTE_CURRICULAR,
                      tokenInsert: UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR,
                        tokenEdit: UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR,
                      tokenRemove: UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR);
        }

        #endregion [ Configurações ]
    }
}