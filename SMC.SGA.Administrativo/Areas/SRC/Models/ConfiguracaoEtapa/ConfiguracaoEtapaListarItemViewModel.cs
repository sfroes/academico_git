using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaListarItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoProcesso { get; set; }

        public string DescricaoConfiguracaoProcesso { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        public string DescricaoConfiguracaoEtapa { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool SolicitacaoAssociada { get; set; }

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool PossuiDocumentosRequeridos { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if (SolicitacaoAssociada)
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
                    return "MensagemDesabilitaBotaoExcluir_EtapaEncerrada";
                }
                else if (this.SolicitacaoAssociada)
                {
                    return "MensagemDesabilitaBotaoExcluir_SolicitacaoAssociada";
                }

                return string.Empty;
            }
        }

        [SMCHidden]
        public bool HabilitarBotaoGrupoDocumento
        {
            get
            {
                if (!PossuiDocumentosRequeridos)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoGrupoDocumento
        {
            get
            {
                if (!PossuiDocumentosRequeridos)
                {
                    return "MensagemDesabilitaBotaoGrupoDocumento_EtapaSemDocumento";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}