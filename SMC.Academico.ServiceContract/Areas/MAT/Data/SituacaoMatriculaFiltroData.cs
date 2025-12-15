using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class SituacaoMatriculaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public List<long> SeqsTipoVinculoAluno { get; set; }

        public bool? VinculoAtivo { get; set; }

        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        public List<string> Tokens { get; set; }
    }
}