using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfiguracaoProcessoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region Processo
       
        [SMCHidden]
        public string DescricaoProcesso { get; set; }

        [SMCHidden]
        public bool ProcessoEncerrado { get; set; }

        #endregion

        [SMCParameter]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        public long SeqProcesso { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Descricao { get; set; }

        #endregion

        #region Habilitar Botoes

        [SMCHidden]
        public bool HabilitarBotaoNovo
        {
            get
            {
                return !this.ProcessoEncerrado;
            }
        }

        [SMCHidden]
        public string MensagemBotaoNovo
        {
            get
            {
                if (this.ProcessoEncerrado)
                {
                    return "MensagemDesabilitaBotaoNovo";
                }

                return string.Empty;
            }
        }
            
        #endregion
    }
}