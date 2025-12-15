using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.Jobs
{
    internal class VerificarBloqueioPendenciaBibliotecaWebJob : SMCWebJobBase<VerificarPendenciaBibliotecaSATVO, VerificarPendenciaBibliotecaAutomaticaVO>
    {
        #region [DomainService]

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => this.Create<PessoaAtuacaoBloqueioDomainService>();

        #endregion

        #region [RawQuery]

        private const string SOLICITACOES_DOCUMENTO_CONCLUSAO_ABERTAS = @"
        select	distinct
	        ss.seq_pessoa_atuacao as SeqPessoaAtuacao,
	        ss.seq_solicitacao_servico as SeqSolicitacaoServico,
	        ss.num_protocolo as NumeroProtocolo,
	        pdp.nom_pessoa as NomePessoa
        from	CNC.solicitacao_documento_conclusao sdc
        join	SRC.solicitacao_servico ss
		        on sdc.seq_solicitacao_servico = ss.seq_solicitacao_servico
        join	CNC.documento_academico da
		        on da.seq_solicitacao_servico = sdc.seq_solicitacao_servico
		        and da.num_via_documento = 1 -- documento de primeira via
        join	CNC.documento_conclusao dc
		        on da.seq_documento_academico = dc.seq_documento_academico
        join	SRC.solicitacao_historico_situacao shs
		        on ss.seq_solicitacao_historico_situacao_atual = shs.seq_solicitacao_historico_situacao
		        and shs.idt_dom_categoria_situacao in (1,2,3) -- Novo, Em andamento, Concluído
        join	PES.pessoa_atuacao pa
		        on ss.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
        join	PES.pessoa_dados_pessoais pdp
		        on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais 
        {0}
        order by pdp.nom_pessoa
        ";

        #endregion

        public int QuantidadeSolicitacoesLote { get; set; }

        public int QuantidadeProcessadas { get; set; }

        /// <summary>
        /// Recupera todas as solicitações de documento de conclusão que estão abertas e 
        /// que geraram documentos de 1a via para verificar os bloqueios.
        /// </summary>
        /// <param name="filter">Filtros para o agendamento SAT</param>
        /// <returns>Lista de objetos para processar no JOB</returns>
        public override ICollection<VerificarPendenciaBibliotecaAutomaticaVO> GetItems(VerificarPendenciaBibliotecaSATVO filtro)
        {
            Scheduler.LogSucess($"Recuperando solicitações");

            // Verifica se informou uma pessoa-atuação específica
            string where = string.Empty;
            if (filtro.SeqPessoaAtuacao.HasValue && filtro.SeqPessoaAtuacao > 0)
                where = $" where pa.seq_pessoa_atuacao = {filtro.SeqPessoaAtuacao.Value}";

            // Busca o lote de registros para processamento
            List<VerificarPendenciaBibliotecaAutomaticaVO> lote = PessoaAtuacaoBloqueioDomainService.RawQuery<VerificarPendenciaBibliotecaAutomaticaVO>(string.Format(SOLICITACOES_DOCUMENTO_CONCLUSAO_ABERTAS, where));

            // Verifica se encontrou algum registro
            if (lote.Count > 0)
            {
                Scheduler.LogSucess($"Foram encontradas {lote.Count} solicitações");
                QuantidadeSolicitacoesLote = lote.Count;
                QuantidadeProcessadas = 0;
            }
            else
                Scheduler.LogSucess("Não foram encontradas solicitações para processamento");

            return lote;
        }

        /// <summary>
        /// Realiza a verificação de pendência de biblioteca de cada item encontrado
        /// </summary>
        /// <param name="item">Item a ser processado</param>
        /// <returns>TRUE em caso de sucesso no processamento, FALSE caso contrário</returns>
        public override bool ProcessItem(VerificarPendenciaBibliotecaAutomaticaVO item)
        {
            using (var transaction = SMCUnitOfWork.Begin())
            {
                try
                {
                    PessoaAtuacaoBloqueioDomainService.VerificaBloqueioPendenciaBiblioteca(item.SeqPessoaAtuacao);
                    QuantidadeProcessadas++;

                    if ((QuantidadeProcessadas % 100) == 0)
                        Scheduler.LogWaring($"Processando {QuantidadeProcessadas} de {QuantidadeSolicitacoesLote}");

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Scheduler.LogWaring($"Ocorreu um erro verificar pendências de biblioteca para a solicitação {item.NumeroProtocolo}. Erro: {ex}", item.SeqSolicitacaoServico, item.NomePessoa);
                    return false;
                }
            }
        }
    }
}