using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public List<long> SeqEntidade { get; set; }

        public string Nome { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public bool ExibirPrimeiroCursoOfertasAtivas { get; set; }
    }
}