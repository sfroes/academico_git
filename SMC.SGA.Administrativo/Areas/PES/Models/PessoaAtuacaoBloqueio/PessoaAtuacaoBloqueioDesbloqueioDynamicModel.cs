using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.PES.Views.PessoaAtuacaoBloqueioDesbloqueio.App_LocalResources;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioDesbloqueioDynamicModel : SMCDynamicViewModel
    {
        #region Propriedades de apoio

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacaoBloqueio { get { return this.Seq; } }

        [SMCHidden]
        public bool PermiteDesbloqueioTemporario { get; set; }

        #endregion Propriedades de apoio

        [SMCHidden]
        [SMCOrder(0)]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid6_24)]
        [SMCSelect]
        [SMCReadOnly]
        //[SMCConditionalRequired(nameof(PermiteDesbloqueioTemporario), SMCConditionalOperation.Equals, true)]
        //[SMCConditionalReadonly(nameof(PermiteDesbloqueioTemporario), SMCConditionalOperation.Equals, false, PersistentValue = true, RuleName = "DT01")]
        //[SMCConditionalReadonly(nameof(Temporario), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "DT02")]
        //[SMCConditionalRule("DT01 || DT02")]
        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        /// <summary>
        /// Recebido como parâmetro na tela, para saber se deve vir com o campo de tipo de desbloqueio temporário como default.
        /// </summary>
        [SMCHidden]
        public bool Temporario { get; set; }

        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCReadOnly]
        public DateTime DataDesbloqueio { get; set; }

        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCReadOnly]
        public string ResponsavelDesbloqueio { get; set; }

        [SMCOrder(5)]
        [SMCMaxLength(1000)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCMultiline]
        [SMCRequired]
        [SMCConditionalDisplay(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, TipoDesbloqueio.Temporario)]
        public string JustificativaDesbloqueio { get; set; }
        
        //Foi feita dentro do dynamic a opção de ocultar os comprovantes opcionais
        [SMCHidden]
        public bool MotivoObrigatorioAnexoDesbloqueio { get; set; }

        [SMCOrder(6)]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 1)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, TipoDesbloqueio.Temporario, RuleName = "RC1")]
        [SMCConditionalDisplay(nameof(MotivoObrigatorioAnexoDesbloqueio), true, RuleName = "RC2")]
        [SMCConditionalRule("RC1 && RC2")]
        public SMCMasterDetailList<PessoaAtuacaoBloqueioComprovanteViewModel> Comprovantes { get; set; }

        [SMCOrder(6)]
        [SMCDetail(SMCDetailType.Modal, windowSize: SMCModalWindowSize.Large, min: 0)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, TipoDesbloqueio.Temporario, RuleName = "RCO1")]
        [SMCConditionalDisplay(nameof(MotivoObrigatorioAnexoDesbloqueio), false, RuleName = "RCO2")]
        [SMCConditionalRule("RCO1 && RCO2")]
        public SMCMasterDetailList<PessoaAtuacaoBloqueioComprovanteViewModel> ComprovantesOpcional { get; set; }

        [SMCHidden]
        public bool PermiteItem { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa")]
        [SMCDisplay]
        [SMCHideLabel()]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        [SMCOrder(7)]
        [SMCConditionalDisplay(nameof(PermiteItem), SMCConditionalOperation.Equals, true, RuleName = "Rule1")]
        [SMCConditionalDisplay(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 && Rule2")]
        public string MensagemItens { get; set; } = UIResource.MSG_Selecione_Itens_Desbloqueio;

        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(type: SMCDetailType.Tabular, HideMasterDetailButtons = true)]
        [SMCConditionalDisplay(nameof(PermiteItem), SMCConditionalOperation.Equals, true, RuleName = "Rule3")]
        [SMCConditionalDisplay(nameof(TipoDesbloqueio), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, RuleName = "Rule4")]
        [SMCConditionalRule("Rule3 && Rule4")]
        public SMCMasterDetailList<PessoaAtuacaoBloqueioDesbloqueioItemViewModel> Itens { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IPessoaAtuacaoBloqueioService>(
                        insert: nameof(IPessoaAtuacaoBloqueioService.PreencherModeloDesbloquearPessoaAtuacaoBloqueio),
                        edit: nameof(IPessoaAtuacaoBloqueioService.PreencherModeloDesbloquearPessoaAtuacaoBloqueio),
                        save: nameof(IPessoaAtuacaoBloqueioService.SalvarPessoaAtuacaoBloqueioDesbloqueio)
                    )
                   .RedirectIndexTo("Index", "PessoaAtuacaoBloqueio", null)
                   .IgnoreFilterGeneration()
                   .Header("BuscarCabecalhoPessoaAtuacaoBloqueio");
        }

        #endregion Configurações
    }
}