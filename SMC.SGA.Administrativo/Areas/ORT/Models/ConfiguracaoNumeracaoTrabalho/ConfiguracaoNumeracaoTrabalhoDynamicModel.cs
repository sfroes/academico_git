using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Views.ConfiguracaoNumeracaoTrabalho.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class ConfiguracaoNumeracaoTrabalhoDynamicModel : SMCDynamicViewModel, ISMCMappable
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarGruposProgramasSelect))]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion [ DataSources ]

        [SMCSize(SMCSize.Grid2_24)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        public override long Seq { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid9_24)]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCRequired]
        public long? SeqEntidadeResponsavel { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCRequired]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public int? NumeroUltimaNumeracao { get; set; }

        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ConfiguracaoNumeracaoTrabalhoOfertaViewModel> Ofertas { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
            .Detail<ConfiguracaoNumeracaoTrabalhoListarDynamicModel>("_DetailList")
            .Service<IConfiguracaoNumeracaoTrabalhoService>(
                index: nameof(IConfiguracaoNumeracaoTrabalhoService.BuscarConfiguracaoNumeracaoTrabalho),
                save: nameof(IConfiguracaoNumeracaoTrabalhoService.SalvarConfiguracaoNumeracaoTrabalho)
                )
              .Assert("MSG_Asset_Confirmar_Alteracao_Configuracao", x =>
              {
                  var model = (x as ConfiguracaoNumeracaoTrabalhoDynamicModel);
                  return  model.Seq != 0;
              })
            .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                        ((ConfiguracaoNumeracaoTrabalhoListarDynamicModel)x).Descricao))
            .Tokens(tokenInsert: UC_ORT_004_03_02.MANTER_NUMERACAO_TRABALHO ,
                    tokenEdit: UC_ORT_004_03_02.MANTER_NUMERACAO_TRABALHO,
                    tokenRemove: UC_ORT_004_03_02.MANTER_NUMERACAO_TRABALHO,
                    tokenList: UC_ORT_004_03_01.PESQUISAR_NUMERACAO_TRABALHO);
        }
    }
}