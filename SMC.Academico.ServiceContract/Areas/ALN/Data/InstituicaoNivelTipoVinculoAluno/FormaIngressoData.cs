using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class FormaIngressoData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("FormaIngresso.Descricao")]        
        public string Descricao { get; set; }

        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public long SeqFormaIngresso { get; set; }
        
        [SMCMapProperty("FormaIngresso.TipoFormaIngresso")]
        public TipoFormaIngresso TipoFormaIngresso { get; set; }
        
        [SMCMapProperty("FormaIngresso.TipoFormaIngresso")]
        public string TipoFormaIngressoDescricao { get; set; }

        public List<InstituicaoNivelTipoProcessoSeletivoData> TiposProcessoSeletivo { get; set; }

    }
}
