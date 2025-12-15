using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class DocumentoConclusaoApostilamentoService : SMCServiceBase, IDocumentoConclusaoApostilamentoService
    {
        #region [ DomainService ]

        private DocumentoConclusaoApostilamentoDomainService DocumentoConclusaoApostilamentoDomainService => Create<DocumentoConclusaoApostilamentoDomainService>();

        #endregion [ DomainService ]

        public DocumentoConclusaoApostilamentoData BuscarDocumentoConclusaoApostilamento(long seq)
        {
            return DocumentoConclusaoApostilamentoDomainService.BuscarDocumentoConclusaoApostilamento(seq).Transform<DocumentoConclusaoApostilamentoData>();
        }

        public SMCPagerData<DocumentoConclusaoApostilamentoListarData> BuscarDocumentosConclusaoApostilamento(DocumentoConclusaoApostilamentoFiltroData filtro)
        {
            return DocumentoConclusaoApostilamentoDomainService.BuscarDocumentosConclusaoApostilamento(filtro.Transform<DocumentoConclusaoApostilamentoFiltroVO>()).Transform<SMCPagerData<DocumentoConclusaoApostilamentoListarData>>();
        }

        public long Salvar(DocumentoConclusaoApostilamentoData modelo)
        {
            return DocumentoConclusaoApostilamentoDomainService.SalvarDocumentoConclusaoApostilamento(modelo.Transform<DocumentoConclusaoApostilamentoVO>());
        }

        public void Excluir(long seq)
        {
            this.DocumentoConclusaoApostilamentoDomainService.ExcluirDocumentoConclusaoApostilamento(seq);
        }
    }
}
