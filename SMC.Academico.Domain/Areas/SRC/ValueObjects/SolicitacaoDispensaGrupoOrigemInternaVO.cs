using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaGrupoOrigemInternaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqSolicitacaoDispensaOrigemInterna { get; set; }

        public string Descricao { get; set; }
    }
}