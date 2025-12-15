using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class TipoFormacaoEspecificaTipoCursoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public TipoCurso TipoCurso { get; set; }
    }
}
