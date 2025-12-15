using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ServicoTipoNotificacaoDomainService : AcademicoContextDomain<ServicoTipoNotificacao>
    {
        public List<ServicoTipoNotificacao> BuscarListaServicoTipoNotificacao(ServicoTipoNotificacaoFilterSpecification spec, bool apenasObrigatorios = false)
        {
            var lista = this.SearchBySpecification(spec).ToList();

            if (apenasObrigatorios)
            {
                lista = lista.Where(c => c.Obrigatorio == true).ToList();
            }

            return lista;
        }

        public List<ServicoTipoNotificacao> BuscarListaServicoTiposNotificacaoNaEtapa(long seqServico, long seqEtapa, bool obrigatorio = false)
        {
            return null;
        }
    }
}