using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ITipoConfiguracaoGrupoCurricularService : ISMCService
    {
        /// <summary>
        /// Salva o registro na base
        /// </summary>
        /// <param name="tipoConfiguracaoGrupoCurricularData">Dados do registro a ser gravado</param>
        /// <returns>Sequencial do registro gravado</returns>
        long SalvarTipoConfiguracaoGrupoCurricular(TipoConfiguracaoGrupoCurricularData tipoConfiguracaoGrupoCurricularData);

        /// <summary>
        /// Recupera um tipo de configuração pelo seu sequencial
        /// </summary>
        /// <param name="seq">Sequencial do tipo de configuração</param>
        /// <returns>Dados do tipo de configuração</returns>
        TipoConfiguracaoGrupoCurricularData BuscarTipoConfiguracaoGrupoCurricular(long seq);

        /// <summary>
        /// Exclui o registro de acordo com a seq fornecida
        /// </summary>
        /// <param name="seq">Sequencial do registro a ser excluído</param>
        void ExcluirTipoConfiguracaoGrupoCurricular(long seq);

        /// <summary>
        /// Lista todos os grupos curriculares filhos do grupo curricular informado
        /// </summary>
        /// <param name="seqTipoConfiguracaoGrupoCurricularSuperior">Sequencial do grupo curricular superior, caso não seja informado serão listados grupos raiz</param>
        /// <returns>Grupos cadastrados como subgrupo do grupo informado ou grupos marcados como raiz caso o grupo superior não seja informado</returns>
        List<SMCDatasourceItem> BuscarTiposConfiguracaoGrupoCurricularSelect(long? seqTipoConfiguracaoGrupoCurricularSuperior);
    }
}
