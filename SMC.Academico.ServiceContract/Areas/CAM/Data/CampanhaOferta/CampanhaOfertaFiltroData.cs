using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaOfertaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool? Ativas { get; set; }

        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long? SeqCampanha { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqProcessoSeletivo { get; set; }
    }
}