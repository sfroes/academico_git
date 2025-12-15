using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Views.ParceriaIntercambio.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCSortable]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public TipoParceriaIntercambio TipoParceriaIntercambio { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public bool? ProcessoNegociacao { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCMapProperty("Vigencias")]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 1)]
        public SMCMasterDetailList<ParceriaIntercambioVigenciaViewModel> Vigencias { get; set; }

        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ParceriaIntercambioTipoTermoViewModel> TiposTermo { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCCssClass("smc-listdetailed-conteudo-subtitulo")]
        [SMCDependency(nameof(SeqInstituicaoEnsino), "BuscarInstituicaoLogadaSelect", "ParceriaIntercambio", true)]
        public string InstituicaoEnsino { get; set; }

        //[SMCRequired]
        //[InstituicaoExternaLookup]
        //[SMCSize(SMCSize.Grid24_24)]
        //[SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        //[SMCDependency(nameof(ListarSomenteInstituicoesEnsino))]
        //public List<ParceriaIntercambioInstituicaoExternaViewModel> InstituicoesExternas { get; set; }

        [SMCDetail(min: 1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ParceriaIntercambioInstituicaoExternaViewModel> InstituicoesExternas { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(min: 0)]
        public SMCMasterDetailList<ParceriaIntercambioArquivoViewModel> Arquivos { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .ViewPartialInsert("_Editar")
                .ViewPartialEdit("_Editar")
                .Service<IParceriaIntercambioService>(
                      index: nameof(IParceriaIntercambioService.ListarParceriaIntercambio),
                      edit: nameof(IParceriaIntercambioService.AlterarParceriaIntercambio),
                      save: nameof(IParceriaIntercambioService.SalvarParceriaIntercambio),
                      delete: nameof(IParceriaIntercambioService.ExcluirParceria),
                      onAfterSave: (model, success) =>
                      {
                          if (!success)
                          {
                              if ((model as ParceriaIntercambioDynamicModel).Vigencias.SMCAny())
                              {
                                  (model as ParceriaIntercambioDynamicModel).Vigencias.UndoClear();
                                  (model as ParceriaIntercambioDynamicModel).TiposTermo.UndoClear();
                              }
                          }
                      }
                 )
                .Detail<ParceriaIntercambioListarDynamicModel>("_DetailList", allowSort: true)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((ParceriaIntercambioListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_ALN_004_01_01.PESQUISAR_PARCERIA_INTERCAMBIO,
                        tokenEdit: UC_ALN_004_01_02.MANTER_PARCERIA_INTERCAMBIO,
                        tokenRemove: UC_ALN_004_01_02.MANTER_PARCERIA_INTERCAMBIO,
                        tokenInsert: UC_ALN_004_01_02.MANTER_PARCERIA_INTERCAMBIO);
                }
        #endregion Configurações

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (viewMode == SMCViewMode.Insert)
            {
                this.TiposTermo = new SMCMasterDetailList<ParceriaIntercambioTipoTermoViewModel>();
                this.TiposTermo.DefaultModel = new ParceriaIntercambioTipoTermoViewModel() { Ativo = true };

                this.InstituicoesExternas = new SMCMasterDetailList<ParceriaIntercambioInstituicaoExternaViewModel>();
                this.InstituicoesExternas.DefaultModel = new ParceriaIntercambioInstituicaoExternaViewModel() { Ativo = true };
            }

            if(viewMode == SMCViewMode.Edit)
            {
                this.TiposTermo.DefaultModel = new ParceriaIntercambioTipoTermoViewModel() { Ativo = true };
                this.InstituicoesExternas.DefaultModel = new ParceriaIntercambioInstituicaoExternaViewModel() { Ativo = true };
            }
        }
    }
}