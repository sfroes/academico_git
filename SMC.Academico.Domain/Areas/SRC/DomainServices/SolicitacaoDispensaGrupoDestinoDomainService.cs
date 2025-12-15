using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoDispensaGrupoDestinoDomainService : AcademicoContextDomain<SolicitacaoDispensaGrupoDestino>
    {
        public void Excluir(long seqSolicitacaoDispensaDestino)
        {
            var spec = new SolicitacaoDispensaGrupoDestinoFilterSpecification() { SeqSolicitacaoDispensaDestino = seqSolicitacaoDispensaDestino };
            var solicitacaoDispensaGrupoDestino = this.SearchByKey(spec);

            this.DeleteEntity(solicitacaoDispensaGrupoDestino);
        }
    }
}
