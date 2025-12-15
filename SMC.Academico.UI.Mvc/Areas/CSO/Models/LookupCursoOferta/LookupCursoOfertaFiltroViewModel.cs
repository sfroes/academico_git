using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Models
{
    public class LookupCursoOfertaFiltroViewModel : SMCLookupFilterViewModel
	{
        public long? Seq { get; set; }
        public string Descricao { get; set; }
        public long? SeqCurso { get; set; }
        public string Nome { get; set; }
        public List<long?> SeqsEntidadesResponsaveis { get; set; }
        public List<long> SeqNivelEnsino { get; set; }
        public long? SeqSituacaoAtual { get; set; }
        public long? SeqTipoFormacaoEspecifica { get; set; }
        public bool? Ativo { get; set; }
    }
}