using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaOfertaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqTipoOferta { get; set; }

        public long? SeqCiloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool? Ativas { get; set; }

        public string Descricao { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqCampanha { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqProcessoSeletivo { get; set; }
    }
}