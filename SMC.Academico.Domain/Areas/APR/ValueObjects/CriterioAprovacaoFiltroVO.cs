using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class CriterioAprovacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqEscalaApuracao { get; set; }

        public bool? SemEscalaApuracao { get; set; }

        public long? SeqAluno { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public bool ConsiderarMatriz { get; set; }
    }
}