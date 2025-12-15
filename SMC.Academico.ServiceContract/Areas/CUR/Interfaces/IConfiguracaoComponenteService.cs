using SMC.Academico.Common.Areas.CUR.Exceptions;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IConfiguracaoComponenteService : ISMCService
    {
        /// <summary>
        /// Buscar os dados de configuração do componente
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Registro da configuração do componente</returns>
        ConfiguracaoComponenteData BuscarConfiguracaoComponente(long seq);

        /// <summary>
        /// Buscar os dados de configuração inicial de acordo com o componente curricular
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Configuração inicial do componente curricular</returns>
        ConfiguracaoComponenteData BuscarConfiguracaoComponenteCurricular(long seqComponenteCurricular);

        /// <summary>
        /// Buscar as organizações cadastradas para o componente que podem ser selecionadas no cadastro de configuração - divisão
        /// </summary>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Lista com as organizações</returns>
        List<SMCDatasourceItem> BuscarComponenteOrganizacoesSelect(long seqComponenteCurricular);

        /// <summary>
        /// Buscar as configurações do componente curricular de um grupo curricular componente
        /// </summary>
        /// <param name="seqGrupoCurricularComponente">Sequencial do grupo curricular compoente</param>
        /// <returns>Lista com as organizações</returns>
        List<SMCDatasourceItem> BuscarConfiguracaoComponentePorGrupoCurricularComponenteSelect(long seqGrupoCurricularComponente);

        /// <summary>
        /// Buscar as configurações de componente, necessario para preencher o combo de entidades descrição na listagem
        /// </summary>
        /// <param name="filtros">Filtros definidos para a configuração de componente</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        SMCPagerData<ConfiguracaoComponenteListaData> BuscarConfiguracoesComponentes(ConfiguracaoComponenteFiltroData filtros);

        /// <summary>
        /// Buscar as configurações de componente para o lookup
        /// </summary>
        /// <param name="filtros">Filtros definidos para a configuração de componente</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        SMCPagerData<ConfiguracaoComponenteLookupListaData> BuscarConfiguracoesComponentesLookup(ConfiguracaoComponenteFiltroData filtros);

        /// <summary>
        /// Salvar a configuração do componente
        /// </summary>
        /// <param name="configuracaoComponenteData"></param>
        /// <returns>Sequencial da configuração do componente</returns>
        /// <exception cref="ConfiguracaoComponenteCargaHorariaDivergenteException">RN_CUR_079 validação de carga horária dos itens de organização</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoAlteracaoProibidaException">Caso seja alterado algum campo não permitido para uma configuração já associada</exception>
        /// <exception cref="ConfiguracaoComponenteDivisaoCargaHorariaDivergenteException">Validação da carga horária das divisões</exception>
        /// <exception cref="ConfiguracaoComponenteInativoException">RN_CUR_083 - Consistência configuração componente</exception>
        /// <exception cref="ConfiguracaoComponenteOrganizacaoInvalidaException">Caso seja informado um tipo de organização inválido</exception>
        /// <exception cref="ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException">Caso sera informado um tipo de divisão inválido</exception>
        long SalvarConfiguracaoComponente(ConfiguracaoComponenteData configuracaoComponenteData);

        /// <summary>
        /// Busca as configurações de componente de acordo com a matriz curricular oferta da pesso atuação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>     
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matricula</param>
        /// <returns>Lista configuração de componentes</returns>
        SMCPagerData<ConfiguracaoComponenteListaData> BuscarConfiguracaoComponentePessoaAtuacaoEntidade(long seqPessoaAtuacao, long seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se exige a configuração de componente pertence a um curriculo
        /// </summary>
        /// <param name="seq">Sequencial da configuração do componente</param>
        /// <returns>Valor Sim caso a configuração de componente esteja associado um grupo de componente com matriz curricular</returns>
        bool VerificaConfiguracaoComponentePertenceCurriculo(long seq);

        /// <summary>
        /// Busca os componentes curriculares por cicloLetivo e curso oferta localidade
        /// </summary>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade</param>
        /// <param name="seqTurnoe">Sequencial do turno</param>
        /// <returns>Configuracoes de compomente encontradas</returns>
        List<SMCDatasourceItem> BuscarConfiguracoesComponentePorCicloLetivoCursoOfertaLocalidadeSelect(long? seqCicloLetivo, long? seqCursoOfertaLocalidade, long? seqTurno);

        /// <summary>
        /// Busca a descrição de uma configuração de componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns></returns>
        string BuscarDescricaoConfiguracaoComponente(long seqConfiguracaoComponente);

        /// <summary>
        /// Buscar dados do cabeçalho configuração do componente por matriz
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial matriz curricular</param>
        ConfiguracaoComponeteMatrizCabecalhoData BuscarCabecalhoConfiguracaoComponentePorMatriz(long seqMatrizCurricular);
    }
}