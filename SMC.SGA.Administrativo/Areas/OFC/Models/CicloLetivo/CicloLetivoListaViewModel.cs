using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class CicloLetivoListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCSortable]
        public long Seq { get; set; }

        [SMCSortable]
        public string Descricao { get; set; }

        [SMCSortable]
        public string NivelEnsino { get; set; }

        [SMCSortable]
        public string RegimeLetivo { get; set; }

        [SMCSortable]
        public int? AnoCiclo { get; set; }

        [SMCSortable]
        public int? NumeroCiclo { get; set; }
    }
}