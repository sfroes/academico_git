using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoFiltroData : ISMCMappable
    {
        public long? Seq { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqProcesso { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public string Descricao { get; set; }
    }
}