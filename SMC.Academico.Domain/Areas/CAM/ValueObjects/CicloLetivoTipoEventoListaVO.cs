using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CicloLetivoTipoEventoListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        [SMCMapProperty("InstituicaoTipoEvento.SeqTipoEventoAgd")]
        public long SeqTipoEventoAgd { get; set; }

        public string DescricaoTipoEvento { get; set; }

        public List<CicloLetivoTipoEventoNivelEnsinoVO> NiveisEnsino { get; set; }

        public List<CicloLetivoTipoEventoParametroVO> Parametros { get; set; }
    }
}