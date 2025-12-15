using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPessoaAtuacaoDocumentoService : ISMCService
    {
        ///<summary>
        ///Lista os Tipos de Documentos para popular o Select
        ///</summary>
        ///<returns></returns>Lista de valores para o Select<returns>
        //[OperationContract]
        //List<SMCDatasourceItem> BuscarTiposDocumentoSelect(long seqPessoaAtuacao);

        ///<summary>
        ///Lista todos os Tipos de Documentos para popular o Select
        ///</summary>
        ///<returns></returns>Lista de valores para o Select<returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarDocumentosSelect(long seqPessoaAtuacao, long? seq);

        /// <summary>
        /// Busca uma lista paginada de tipos de documentos pelos filtros informados
        /// </summary>
        /// <param name="filtro">Filtros de pesquisa</param>
        /// <returns>Lista paginada de tipos de documentos</returns>
        [OperationContract]
        SMCPagerData<PessoaAtuacaoDocumentoListarData> BuscarTiposDocumentoLista(PessoaAtuacaoDocumentoFiltroData filtro);

        /// <summary>
        /// Busca documento(s) que geraram bloqueio para uma pessoa atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao"></param>
        /// <returns>Retorna documento(s)</returns>
        [OperationContract]
        List<string> BuscarItensBloqueio(long seqPessoaAtuacao);

        //<summary>
        //Exclui um documento pelo sequencial
        //</summary>
        ///<param name="seqPessoaAtuacaoDocumento"></param>
        void ExcluirDocumento(long seqPessoaAtuacaoDocumento);

        //<summary>
        //Verifica se um documento é obrigatório
        //</summary>
        /// <param name="seqTipoDocumento"></param>
        ///  /// <param name="seqPessoaAtuacao"></param>
        bool VerificaDocumentoObrigatorio(long seqTipoDocumento, long seqPessoaAtuacao);

        ///<summary>
        ///Associa um bloqueio para a pessoa-atuação 
        ///<param name="seqPessoaAtuacao"></param>
        ///<param name="seqTipoDocumento"></param>
        ///</summary>
        void GerarBloqueio(long seqPessoaAtuacao, long seqTipoDocumento);

        /// <summary>
        /// Salvar o documento da pessoa atuação
        /// <param name="pessoaAtuacaoDocumento">Modelo da pessoa atuação documento a ser salvo</param>        
        /// </summary>
        long SalvarDocumento(PessoaAtuacaoDocumentoData pessoaAtuacaoDocumento);

        /// <summary>
        /// Verifica se há serviço em aberto relacionado ao documento da pessoa
        /// <param name="seqPessoaAtuacao"></param>      
        /// </summary>
        bool VerificarServico(long seqPessoaAtuacao);
    }

}
