using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class AssociacaoEntidadeFiltroData : SMCPagerFilterData, ISMCMappable 
    {
        public long? Seq { get; set; }

        public long? SeqAtoNormativo { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        public int? CodigoCursoOferta { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }
    }
}
