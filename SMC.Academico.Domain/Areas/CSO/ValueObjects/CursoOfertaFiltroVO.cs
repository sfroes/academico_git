using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoOfertaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqCurso{ get; set; }

        public string Nome { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public long? SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public long? SeqEntidadeResponsavelFormacao { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }
    }
}
