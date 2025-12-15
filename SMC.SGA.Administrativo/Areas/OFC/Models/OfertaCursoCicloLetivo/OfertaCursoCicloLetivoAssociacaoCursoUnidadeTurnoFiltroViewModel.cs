using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCFilterKey]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        [SMCFilter]
        [SMCSelect("Modalidades")]
        [SMCSize(SMCSize.Grid3_24)]
        public long? SeqModalidade { get; set; }

        public List<SMCSelectItem> Modalidades { get; set; }

        [SMCFilter]
        [SMCSelect("Turnos")]
        [SMCSize(SMCSize.Grid3_24)]
        public long? SeqTurno { get; set; }

        public List<SMCSelectItem> Turnos { get; set; }

        [SMCFilter]
        [SMCSelect("UnidadesLocalidades")]
        [SMCSize(SMCSize.Grid7_24)]
        public long? SeqUnidadeLocalidadeSelecionada { get; set; }

        public List<SMCSelectItem> UnidadesLocalidades { get; set; }

        [SMCFilter]
        [LookupCurso]
        [SMCSize(SMCSize.Grid7_24)]
        public SGALookupViewModel SeqCurso { get; set; }

        public OfertaCursoCicloLetivoDadosViewModel DadosOfertaCursoCicloLetivo { get; set; }
    }
}