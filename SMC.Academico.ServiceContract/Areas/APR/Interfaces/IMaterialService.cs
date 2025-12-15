using System;
using System.Collections.Generic;
using System.ServiceModel;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IMaterialService : ISMCService
    {
        /// <summary>
        /// Lista materiais (didáticos ou arquivos) de uma determinada origem (turma, entidade, etc)
        /// </summary>
        /// <param name="filtro">Filtros para recuperar a origem do material</param>
        /// <returns>Lista de materiais</returns>
        [OperationContract]
        MaterialData[] ListarMateriais(MaterialFiltroData filtro);

        [OperationContract]
        MaterialData[] ListarMateriaisParaDownload(DownloadMaterialParametrosData parametros);

        long SalvarMaterial(MaterialData materiais);

        MaterialData InserirMaterial(MaterialFiltroData filtro);

        [OperationContract]
        MaterialData AlterarMaterial(long seq);

        OrigemMaterialData BuscarOrigemMaterial(long seqOrigemMaterial);

        void ExcluirMaterial(long seq);

        /// <summary>
        /// Busca o sequencial de um arquivo anexado com base no sequencial do material.
        /// </summary>
        /// <param name="seqMaterial">Sequencial do materuial.</param>
        /// <returns>Sequencial do arquivo anexado.</returns>
        long BuscarSequencialArquivoAnexado(long seqMaterial);

        /// <summary>
        /// Busca o uid de um arquivo anexado com base no sequencial do material.
        /// </summary>
        /// <param name="seqMaterial">Sequencial do material.</param>
        /// <returns>Uid do arquivo anexado.</returns>
        Guid BuscarUidArquivoAnexado(long seqMaterial);

        /// <summary>
        /// Gera um arquivo .zip para download.
        /// </summary>
        /// <param name="seqsMateriais">Sequenciais dos materiais do tipo arquivo para extrair os arquivos e gerar o arquivo .zip.</param>
        /// <returns>Arquivo .zip dos materiais em questão para download.</returns>
        [OperationContract]
        SMCFile DownloadMateriais(List<long> seqsMateriais);
    }
}