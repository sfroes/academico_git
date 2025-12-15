using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public long? SeqCurso { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long? SeqEntidadeResponsavel { get; set; }
        public long? SeqLocalidade { get; set; }
        public List<long> SeqNivelEnsino { get; set; }
        public long? SeqSituacaoAtual { get; set; }
        public long? SeqTipoFormacaoEspecifica { get; set; }
        public long? SeqEntidadeResponsavelFormacao { get; set; }
        public List<long?> SeqsEntidadesResponsaveis { get; set; }
        public bool? Ativo { get; set; }
    }
}