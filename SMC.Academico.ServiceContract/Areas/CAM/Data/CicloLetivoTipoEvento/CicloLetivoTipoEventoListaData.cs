using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTipoEventoListaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoTipoEvento { get; set; }

        public List<CicloLetivoTipoEventoNivelEnsinoData> NiveisEnsino { get; set; }

        public List<CicloLetivoTipoEventoParametroData> Parametros { get; set; }
    }
}