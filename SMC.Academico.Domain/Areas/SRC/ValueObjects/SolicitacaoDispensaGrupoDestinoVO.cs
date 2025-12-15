using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaGrupoDestinoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }
        public long SeqSolicitacaoDispensaGrupo { get; set; }
    }
}