namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    /// <summary>
    /// Representa o valor em créditou e carga horária de uma divisão de uma matriz curricular
    /// </summary>
    public class DivisaoMatrizCurricularValorVO
    {
        public short NumeroDivisao { get; set; }

        public int Creditos { get; set; }

        public int HorasAula { get; set; }
    }
}