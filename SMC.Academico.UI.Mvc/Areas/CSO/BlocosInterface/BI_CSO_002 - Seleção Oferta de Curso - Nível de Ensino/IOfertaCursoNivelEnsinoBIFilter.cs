using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.InterfaceBlocks
{
    public interface IOfertaCursoNivelEnsinoBIFilter
    {
        List<long?> SeqsEntidadesResponsaveis { get; set; }

        long? SeqLocalidade { get; set; }

        long? SeqNivelEnsino { get; set; }

        CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        long? SeqTurno { get; set; }

        bool EntidadesResponsaveisApenasAtivas { get; }
        bool LocalidadesApenasAtivas { get; }
    }
}