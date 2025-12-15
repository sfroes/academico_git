using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class LookupTurmaFiltroViewModel : SMCLookupFilterViewModel
	{
        public long? Seq { get; set; }

        public long? SeqCicloLetivoInicio { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqCursoOferta { get; set; }

        public string CodigoFormatado { get; set; }

        public string DescricaoConfiguracao { get; set; }
    }
}