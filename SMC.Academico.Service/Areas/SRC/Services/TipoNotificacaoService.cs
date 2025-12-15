using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class TipoNotificacaoService : SMCServiceBase, ITipoNotificacaoService
    {
        #region Domain Service

        private TipoNotificacaoDomainService TipoNotificacaoDomainService { get => Create<TipoNotificacaoDomainService>(); }

        #endregion

        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="seqTipoNotificacao">Sequencial da notificação</param>
        /// <returns>Dados do Tipo de Notificacao</returns>
        public TipoNotificacaoData BuscarTipoNotificacao(long seq)
        {
            return this.TipoNotificacaoDomainService.BuscarTipoNotificacao(seq).Transform<TipoNotificacaoData>();
        }

        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="filtro">Filtros selecionados na tela</param>
        /// <returns>Lista com os tipos de notificacação</returns>
        public SMCPagerData<TipoNotificacaoListarData> BuscarTiposNotificacao(TipoNotificacaoFiltroData filtros)
        {
            var result = this.TipoNotificacaoDomainService.BuscarTiposNotificacao(filtros.Transform<TipoNotificacaoFiltroVO>());
            return new SMCPagerData<TipoNotificacaoListarData>(result.TransformList<TipoNotificacaoListarData>(), result.Total);
        }

        /// <summary>
        /// Buscar todos os tipos de notificação 
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoSelect()
        {
            return this.TipoNotificacaoDomainService.BuscarTiposNotificacaoSelect();
        }

        /// <summary>
        /// Buscar tipos de notificação por serviço
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorServicoSelect(long seqServico)
        {
            return this.TipoNotificacaoDomainService.BuscarTiposNotificacaoPorServicoSelect(seqServico);
        }

        /// <summary>
        /// Buscar tipo de notificação
        /// </summary>
        /// <param name="seqTipoNotificacao">Sequencial da notificação</param>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorUnidadeResponsavelSelect(long seqUnidadeResponsavelNotificacao)
        {
            return this.TipoNotificacaoDomainService.BuscarTiposNotificacaoPorUnidadeResponsavelSelect(seqUnidadeResponsavelNotificacao);
        }

        /// <summary>
        /// Verifica se já existe registro de envio de notificação com a configuração em questão
        /// </summary>
        /// <param name="seqConfiguracaoTipoNotificacao"></param>
        /// <returns></returns>
        public bool VerificarConfiguracaoPossuiNotificacoes(long seqConfiguracaoTipoNotificacao)
        {
            return this.TipoNotificacaoDomainService.VerificarConfiguracaoPossuiNotificacoes(seqConfiguracaoTipoNotificacao);
        }

        /// <summary>
        /// Salva o tipo de notificação
        /// </summary>
        /// <param name="modelo">Objeto com os atributos do tipo de notificação</param>
        /// <returns>Sequencial do tipo de notificação salvo</returns>
        public long Salvar(TipoNotificacaoData modelo)
        {
            return this.TipoNotificacaoDomainService.SalvarTipoNotificacao(modelo.Transform<TipoNotificacaoVO>());
        }

        /// <summary>
        /// Exclui o tipo de notificação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de notificação</param>
        public void Excluir(long seq)
        {
            this.TipoNotificacaoDomainService.ExcluirTipoNotificacao(seq);
        }

        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorEtapaServico(long seqProcessoEtapa)
        {
            return this.TipoNotificacaoDomainService.BuscarTiposNotificacaoPorEtapaServico(seqProcessoEtapa);
        }

        /// <summary>
        /// Buscar todos os tipos de notificação que tem cadastro no acadêmico
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoNoAcademicoSelect()
        {
            return this.TipoNotificacaoDomainService.BuscarTiposNotificacaoNoAcademicoSelect();
        }
    }
}