using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class InstituicaoTipoEventoParametroVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoTipoEvento { get; set; }

        public TipoParametroEvento TipoParametroEvento { get; set; }

        public bool Obrigatorio { get; set; }

        public bool Ativo { get; set; }
    }
}