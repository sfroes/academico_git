using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelCalendarioListaData : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqInstituicaoNivel { get; set; }

        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        public string DscNivelEnsino { get; set; }

        [SMCMapProperty("InstituicaoNivel.InstituicaoEnsino.SeqUnidadeResponsavelAgd")]
        public long SeqUnidadeResponsavelAgd { get; set; }
        public string NomeCalendario { get; set; }
        public long SeqCalendarioAgd { get; set; }
        public UsoCalendario UsoCalendario { get; set; }
    }
}
