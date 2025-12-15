using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioTipoMobilidadeVO : ISMCMappable
    {
        public virtual long Seq { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public TipoMobilidade TipoMobilidade { get; set; }

        public short QuantidadeVagas { get; set; }

        public List<TermoIntercambioPessoaVO> Pessoas { get; set; }
    } 
}
