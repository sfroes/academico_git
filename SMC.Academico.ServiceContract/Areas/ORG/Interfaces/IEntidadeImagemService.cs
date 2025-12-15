using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IEntidadeImagemService : ISMCService
    {
        
        /// <summary>
        /// Busca uma entidade imagem
        /// </summary>
        /// <param name="seq">Sequencial da entidade imagem</param>
        /// <returns>Dados da entidade imagem com o sequencial informado</returns>
        EntidadeImagemData BuscarEntidadeImagem(long seq);

        /// <summary>
        /// Busca as entidades imagens de uma entidade
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa das imagens de uma entidade</param>
        /// <returns>Lista de imagens desta entidade</returns>
        SMCPagerData<EntidadeImagemData> BuscarEntidadeImagens(EntidadeImagemFiltroData filtros);

        /// <summary>
        /// Buscar o logotipo conforme a entidade e o tipo de imagem
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade a ser recuperada</param>
        /// <param name="tipoImagem">Tipo da imagem a ser recuperada</param>
        [OperationContract]
        SMCUploadFile BuscarImagemEntidade(long seqEntidade, TipoImagem tipoImagem);

        /// <summary>
        /// Salva uma entidade imagem
        /// </summary>
        /// <param name="entidadeImagemData">Dados da imagem a ser salva</param>
        /// <returns>Sequencial da imagem salva</returns>
        long SalvarEntidadeImagem(EntidadeImagemData entidadeImagemData);


        /// <summary>
        /// Exclui uma entidade imagem
        /// </summary>
        /// <param name="seq">Sequencial da entidade imagem para exclusão</param>
        void ExcluirEntidadeImagem(long seq);
       
    }
}
