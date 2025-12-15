using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IMatrizCurricularOfertaService : ISMCService
    {
        /// <summary>
        /// Busca as matrizes que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>KeyValue das matrizes com a chave e a descrição no formato
        /// [RN_CUR_016 - Exibição de descrição de oferta de matriz curricular] + [Situação atual da oferta de matriz]</returns>
        List<SMCDatasourceItem> BuscarMatrizesCurricularesOfertasPorCampanhaSelect(MatrizCurricularOfertaFiltroData filtros);

        /// <summary>
        /// Busca matriz curricular oferta
        /// </summary>
        /// <param name="seq">Sequencial da matriz curricular oferta</param>
        /// <returns>Matriz curricular oferta</returns>
        MatrizCurricularOfertaData BuscarMatrizCurricularOferta(long seq);

        /// <summary>
        /// Lista matriz curricular oferta de acordo com filtros
        /// </summary>
        /// <param name="filtro">Filtro matriz curricular oferta</param>
        /// <returns>Lista das matrizes curriculares ofertas</returns>
        List<MatrizCurricularOfertaData> BuscarMatrizesCurricularesOfertas(MatrizCurricularOfertaFiltroData filtro);

        /// <summary>
        /// Método que valida se alguma das matrizes de ofertas possuem vínculo com a hierarquia de entidade do usuário.
        /// </summary>
        /// <param name="seqs">Lista de seqs de MatrizCurricularOferta</param>
        /// <returns>True = Alguma das ofertas pode ser acessada pelo usuário | False = Nenhuma oferta pode ser acessada pelo usuário</returns>
        bool ValidarMatrizCurricularOfertas(List<long> seqs);

        /// <summary>
        /// Busca as matrizes curricular Ofertas que atendam os filtros informados e com a paginação correta
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns>SMCPagerData com a lista de matrizes curricular Ofertas</returns>
        SMCPagerData<MatrizCurricularOfertaData> BuscarMatrizesCurricularLookupOferta(MatrizCurricularOfertaFiltroData filtros);

        /// <summary>
        /// Busca a matrize curricular com a oferta selecionada para retorno do lookup
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <returns>Objeto matriz curricular oferta</returns>
        MatrizCurricularOfertaData BuscarMatrizesCurricularLookupOfertaSelecionado(long seqMatrizCurricularOferta);

        /// <summary>
        /// Valida se a matriz curricular oferta possui umas associação com a configuração de componente da turma
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da matriz curricular oferta</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>True: Se a oferta estiver associada a configuração de componente, caso contrário False</returns>
        bool ValidarMatrizCurricularOfertaConfiguracaoComponente(long seqMatrizCurricularOferta, long seqConfiguracaoComponente);
    }
}