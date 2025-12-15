using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORT.DomainServices
{
    public class PublicacaoBdpAutorizacaoDomainService : AcademicoContextDomain<PublicacaoBdpAutorizacao>
    {
        public PublicacaoBdpAutorizacao BuscarDadosAutorizacaoAtiva(long seqPublicacaoBdp)
        {
            var spec = new PublicacaoBdpAutorizacaoFilterSpecification() { SeqPublicacaoBdp = seqPublicacaoBdp };
            spec.SetOrderBy(a => a.DataAutorizacao);
            var lista = this.SearchBySpecification(spec).ToList();
            return lista.LastOrDefault(a => a.DataAutorizacao < DateTime.Now);
        }

        public void ExcluirAutorizacoes(long seqPublicacaoBdp)
        {
            var spec = new PublicacaoBdpAutorizacaoFilterSpecification() { SeqPublicacaoBdp = seqPublicacaoBdp };
            var autorizacoes = this.SearchBySpecification(spec).ToList();
            foreach (var autorizacao in autorizacoes)
            {
                this.DeleteEntity(autorizacao);
            }
        }

    }
}