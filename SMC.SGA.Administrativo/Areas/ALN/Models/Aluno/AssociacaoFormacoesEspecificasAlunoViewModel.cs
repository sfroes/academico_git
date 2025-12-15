using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class AssociacaoFormacoesEspecificasAlunoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqAluno { get; set; }

        [SMCHidden]
        public bool SelecaoNivelFolha => false;

        [SMCHidden]
        public bool SelecaoNivelSuperior => false;

        [SMCHidden]
        public long? SeqCurso { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidade { get; set; }

        [SMCHidden]
        public long? SeqFormacaoEspecifica { get; set; }

        [SMCHidden]
        public long[] SeqsFormacoesEspecificasOrigem { get; set; }

        [SMCHidden]
        public bool ConsiderarSometeTipoFomacaoEspecifica { get; set; } = false;

        [FormacaoEspecificaLookup]
        [SMCDependency(nameof(ConsiderarSometeTipoFomacaoEspecifica))]
        [SMCDependency(nameof(SelecaoNivelFolha))]
        [SMCDependency(nameof(SelecaoNivelSuperior))]
        [SMCDependency(nameof(SeqCurso))]
        [SMCDependency(nameof(SeqCursoOfertaLocalidade))]
        [SMCDependency(nameof(SeqFormacaoEspecifica))]
        [SMCDependency(nameof(SeqsFormacoesEspecificasOrigem))]
        [SMCSize(SMCSize.Grid24_24)]
        public List<FormacaoEspecificaLookupGridViewModel> FormacoesEspecificas { get; set; }
    }
}