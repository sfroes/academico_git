using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCParameter]
        [SMCSize(SMCSize.Grid12_24)]
        public long Seq { get; set; }

        [SMCHidden]  
        [SMCParameter]
        public long? SeqTipoServico { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, true, "TipoServico.Descricao", SMCSortDirection.Ascending)]
        public string DescTipoServico { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, false)]
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, false)]
        public string Descricao { get; set; }

        #region Habilitar Botoes

        [SMCHidden]
        public bool EsconderBotaoConsultarTaxas { get; set; }

        #endregion
    }
}