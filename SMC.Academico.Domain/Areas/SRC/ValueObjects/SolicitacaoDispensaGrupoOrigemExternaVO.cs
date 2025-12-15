using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaGrupoOrigemExternaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoDispensaOrigemExterna { get; set; }

        public string Descricao { get; set; }
    }
}
