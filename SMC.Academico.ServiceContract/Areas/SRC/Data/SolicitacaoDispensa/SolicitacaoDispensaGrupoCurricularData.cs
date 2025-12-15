using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaGrupoCurricularData : ISMCMappable
    {
        public long Seq { get; set; }
        public bool BloquearTotalDispensado { get; set; }
        public long? SeqGrupoCurricular { get; set; }
        public short? QuantidadeDispensaGrupo { get; set; }
    }
}