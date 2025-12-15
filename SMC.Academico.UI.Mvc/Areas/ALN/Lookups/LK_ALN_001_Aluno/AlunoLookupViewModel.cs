using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class AlunoLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCSortable(true)]
        public long? NumeroRegistroAcademico { get; set; }

        [SMCDescription]
        [SMCSortable(true)]
        public string Nome { get; set; }

        [SMCSortable(true)]
        public string DescricaoNivelEnsino { get; set; }

        [SMCSortable(true)]
        public string TipoVinculoAluno { get; set; }

        public bool? VinculoAlunoAtivo { get; set; }

        [SMCSortable(true)]
        public string DescricaoCursoOferta { get; set; }

        [SMCSortable(true)]
        public string DescricaoLocalidade { get; set; }

        [SMCSortable(true)]
        public string DescricaoTurno { get; set; }

    }
}
