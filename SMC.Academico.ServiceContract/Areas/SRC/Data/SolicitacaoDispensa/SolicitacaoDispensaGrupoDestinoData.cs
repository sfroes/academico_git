using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaGrupoDestinoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqSolicitacaoDispensaGrupo { get; set; }

        public long SeqSolicitacaoDispensaDestino { get; set; }
    }
}