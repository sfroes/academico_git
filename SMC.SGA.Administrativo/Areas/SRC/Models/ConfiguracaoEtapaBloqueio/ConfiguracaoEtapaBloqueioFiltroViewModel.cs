using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaBloqueioFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCFilter(true, true)]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCFilter(true, true)]
        public long SeqConfiguracaoEtapa { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovo
        {
            get
            {
                if(SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return false;
                }
                else if(SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return false;
                }

                return true;
            }
        }

        [SMCHidden]
        public string MensagemBotaoNovo
        {
            get
            {
                if (SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotao_EtapaEncerrada";
                }
                else if (SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotao_EtapaLiberada";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}