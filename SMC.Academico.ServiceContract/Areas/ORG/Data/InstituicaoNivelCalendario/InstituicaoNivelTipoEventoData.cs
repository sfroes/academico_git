using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelTipoEventoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelCalendario { get; set; }

        public long SeqTipoEventoAgd { get; set; }

        public string TokenTipoEventoAgd { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }

        public virtual InstituicaoNivelCalendarioData InstituicaoNivelCalendario { get; set; }
    }
}
