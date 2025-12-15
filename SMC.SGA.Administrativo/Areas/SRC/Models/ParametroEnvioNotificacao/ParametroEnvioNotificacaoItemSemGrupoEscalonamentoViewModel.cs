using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ParametroEnvioNotificacaoItemSemGrupoEscalonamentoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        #region [ Campos Edição ]

        [SMCHidden(SMCViewMode.List)]
        [SMCMask("99999")]
        [SMCMaxValue(10000)]
        [SMCMinValue(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        //[SMCConditionalReadonly(nameof(PossuiNotificacaoEnviada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public short QuantidadeDiasInicioEnvio { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        //[SMCConditionalReadonly(nameof(PossuiNotificacaoEnviada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public Temporalidade Temporalidade { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect(nameof(ParametroEnvioNotificacaoViewModel.AtributosDisponiveis), NameDescriptionField = nameof(DescricaoAtributoAgendamento))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        //[SMCConditionalReadonly(nameof(PossuiNotificacaoEnviada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long AtributoAgendamento { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCMask("99999")]
        [SMCMaxValue(10000)]
        [SMCMinValue(0)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        //[SMCConditionalReadonly(nameof(PossuiNotificacaoEnviada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public short? QuantidadeDiasRecorrencia { get; set; }

        [SMCConditionalReadonly(nameof(QuantidadeDiasRecorrencia), SMCConditionalOperation.Equals, "")]
        //ANA EM 08/07/2020: COMENTADO PORQUE SERÃO APLICADAS NOVAS REGRAS NA PRÓXIMA SPRINT
        //[SMCConditionalReadonly(nameof(QuantidadeDiasRecorrencia), SMCConditionalOperation.Equals, "", PersistentValue = true, RuleName = "R1")]
        //[SMCConditionalReadonly(nameof(PossuiNotificacaoEnviada), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "R2")]
        //[SMCConditionalRule("R1 || R2")]
        [SMCConditionalRequired(nameof(QuantidadeDiasRecorrencia), SMCConditionalOperation.NotEqual, "")]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public bool? ReenviarNotificacao { get; set; }

        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public bool Ativo { get; set; } = true;

        #endregion [ Campos Edição ]

        #region [ Campos Lista ]

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoQuantidadeDiasInicioEnvio => QuantidadeDiasInicioEnvio.ToString();

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoTemporalidade => Temporalidade.SMCGetDescription();

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoAtributoAgendamento
        {
            get
            {
                if (_descricaoAtributoAgendamento == null)
                    _descricaoAtributoAgendamento = ((Enum)Enum.Parse(typeof(AtributoAgendamento), AtributoAgendamento.ToString())).SMCGetDescription();
                return _descricaoAtributoAgendamento;
            }
            set => _descricaoAtributoAgendamento = value;
        }

        private string _descricaoAtributoAgendamento;

        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoQuantidadeDiasRecorrencia => QuantidadeDiasRecorrencia.ToString();

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public bool? ListaReenviarNotificacao => ReenviarNotificacao;

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public bool ListaAtivo => Ativo;

        [SMCHidden]
        public bool PossuiNotificacaoEnviada { get; set; }

        #endregion [ Campos Lista ]
    }
}