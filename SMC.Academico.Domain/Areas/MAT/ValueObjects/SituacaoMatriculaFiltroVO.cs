using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class SituacaoMatriculaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public List<long> SeqsTipoVinculoAluno { get; set; }

        public bool? VinculoAtivo { get; set; }

        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        public List<string> Tokens { get; set; }
    }
}