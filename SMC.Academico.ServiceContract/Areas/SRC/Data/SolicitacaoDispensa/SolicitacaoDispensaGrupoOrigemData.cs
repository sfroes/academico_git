using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaGrupoOrigemData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqSolicitacaoDispensaGrupo { get; set; }

        [SMCMapProperty("SeqSolicitacaoDispensaOrigemInterna")]
        [SMCMapProperty("SeqSolicitacaoDispensaOrigemExterna")]
        public long SeqSolicitacaoDispensaOrigem { get; set; }
    }
}