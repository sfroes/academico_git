using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class InstituicaoTipoEventoParametroData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEvento { get; set; }

        public TipoParametroEvento? TipoParametroEvento { get; set; }

        public bool? Obrigatorio { get; set; }

        public bool? Ativo { get; set; }
    }
}