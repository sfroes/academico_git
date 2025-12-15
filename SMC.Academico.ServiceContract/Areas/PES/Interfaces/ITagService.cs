using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface ITagService : ISMCService
    {
        /// <summary>
        /// Buscar os dados da tag pelo seq
        /// </summary>
        /// <returns>Retorna a tag</returns>
        TagData BuscarTag(long seq);


        /// <summary>
        /// Salva os dados da tag
        /// </summary>
        /// <param name="tag">Dados da tag</param>
        /// <returns>Sequencial do aluno atualizado</returns>
        long SalvarTag(TagData tag);

        bool ExibirMensagem(TagData tag);
    }
}