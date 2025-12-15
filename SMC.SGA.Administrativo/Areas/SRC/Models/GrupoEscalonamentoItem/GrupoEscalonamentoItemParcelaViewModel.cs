using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoItemParcelaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCRequired]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqMotivoBloqueio), nameof(GrupoEscalonamentoItemController.ValidarBloqueioPercentualParcela), "GrupoEscalonamentoItem", "SRC", false, includedProperties: new [] {nameof(GrupoEscalonamentoItemDynamicModel.ValorPercentualBanco) })]
        public bool DesativarPercentualParcela { get; set; }

        [SMCConditionalReadonly(nameof(GrupoEscalonamentoItemDynamicModel.ObrigatorioIdentificarParcela), false, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(GrupoEscalonamentoItemDynamicModel.CamposReadOnly), true, PersistentValue = true, RuleName = "R2")]
        [SMCConditionalRule("R1 || R2")]
        [SMCConditionalRequired(nameof(GrupoEscalonamentoItemDynamicModel.ObrigatorioIdentificarParcela), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid2_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid2_24)]
        [SMCMinValue(1)]
        public short? NumeroParcela { get; set; }

        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid5_24)]
        [SMCRequired]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCRequired]
        public DateTime? DataVencimentoParcela { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(DesativarPercentualParcela), true, PersistentValue = true)]
        [SMCDependency(nameof(DesativarPercentualParcela), nameof(GrupoEscalonamentoItemController.PreencherPercentual), "GrupoEscalonamentoItem", "SRC", false, includedProperties: new[] {nameof(GrupoEscalonamentoItemDynamicModel.ValorPercentualBanco), nameof(ValorPercentualParcela)})]
        [SMCMinValue(1)]
        [SMCMaxValue(100)]
        [SMCRequired]
        [SMCMask("999")]
        public decimal? ValorPercentualParcela { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCSelect("MotivosBloqueio", AutoSelectSingleItem = true)]
        [SMCRequired]
        public long? SeqMotivoBloqueio { get; set; }

        /// <summary>
        /// Esta propriedade foi criada para bloquear o campo serviço adicional
        /// independente da condição de bloqueio ou não do mestre detalhe (fora dessa classe)
        /// Quando tempos um contitional read only para um mestre detalhe que não o bloqueia, ele fica ativo
        /// e tambem ativa os campos dentro dele. Mesmo que o campo esteja anotado com SMCReadOnly.
        /// Como existe uma regra de negócio que diz que o serviço adicional deve ser bloqueado sempre,
        /// foi necessário implementar dessa forma.
        /// </summary>
        [SMCHidden]
        public bool BloquearServicoAdicional { get { return true; } }

        [SMCSelect]
        [SMCConditionalReadonly(nameof(BloquearServicoAdicional), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqMotivoBloqueio), nameof(GrupoEscalonamentoItemController.ValidarTokenMotivoBloqueio), "GrupoEscalonamentoItem", true)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid3_24)]
        public bool? ServicoAdicional { get; set; }
    }
}