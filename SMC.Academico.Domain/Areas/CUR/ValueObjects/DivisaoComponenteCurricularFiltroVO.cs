using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class DivisaoComponenteCurricularFiltroVO : ISMCMappable
    {
        public long SeqAluno { get; set; }
        public long SeqTipoTrabalho { get; set; }
        public long SeqNivelEnsino { get; set; }
        public long SeqInstituicaoEnsino { get; set; }
    }
}
