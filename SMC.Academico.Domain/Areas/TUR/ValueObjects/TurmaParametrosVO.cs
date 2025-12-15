using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaParametrosVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public bool ExiteOfertasConfiguracao { get; set; }

        public List<TurmaParametrosOfertaVO> ParametrosOfertas { get; set; }
    }
}
