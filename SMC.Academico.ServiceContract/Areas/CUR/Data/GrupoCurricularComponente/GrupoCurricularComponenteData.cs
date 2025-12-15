using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularComponenteData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoGrupoCurricular { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public FormatoCargaHoraria? Formato { get; set; }

        public string DescricaoFormatada { get; set; }
    }
}