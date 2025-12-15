using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoProcessoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        #region Processo

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public bool SolicitacaoAssociada { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        #endregion

        [SMCKey]
        [SMCParameter]
        [SMCSize(SMCSize.Grid12_24)]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Descricao { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool HabilitarBotaoExcluir
        {
            get
            {
                return !this.ProcessoEncerrado && !this.SolicitacaoAssociada;
            }
        }

        [SMCHidden]
        public string MensagemBotaoExcluir
        {
            get
            {
                if(this.ProcessoEncerrado)
                {
                    return "MensagemDesabilitaBotaoExcluirProcessoEncerrado";
                }
                else if (this.SolicitacaoAssociada)
                {
                    return "MensagemDesabilitaBotaoExcluirSolicitacaoAssociada";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}