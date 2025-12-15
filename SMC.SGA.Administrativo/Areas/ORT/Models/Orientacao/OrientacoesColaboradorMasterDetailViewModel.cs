using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class OrientacoesColaboradorMasterDetailViewModel : SMCViewModelBase
    {
        public OrientacoesColaboradorMasterDetailViewModel()
        {
            this.DataInicioOrientacao = DateTime.Today;
        }

        #region DataSource

        [SMCHidden]
        [SMCIgnoreProp]
        [SMCDataSource(StorageType = SMCStorageType.None)]
        public List<SMCDatasourceItem> InstituicoesExternas { get; set; }

        #endregion DataSource

        #region Hidden

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string ColaboradorNameDescriptionField { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string TipoParticipacaoNameDescriptionField { get => SMCEnumHelper.GetDescription(this.TipoParticipacaoOrientacao); }

        [SMCIgnoreProp]
        public string NomeInstiuicaoFinanceiraConfirmacao { get; set; }

        [SMCIgnoreProp]
        public string NomeOrientadorConfirmacao { get; set; }

        [SMCHidden]
        [SMCIgnoreProp]
        public string InstituicaoExternaNameDescriptionField { get; set; }

        #endregion Hidden

        [SMCRequired]
        [SMCSelect(nameof(OrientacaoDynamicModel.Colaboradores), UseCustomSelect = true, NameDescriptionField = nameof(ColaboradorNameDescriptionField))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        //[SMCUnique]
        public long SeqColaborador { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(OrientacaoDynamicModel.TiposParticipacao), NameDescriptionField = nameof(TipoParticipacaoNameDescriptionField), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public TipoParticipacaoOrientacao TipoParticipacaoOrientacao { get; set; }

        [SMCMapForceFromTo]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime DataInicioOrientacao { get; set; }

        [SMCMinDate(nameof(DataInicioOrientacao))]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFimOrientacao { get; set; }

        [SMCHidden]
        public bool? Ativo { get; set; } = null;

        [SMCDependency(nameof(SeqColaborador), nameof(OrientacaoController.BuscarInstituicaoExternaPorColaboradoroSelect), "Orientacao", true, new string[] { nameof(Ativo) })]
        [SMCRequired]
        [SMCSelect(nameof(InstituicoesExternas), NameDescriptionField = nameof(InstituicaoExternaNameDescriptionField), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public long SeqInstituicaoExterna { get; set; }
    }
}