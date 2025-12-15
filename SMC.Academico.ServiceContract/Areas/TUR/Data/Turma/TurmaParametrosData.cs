using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaParametrosData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public bool ExiteOfertasConfiguracao { get; set; }

        public List<TurmaParametrosOfertaData> ParametrosOfertas { get; set; }
    }
}
