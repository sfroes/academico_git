using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AssociacaoFormacaoEspecificaIngressanteViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public bool SelecaoNivelFolha => false;

        [SMCHidden]
        public bool SelecaoNivelSuperior => true;

        [SMCHidden]
        public long? SeqCurso { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public bool ConsiderarSometeTipoFomacaoEspecifica => false;

        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(ConsiderarSometeTipoFomacaoEspecifica))]
        [SMCDependency(nameof(SelecaoNivelFolha))]
        [SMCDependency(nameof(SelecaoNivelSuperior))]
        [SMCDependency(nameof(SeqCurso))]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade))]
        [SMCDependency(nameof(SeqFormacaoEspecifica))]
        [SMCSize(SMCSize.Grid24_24)]
        public List<FormacaoEspecificaLookupGridViewModel> FormacoesEspecificas { get; set; }
    }
}