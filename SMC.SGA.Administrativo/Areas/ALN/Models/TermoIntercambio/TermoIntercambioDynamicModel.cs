using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.ALN.Views.TermoIntercambio.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioDynamicModel : SMCDynamicViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IParceriaIntercambioInstituicaoExternaService), nameof(IParceriaIntercambioInstituicaoExternaService.BuscarParceriaIntercambioInstituicoesExternasSelect), values: new string[] { nameof(SeqParceriaIntercambio), nameof(Ativo) })]
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(INivelEnsinoService), nameof(INivelEnsinoService.BuscarNiveisEnsinoParceriaIntercambioSelect), values: new string[] { nameof(SeqParceriaIntercambio) })]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect), values: new string[] { nameof(SeqNivelEnsino), nameof(SeqParceriaIntercambio), nameof(Ativo) })]
        public List<SMCDatasourceItem> ParceriaIntercambioTipoTermo { get; set; }

        #endregion

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqParceriaIntercambio { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSelect(nameof(NiveisEnsino), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        public long? SeqNivelEnsino { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        [SMCSelect(nameof(ParceriaIntercambioTipoTermo), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true, NameDescriptionField = nameof(NameDescriptionParceriraIntercabioTipoTermo))]
        [SMCDependency(nameof(SeqNivelEnsino), nameof(TermoIntercambioController.BuscarTermosPorNivelEnsinoSelect), "TermoIntercambio", true, new[] { nameof(SeqParceriaIntercambio), nameof(Ativo) })]
        public long? SeqParceriaIntercambioTipoTermo { get; set; }

        [SMCHidden]
        public string NameDescriptionParceriraIntercabioTipoTermo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(InstituicoesExternas), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        public long SeqParceriaIntercambioInstituicaoExterna { get; set; }

        [SMCHidden]
        public bool PossuiPessoaAtuacao { get; set; }

        [SMCHidden]
        public bool ConcedeFormacao { get; set; }

        /// <summary>
        /// Utilizado para buscar tipos de parceria intercambio ativos
        /// </summary>
        [SMCHidden]
        public bool Ativo => true;

        [SMCHidden]
        public bool SomenteLeituraJustificativaPeriodoVigencia => Seq == 0;

        [SMCHidden]
        [SMCDependency(nameof(SeqParceriaIntercambioTipoTermo), nameof(TermoIntercambioController.ValidarTipoTermoIntercambio), "TermoIntercambio", false, new[] { nameof(SeqParceriaIntercambio), nameof(SeqNivelEnsino) })]        
        public bool ExibeVigencias { get; set; }

        [SMCDetail(min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(ExibeVigencias), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<TermoIntercambioVigenciaViewModel> Vigencias { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 1)]
        public SMCMasterDetailList<TermoIntercambioTipoMobilidadeViewModel> TiposMobilidade { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(min: 0)]
        public SMCMasterDetailList<TermoIntercambioArquivoViewModel> Arquivos { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .ViewPartialEdit("_Editar")
                .ViewPartialInsert("_Inserir")
                .Service<ITermoIntercambioService>(
                      index: nameof(ITermoIntercambioService.ListarTermoIntercambio),
                      edit: nameof(ITermoIntercambioService.PreencherModeloTermoIntercambio),
                      save: nameof(ITermoIntercambioService.SalvarTermoIntercambio),
                      delete: nameof(ITermoIntercambioService.ExcluirTermoIntercambio)
                 )
                .HeaderIndex("CabecalhoTermoIntercambio")
                .Detail<TermoIntercambioListarDynamicModel>("_DetailList", allowSort: true)
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((TermoIntercambioListarDynamicModel)x).Descricao))
                .ButtonBackIndex("Index", "ParceriaIntercambio")
                .Assert("MSG_Asset_ConcedeFormacao_TermoIntercambio", x => (x as TermoIntercambioDynamicModel).ConcedeFormacao)
                .Tokens(tokenList: UC_ALN_004_01_03.PESQUISAR_TERMO_INTERCAMBIO,
                        tokenInsert: UC_ALN_004_01_04.MANTER_TERMO_INTERCAMBIO,
                        tokenRemove: UC_ALN_004_01_04.MANTER_TERMO_INTERCAMBIO,
                        tokenEdit: UC_ALN_004_01_04.MANTER_TERMO_INTERCAMBIO);
        }
    }
}