using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Formularios.ServiceContract.Areas.FRM.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using SMC.Academico.Common.Constants;
using SMC.Formularios.Common.Areas.FRM.Includes;
using SMC.Academico.Service.Areas.SRC.Services;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ConfiguracaoEtapaPaginaDomainService : AcademicoContextDomain<ConfiguracaoEtapaPagina>
    {
        #region DomainServices

        private TextoSecaoPaginaDomainService TextoSecaoPaginaDomainService
        {
            get { return this.Create<TextoSecaoPaginaDomainService>(); }
        }

        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService
        {
            get { return this.Create<SolicitacaoHistoricoNavegacaoDomainService>(); }
        }

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        private ViewCentralSolicitacaoServicoDomainService ViewCentralSolicitacaoServicoDomainService
        {
            get { return this.Create<ViewCentralSolicitacaoServicoDomainService>(); }
        }

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService
        {
            get { return this.Create<SolicitacaoServicoEtapaDomainService>(); }
        }

        #endregion

        #region Services

        private IAplicacaoService AplicacaoService
        {
            get { return this.Create<IAplicacaoService>(); }
        }

        private IPaginaService PaginaService
        {
            get { return this.Create<IPaginaService>(); }
        }

        private IFormularioService FormularioService
        {
            get { return this.Create<IFormularioService>(); }
        }

        #endregion

        #region [ Querys ]

        private const string QUERY_HISTORICO_NAVEGACAO_ULTIMA_PAGINA = @"
            declare 
	            @LISTA_SOLICITACOES varchar(255)

            set	@LISTA_SOLICITACOES = {0}

            /*Solicitação x Navegação*/
            select
	             shn.seq_solicitacao_historico_navegacao as Seq,
	             shn.seq_solicitacao_servico_etapa as SeqSolicitacaoServicoEtapa,
	             shn.seq_configuracao_etapa_pagina as SeqConfiguracaoEtapaPagina,
	             shn.dat_entrada as DataEntrada,
	             shn.dat_saida as DataSaida, 
                 shn.dat_inclusao as DataInclusao,
	             shn.usu_inclusao as UsuarioInclusao,
	             shn.dat_alteracao as DataAlteracao,
	             shn.usu_alteracao as UsuarioAlteracao
            from src.solicitacao_servico ss
		         join src.solicitacao_servico_etapa sse on sse.seq_solicitacao_servico = ss.seq_solicitacao_servico		
		         join src.solicitacao_historico_navegacao shn on shn.seq_solicitacao_servico_etapa = sse.seq_solicitacao_servico_etapa
		         join src.configuracao_etapa ce on ce.seq_configuracao_etapa = sse.seq_configuracao_etapa
		         join src.processo_etapa pe on pe.seq_processo_etapa = ce.seq_processo_etapa
		         --join src.processo p on p.seq_processo = pe.seq_processo
		         --join src.configuracao_etapa_pagina cep on cep.seq_configuracao_etapa = ce.seq_configuracao_etapa and shn.seq_configuracao_etapa_pagina = cep.seq_configuracao_etapa_pagina
            where 
                 ss.num_protocolo in (select cast(item as varchar) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_SOLICITACOES, ','))  
                 and shn.dat_saida is null
            order by ss.num_protocolo, shn.dat_entrada";

        private const string QUERY_HISTORICO_NAVEGACAO_PAGINA = @"
            declare 
	            @LISTA_SOLICITACOES varchar(255), 
                @SEQ_CONFIGURACAO_ETAPA_PAGINA bigint

            set	@LISTA_SOLICITACOES = {0}
            set @SEQ_CONFIGURACAO_ETAPA_PAGINA = {1}

            /*Solicitação x Navegação*/
            select
	             shn.seq_solicitacao_historico_navegacao as Seq,
	             shn.seq_solicitacao_servico_etapa as SeqSolicitacaoServicoEtapa,
	             shn.seq_configuracao_etapa_pagina as SeqConfiguracaoEtapaPagina,
	             shn.dat_entrada as DataEntrada,
	             shn.dat_saida as DataSaida
            from src.solicitacao_servico ss
                join src.solicitacao_servico_etapa sse on sse.seq_solicitacao_servico = ss.seq_solicitacao_servico        
                join src.solicitacao_historico_navegacao shn on shn.seq_solicitacao_servico_etapa = sse.seq_solicitacao_servico_etapa
                join src.configuracao_etapa ce on ce.seq_configuracao_etapa = sse.seq_configuracao_etapa
                join src.processo_etapa pe on pe.seq_processo_etapa = ce.seq_processo_etapa
                --join src.processo p on p.seq_processo = pe.seq_processo
                --join src.configuracao_etapa_pagina cep on cep.seq_configuracao_etapa = ce.seq_configuracao_etapa and shn.seq_configuracao_etapa_pagina = cep.seq_configuracao_etapa_pagina
            where 
                ss.num_protocolo in (select cast(item as varchar) from DPD.dbo.fn_converte_lista_em_tabela(@LISTA_SOLICITACOES, ','))  
                and shn.seq_configuracao_etapa_pagina = @SEQ_CONFIGURACAO_ETAPA_PAGINA
            order by ss.num_protocolo, shn.dat_entrada";

        #endregion

        public ConfiguracaoEtapaPaginaVO BuscarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            var spec = new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina);

            var configuracaoEtapaPagina = this.SearchByKey(spec, x => x.ConfiguracaoEtapa.ProcessoEtapa);

            var retorno = configuracaoEtapaPagina.Transform<ConfiguracaoEtapaPaginaVO>();
            var pagina = this.PaginaService.BuscarPaginasEtapaCompletas(new long[] { retorno.SeqPaginaEtapaSgf })[0].Pagina;
            retorno.DescricaoPagina = pagina.Descricao;
            retorno.SeqEtapaSgf = configuracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf;
            retorno.ExibeFormulario = pagina.ExibeFormulario;

            var situacao = configuracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;
            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                retorno.CamposReadyOnly = true;

            return retorno;
        }  
        public List<ConfiguracaoEtapaPaginaVO> BuscarConfiguracaoEtapaPaginaPorSeqProcessoEtapa(long seqProcessoEtapa)
        {
            var specConfiguracaoEtapa = new ConfiguracaoEtapaFilterSpecification { SeqProcessoEtapa = seqProcessoEtapa };
            var configuracaoEtapa = ConfiguracaoEtapaDomainService.SearchByKey(specConfiguracaoEtapa);

            var spec = new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = configuracaoEtapa.Seq};

            var configuracaoEtapaPagina = this.SearchBySpecification(spec, x => x.ConfiguracaoEtapa.ProcessoEtapa);

            var retorno = configuracaoEtapaPagina.TransformList<ConfiguracaoEtapaPaginaVO>();

            return retorno;
        }

        public List<ConfiguracaoEtapaPaginaVO> BuscarConfiguracoesEtapaPaginaPorSeqConfiguracaoEtapa(long seqConfiguracaoEtapa)
        {
            var spec = new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = seqConfiguracaoEtapa };

            var configuracaoEtapaPagina = this.SearchBySpecification(spec, x => x.ConfiguracaoEtapa.ProcessoEtapa);

            var retorno = configuracaoEtapaPagina.TransformList<ConfiguracaoEtapaPaginaVO>();

            return retorno;
        }

        public List<SMCDatasourceItem> BuscarPaginasPorEtapa(long seqEtapaSgf)
        {
            var paginas = this.PaginaService.BuscarPaginasCompletasPorEtapa(seqEtapaSgf).Select(e => new SMCDatasourceItem()
            {
                Seq = e.Seq,
                Descricao = e.Pagina.Titulo

            }).ToList();

            return paginas;
        }

        public List<SMCDatasourceItem> BuscarFormularios()
        {
            var sequenciaisAplicacoesSGA = this.AplicacaoService.BuscarsSeqPelaSigla(new string[] { SIGLA_APLICACAO.SGA_ADMINISTRATIVO, SIGLA_APLICACAO.SGA_ALUNO });

            var formularios = this.FormularioService.BuscarFormulariosPorAplicacao(sequenciaisAplicacoesSGA).Select(e => new SMCDatasourceItem()
            {
                Seq = e.Seq,
                Descricao = e.Nome

            }).ToList();

            if (!formularios.Any())
            {
                var grupoAplicacaoSGA = this.AplicacaoService.BuscarGrupoAplicacaoPelaSigla(SIGLA_APLICACAO.GRUPO_APLICACAO);

                formularios = this.FormularioService.BuscarFormulariosPorGrupoAplicacao(grupoAplicacaoSGA.Seq).Select(e => new SMCDatasourceItem()
                {
                    Seq = e.Seq,
                    Descricao = e.Nome

                }).ToList();
            }

            return formularios;
        }

        public List<SMCDatasourceItem> BuscarVisoesPorFormularioSelect(long seqFormulario)
        {
            var formulario = this.FormularioService.BuscarFormulario(seqFormulario, IncludesFormulario.Nenhum);
            return this.FormularioService.BuscarVisoesPorTipoFormularioSelect(formulario.SeqTipoFormulario);
        }

        public List<NoArvoreConfiguracaoEtapaVO> BuscarArvoreConfiguracaoEtapa(ConfiguracaoEtapaPaginaFiltroVO filtro)
        {
            List<NoArvoreConfiguracaoEtapaVO> ret = new List<NoArvoreConfiguracaoEtapaVO>();

            var spec = filtro.Transform<ConfiguracaoEtapaPaginaFilterSpecification>();
            spec.SetOrderBy(x => x.Ordem);

            var paginas = this.SearchBySpecification(spec, x => x.ConfiguracaoEtapa, x => x.ConfiguracaoEtapa.ProcessoEtapa, x => x.TextosSecao).ToList();
            int seqNo = 1;

            foreach (var pagina in paginas)
            {
                //Criar nó da página
                var paginaEtapaSGF = this.PaginaService.BuscarPaginaEtapa(pagina.SeqPaginaEtapaSgf);
                var paginaVO = new NoArvoreConfiguracaoEtapaVO
                {
                    SeqConfiguracaoEtapa = pagina.SeqConfiguracaoEtapa,
                    SituacaoEtapa = pagina.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                    SeqEtapaProcesso = pagina.ConfiguracaoEtapa.SeqProcessoEtapa,
                    SeqProcesso = pagina.ConfiguracaoEtapa.ProcessoEtapa.SeqProcesso,
                    SeqItem = pagina.Seq,
                    Tipo = TipoItemPaginaEtapa.Pagina,
                    PaginaExibeFormulario = paginaEtapaSGF.Pagina.ExibeFormulario,
                    PaginaPermiteExibicaoOutrasPaginas = paginaEtapaSGF.Pagina.PermiteExibirEmOutraPagina,
                    PaginaPermiteDuplicar = paginaEtapaSGF.PermiteDuplicar,
                    PaginaObrigatoria = paginaEtapaSGF.Obrigatorio,
                    Descricao = paginaEtapaSGF.Pagina.Titulo,
                    Seq = seqNo++
                };
                ret.Add(paginaVO);

                //Buscar secoes de Texto salvas na Configuração Etapa Página
                foreach (var secaoTexto in pagina.TextosSecao)
                {
                    //Criar nó da seção de texto
                    var secaoTextoVO = new NoArvoreConfiguracaoEtapaVO
                    {
                        Seq = seqNo++,
                        SeqPai = paginaVO.Seq,
                        Tipo = TipoItemPaginaEtapa.Secao,
                        SeqProcesso = paginaVO.SeqProcesso,
                        SeqConfiguracaoEtapa = paginaVO.SeqConfiguracaoEtapa,
                        SeqEtapaProcesso = paginaVO.SeqEtapaProcesso,
                        SeqItem = secaoTexto.Seq
                    };

                    var secaoSGF = this.PaginaService.BuscarSecaoPagina(secaoTexto.SeqSecaoPaginaSgf);
                    secaoTextoVO.Descricao = secaoSGF.Descricao;
                    ret.Add(secaoTextoVO);
                }

                //Buscar secoes de Arquivo no SGF
                var secoesArquivo = this.PaginaService.BuscarSecoesPagina(paginaEtapaSGF.SeqPagina, TipoSecaoPagina.Arquivo);

                foreach (var secaoArquivo in secoesArquivo)
                {
                    //Criar nó da seção de arquivo                        
                    var secaoArquivoVO = new NoArvoreConfiguracaoEtapaVO
                    {
                        Seq = seqNo++,
                        SeqPai = paginaVO.Seq,
                        Tipo = TipoItemPaginaEtapa.Arquivo,
                        SeqProcesso = paginaVO.SeqProcesso,
                        SeqConfiguracaoEtapa = paginaVO.SeqConfiguracaoEtapa,
                        SeqEtapaProcesso = paginaVO.SeqEtapaProcesso,
                        SeqItem = secaoArquivo.Seq,
                        Descricao = secaoArquivo.Descricao
                    };

                    ret.Add(secaoArquivoVO);
                }
            }

            return ret;
        }

        public void DuplicarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            var configuracaoEtapaPaginaCompleto = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina), x => x.ConfiguracaoEtapa.ProcessoEtapa);

            if (configuracaoEtapaPaginaCompleto.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa == SituacaoEtapa.Liberada || configuracaoEtapaPaginaCompleto.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var dadosPagina = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seqConfiguracaoEtapaPagina));
                    var paginaEtapaSGF = this.PaginaService.BuscarPaginaEtapa(dadosPagina.SeqPaginaEtapaSgf);

                    var paginasConfiguracao = this.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = dadosPagina.SeqConfiguracaoEtapa, SeqPaginaEtapaSgf = dadosPagina.SeqPaginaEtapaSgf }).ToList();
                    var ultimaOrdemPagina = paginasConfiguracao.Max(a => a.Ordem);

                    short ordem = ultimaOrdemPagina;
                    ordem++;

                    ConfiguracaoEtapaPagina configuracaoEtapaPagina = new ConfiguracaoEtapaPagina
                    {
                        SeqConfiguracaoEtapa = dadosPagina.SeqConfiguracaoEtapa,
                        SeqPaginaEtapaSgf = dadosPagina.SeqPaginaEtapaSgf,
                        TokenPagina = dadosPagina.TokenPagina,
                        Ordem = ordem,
                        TituloPagina = dadosPagina.TituloPagina,
                        SeqFormulario = null,
                        SeqVisaoFormulario = null
                    };

                    var listaTextosSecaoPagina = new List<TextoSecaoPagina>();

                    foreach (var secaoPagina in paginaEtapaSGF.Pagina.Secoes.Where(x => x.TipoSecaoPagina == TipoSecaoPagina.Texto))
                    {
                        TextoSecaoPagina textoSecaoPagina = new TextoSecaoPagina
                        {
                            SeqSecaoPaginaSgf = secaoPagina.Seq,
                            TokenSecao = secaoPagina.Token,
                            Texto = secaoPagina.TextoPadrao
                        };

                        listaTextosSecaoPagina.Add(textoSecaoPagina);
                    }

                    configuracaoEtapaPagina.TextosSecao = listaTextosSecaoPagina;

                    this.SaveEntity(configuracaoEtapaPagina);

                    //SE HOUVER SOLICITAÇÃO DE SERVIÇO QUE AINDA NÃO FINALIZOU A RESPECTIVA ETAPA, ATUALIZAR O SEU HISTÓRICO DE NAVEGAÇÃO PARA: 
                    //-ATUALIZAR A DATA DE SAÍDA DA ÚLTIMA PÁGINA COM A DATA CORRENTE E,
                    //-INSERIR A PRIMEIRA PÁGINA PARAMETRIZADA PARA A ETAPA EM QUESTÃO E, PREENCHER A DATA DE ENTRADA COM A DATA CORRENTE E A DATA DE SAÍDA IGUAL A NULO.

                    //OBSERVAÇÃO: FOI UTILIZADA A BUSCA NA VIEW PORQUE AO BUSCAR NA SOLICITACAOSERVICODOMAINSERVICE ENVIANDO
                    //O SEQPROCESSOETAPA NÃO RETORNA NENHUM REGISTRO

                    var listaSolicitacoesNaoFinalizaramEtapa = this.ViewCentralSolicitacaoServicoDomainService.SearchBySpecification(new ViewCentralSolicitacaoServicoFilterSpecification()
                    {
                        SeqProcessoEtapa = configuracaoEtapaPaginaCompleto.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                        TipoFiltroCentralSolicitacao = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao,
                        CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                        SeqProcesso = configuracaoEtapaPaginaCompleto.ConfiguracaoEtapa.ProcessoEtapa.SeqProcesso

                    }).ToList();

                    if (listaSolicitacoesNaoFinalizaramEtapa.Any())
                    {
                        var primeiraPaginaFluxo = this.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification()
                        {
                            SeqConfiguracaoEtapa = configuracaoEtapaPaginaCompleto.SeqConfiguracaoEtapa

                        }).OrderBy(o => o.Ordem).FirstOrDefault();

                        string valueNumerosProtocolos = $"'{string.Join(",", listaSolicitacoesNaoFinalizaramEtapa.Select(a => a.NumeroProtocolo).Distinct())}'";
                        var queryHistoricoNavegacaoUltimaPagina = string.Format(QUERY_HISTORICO_NAVEGACAO_ULTIMA_PAGINA, valueNumerosProtocolos);

                        var listaHistoricoNavegacaoUltimasPaginas = this.RawQuery<SolicitacaoHistoricoNavegacao>(queryHistoricoNavegacaoUltimaPagina);

                        foreach (var historicoNavegacaoUltimaPagina in listaHistoricoNavegacaoUltimasPaginas)
                        {
                            var solicitacaoServicoEtapaNavegacao = this.SolicitacaoServicoEtapaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServicoEtapa>(historicoNavegacaoUltimaPagina.SeqSolicitacaoServicoEtapa), x => x.ConfiguracaoEtapa.ProcessoEtapa);

                            //VALIDAÇÃO PARA ATUALIZAR O HISTÓRICO DA SOLICITAÇÃO, SE FOR DA ETAPA EM QUESTÃO, PORQUE RETORNA OS 
                            //HISTÓRICOS DE NAVEGAÇÃO DA SOLICITAÇÃO PROCESSO, E PODE SER DE OUTRA ETAPA 
                            if (configuracaoEtapaPaginaCompleto.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == solicitacaoServicoEtapaNavegacao.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf)
                            {
                                //ATUALIZAR A DATA DE SAÍDA DA ÚLTIMA PÁGINA COM A DATA CORRENTE (HISTÓRICO SEM DATA DE SAÍDA)
                                historicoNavegacaoUltimaPagina.DataSaida = DateTime.Now;

                                this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(historicoNavegacaoUltimaPagina);

                                //INSERIR A PRIMEIRA PÁGINA PARAMETRIZADA PARA A ETAPA EM QUESTÃO E, PREENCHER A DATA DE ENTRADA COM A DATA CORRENTE E A DATA DE SAÍDA IGUAL A NULO
                                SolicitacaoHistoricoNavegacao historicoNavegacaoPrimeiraPagina = historicoNavegacaoUltimaPagina.Transform<SolicitacaoHistoricoNavegacao>();
                                historicoNavegacaoPrimeiraPagina.Seq = 0;
                                historicoNavegacaoPrimeiraPagina.SeqConfiguracaoEtapaPagina = primeiraPaginaFluxo.Seq;
                                historicoNavegacaoPrimeiraPagina.DataEntrada = DateTime.Now;
                                historicoNavegacaoPrimeiraPagina.DataSaida = null;

                                this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(historicoNavegacaoPrimeiraPagina);
                            }
                        }
                    }

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void AdicionarPaginas(long seqConfiguracaoEtapa, List<(long, ConfiguracaoDocumento?)> seqsPaginasEConfiguracoesDocumento) //IEnumerable<long> seqPaginasSGF)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloAdicionarPaginas(seqConfiguracaoEtapa);

                    List<long> seqPaginasSGF = seqsPaginasEConfiguracoesDocumento.Select(c => c.Item1).ToList();

                    //Buscar as páginas no SGF
                    var etapasPagina = this.PaginaService.BuscarPaginasEtapaCompletas(seqPaginasSGF.ToArray());

                    //Adicionar as páginas na configuracao Etapa
                    foreach (var etapaPagina in etapasPagina)
                    {
                        var configDocumento = seqsPaginasEConfiguracoesDocumento.Where(c => c.Item1 == etapaPagina.Seq).FirstOrDefault().Item2;

                        ConfiguracaoEtapaPagina configuracaoEtapaPagina = new ConfiguracaoEtapaPagina
                        {
                            SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                            SeqPaginaEtapaSgf = etapaPagina.Seq,
                            TokenPagina = etapaPagina.Pagina.Token,
                            Ordem = (short)(etapaPagina.Ordem * 10),
                            TituloPagina = etapaPagina.Pagina.Titulo,
                            SeqFormulario = etapaPagina.SeqFormulario,
                            ConfiguracaoDocumento = configDocumento != ConfiguracaoDocumento.Nenhum ? configDocumento : null,
                            SeqVisaoFormulario = null
                        };

                        var listaTextosSecaoPagina = new List<TextoSecaoPagina>();

                        foreach (var secaoPagina in etapaPagina.Pagina.Secoes.Where(x => x.TipoSecaoPagina == TipoSecaoPagina.Texto))
                        {
                            TextoSecaoPagina textoSecaoPagina = new TextoSecaoPagina
                            {
                                SeqSecaoPaginaSgf = secaoPagina.Seq,
                                TokenSecao = secaoPagina.Token,
                                Texto = secaoPagina.TextoPadrao
                            };

                            listaTextosSecaoPagina.Add(textoSecaoPagina);
                        }

                        configuracaoEtapaPagina.TextosSecao = listaTextosSecaoPagina;

                        this.SaveEntity(configuracaoEtapaPagina);
                    }

                    //SE HOUVER SOLICITAÇÃO DE SERVIÇO QUE AINDA NÃO FINALIZOU A RESPECTIVA ETAPA, ATUALIZAR O SEU HISTÓRICO DE NAVEGAÇÃO PARA: 
                    //-ATUALIZAR A DATA DE SAÍDA DA ÚLTIMA PÁGINA COM A DATA CORRENTE E,
                    //-INSERIR A PRIMEIRA PÁGINA PARAMETRIZADA PARA A ETAPA EM QUESTÃO E, PREENCHER A DATA DE ENTRADA COM A DATA CORRENTE E A DATA DE SAÍDA IGUAL A NULO.

                    //OBSERVAÇÃO: FOI UTILIZADA A BUSCA NA VIEW PORQUE AO BUSCAR NA SOLICITACAOSERVICODOMAINSERVICE ENVIANDO
                    //O SEQPROCESSOETAPA NÃO RETORNA NENHUM REGISTRO

                    var configuracaoEtapa = this.ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), x => x.ProcessoEtapa);
                    //Levando em consideração a configuração de etapa
                    var listaSolicitacoesNaoFinalizaramEtapa = this.ViewCentralSolicitacaoServicoDomainService.SearchBySpecification(new ViewCentralSolicitacaoServicoFilterSpecification()
                    {
                        SeqProcessoEtapa = configuracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                        TipoFiltroCentralSolicitacao = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao,
                        CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                        SeqProcesso = configuracaoEtapa.ProcessoEtapa.SeqProcesso,
                        SeqConfiguaracaoEtapa = seqConfiguracaoEtapa

                    }).ToList();

                    if (listaSolicitacoesNaoFinalizaramEtapa.Any())
                    {
                        var primeiraPaginaFluxo = this.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification()
                        {
                            SeqConfiguracaoEtapa = configuracaoEtapa.Seq

                        }).OrderBy(o => o.Ordem).FirstOrDefault();

                        string valueNumerosProtocolos = $"'{string.Join(",", listaSolicitacoesNaoFinalizaramEtapa.Select(a => a.NumeroProtocolo).Distinct())}'";
                        var queryHistoricoNavegacaoUltimaPagina = string.Format(QUERY_HISTORICO_NAVEGACAO_ULTIMA_PAGINA, valueNumerosProtocolos);

                        var listaHistoricoNavegacaoUltimasPaginas = this.RawQuery<SolicitacaoHistoricoNavegacao>(queryHistoricoNavegacaoUltimaPagina);

                        foreach (var historicoNavegacaoUltimaPagina in listaHistoricoNavegacaoUltimasPaginas)
                        {
                            var solicitacaoServicoEtapaNavegacao = this.SolicitacaoServicoEtapaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServicoEtapa>(historicoNavegacaoUltimaPagina.SeqSolicitacaoServicoEtapa), x => x.ConfiguracaoEtapa.ProcessoEtapa);

                            //VALIDAÇÃO PARA ATUALIZAR O HISTÓRICO DA SOLICITAÇÃO, SE FOR DA ETAPA EM QUESTÃO, PORQUE RETORNA OS 
                            //HISTÓRICOS DE NAVEGAÇÃO DA SOLICITAÇÃO PROCESSO, E PODE SER DE OUTRA ETAPA 
                            if (configuracaoEtapa.ProcessoEtapa.SeqEtapaSgf == solicitacaoServicoEtapaNavegacao.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf)
                            {
                                //ATUALIZAR A DATA DE SAÍDA DA ÚLTIMA PÁGINA COM A DATA CORRENTE (HISTÓRICO SEM DATA DE SAÍDA)
                                historicoNavegacaoUltimaPagina.DataSaida = DateTime.Now;

                                this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(historicoNavegacaoUltimaPagina);

                                //INSERIR A PRIMEIRA PÁGINA PARAMETRIZADA PARA A ETAPA EM QUESTÃO E, PREENCHER A DATA DE ENTRADA COM A DATA CORRENTE E A DATA DE SAÍDA IGUAL A NULO
                                SolicitacaoHistoricoNavegacao historicoNavegacaoPrimeiraPagina = historicoNavegacaoUltimaPagina.Transform<SolicitacaoHistoricoNavegacao>();
                                historicoNavegacaoPrimeiraPagina.Seq = 0;
                                historicoNavegacaoPrimeiraPagina.SeqConfiguracaoEtapaPagina = primeiraPaginaFluxo.Seq;
                                historicoNavegacaoPrimeiraPagina.DataEntrada = DateTime.Now;
                                historicoNavegacaoPrimeiraPagina.DataSaida = null;

                                this.SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(historicoNavegacaoPrimeiraPagina);
                            }
                        }
                    }

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public long Salvar(ConfiguracaoEtapaPaginaVO modelo)
        {
            ValidarModeloSalvar(modelo);

            var dominio = modelo.Transform<ConfiguracaoEtapaPagina>();

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        private void ValidarModeloAdicionarPaginas(long seqConfiguracaoEtapa)
        {
            var situacao = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(seqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacaoEtapa);

            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();
        }

        private void ValidarModeloSalvar(ConfiguracaoEtapaPaginaVO modelo)
        {
            var situacao = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacaoEtapa);

            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();

            if (modelo.SeqFormulario.HasValue)
            {
                var configuracaoPaginaPorFormulario = this.SearchBySpecification(new ConfiguracaoEtapaPaginaFilterSpecification() { SeqConfiguracaoEtapa = modelo.SeqConfiguracaoEtapa, SeqFormulario = modelo.SeqFormulario }).ToList();

                if (configuracaoPaginaPorFormulario.Any(a => a.Seq != modelo.Seq))
                    throw new FormularioJaAssociadoEmOutraPaginaEtapaException();
            }
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloExcluir(seq);

                    //AVALIAR SE HÁ ALGUMA SOLICITAÇÃO QUE A ETAPA ATUAL É A RESPECTIVA ETAPA, A SOLICITAÇÃO ESTÁ EM ABERTO 
                    //E POSSUI ASSOCIADA A RESPECTIVA PÁGINA NO HISTÓRICO DE NAVEGAÇÃO. EM CASO POSITIVO, 
                    //ABORTAR A OPERAÇÃO E EXIBIR A SEGUINTE MENSAGEM IMPEDITIVA: 
                    //"OPÇÃO INDISPONÍVEL. EXISTE SOLICITAÇÃO EM ABERTO E CONSTA ESSA PÁGINA NO FLUXO DE NAVEGAÇÃO."

                    //OBSERVAÇÃO: FOI UTILIZADA A BUSCA NA VIEW PORQUE AO BUSCAR NA SOLICITACAOSERVICODOMAINSERVICE ENVIANDO
                    //O SEQPROCESSOETAPA NÃO RETORNA NENHUM REGISTRO

                    var configuracaoEtapaPagina = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seq), x => x.ConfiguracaoEtapa.ProcessoEtapa);

                    var listaSolicitacoesEmAberto = this.ViewCentralSolicitacaoServicoDomainService.SearchBySpecification(new ViewCentralSolicitacaoServicoFilterSpecification()
                    {
                        SeqProcessoEtapa = configuracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                        TipoFiltroCentralSolicitacao = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao,
                        CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento, CategoriaSituacao.Concluido },
                        SeqProcesso = configuracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SeqProcesso

                    }).ToList();

                    #region Validação antiga para verificar se existe página associada ao histórico de navegação

                    //bool existeSolicitacaoHistorico = this.SolicitacaoHistoricoNavegacaoDomainService.ValidarSeExisteSolicitacaoHistoricoPorConfiguracaoEtapaPagina(seq);
                    //if (existeSolicitacaoHistorico)

                    #endregion

                    if (listaSolicitacoesEmAberto.Any())
                    {
                        string valueNumerosProtocolos = $"'{string.Join(",", listaSolicitacoesEmAberto.Select(a => a.NumeroProtocolo).Distinct())}'";
                        var queryHistoricoNavegacaoPagina = string.Format(QUERY_HISTORICO_NAVEGACAO_PAGINA, valueNumerosProtocolos, seq);

                        var listaHistoricoNavegacaoPagina = this.RawQuery<SolicitacaoHistoricoNavegacao>(queryHistoricoNavegacaoPagina);

                        if (listaHistoricoNavegacaoPagina.Any())
                            throw new JaExisteSolicitacaoHistoricoNavegacaoException();
                    }

                    this.DeleteEntity(configuracaoEtapaPagina);

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public void ValidarModeloExcluir(long seq)
        {
            var configuracaoEtapaPagina = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(seq), x => x.ConfiguracaoEtapa.ProcessoEtapa);

            var situacao = configuracaoEtapaPagina.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;
            if (situacao == SituacaoEtapa.Liberada || situacao == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOpcaoIndisponivelEtapaException();
        }
    }
}