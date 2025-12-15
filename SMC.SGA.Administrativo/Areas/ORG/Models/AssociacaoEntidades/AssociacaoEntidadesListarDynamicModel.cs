using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class AssociacaoEntidadesListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        public long SeqEntidade { get; set; }

        [SMCSortable(true, true, "Entidade.Nome")]
        public string NomeEntidade { get; set; }

        [SMCValueEmpty("-")]
        [SMCSortable(true, false, "GrauAcademico.Descricao")]
        public string DescricaoGrauAcademico { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoOrgaoRegulador { get; set; }

        [SMCValueEmpty("-")]
        public long? SeqCursoOferta { get; set; }
    }
}