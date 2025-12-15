using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTipoEventoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        [SMCMapProperty("InstituicaoTipoEvento.SeqInstituicaoEnsino")]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCMapProperty("InstituicaoTipoEvento.SeqTipoAgenda")]
        public long SeqTipoAgenda { get; set; }

        [SMCMapProperty("InstituicaoTipoEvento.SeqTipoEventoAgd")]
        public long SeqTipoEventoAgd { get; set; }

        public long SeqCicloLetivo { get; set; }

        public long SeqIntituicaoTipoEvento { get; set; }

        public List<CicloLetivoTipoEventoNivelEnsinoVO> NiveisEnsino { get; set; }
        public List<CicloLetivoTipoEventoParametroVO> Parametros { get; set; }
    }
}