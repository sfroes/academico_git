using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioItemViewModel : SMCViewModelBase, ISMCSeq
    {
        #region [ Campos Edição ]

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacaoBloqueio { get; set; }
        [SMCOrder(1)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(255)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.ReadOnly)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSelect(SMCIgnoreOrUseOnlyEnumItems.UseOnly, SituacaoBloqueio.Bloqueado, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCOrder(3)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCReadOnly]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public SituacaoBloqueio SituacaoBloqueioReadOnly => SituacaoBloqueio;

        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Insert)]
        [SMCMaxLength(255)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public string CodigoIntegracaoSistemaOrigem { get; set; }

        [SMCOrder(5)]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit)]
        public DateTime? ListaDataAlteracao => DataAlteracao;

        [SMCOrder(6)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public string UsuarioInclusao { get; set; }

        [SMCOrder(7)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public DateTime? DataInclusao { get; set; }

        [SMCOrder(8)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Insert)]
        [SMCMaxLength(100)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid18_24)]
        public string UsuarioAlteracao { get; set; }

        #endregion [ Campos Edição ]

        #region [ Campos Listagem ]

        [SMCOrder(1)]
        [SMCCssClass("smc-size-md-12 smc-size-xs-12 smc-size-sm-10 smc-size-lg-13")]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public string ListaDescricao => Descricao;

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCIgnoreProp(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.ReadOnly)]
        public SituacaoBloqueio ListaSituacaoBloqueio => SituacaoBloqueio;

        [SMCOrder(11)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Insert)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        public DateTime? DataAlteracao { get; set; }

        #endregion [ Campos Listagem ]
    }
}