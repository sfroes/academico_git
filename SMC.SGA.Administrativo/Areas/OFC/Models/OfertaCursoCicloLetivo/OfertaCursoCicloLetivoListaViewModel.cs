using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCSortable]
        public long Seq { get; set; }

        [SMCSortable]
        public string Descricao { get; set; }

        [SMCSortable]
        public string NivelEnsino { get; set; }

        [SMCSortable]
        public string CicloLetivo { get; set; }

        [SMCSortable]
        public string FormaIngresso { get; set; }

        /// <summary>
        /// Indica se o menu de Associação de Polo deve ser exibido.
        /// Esta funcionalidade somente poderá ser acionada se a localidade "Puc Minas Virtual" estiver 
        /// associada a oferta temporal em questão.
        /// </summary>
        [SMCHidden]
        public bool ExibirAssociacaoPolo { get; set; }
    }
}