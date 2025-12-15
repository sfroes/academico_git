using System.Collections.Generic;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IEntidadeService : ISMCService
    {
        /// <summary>
        /// Buscar uma lista de entidades
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades</returns>
        SMCPagerData<EntidadeListaData> BuscarEntidades(EntidadeFiltroData filtros);

        /// <summary>
        /// Buscar uma lista de entidades para o lookup de entidades seleção multipla
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades</returns>
        SMCPagerData<EntidadeListaData> BuscarEntidadesLookupSelecaoMultipla(EntidadeFiltroData filtros);

        /// <summary>
        /// Buscar uma lista de entidades para lookup
        /// </summary>
        /// <param name="filtros">Filtros para pesquisa</param>
        /// <returns>Lista de entidades ordenadas pela descrição do tipo e nome da entidade</returns>
        SMCPagerData<EntidadeListaData> BuscarEntidadesLookup(long seq);

        /// <summary>
        /// Busca todas as entidades que sejam de tipos vinculáveis aos colaboradores na instituição atual.
        /// </summary>
        /// <param name="ignorarFiltros">Quando setado ignora todos filtros exceto o filtro de instituição</param>
        /// <returns>Sequenciais e nomes das entidades encontradas</returns>
        List<SMCDatasourceItem> BuscarEntidadesVinculoColaboradorSelect(bool ignorarFiltros);

        /// <summary>
        /// Busca uma entidade
        /// </summary>
        /// <param name="seq">Sequencial da entidade</param>
        /// <returns>Dados da entidade com sequencial informado</returns>
        EntidadeData BuscarEntidade(long seq);

        /// <summary>
        /// Busca os dados para serem apresentados em cabeçalho para uma entidade
        /// </summary>
        /// <param name="seq">Sequencial da entidade</param>
        /// <returns>DTO com os dados de cabeçalho da entidade</returns>
        EntidadeCabecalhoData BuscarEntidadeCabecalho(long seq);

        /// <summary>
        /// Salva uma entidade
        /// </summary>
        /// <param name="entidade">Dados da entidade a ser salva</param>
        /// <returns>Sequencial da entidade salva</returns>
        long SalvarEntidade(EntidadeData entidade);

        /// <summary>
        /// Exclui uma entidade
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade para exclusão</param>
        void ExcluirEntidade(long seqEntidade);

        /// <summary>
        /// Busca as configurações de entidade para cadastro de uma entidade externada
        /// </summary>
        /// <param name="tokenEntidadeExternada">Token do tipo de entidade externada</param>
        /// <returns>Dados da configuração de entidade</returns>
        EntidadeData BuscaConfiguracoesEntidadeExternada(string tokenEntidadeExternada);

        /// <summary>
        /// Busca as configurações de um tipo de entidade com as configurações de classificação
        /// </summary>
        /// <param name="tokenEntidade">Token do tipo da entidade</param>
        /// <returns>Configurações da entidade com a lista de hierarquias de entidades</returns>
        EntidadeData BuscarConfiguracoesEntidadeComClassificacao(string tokenEntidade);

        /// <summary>
        /// Busca as hierarquias de classificação por nivel de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Seq do nível de ensino</param>
        /// <returns>Lista de hierarquias de entidade</returns>
        List<EntidadeHierarquiaClassificacaoData> BuscarHierarquiaClassificacaoPorNivelEnsino(long seqNivelEnsino);

        /// <summary>
        /// Busca todas as unidades responsáveis no sisterma de agendas
        /// </summary>
        /// <returns>Lista com as unidades responsaveis do AGD</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisAGDSelect();

        /// <summary>
        /// Busca todas as unidades responsáveis no sistema GPI
        /// </summary>
        /// <returns>Lista com as unidades responsáveis do GPI</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisGPISelect();

        List<SMCDatasourceItem> BuscarUnidadesResponsaveisGPILocalSelect();

        /// <summary>
        /// Lista as entidades responsáveis na visão organizacional para select
        /// </summary>
        /// <param name="listarApenasEntidadesAtivas">Listar apenas as entidades ativas?</param>
        /// <param name="usarNomeReduzido">Indica se a listagem deve apresentar o nome reduzido ou completo da entidade</param>
        /// <returns>Lista de entidades responsáveis</returns>
        List<SMCDatasourceItem> BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(bool listarApenasEntidadesAtivas = false, bool usarNomeReduzido = false);

        /// <summary>
        /// Busca todas as unidades responsáveis no sistema de notificações
        /// </summary>
        /// <returns>Lista com as unidades responsáveis do sistema de notificacoes</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisNotificacaoSelect();

        /// <summary>
        /// Busca todas as unidades responsáveis no sistema de formulários
        /// </summary>
        /// <returns>Lista com as unidades responsáveis do sistema de formulários</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisFormularioSelect();

        /// <summary>
        /// Busca todas as unidades responsáveis no sisterma
        /// </summary>
        /// <returns>Lista com as unidades responsaveis do sistema</returns>
        List<SMCDatasourceItem> BuscarEntidadesSelect(string token, long seqInstituicaoLogada);

        /// <summary>
        /// Buscar Programas de Pós Novo acadêmico
        /// </summary>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarGruposProgramasSelect();

        /// <summary>
        /// Listar departamento do grupo de programa para select
        /// </summary>
        /// <param name="usarNomeReduzido">Se irá usar o nome reduzido caso exista</param>
        /// <returns>Lista SMCDatasourceItem</returns>
        List<SMCDatasourceItem> BuscarDepartamentosGruposProgramasSelect(bool usarNomeReduzido = false);

        /// <summary>
        /// Busca os nomes das eentidades
        /// </summary>
        /// <param name="seqsEntidades"></param>
        /// <returns></returns>
        Dictionary<long,string> BuscarEntidadesNomes(List<long> seqsEntidades);


        /// <summary>
        /// Buscar unidades responsaveis que tenha correspondência com a Unidade Responsável do sistema de Notificação
        /// SeqUnidadeResponsavelNotificacao != null
        /// </summary>
        /// <returns>Lista das unidades responsaveisque tenha correspondência com a Unidade Responsável do sistema de Notificação</returns>
        List<SMCDatasourceItem> BuscarUnidadesResponsaveisCorrespondenciaNotificacaoSelect();

        /// <summary>
        /// Recupera o código da unidade SEO segundo a RN_GRD_007
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliacao</param>
        /// <returns>Código da unidade SEO responsável pelo curso oferta localidade informado</returns>
        int? RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(long seqOrigemAvaliacao);

        /// <summary>
        /// Lista as entidades de acordo com o tipo de entidade
        /// </summary>
        /// <param name="seqTipoEntidade"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarEntidadePorTipoEntidade(long seqTipoEntidade);


        string BuscarEntidadeNome(long seqEntidade);

        /// <summary>
        /// Busca nome, endereço e telefone da <see cref="EntidadeRodapeData"/>
        /// </summary>
        /// <param name="siglaEntidade"></param>
        /// <param name="tokenEntidade"></param>
        /// <returns></returns>
        EntidadeRodapeData BuscarInformacoesRodapeEntidade(string siglaEntidade, string tokenEntidade);

    }
}