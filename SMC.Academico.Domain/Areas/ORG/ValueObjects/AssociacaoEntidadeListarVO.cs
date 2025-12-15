using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class AssociacaoEntidadeListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoOrgaoRegulador { get; set; }

        public long? SeqCursoOferta { get; set; }
    }
}
