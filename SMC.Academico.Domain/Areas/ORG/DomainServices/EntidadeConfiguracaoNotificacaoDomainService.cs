using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using System.Collections.Generic;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System;
using SMC.Academico.Common.Constants;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class EntidadeConfiguracaoNotificacaoDomainService : AcademicoContextDomain<EntidadeConfiguracaoNotificacao>
    {
        #region Serviços / DomainServices

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        private TipoNotificacaoDomainService TipoNotificacaoDomainService { get => Create<TipoNotificacaoDomainService>(); }

        private EntidadeDomainService EntidadeDomainService { get => Create<EntidadeDomainService>(); }

        #endregion

        /// <summary>
        /// Busca os registros de entidade configuração notificação
        /// </summary>
        /// <param name="filtro">Filtros da entidade configuração notificação</param>
        /// <returns></returns>
        public SMCPagerData<EntidadeConfiguracaoNotificacaoListarVO> BuscarEntidadeConfiguracaoNotificacao(EntidadeConfiguracaoNotificacaoFiltroVO filtro)
        {            
            //Monta o specification para buscar os dados dos registros de entidade configuração notificação
            var spec = filtro.Transform<EntidadeConfiguracaoNotificacaoFilterSpecification>();                   
            spec.SetOrderBy(o => o.Entidade.Nome);
            spec.SetOrderBy(o => o.TokenTipoNotificacao);

            //Realiza a pesquisa
            var lista = this.SearchProjectionBySpecification(spec, a => new EntidadeConfiguracaoNotificacaoListarVO()
            {
                Seq = a.Seq,
                SeqEntidade = a.SeqEntidade,
                DescricaoEntidade = a.Entidade.Nome,
                SeqTipoNotificacao = a.SeqTipoNotificacao,               
                DataInicioValidade = a.DataInicioValidade,
                DataFimValidade = a.DataFimValidade
            }, out int total);

            List<EntidadeConfiguracaoNotificacaoListarVO> listaVO = lista.ToList();
            listaVO.ForEach(a => a.DescricaoTipoNotificacao = this.NotificacaoService.BuscarTipoNotificacao(a.SeqTipoNotificacao.Value)?.Descricao);
          
            return new SMCPagerData<EntidadeConfiguracaoNotificacaoListarVO>(listaVO, total);
        }

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial entidade configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        public EntidadeConfiguracaoNotificacaoVO BuscarEntidadeConfiguracaoNotificacao(long seq)
        {          
            var result = this.SearchByKey(new SMCSeqSpecification<EntidadeConfiguracaoNotificacao>(seq));

            ///Busca no serviço de notificação os dados da configurações de email
            var ConfiguracaoNotificacaoEmail = this.NotificacaoService.BuscarConfiguracaoNotificacaoEmail(result.SeqConfiguracaoTipoNotificacao);

            var retorno = result.Transform<EntidadeConfiguracaoNotificacaoVO>();
            retorno.ConfiguracaoNotificacao = ConfiguracaoNotificacaoEmail;         

            return retorno;
        }

        /// <summary>
        /// Salvar entidade configuração notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial da entidade configuração notificação</returns>
        public long Salvar(EntidadeConfiguracaoNotificacaoVO modelo)
        {
            modelo.TokenTipoNotificacao = NotificacaoService.BuscarTipoNotificacao(modelo.SeqTipoNotificacao).Token;

            var entidadeConfiguracaoNotificacao = modelo.Transform<EntidadeConfiguracaoNotificacao>();          
            var entidade = EntidadeDomainService.BuscarEntidade(modelo.SeqEntidade);

            ConfiguracaoTipoNotificacaoData configuracaoTipoNotificacao = modelo.ConfiguracaoNotificacao.Transform<ConfiguracaoTipoNotificacaoData>();
            configuracaoTipoNotificacao.SeqUnidadeResponsavel = entidade.SeqUnidadeResponsavelNotificacao;
            configuracaoTipoNotificacao.DataInicioValidade = modelo.DataInicioValidade;
            configuracaoTipoNotificacao.DataFimValidade = modelo.DataFimValidade;

            ///1.Verificar se no texto da mensagem da notificação existe alguma tag não cadastrada para o tipo de
            ///notificação informado. Caso exista, abortar a operação e emitir a mensagem de erro abaixo:
            Regex regex = new Regex(@"{{[_A-Za-z_]+}}");
            if (modelo != null && modelo.ConfiguracaoNotificacao != null && !string.IsNullOrEmpty(modelo.ConfiguracaoNotificacao.Mensagem))
            {
                var tagsMensagem = regex.Matches(modelo.ConfiguracaoNotificacao.Mensagem).Cast<Match>().Select(m => m.Value).ToList();

                string tagsInvalidas = string.Empty;

                if (modelo.ConfiguracaoNotificacao.Tags == null)
                    modelo.ConfiguracaoNotificacao.Tags = NotificacaoService.BuscarConfiguracaoTipoNotificacaoCompleta(modelo.SeqConfiguracaoTipoNotificacao).Tags;

                foreach (var item in tagsMensagem)
                {
                    if (modelo.ConfiguracaoNotificacao.Tags == null || !modelo.ConfiguracaoNotificacao.Tags.Any(a => a.Nome.ToLower() == item.ToLower()))
                        tagsInvalidas += $"<br />- {item}";
                }

                if (!string.IsNullOrEmpty(tagsInvalidas))
                    throw new EntidadeConfiguracaoNotificacaoTagException(tagsInvalidas, modelo.ConfiguracaoNotificacao.DescricaoTipoNotificacao);
            }

            //Verifica se esta alterando uma configuração notificacao e aplica as regras de alteração
            if (modelo.Seq > 0)
            {
                //Recupera o seq do tipo de notificação atual antes da alteração
                var seqTipoNotificacaoAtual = this.SearchProjectionByKey(modelo.Seq, x => x.SeqTipoNotificacao);
                if (seqTipoNotificacaoAtual != modelo.SeqTipoNotificacao)
                {
                    ///1.O Tipo de Notificação não pode ser alterado se já existir registro de envio de notificação com a
                    ///configuração em questão. Em caso de violação, exibir a mensagem abaixo e abortar a operação.
                    if (this.NotificacaoService.VerificarConfiguracaoPossuiNotificacoes(new long[] { modelo.ConfiguracaoNotificacao.Seq }))
                        throw new EntidadeConfiguracaoNotificacaoJaEnviadaException();                   
                }
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var seqNot = this.NotificacaoService.SalvarConfiguracaoTipoNotificacao(configuracaoTipoNotificacao);
                entidadeConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao = seqNot;
                this.SaveEntity(entidadeConfiguracaoNotificacao);
                unitOfWork.Commit();
            }

            return entidadeConfiguracaoNotificacao.Seq;
        }

        /// <summary>
        /// Excluir a configuração notificação 
        /// </summary>
        /// <param name="seq">Sequencial da entidade configuração notificação</param>
        public void Excluir(long seq)
        {
            var entidadeConfiguracaoNotificacao = this.BuscarEntidadeConfiguracaoNotificacao(seq);

            ///1.Não é permitida a exclusão de uma configuração de notificação se já tiver registro de envio de
            ///notificação com a configuração em questão. Em caso de violação, exibir a mensagem de erro abaixo e
            ///abortar a operação.
            if (this.NotificacaoService.VerificarConfiguracaoPossuiNotificacoes(new long[] { entidadeConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao }))
            {
                throw new EntidadeConfiguracaoNotificacaoExcluirException();
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<EntidadeConfiguracaoNotificacao>(seq));
                    this.DeleteEntity(configToDelete);

                    NotificacaoService.ExcluirConfiguracaoTipoNotificacao(new long[] { entidadeConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao });

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Buscar o sequencial da configuração de notificação ativo para uma entidade com um token.
        /// Desabilita o filtro de dados para realizar a pesquisa. Em algunas situações a configuração está na entidade da instituição
        /// de ensino e a pessoa logada tem filtro de dados pelo programa e não encontra a configuração para o envio da notificação.
        /// </summary>
        /// <param name="seqEntidade">Sequencial da entidade para pesquisa</param>
        /// <param name="tokenNotificacao">Token da notificação para pesquisa</param>
        /// <returns>Sequencial da notificação encontrada ou 0 (zero) caso não encontre nenhuma notificação configurada</returns>
        public long BuscarSeqConfiguracaoNotificacaoAtivo(long seqEntidade, string tokenNotificacao)
        {
            // Desabilita o filtro de dados
            this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            // Cria o specification para consulta
            var specConfig = new EntidadeConfiguracaoNotificacaoFilterSpecification()
            {
                SeqEntidade = seqEntidade,
                TokenTipoNotificacao = tokenNotificacao,
                Vigente = true
            };

            // Realiza a pesquisa
            var seqConfigNotificacao = this.SearchProjectionBySpecification(specConfig, p => p.SeqConfiguracaoTipoNotificacao).FirstOrDefault();

            // Habilita o filtro de dados
            this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            // Retorna o valor encontrado
            return seqConfigNotificacao;
        }
    }
}