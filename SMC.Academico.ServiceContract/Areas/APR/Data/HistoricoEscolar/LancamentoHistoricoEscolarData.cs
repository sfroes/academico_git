using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoHistoricoEscolarData : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public List<LancamentoHistoricoEscolarDetalhesData> Lancamentos { get; set; }

        public bool MateriaLecionadaObrigatoria { get; set; }
    }
}