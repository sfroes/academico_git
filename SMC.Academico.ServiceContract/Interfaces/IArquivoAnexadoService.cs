using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;
using System.Collections.Generic;
using System.IO;
using System;
using SMC.Academico.ServiceContract.Data;

namespace SMC.Academico.ServiceContract.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IArquivoAnexadoService : ISMCService
    {
        /// <summary>
        /// Busca um arquivo anexado por seu sequencial.
        /// </summary>
        /// <param name="seq">O sequencial do arquivo.</param>
        /// <returns>Dados do arquivo anexado</returns>
        SMCUploadFile BuscarArquivoAnexado(long seq);

        /// <summary>
        /// Busca um arquivo anexado por seu sequencial.
        /// </summary>
        /// <param name="seq">O sequencial do arquivo.</param>
        /// <returns>Dados do arquivo anexado convertido para um data</returns>
        ArquivoAnexadoData BuscarArquivoAnexadoData(long seq);

        /// <summary>
        /// Busca um arquivo anexado por seu guid.
        /// </summary>
        /// <param name="uidArquivo">O guid do arquivo.</param>
        /// <returns>Dados do arquivo anexado</returns>
        SMCUploadFile BuscarArquivoAnexadoPorGuid(Guid uidArquivo);

        /// <summary>
        /// Salva um arquivo no banco de dados
        /// </summary>
        /// <param name="arquivos"></param>
        /// <returns></returns>
        long SalvarArquivo(SMCUploadFile arquivo);

        /// <summary>
        /// Busca vários arquivos, compacta e retorna o vetor de bytes
        /// </summary>
        FileInfo BuscarECompactarArquivos(IEnumerable<long> seqs);
    }
}
