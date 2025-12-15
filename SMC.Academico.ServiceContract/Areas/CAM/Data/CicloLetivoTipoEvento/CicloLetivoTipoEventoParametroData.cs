using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CicloLetivoTipoEventoParametroData : ISMCMappable
    {
        public long SeqInstituicaoTipoEventoParametro { get; set; }

        public TipoParametroEvento TipoParametroEvento { get; set; }
    }
}