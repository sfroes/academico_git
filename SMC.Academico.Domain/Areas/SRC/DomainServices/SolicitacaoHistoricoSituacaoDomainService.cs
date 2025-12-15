using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Helpers;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoHistoricoSituacaoDomainService : AcademicoContextDomain<SolicitacaoHistoricoSituacao>
    {
        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService
        {
            get { return this.Create<SolicitacaoServicoEtapaDomainService>(); }
        }

        private IngressanteDomainService IngressanteDomainService
        {
            get { return this.Create<IngressanteDomainService>(); }
        }

        public void AtualizarHistoricoSituacao(long seqSolicitacaoServicoEtapa, long seqSituacaoEtapaSGF, string observacao = null)
        {
            // Recupera qual a solicitação e a etapa
            var solicitacaoServicoEtapa = SolicitacaoServicoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServicoEtapa>(seqSolicitacaoServicoEtapa), x => new
            {
                SeqEtapaSGF = x.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqPessoaAtuacao = x.SolicitacaoServico.SeqPessoaAtuacao,
                SeqSolicitacaoServico = x.SolicitacaoServico.Seq
            });

            var etapas = SGFHelper.BuscarEtapas(solicitacaoServicoEtapa.SeqSolicitacaoServico).OrderBy(e => e.OrdemEtapaSGF);
            var etapaAtual = etapas.FirstOrDefault(e => e.SeqEtapaSGF == solicitacaoServicoEtapa.SeqEtapaSGF);
            var situacaoAtualizar = etapaAtual.Situacoes.FirstOrDefault(s => s.Seq == seqSituacaoEtapaSGF);
            var categoriaSituacao = situacaoAtualizar.CategoriaSituacao;

            var historico = new SolicitacaoHistoricoSituacao();
            historico.SeqSolicitacaoServicoEtapa = seqSolicitacaoServicoEtapa;
            historico.SeqSituacaoEtapaSgf = seqSituacaoEtapaSGF;
            historico.CategoriaSituacao = categoriaSituacao;
            historico.Observacao = observacao;

            this.SaveEntity(historico);
        }
    }
}