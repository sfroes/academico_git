using SMC.Academico.Domain.Areas.CUR.ValueObjects;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaDuplicadaVO
    {
        public int Codigo { get; set; }

        public short Numero { get; set; }

        public string Descricao { get; set; }

        public long? SeqTurma { get; set; }

        public ComponenteCurricularVO ComponenteCurricular { get; set; }
    }
}
