using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Service.Areas.SRC.Services;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.Jobs
{
    public class EfetivarRenovacaoMatriculaAutomaticaWebJob : SMCWebJobBase<EfetivarRenovacaoMatriculaAutomaticaSATVO, EfetivarRenovacaoMatriculaAutomaticaVO>
    {
        #region [ DomainService ]

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => new SolicitacaoMatriculaDomainService();

        private RegistroDocumentoDomainService RegistroDocumentoDomainService => new RegistroDocumentoDomainService();

        private ViewCentralSolicitacaoServicoDomainService ViewCentralSolicitacaoServicoDomainService => new ViewCentralSolicitacaoServicoDomainService();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => new SolicitacaoMatriculaItemDomainService();

        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService => new SolicitacaoHistoricoNavegacaoDomainService();

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService => new SolicitacaoDocumentoRequeridoDomainService();

        #endregion [ DomainService ]

        #region [ RawQuery ]

        private const string BUSCAR_SOLICITACOES_RENOVACAO_MATRICULA = @"
select
	-- dados do processo
	SeqProcesso = pr.seq_processo,
	DescricaoProcesso = pr.dsc_processo,
    TokenServico = sr.dsc_token,
	-- dados da solicitação
	SeqSolicitacaoServico = ss.seq_solicitacao_servico,
	NumProtocolo = ss.num_protocolo,
	SeqConfiguracaoProcesso = ss.seq_configuracao_processo,
	SituacaoDocumentacao = ss.idt_dom_situacao_documentacao,
	-- dados da etapa
	SeqSolicitacaoServicoEtapa = sse.seq_solicitacao_servico_etapa,
	SeqConfiguracaoEtapa = ce.seq_configuracao_etapa,
	SeqSituacaoEtapaSgf = hs.seq_situacao_etapa_sgf,
	SeqConfiguracaoEtapaPagina = cep.seq_configuracao_etapa_pagina,
	-- dados do solicitante
    SeqPessoaAtuacao = ss.seq_pessoa_atuacao,
	NomePessoa = pdp.nom_pessoa
from	src.solicitacao_servico ss
join	src.configuracao_processo cp
		on ss.seq_configuracao_processo = cp.seq_configuracao_processo
join	src.processo pr
		on cp.seq_processo = pr.seq_processo
join	src.servico sr
		on pr.seq_servico = sr.seq_servico
		and sr.dsc_token = 'SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU' -- solicitações de renovação
join	src.solicitacao_historico_situacao hs
		on ss.seq_solicitacao_historico_situacao_atual = hs.seq_solicitacao_historico_situacao
		and hs.idt_dom_categoria_situacao = 2 -- Em andamento
join	src.solicitacao_servico_etapa sse
		on hs.seq_solicitacao_servico_etapa = sse.seq_solicitacao_servico_etapa
join	src.configuracao_etapa ce
		on sse.seq_configuracao_etapa = ce.seq_configuracao_etapa
join	src.processo_etapa pe
		on ce.seq_processo_etapa = pe.seq_processo_etapa
		and pe.dsc_token = 'EFETIVACAO_RENOVACAO_MATRICULA' -- solicitação na etapa de efetivação
		and pe.idt_dom_situacao_etapa = 2 -- etapa de efetivação está liberada
join	pes.pessoa_atuacao pa
		on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
join	pes.pessoa_dados_pessoais pdp
		on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
join	src.grupo_escalonamento ge
		on ss.seq_grupo_escalonamento = ge.seq_grupo_escalonamento
join	src.grupo_escalonamento_item gei
		on ge.seq_grupo_escalonamento = gei.seq_grupo_escalonamento
join	src.escalonamento e
		on gei.seq_escalonamento = e.seq_escalonamento
		and e.seq_processo_etapa = pe.seq_processo_etapa
join	src.configuracao_etapa_pagina cep
		on ce.seq_configuracao_etapa = cep.seq_configuracao_etapa
		and cep.dsc_token_pagina = 'CONCLUSAO_MATRICULA'
where	getdate() between e.dat_inicio_escalonamento and pr.dat_fim -- hoje está entre o inicio do escalonamento da etapa e o fim do processo
{0}
order by
	pr.dsc_processo,
	pdp.nom_pessoa
";

        #endregion [ RawQuery ]

        #region [ Variaveis auxiliares ]

        public Dictionary<int, Dictionary<string, int>> SolicitacoesProcesso { get; set; }

        public string DescricaoProcessoAtual { get; set; }

        public int QuantidadeSolicitacoesProcessoAtual { get; set; }

        public int QuantidadeProcessadas { get; set; }

        public int IndiceAtual { get; set; }

        #endregion [ Variaveis auxiliares ]

        /// <summary>
        /// Recupera todas as solicitações de matrícula que devem ser efetivadas automaticamente
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public override ICollection<EfetivarRenovacaoMatriculaAutomaticaVO> GetItems(EfetivarRenovacaoMatriculaAutomaticaSATVO filter)
        {
            Scheduler.LogSucess($"Recuperando solicitações");

            string queryAnd = filter.SeqProcesso.HasValue ? $" and pr.seq_processo = { filter.SeqProcesso.Value }" : "";

            List<EfetivarRenovacaoMatriculaAutomaticaVO> lote = SolicitacaoMatriculaDomainService.RawQuery<EfetivarRenovacaoMatriculaAutomaticaVO>(string.Format(BUSCAR_SOLICITACOES_RENOVACAO_MATRICULA, queryAnd));
            Scheduler.LogSucess($"Foram encontradas {lote.Count} solicitações");

            //Agrupamento dos processos e a quantidade de solicitações por processo
            var agrupamentos = lote.GroupBy(g => g.DescricaoProcesso).Select(s => new
            {
                descricao = s.Key,
                quantidade = s.Count()
            }).ToList();

            //Criação de dicionario com valores encontrados no lote
            int indiceDicionario = 1;
            SolicitacoesProcesso = new Dictionary<int, Dictionary<string, int>>();
            foreach (var agrupamento in agrupamentos)
            {
                Dictionary<string, int> item = new Dictionary<string, int>();
                item.Add(agrupamento.descricao, agrupamento.quantidade);
                SolicitacoesProcesso.Add(indiceDicionario, item);
                indiceDicionario++;
            }

            if (lote.SMCAny())
            {
                //Preenchimento de variaveis globais
                IndiceAtual = 1;
                DescricaoProcessoAtual = SolicitacoesProcesso[IndiceAtual].First().Key;
                QuantidadeSolicitacoesProcessoAtual = SolicitacoesProcesso[IndiceAtual].First().Value;
                QuantidadeProcessadas = 1;

                //Titulo do primeiro processo
                //Processando a quantidade de solicitações processo atual
                Scheduler.LogSucess($"Processando {QuantidadeSolicitacoesProcessoAtual} { (QuantidadeSolicitacoesProcessoAtual > 1 ? "solicitações" : "solicitação") } do {DescricaoProcessoAtual} ");
            }

            return lote;
        }

        /// <summary>
        /// Processa a atualização da data de término de um beneficio
        /// </summary>
        /// <param name="item">Dados do beneficio</param>
        /// <returns>True caso seja atualizado com sucesso</returns>
        public override bool ProcessItem(EfetivarRenovacaoMatriculaAutomaticaVO item)
        {
            try
            {
                if (QuantidadeProcessadas > QuantidadeSolicitacoesProcessoAtual)
                {
                    IndiceAtual++;
                    DescricaoProcessoAtual = SolicitacoesProcesso[IndiceAtual].First().Key;
                    QuantidadeSolicitacoesProcessoAtual = SolicitacoesProcesso[IndiceAtual].First().Value;
                    QuantidadeProcessadas = 1;
                    //Processando a quantidade de solicitações processo atual
                    Scheduler.LogSucess($"Processando {QuantidadeSolicitacoesProcessoAtual} { (QuantidadeSolicitacoesProcessoAtual > 1 ? "solicitações" : "solicitação") } do {DescricaoProcessoAtual} ");
                }

                // So permite efetivar caso a situação seja entregue ou entregue com pendência, ou caso não tenha nenhum obrigatório
                if (item.SituacaoDocumentacao != SituacaoDocumentacao.Entregue && 
                    item.SituacaoDocumentacao != SituacaoDocumentacao.EntregueComPendencia &&
                    item.SituacaoDocumentacao != SituacaoDocumentacao.NaoRequerida &&
                    item.SituacaoDocumentacao != SituacaoDocumentacao.Nenhum)
                {
                    Scheduler.LogWaring($"A solicitação de protocolo {item.NumProtocolo} - {item.NomePessoa} não foi processada, pois existe documento aguardando validação.");
                }
                else
                {
                    // Verifica se existe documento na situação "Pendente" sem data de prazo de entrega
                    var specDoc = new SolicitacaoDocumentoRequeridoFilterSpecification()
                    {
                        SeqSolicitacaoServico = item.SeqSolicitacaoServico,
                        SituacaoEntregaDocumento = SituacaoEntregaDocumento.Pendente
                    };
                    var listaDocPendente = SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(specDoc).ToList();
                    if (listaDocPendente.Any(d => !d.DataPrazoEntrega.HasValue))
                    {
                        Scheduler.LogWaring($"A solicitação de protocolo {item.NumProtocolo} - {item.NomePessoa} não foi processada, pois é necessário informar a data de prazo de entrega para os documentos que estão pendentes.");
                    }
                    else
                    {
                        // Verifica se solicitação tem bloqueio
                        ViewCentralSolicitacaoServicoFilterSpecification spec = new ViewCentralSolicitacaoServicoFilterSpecification() { NumeroProtocolo = item.NumProtocolo };
                        bool? solicitacaoPossuiBloqueio = ViewCentralSolicitacaoServicoDomainService.SearchProjectionBySpecification(spec, p => p.BloqueioFimEtapa).FirstOrDefault();
                        if (solicitacaoPossuiBloqueio.GetValueOrDefault())
                        {
                            Scheduler.LogWaring($"A solicitação de protocolo {item.NumProtocolo} - {item.NomePessoa} não foi processada, pois possui bloqueio.");
                        }
                        else 
                        {
                            // Realiza a efetivação da matricula automática
                            EfetivacaoMatriculaVO modeloEfetivacao = new EfetivacaoMatriculaVO()
                            {
                                SeqSolicitacaoServico = item.SeqSolicitacaoServico,
                                SeqConfiguracaoEtapa = item.SeqConfiguracaoEtapa,
                                TokenServico = item.TokenServico
                            };
                            SolicitacaoMatriculaDomainService.EfetivarRenovacaoMatricula(modeloEfetivacao);

                            // Verifica os documentos da pessoa
                            var documentosSolicitacao = RegistroDocumentoDomainService.ValidarDocumentoObrigatorio(item.SeqSolicitacaoServico);
                            SolicitacaoMatriculaDomainService.SalvarDocumentoPessoaAtuacao(documentosSolicitacao, item.SeqPessoaAtuacao, item.SeqPessoaAtuacao, item.TokenServico, item.SeqSolicitacaoServico);

                            // Prosseguir e acionar a próxima página de acordo com a RN_MAT_004 - Fluxo de Páginas.
                            SolicitacaoHistoricoNavegacao historico = new SolicitacaoHistoricoNavegacao
                            {
                                DataEntrada = DateTime.Now,
                                SeqConfiguracaoEtapaPagina = item.SeqConfiguracaoEtapaPagina,
                                SeqSolicitacaoServicoEtapa = item.SeqSolicitacaoServicoEtapa
                            };
                            SolicitacaoHistoricoNavegacaoDomainService.SaveEntity(historico);

                            Scheduler.LogSucess($"A solicitação de protocolo {item.NumProtocolo} - {item.NomePessoa} processada com sucesso.");
                        }
                    }
                }

                QuantidadeProcessadas++;
            }
            catch (SMCApplicationException ex)
            {
                Scheduler.LogWaring($"Ocorreu um erro ao efetivar a matricula de protocolo {item.NumProtocolo}. Mensagem: {ex.Message}", item.SeqSolicitacaoServico, item.NomePessoa);
            }
            catch (Exception ex)
            {
                Scheduler.LogWaring($"Ocorreu um erro ao efetivar a matricula de protocolo {item.NumProtocolo}. Erro: {ex}", item.SeqSolicitacaoServico, item.NomePessoa);
                return false;
            }
            return true;
        }
    }
}