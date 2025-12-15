using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Models
{
    public class TipoEventoFiltroViewModel : SMCViewModelBase, ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoAgenda { get; set; }

        [SMCMapProperty("SeqUnidadeResponsavel")]
        public long? SeqUnidadeResponsavelAgd { get; set; }

        public string Descricao { get; set; }

        public bool? ApenasAtivos { get; set; }

        public long? SeqCicloLetivo { get; set; }
    }
}