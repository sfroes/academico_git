using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class TitulacaoDocumentoComprobatorioService : SMCServiceBase, ITitulacaoDocumentoComprobatorioService
    {
        #region [ DomainService ]

        private TitulacaoDocumentoComprobatorioDomainService TitulacaoDocumentoComprobatorioDomainService => Create<TitulacaoDocumentoComprobatorioDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os tipos de documentos associados à titulação informada
        /// </summary>
        /// <param name="seqTitulacao">Sequencial da titulação</param>
        /// <returns>Dados dos tipos de documentos associadoas à titulação informada</returns>
        public List<SMCDatasourceItem> BuscarTitulacaoDocumentosComprobatorios(long seqTitulacao)
        {
            return TitulacaoDocumentoComprobatorioDomainService.BuscarTitulacaoDocumentosComprobatorios(seqTitulacao);
        }
    }
}