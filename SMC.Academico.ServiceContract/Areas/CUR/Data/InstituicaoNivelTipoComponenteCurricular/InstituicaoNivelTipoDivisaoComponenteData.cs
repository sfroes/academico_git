using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class InstituicaoNivelTipoDivisaoComponenteData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoDivisaoComponente { get; set; }

        //[SMCMapProperty("TipoDivisaoComponente.TipoGestaoDivisaoComponente")]
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }

        public long? SeqTipoTrabalho { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public string DescricaoTipoEventoAgd { get; set; }

        public bool PermiteCargaHorariaGrade { get; set; }

    }
}
