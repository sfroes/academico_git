using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDispensaService : ISMCService
    {
        /// <summary>
        /// Busca uma dispensa com seus respectivos grupos de componentes
        /// Estruturados de acordo com as definições de tela
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        /// <returns>Objeto dispensa com seus detalhes</returns>
        DispensaData BuscarDispensa(long seq);

        /// <summary>
        /// Busca as dispensas que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros">Filtros da listagem de dispensas</param>
        /// <returns>SMCPagerData com a lista de dispensas</returns>
        SMCPagerData<DispensaData> BuscarDispensas(DispensaFiltroData filtros);

        /// <summary>
        /// Grava uma dispensa com seus respectivos grupos de componentes
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        long SalvarDispensa(DispensaData dispensa);

        /// <summary>
        /// Grava uma dispensa com suas matrizes de exceção
        /// </summary>
        /// <param name="dispensa">Dados da dispensa a ser gravado</param>
        /// <returns>Sequencial da dispensa gravado</returns>
        long SalvarDispensaMatriz(DispensaData dispensa);

        /// <summary>
        /// Exclui uma dispensa
        /// </summary>
        /// <param name="seq">Sequencial da dispensa</param>
        void ExcluirDispensa(long seq);
    }
}
