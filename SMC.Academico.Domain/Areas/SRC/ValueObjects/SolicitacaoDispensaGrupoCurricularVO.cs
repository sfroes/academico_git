using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaGrupoCurricularVO : ISMCMappable
    {
        public long Seq { get; set; }
        public bool BloquearTotalDispensado { get; set; }
        public long? SeqGrupoCurricular { get; set; }
        public short? QuantidadeDispensaGrupo { get; set; }
    }
}