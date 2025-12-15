using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IMatrizCurricularConfiguracaoComponenteService : ISMCService
    {
        /// <summary>
        /// Buscar uma matriz curricular configuracao componente pelo sequencial 
        /// </summary>
        /// <param name="seq">Sequencia do matriz curricular configuracao componente a ser recuperada</param>
        /// <returns>Matriz curricular configuracao componente recuperado</returns>
        MatrizCurricularConfiguracaoComponenteData BuscarMatrizCurricularConfiguracaoComponente(long seq);

        /// <summary>
        /// Buscar as matrizes curriculares configuracao componentes que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de matriz curricular configuracao componente</param>
        /// <returns>SMCPagerData com a lista de matriz curricular configuracao componente</returns>
        SMCPagerData<MatrizCurricularConfiguracaoComponenteData> BuscarMatrizesCurricularesConfiguracoesComponente(MatrizCurricularConfiguracaoComponenteFiltroData filtros);

        /// <summary>
        /// Salva uma matriz curricular configuracao componente
        /// </summary>
        /// <param name="matrizCurricularConfiguracaoComponente">Dados da matriz curricular configuracao componente a gravada</param>
        /// <returns>Sequencial da matriz curricular configuracao componente gravada</returns>
        long SalvarMatrizCurricularConfiguracaoComponente(MatrizCurricularConfiguracaoComponenteData matrizCurricularConfiguracaoComponente);

        /// <summary>
        /// Exclui todas divisões matriz curriculares grupo de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        void ExcluirMatrizCurricularConfiguracaoComponente(long seq);
    }
}
