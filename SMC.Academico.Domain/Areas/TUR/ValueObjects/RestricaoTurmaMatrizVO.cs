using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class RestricaoTurmaMatrizVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
                
        public long SeqTurmaConfiguracaoComponente { get; set; }
                
        public long? SeqMatrizCurricularOferta { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }
        
        public bool OfertaMatrizPrincipal { get; set; }

        public short? QuantidadeVagasReservadas { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }
    }
}
