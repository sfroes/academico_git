using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCHidden]
        public long SeqAssociacaoCursoUnidadeTurno { get; set; }

        public string Curso { get; set; }
        public string Unidade { get; set; }
        public string Localidade { get; set; }
        public string Modalidade { get; set; }
        public string Turno { get; set; }

        public string CurriculoPadrao { get; set; }
    }
}