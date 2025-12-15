using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IInstituicaoNivelTipoComponenteCurricularService : ISMCService
    {
        /// <summary>
        /// Busca a instituição nível tipo componente curricular pelo sequencia para popular o datasource de TiposDivisão
        /// </summary>
        /// <param name="seq">Sequencial do Instituição Nivel Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricular(long seq);

        /// <summary>
        /// Busca a lista de Tipo Componente Curricular de acordo com a Instituição e Nivel Ensino para popular um Select
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial Instituição Nível Ensino</param>
        /// <returns>Lista de Tipo Componente Curricular</returns>
        List<SMCDatasourceItem> BuscarTipoComponenteCurricularSelect(long seqInstituicaoNivelResponsavel);

        /// <summary>
        /// Busca a lista dos tipos de componente curriculares associados ao tipo do grupo curricular informado
        /// </summary>
        /// <param name="seqGrupoCurricular">Sequencial do grupo currícular</param>
        /// <returns>Tipos de componentes associados ao tipo do grupo curricular informado</returns>
        List<SMCDatasourceItem> BuscarTipoComponenteCurricularPorGrupoSelect(long seqGrupoCurricular);

        /// <summary>
        /// Busca a configuração do tipo componente curricular de acordo com a instituição nível ensino e  do tipo componente
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial do Instituição Nivel Ensino selecionado</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial do Tipo Componente Curricular selecionado</param>
        /// <returns>Instituicao nivel tipo componente curricular</returns>
        InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(long seqInstituicaoNivel, long seqTipoComponenteCurricular);

        /// <summary>
        /// Busca um componente curricular de um nível de ensino e com uma divisão com a gestão do tipo informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nivel Ensino selecionado</param>
        /// <param name="tipoGestaoDivisaoComponente">Tipo de gestão de uma das divisões do tipo de componente</param>
        /// <returns>Dados do tipo do compomente</returns>
        InstituicaoNivelTipoComponenteCurricularData BuscarInstituicaoNivelTipoComponenteCurricularGestaoDivisao(long seqNivelEnsino, TipoGestaoDivisaoComponente tipoGestaoDivisaoComponente);

        /// <summary>
        /// Busca a lista de Entidades de acordo com a Instituição e Nivel Ensino para popular um Select
        /// </summary>
        /// <param name="seqInstituicaoNivelResponsavel">Sequencial Instituição Nível Ensino</param>
        /// <param name="seqTipoComponenteCurricular">Sequencial Tipo Componente</param>
        /// <returns>Lista de Entidades do mesmo tipo</returns>
        List<SMCDatasourceItem> BuscarEntidadesPorTipoComponenteSelect(long seqInstituicaoNivelResponsavel, long seqTipoComponenteCurricular);

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de aceita dispensa
        /// </summary>
        /// <returns>Lista de sequenciais tipos componente curricular</returns>
        List<long> BuscarTipoComponenteCurricularDispensa();

        /// <summary>
        /// Salva um componente curricular de um nível de ensino e com suas divisões com a gestão dos tipos informados
        /// </summary>
        /// <param name="model">Modelo a ser persistido</param>
        long SalvarInstituicaoNivelTipoComponenteCurricular(InstituicaoNivelTipoComponenteCurricularData model);

        /// <summary>
        /// Busca tipo componente curricular configurado na instituição nivel pela matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da Matriz Curricular</param>
        /// <returns>Tipos compontente configurados instituição nivel pela matriz</returns>
        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoComponenteMatrizCurricularSelect(long seqMatrizCurricular);
    }
}