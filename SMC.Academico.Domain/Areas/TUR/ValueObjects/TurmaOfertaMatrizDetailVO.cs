using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaOfertaMatrizDetailVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public bool OfertaMatrizPrincipal { get; set; }

        public short? QuantidadeVagasReservadas { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public short? ReservaVagas { get; set; }

        public string DivisaoCompleto { get; set; }

        public string OfertaCompleto
        {
            get
            {
                return string.IsNullOrEmpty(DescricaoComplementarMatrizCurricular) ?
                        $"{DescricaoMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}"
                      : $"{DescricaoMatrizCurricular} - {DescricaoComplementarMatrizCurricular} - {DescricaoLocalidade} - {DescricaoTurno}";
            }
        }

        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoUnidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string Codigo { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }
    }
}
