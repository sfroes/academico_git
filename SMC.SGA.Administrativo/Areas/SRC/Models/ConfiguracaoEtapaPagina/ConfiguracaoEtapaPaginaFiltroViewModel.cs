using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoEtapaPaginaFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCParameter]
        [SMCFilterKey]
        [SMCFilter(true, true)]
        public long SeqConfiguracaoEtapa { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long? SeqProcessoEtapa { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool PossuiPaginas { get; set; }

        [SMCHidden]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCHidden]
        public bool HabilitarBotaoNovo
        {
            get
            {
                return PossuiPaginas && !(this.SituacaoEtapa == SituacaoEtapa.Liberada || this.SituacaoEtapa == SituacaoEtapa.Encerrada);
            }
        }

        [SMCHidden]
        public string MensagemBotaoNovo
        {
            get
            {
                if (this.SituacaoEtapa == SituacaoEtapa.Encerrada)
                {
                    return "MensagemDesabilitaBotaoNovo_EtapaEncerrada";
                }
                else if (this.SituacaoEtapa == SituacaoEtapa.Liberada)
                {
                    return "MensagemDesabilitaBotaoNovo_EtapaLiberada";
                }

                return string.Empty;
            }
        }

        #endregion
    }
}