namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    /// <summary>
    /// POCO para ajudar na criação de um ingressante.
    /// </summary>
    public class IngressanteTiposTermoIntercambioConfigVO
    {
        public long SeqTipoParceriaIntercambio { get; set; }

        public bool ExigePeriodoIngresso { get; set; }
    }
}