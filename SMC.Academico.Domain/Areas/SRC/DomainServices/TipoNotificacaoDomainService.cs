using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class TipoNotificacaoDomainService : AcademicoContextDomain<TipoNotificacao>
    {
        #region Services

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        private IAplicacaoService AplicacaoService { get => Create<IAplicacaoService>(); }

        #endregion Services

        #region DomainServices

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService
        {
            get { return this.Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>(); }
        }

        private TipoNotificacaoAtributoAgendamentoDomainService TipoNotificacaoAtributoAgendamentoDomainService
        {
            get { return this.Create<TipoNotificacaoAtributoAgendamentoDomainService>(); }
        }

        private ServicoTipoNotificacaoDomainService ServicoTipoNotificacaoDomainService
        {
            get { return this.Create<ServicoTipoNotificacaoDomainService>(); }
        }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return this.Create<ProcessoEtapaDomainService>(); }
        }


        #endregion DomainServices

        #region Queries

        private string _inserirTipoNotificacao =
                        @" INSERT INTO SRC.tipo_notificacao (seq_tipo_notificacao, dsc_token, ind_permite_agendamento, usu_inclusao, dat_inclusao, idt_dom_permite_reenvio)
                           VALUES (@seqTipoNotificacao, @token, @permiteAgendamento, @usuInclusao, @datInclusao, @permiteReenvio)";

        private string _inserirTipoNotificacaoAtributoAgendamento =
                       @" INSERT INTO SRC.tipo_notificacao_atributo_agendamento (seq_tipo_notificacao, idt_dom_atributo_agendamento, usu_inclusao, dat_inclusao)
                          VALUES (@seqTipoNotificacao, @seqAtributoAgendamento, @usuInclusao, @datInclusao)";

        private string _updateTipoNotificacao =
                        @" UPDATE SRC.tipo_notificacao set
                                seq_tipo_notificacao = @seqTipoNotificacao,
                                dsc_token = @token,
                                ind_permite_agendamento = @permiteAgendamento,
                                usu_alteracao = @usuAlteracao,
                                dat_alteracao = @datAlteracao,
                                idt_dom_permite_reenvio = @permiteReenvio
                           WHERE seq_tipo_notificacao = @seqTipoNotificacaoUpdate";

        #endregion Queries

        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="seqTipoNotificacao">Sequencial da notificação</param>
        /// <returns>Dados do Tipo de Notificacao</returns>
        public TipoNotificacaoVO BuscarTipoNotificacao(long seqTipoNotificacao)
        {
            ///Notificações do SGA
            var tipoNotificacaoSGA = this.SearchByKey(new SMCSeqSpecification<TipoNotificacao>(seqTipoNotificacao), IncludesTipoNotificacao.AtributosAgendamento);

            ///Buscar os dados do serviço de notificação
            var tipoNotificacaoService = this.NotificacaoService.BuscarTipoNotificacao(seqTipoNotificacao);
            var retorno = new TipoNotificacaoVO();

            if (tipoNotificacaoSGA != null)
            {
                retorno = tipoNotificacaoSGA.Transform<TipoNotificacaoVO>();
                if (retorno == null)
                    return null;

                ///Complementa as informações com os dados do serviço de notificação
                retorno.Descricao = tipoNotificacaoService.Descricao;
                retorno.Observacao = tipoNotificacaoService.Observacao;
            }
            else
                retorno = tipoNotificacaoService.Transform<TipoNotificacaoVO>();

            if (retorno == null)
                return null;

            retorno.AtributosAgendamento = retorno.AtributosAgendamento.SMCAny() ? retorno.AtributosAgendamento : null;
            retorno.SeqTipoNotificacao = seqTipoNotificacao;
            retorno.SeqAuxiliar = seqTipoNotificacao;
            retorno.ModoEdicao = true;

            return retorno;
        }

        /// <summary>
        /// Buscar tipo de notificacao
        /// </summary>
        /// <param name="filtro">Filtros selecionados na tela</param>
        /// <returns>Lista com os tipos de notificacação</returns>
        public SMCPagerData<TipoNotificacaoListarVO> BuscarTiposNotificacao(TipoNotificacaoFiltroVO filtro)
        {
            /*O FILTRO, ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE PARA FILTRAR E ORDENAR
            PELO CAMPO DESCRIÇÃO, QUE SE ENCONTRA NO BANCO NOTIFICAÇÃO E NÃO NO ACADÊMICO*/

            var spec = filtro.Transform<TipoNotificacaoFilterSpecification>();

            //LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            int qtdeRegistros = this.Count();
            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var lista = this.SearchProjectionBySpecification(spec, a => new TipoNotificacaoListarVO()
            {
                Seq = a.Seq,
                PermiteAgendamento = a.PermiteAgendamento
            });

            List<TipoNotificacaoListarVO> listaVO = lista.ToList();
            listaVO.ForEach(a => a.Descricao = this.NotificacaoService.BuscarTipoNotificacao(a.Seq.Value)?.Descricao);

            //FILTRANDO PELA DESCRIÇÃO DO TIPO DE NOTIFICAÇÃO EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            if (!string.IsNullOrEmpty(filtro.Descricao))
            {
                listaVO = listaVO.Where(a => a.Descricao.Contains(filtro.Descricao)).ToList();
            }

            int total = listaVO.Count();

            //ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            List<SMCSortInfo> listaOrdenacao = filtro.PageSettings.SortInfo;

            foreach (var sort in listaOrdenacao)
            {
                if (sort.FieldName == nameof(TipoNotificacaoListarVO.Seq))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.Seq).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.Seq).ToList();
                }
                if (sort.FieldName == nameof(TipoNotificacaoListarVO.Descricao))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.Descricao).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.Descricao).ToList();
                }
                if (sort.FieldName == nameof(TipoNotificacaoListarVO.PermiteAgendamento))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.PermiteAgendamento).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.PermiteAgendamento).ToList();
                }
            }

            //CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            listaVO = listaVO.Skip((filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize).Take(filtro.PageSettings.PageSize).ToList();

            return new SMCPagerData<TipoNotificacaoListarVO>(listaVO, total);
        }

        /// <summary>
        /// Buscar todos os tipos de notificação
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoSelect()
        {
            var grupoAplicacao = AplicacaoService.BuscarGrupoAplicacaoPelaSigla(SIGLA_APLICACAO.GRUPO_APLICACAO);
            return this.NotificacaoService.BuscarTiposNoficicacaoPorGrupoAplicacao(grupoAplicacao?.Seq ?? 0).OrderBy(a => a.Descricao).ToList();
        }

        /// <summary>
        /// Buscar tipos de notificação por serviço
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorServicoSelect(long seqServico)
        {
            // Busca os tipos de notificação configurados para o serviço
            var spec = new ServicoTipoNotificacaoFilterSpecification() { SeqServico = seqServico };
            var seqs = ServicoTipoNotificacaoDomainService.SearchProjectionBySpecification(spec, x => x.SeqTipoNotificacao).Distinct().ToList();

            // Monta a lista para retorno
            var lista = new List<SMCDatasourceItem>();
            foreach (var item in seqs)
            {
                lista.Add(new SMCDatasourceItem() { Seq = item, Descricao = this.NotificacaoService.BuscarTipoNotificacao(item).Descricao });
            }
            return lista.OrderBy(o => o.Descricao).ToList();
        }

        /// <summary>
        /// Buscar todos os tipos de notificação por unidade responsavel
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorUnidadeResponsavelSelect(long seqUnidadeResponsavelNotificacao)
        {
            var grupoAplicacao = AplicacaoService.BuscarGrupoAplicacaoPelaSigla(SIGLA_APLICACAO.GRUPO_APLICACAO);
            return this.NotificacaoService.BuscarTiposNotificacaoPorUnidadeResponsavel(seqUnidadeResponsavelNotificacao, grupoAplicacao?.Seq ?? 0).OrderBy(a => a.Descricao).ToList();
        }

        /// <summary>
        /// Verifica se já existe registro de envio de notificação com a configuração em questão
        /// </summary>
        /// <param name="seqConfiguracaoTipoNotificacao"></param>
        /// <returns></returns>
        public bool VerificarConfiguracaoPossuiNotificacoes(long seqConfiguracaoTipoNotificacao)
        {
            return this.NotificacaoService.VerificarConfiguracaoPossuiNotificacoes(new long[] { seqConfiguracaoTipoNotificacao });
        }

        /// <summary>
        /// Salva o tipo de notificação
        /// </summary>
        /// <param name="modelo">Objeto com os atributos do tipo de notificação</param>
        /// <returns>Sequencial do tipo de notificação salvo</returns>
        public long SalvarTipoNotificacao(TipoNotificacaoVO modelo)
        {
            /*FORAM CRIADOS OS DOIS SEQS PARA AO EDITAR UM REGISTRO QUE ESTEJA SELECIONADO OUTRO TIPO DE NOTIFICAÇÃO,
            E NÃO PASSAR NA VALIDAÇÃO, A TELA SEJA REDIRECIONADA COM O SEQUENCIAL ORIGINAL DO REGISTRO, GUARDADO SEMPRE NO SEQ*/
            modelo.Seq = modelo.SeqTipoNotificacao;

            if (!modelo.ModoEdicao)
            {
                ValidarModeloInsercao(modelo);

                var paramTipoNotificacao = new List<SMCFuncParameter>()
                {
                    new SMCFuncParameter("seqTipoNotificacao", modelo.Seq),
                    new SMCFuncParameter("token", modelo.Token),
                    new SMCFuncParameter("permiteAgendamento", modelo.PermiteAgendamento),
                    new SMCFuncParameter("usuInclusao", SMCContext.User.SMCGetSequencialUsuario()),
                    new SMCFuncParameter("datInclusao", DateTime.Now),
                    new SMCFuncParameter("permiteReenvio", modelo.PermiteReenvio),
                };

                /*OS REGISTROS SÃO INSERIDOS VIA RAWQUERY POIS O SEQUENCIAL SEMPRE VIRÁ PREENCHIDO, O TIPO DE NOTIFICAÇÃO É SELECIONADO
                NA TELA, E PARA O FRAMEWORK SE O SEQUENCIAL ESTÁ PREENCHIDO É FEITO UM UPDATE*/
                ExecuteSqlCommand(_inserirTipoNotificacao, paramTipoNotificacao);

                if (modelo.PermiteAgendamento)
                {
                    foreach (var atributo in modelo.AtributosAgendamento)
                    {
                        var paramAtributoAgendamento = new List<SMCFuncParameter>()
                        {
                            new SMCFuncParameter("seqTipoNotificacao", modelo.Seq),
                            new SMCFuncParameter("seqAtributoAgendamento", atributo.AtributoAgendamento),
                            new SMCFuncParameter("usuInclusao", SMCContext.User.SMCGetSequencialUsuario()),
                            new SMCFuncParameter("datInclusao", DateTime.Now)
                        };

                        ExecuteSqlCommand(_inserirTipoNotificacaoAtributoAgendamento, paramAtributoAgendamento);
                    }
                }
            }
            else
            {
                ValidarModeloEdicao(modelo);

                if (modelo.Seq == modelo.SeqAuxiliar)
                {
                    /*SE NÃO MUDOU O SEQUENCIAL DO TIPO DE NOTIFICAÇÃO (VALIDAÇÃO DO SEQUENCIAL ATUAL COM O SEQUENCIAL ANTERIOR),
                    O FRAMEWORK CONSEGUE FAZER O UPDATE CORRETAMENTE*/
                    var dominio = modelo.Transform<TipoNotificacao>();
                    dominio.AtributosAgendamento = dominio.AtributosAgendamento != null ? dominio.AtributosAgendamento : new List<TipoNotificacaoAtributoAgendamento>();

                    this.SaveEntity(dominio);
                }
                else
                {
                    /*SE MUDOU O SEQUENCIAL DO TIPO DE NOTIFICAÇÃO (VALIDAÇÃO DO SEQUENCIAL ATUAL COM O SEQUENCIAL ANTERIOR),
                    É NECESSÁRIO FAZER O UPDATE VIA RAWQUERY, VIA FRAMEWORK O UPDATE NÃO CONSEGUE SER REALIZADO POIS DÁ ERRO NOS ATRIBUTOS*/
                    var tipoNotificacao = this.SearchByKey(new SMCSeqSpecification<TipoNotificacao>(modelo.SeqAuxiliar), IncludesTipoNotificacao.AtributosAgendamento);

                    foreach (var atributo in tipoNotificacao.AtributosAgendamento)
                    {
                        this.TipoNotificacaoAtributoAgendamentoDomainService.DeleteEntity(atributo);
                    }

                    var paramTipoNotificacao = new List<SMCFuncParameter>()
                    {
                        new SMCFuncParameter("seqTipoNotificacao", modelo.Seq),
                        new SMCFuncParameter("token", modelo.Token),
                        new SMCFuncParameter("permiteAgendamento", modelo.PermiteAgendamento),
                        new SMCFuncParameter("usuAlteracao", SMCContext.User.SMCGetSequencialUsuario()),
                        new SMCFuncParameter("datAlteracao", DateTime.Now),
                        new SMCFuncParameter("seqTipoNotificacaoUpdate", modelo.SeqAuxiliar),
                        new SMCFuncParameter("permiteReenvio", modelo.PermiteReenvio),
                    };

                    ExecuteSqlCommand(_updateTipoNotificacao, paramTipoNotificacao);

                    if (modelo.PermiteAgendamento)
                    {
                        foreach (var atributo in modelo.AtributosAgendamento)
                        {
                            var paramAtributoAgendamento = new List<SMCFuncParameter>()
                            {
                            new SMCFuncParameter("seqTipoNotificacao", modelo.Seq),
                            new SMCFuncParameter("seqAtributoAgendamento", atributo.AtributoAgendamento),
                            new SMCFuncParameter("usuInclusao", SMCContext.User.SMCGetSequencialUsuario()),
                            new SMCFuncParameter("datInclusao", DateTime.Now)
                            };

                            ExecuteSqlCommand(_inserirTipoNotificacaoAtributoAgendamento, paramAtributoAgendamento);
                        }
                    }
                }
            }

            return modelo.Seq;
        }

        private void ValidarModeloInsercao(TipoNotificacaoVO modelo)
        {
            var tipoNotificacaoPorSeq = this.SearchByKey(new SMCSeqSpecification<TipoNotificacao>(modelo.Seq));
            var tipoNotificacaoPorToken = this.SearchBySpecification(new TipoNotificacaoFilterSpecification() { Token = modelo.Token }).FirstOrDefault();

            if (tipoNotificacaoPorSeq != null)
                throw new JaExisteTipoNotificacaoException();

            if (tipoNotificacaoPorToken != null)
                throw new JaExisteTokenTipoNotificacaoException();

            if (modelo.PermiteAgendamento && !modelo.AtributosAgendamento.Any())
                throw new AtributosAgendamentoNaoCadastradosException();

            if (modelo.PermiteAgendamento)
            {
                var listaQuantidadeAtributo = modelo.AtributosAgendamento.GroupBy(x => (int)x.AtributoAgendamento).Select(x => new { Seq = x.Key, Count = x.Count() });

                /*VERIFICA SE O MESMO ATRIBUTO EXISTE MAIS DE UMA VEZ DENTRO DA LISTA*/
                if (listaQuantidadeAtributo.Any(x => x.Count > 1))
                    throw new AtributoAgendamentoJaAssociadoException();
            }
        }

        private void ValidarModeloEdicao(TipoNotificacaoVO modelo)
        {
            var tipoNotificacaoPorSeq = this.SearchByKey(new SMCSeqSpecification<TipoNotificacao>(modelo.Seq));
            var tipoNotificacaoPorToken = this.SearchBySpecification(new TipoNotificacaoFilterSpecification() { Token = modelo.Token }).FirstOrDefault();

            /*VERIFICA SE O SEQUENCIAL JÁ EXISTE NO BANCO, E SE NÃO É O MESMO REGISTRO QUE ESTÁ ABERTO NA TELA DE EDIÇÃO*/
            if (tipoNotificacaoPorSeq != null && modelo.Seq != modelo.SeqAuxiliar)
                throw new JaExisteTipoNotificacaoException();

            /*VERIFICA SE O TOKEN JÁ EXISTE NO BANCO, E SE NÃO É O MESMO REGISTRO QUE ESTÁ ABERTO NA TELA DE EDIÇÃO*/
            if (tipoNotificacaoPorToken != null && tipoNotificacaoPorToken.Seq != modelo.SeqAuxiliar)
                throw new JaExisteTokenTipoNotificacaoException();

            if (modelo.PermiteAgendamento && !modelo.AtributosAgendamento.Any())
                throw new AtributosAgendamentoNaoCadastradosException();

            if (modelo.PermiteAgendamento)
            {
                var listaQuantidadeAtributo = modelo.AtributosAgendamento.GroupBy(x => (int)x.AtributoAgendamento).Select(x => new { Seq = x.Key, Count = x.Count() });

                /*VERIFICA SE O MESMO ATRIBUTO EXISTE MAIS DE UMA VEZ DENTRO DA LISTA*/
                if (listaQuantidadeAtributo.Any(x => x.Count > 1))
                    throw new AtributoAgendamentoJaAssociadoException();
            }
        }

        /// <summary>
        /// Exclui o tipo de notificação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de notificação</param>
        public void ExcluirTipoNotificacao(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<TipoNotificacao>(seq));
                    this.DeleteEntity(configToDelete);

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
        /// Buscar todos os tipos de notificação por etapa
        /// </summary>
        /// <returns>Lista select item dos tipos de notificação</returns>
        public List<SMCDatasourceItem> BuscarTiposNotificacaoPorEtapaServico(long seqProcessoEtapa)
        {
            var specProcessoEtapa = new ProcessoEtapaFilterSpecification { Seq = seqProcessoEtapa };
            var processoEtapa = ProcessoEtapaDomainService.SearchProjectionByKey(specProcessoEtapa, s => new
            {
                s.Processo.SeqServico,
                s.SeqEtapaSgf
            });

            var specTipoNotificacao = new ServicoTipoNotificacaoFilterSpecification() { SeqServico = processoEtapa.SeqServico, SeqEtapaSgf = processoEtapa.SeqEtapaSgf };
            var tiposNotificacao = ServicoTipoNotificacaoDomainService.SearchBySpecification(specTipoNotificacao).ToList();

            var lista = new List<SMCDatasourceItem>();
            foreach (var item in tiposNotificacao)
            {
                lista.Add(new SMCDatasourceItem()
                {
                    Seq = item.SeqTipoNotificacao,
                    Descricao = NotificacaoService.BuscarTipoNotificacao(item.SeqTipoNotificacao).Descricao
                });
            }
            return lista.OrderBy(o => o.Descricao).ToList();

        }

        public List<SMCDatasourceItem> BuscarTiposNotificacaoNoAcademicoSelect()
        {
            var tiposNotificacao = this.SearchAll();
            var seqsTiposNotificacao = tiposNotificacao.Select(a => a.Seq).ToArray();
            var notifcacoes = NotificacaoService.BuscarTiposNotificacao(seqsTiposNotificacao);

            var lista = new List<SMCDatasourceItem>();
            foreach (var notificacao in notifcacoes)
                lista.Add(new SMCDatasourceItem() { Seq = notificacao.Seq, Descricao = notificacao.Descricao });

            return lista.OrderBy(o => o.Descricao).ToList();
        }
    }
}