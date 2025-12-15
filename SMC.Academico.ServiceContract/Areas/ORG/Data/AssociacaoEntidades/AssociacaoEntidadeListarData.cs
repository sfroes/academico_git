using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AssociacaoEntidadeListarData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoOrgaoRegulador { get; set; }

        public long? SeqCursoOferta { get; set; }
    }
}
