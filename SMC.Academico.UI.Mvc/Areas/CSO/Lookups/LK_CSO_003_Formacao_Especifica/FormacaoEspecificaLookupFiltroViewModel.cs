using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long[] SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public long? SeqCurso { get; set; }

        [SMCHidden]
        public long? SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecificaOrigem { get; set; }

        [SMCHidden]
        public bool? SelecaoNivelFolha { get; set; }

        [SMCHidden]
        public bool? SelecaoNivelSuperior { get; set; }

        [SMCHidden]
        public bool? Ativo { get; set; }

        [SMCHidden]
        public long? SeqTipoOferta { get; set; }

        [SMCHidden]
        public long? SeqCursoOferta { get; set; }

        //[SMCHidden]
        //public List<long?> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public bool ConsiderarSometeTipoFomacaoEspecifica { get; set; }

        [SMCHidden]
        public List<long> SeqTipoFormacaoEspecifica { get; set; }
    }
}