using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class AutorData : ISMCMappable
    {
        public string Nome { get; set; }

        public string DescricaoNivelEnsino { get; set; }

        public string DescricaoCurso { get; set; }

    }
}
