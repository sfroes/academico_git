namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarComponentesAprovadoDispensadoVO
    {
        public long? SeqComponenteCurricular { get; set; }
        public long? SeqComponenteCurricularAssunto { get; set; }
        public string DescricaoComponente { get; set; }
        public string DescricaoAssunto { get; set; }
    }
}