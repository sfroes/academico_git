using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ITitulacaoDocumentoComprobatorioService : ISMCService
    {
        /// <summary>
        /// Busca os tipos de documentos associados à titulação informada
        /// </summary>
        /// <param name="seqTitulacao">Sequencial da titulação</param>
        /// <returns>Dados dos tipos de documentos associadoas à titulação informada</returns>
        List<SMCDatasourceItem> BuscarTitulacaoDocumentosComprobatorios(long seqTitulacao);
    }
}