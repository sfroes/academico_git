using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTipoEventoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqTipoEventoAgd { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long SeqIntituicaoTipoEvento { get; set; }

        public List<CicloLetivoTipoEventoNivelEnsinoData> NiveisEnsino { get; set; }

        public List<CicloLetivoTipoEventoParametroData> Parametros { get; set; }
    }
}