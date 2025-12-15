using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ITipoDivisaoComponenteService : ISMCService
    {
        /// <summary>
        /// Buscar o Tipo Divisão Componente
        /// </summary>
        /// <param name="seqTipoDivisaoComponente">Sequencial do Tipo Divisão Componente</param>
        /// <returns>Tipo Divisão Componente</returns>
        TipoDivisaoComponenteData BuscarTipoDivisaoComponente(long seqTipoDivisaoComponente);

        /// <summary>
        /// Busca os tipos de divisão de componente de um tipo de componente
        /// </summary>
        /// <param name="seqTipoComponenteCurricular">Sequencial do tipo de componente</param>
        /// <returns>Lista de tipos de divisão para select</returns>
        List<SMCDatasourceItem> BuscarTipoDivisaoComponenteSelect(long seqTipoComponenteCurricular);

        /// <summary>
        /// Busca os tipo divisão componente de acordo com o componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencia do Componente Curricular selecionado</param>
        /// <returns>Lista de tipos de divisão do componente</returns>
        List<SMCDatasourceItem> BuscarTipoDivisaoComponentePorComponenteSelect(long seqComponenteCurricular);

        /// <summary>
        /// Busca o tipo divisão componente por divisão de componente
        /// </summary>
        /// <param name="seqDivisaoComponente">Sequencia do divisão componente</param>
        /// <returns>Dados tipo divisão componente</returns>
        TipoDivisaoComponenteData BuscarTipoDivisaoComponentePorDivisaoComponente(long seqDivisaoComponente);

        /// <summary>
        /// Busca os tipo componente curricular de acordo com o parâmetro de tipos gestão divisão componente
        /// </summary>
        /// <param name="tiposGestaoDivisaoComponente">Tipos de gestão divisão componente informados como parâmetro</param>
        /// <returns>Lista de sequenciais tipos componente curricular</returns>
        List<long> BuscarTipoComponenteCurricularPorTipoGestaoDivisaoComponente(TipoGestaoDivisaoComponente[] tiposGestaoDivisaoComponente);

        /// <summary>
        /// Listar, em ordem alfabética, todos os tipos de divisão de componente que estejam associados às divisões de
        /// configurações de componentes do tipo "atividade complementar" e associados ao grupos curriculares do currículo do
        /// aluno em questão.
        /// </summary>
        List<SMCDatasourceItem> BuscarTiposDivisaoComponenteAlunoComGestao(long seqAluno, TipoGestaoDivisaoComponente tipoGestao);
    }
}