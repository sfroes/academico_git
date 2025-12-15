using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteListaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCKey]
        [SMCHidden]
        public long SeqAssociacaoCursoUnidadeTurno { get; set; }

        [SMCHidden]
        public long SeqCurso { get; set; }

        [SMCOrder(1)]
        public string Curso { get; set; }

        [SMCHidden]
        public long SeqUnidade { get; set; }

        [SMCOrder(2)]
        public string Unidade { get; set; }

        [SMCHidden]
        public long SeqLocalidade { get; set; }

        [SMCOrder(3)]
        public string Localidade { get; set; }

        [SMCHidden]
        public long SeqModalidade { get; set; }

        [SMCOrder(4)]
        public string Modalidade { get; set; }

        [SMCHidden]
        public long SeqTurno { get; set; }

        [SMCOrder(5)]
        public string Turno { get; set; }

        [SMCHidden]
        public long SeqCurriculoPadrao { get; set; }

        [SMCOrder(6)]
        public string CurriculoPadrao { get; set; }
    }
}