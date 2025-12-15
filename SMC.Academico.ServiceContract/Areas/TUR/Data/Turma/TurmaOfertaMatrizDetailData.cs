using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaOfertaMatrizDetailData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public bool OfertaMatrizPrincipal { get; set; }

        public short? QuantidadeVagasReservadas { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public short? ReservaVagas { get; set; }

        public string DivisaoCompleto { get; set; }

        public string OfertaCompleto { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }


        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoComplementarMatrizCurricular { get; set; }
    }
}
