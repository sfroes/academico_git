using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class LancamentoHistoricoEscolarVO : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public string MateriaLecionada { get; set; }

        public List<LancamentoHistoricoEscolarDetalhesVO> Lancamentos { get; set; }
    }
}