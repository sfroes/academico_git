using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class FormaIngressoFiltroVO : ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }
    }
}