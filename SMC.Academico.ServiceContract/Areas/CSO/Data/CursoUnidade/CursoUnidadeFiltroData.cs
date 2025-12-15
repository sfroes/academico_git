using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoUnidadeFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCurso { get; set; }

        public long? SeqUnidade { get; set; }

        public bool ExibirPrimeiroCursoOfertaLocalidadeAtivo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqModalidade { get; set; }

        public TipoOrgaoRegulador? SeqTipoOrgaoRegulador { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }
    }
}
