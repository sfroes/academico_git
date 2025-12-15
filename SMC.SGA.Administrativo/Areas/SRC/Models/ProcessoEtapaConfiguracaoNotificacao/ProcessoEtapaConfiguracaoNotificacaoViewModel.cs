using SMC.Notificacoes.UI.Mvc.Models;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using SMC.Framework.Mapper;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.SGA.Administrativo.Areas.SRC.Views.ProcessoEtapaConfiguracaoNotificacao.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaConfiguracaoNotificacaoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        #region Datasource

        public List<SMCDatasourceItem> TiposNotificacao { get; set; }

        public List<SMCDatasourceItem> UnidadesResponsaveis { get; set; }

        #endregion

        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }
        
        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(UnidadesResponsaveis), AutoSelectSingleItem = true, SortDirection = SMCSortDirection.Ascending, NameDescriptionField = nameof(DescricaoProcessoUnidadeResponsavel))]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqProcessoUnidadeResponsavel { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string DescricaoProcessoUnidadeResponsavel { get; set; }

        [SMCReadOnly]
        public string DescricaoTipoNotificacaoAuxiliar { get; set; }

        [SMCRequired]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        [SMCSelect(nameof(TiposNotificacao), SortDirection = SMCSortDirection.Ascending, NameDescriptionField = nameof(DescricaoTipoNotificacao))]
        public long SeqTipoNotificacao { get; set; }

        /// <summary>
        /// Sequencial de comparação caso exista alteração no Sequncial de Tipo Notificação
        /// </summary>
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqTipoNotificacaoComparacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public long SeqConfiguracaoTipoNotificacao { get; set; }

        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public bool EnvioAutomatico { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string DescricaoTipoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public bool TipoNotificacaoPermiteAgendamento { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string ObservacaoTipoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public ConfiguracaoNotificacaoEmailViewModel ConfiguracaoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public virtual List<ParametroEnvioNotificacaoItemViewModel> ParametrosEnvioNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public virtual TipoNotificacaoViewModel TipoNotificacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public virtual ProcessoUnidadeResponsavelViewModel ProcessoUnidadeResponsavel { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public string Observacao { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCMapProperty("ProcessoUnidadeResponsavel.NomeReduzido")]
        public string NomeReduzidoProcessoUnidadeResponsavel { get; set; }

        public string MensagemInformativa
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return UIResource.Mensagem_Informativa_Etapa_Encerrada;
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return UIResource.Mensagem_Informativa_Etapa_Liberada;
                }

                return null;
            }
        }

        [SMCHidden]
        public bool PossuiRegistroEnvioNotificacao { get; set; }

        [SMCHidden]
        public bool HabilitaBtnComPermissaoManutencaoProcesso { get; set; }

        [SMCHidden]
        public bool NotificacaoObrigatoriaNaEtapa { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (PossuiRegistroEnvioNotificacao)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "Botao_Instruction_Excluir_Configuracao_Etapa_Encerrada";
                }
                else if (PossuiRegistroEnvioNotificacao)
                {
                    return "Botao_Instruction_Excluir_Configuracao_Registro_Envio";
                }
                else if (NotificacaoObrigatoriaNaEtapa)
                {
                    return "Botao_Instruction_Excluir_Configuracao_Etapa_Notificacao_Obrigatoria";
                }

                return null;
            }
        }
    }
}