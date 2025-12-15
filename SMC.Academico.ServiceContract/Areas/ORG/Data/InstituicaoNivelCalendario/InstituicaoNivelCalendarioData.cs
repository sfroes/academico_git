using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelCalendarioData : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqInstituicaoNivel { get; set; }

        [SMCMapProperty("InstituicaoNivel.InstituicaoEnsino.SeqUnidadeResponsavelAgd")]
        public long SeqUnidadeResponsavelAgd { get; set; }
        public long SeqCalendarioAgd { get; set; }
        public UsoCalendario UsoCalendario { get; set; }
        public InstituicaoNivelData InstituicaoNivel { get; set; }
        public List<InstituicaoNivelTipoEventoData> TiposEvento { get; set; }
    }
}
