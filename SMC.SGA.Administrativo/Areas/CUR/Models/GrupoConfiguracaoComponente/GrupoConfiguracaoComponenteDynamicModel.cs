using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoConfiguracaoComponenteDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public bool Ativo { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect(true)]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoGrupoConfiguracaoComponente TipoGrupoConfiguracaoComponente { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 2)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<GrupoConfiguracaoComponenteItemViewModel> Itens { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IGrupoConfiguracaoComponenteService>(index: nameof(IGrupoConfiguracaoComponenteService.BuscarGruposConfiguracoesComponentes),
                                                                 save: nameof(IGrupoConfiguracaoComponenteService.SalvarGrupoConfiguracaoComponente),
                                                                 delete: nameof(IGrupoConfiguracaoComponenteService.ExcluirGrupoConfiguracaoComponente))
                   .Detail<GrupoConfiguracaoComponenteDynamicModel>("_DetailList")
                   .Tokens(tokenList: UC_CUR_002_04_01.PESQUISAR_GRUPO_CONFIGURACAO_COMPONENTE,
                           tokenInsert: UC_CUR_002_04_02.MANTER_GRUPO_CONFIGURACAO_COMPONENTE,
                           tokenEdit: UC_CUR_002_04_02.MANTER_GRUPO_CONFIGURACAO_COMPONENTE,
                           tokenRemove: UC_CUR_002_04_02.MANTER_GRUPO_CONFIGURACAO_COMPONENTE);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
                this.Ativo = true;
        }

        #endregion [ Configurações ]
    }
}