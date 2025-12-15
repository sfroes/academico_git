using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ITipoNotificacaoService : ISMCService
    {
        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="seqTipoNotificacao">Sequencial da notificação</param>
        /// <returns>Dados do Tipo de Notificacao</returns>
        TipoNotificacaoData BuscarTipoNotificacao(long seq);

        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="filtro">Filtros selecionados na tela</param>
        /// <returns>Lista com os tipos de notificacação</returns>
        SMCPagerData<TipoNotificacaoListarData> BuscarTiposNotificacao(TipoNotificacaoFiltroData filtros);

        /// <summary>
        /// Buscar todos os tipos de notificação 
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        List<SMCDatasourceItem> BuscarTiposNotificacaoSelect();

        /// <summary>
        /// Buscar tipos de notificação por serviço
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        List<SMCDatasourceItem> BuscarTiposNotificacaoPorServicoSelect(long seqServico);

        /// <summary>
        /// Buscar tipo de notificação
        /// </summary>
        /// <param name="seqTipoNotificacao">Sequencial da notificação</param>
        /// <returns>Lista select item dos tipos de notificação</returns>
        List<SMCDatasourceItem> BuscarTiposNotificacaoPorUnidadeResponsavelSelect(long seqUnidadeResponsavelNotificacao);

        /// <summary>
        /// Verifica se já existe registro de envio de notificação com a configuração em questão
        /// </summary>
        /// <param name="seqConfiguracaoTipoNotificacao"></param>
        /// <returns></returns>
        bool VerificarConfiguracaoPossuiNotificacoes(long seqConfiguracaoTipoNotificacao);

        /// <summary>
        /// Salva o tipo de notificação
        /// </summary>
        /// <param name="modelo">Objeto com os atributos do tipo de notificação</param>
        /// <returns>Sequencial do tipo de notificação salvo</returns>
        long Salvar(TipoNotificacaoData modelo);

        /// <summary>
        /// Exclui o tipo de notificação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de notificação</param>
        void Excluir(long seq);

        List<SMCDatasourceItem> BuscarTiposNotificacaoPorEtapaServico(long seqProcessoEtapa);

        /// <summary>
        /// Buscar todos os tipos de notificação que tem cadastro no acadêmico
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        List<SMCDatasourceItem> BuscarTiposNotificacaoNoAcademicoSelect();
    }
}