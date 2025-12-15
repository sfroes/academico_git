using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class MensagemDomainService : AcademicoContextDomain<Mensagem>
    {
        #region [ DomainServices ]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private InstituicaoNivelTipoMensagemDomainService InstituicaoNivelTipoMensagemDomainService => Create<InstituicaoNivelTipoMensagemDomainService>();

        private MensagemPessoaAtuacaoDomainService MensagemPessoaAtuacaoDomainService => Create<MensagemPessoaAtuacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private TipoMensagemDomainService TipoMensagemDomainService => Create<TipoMensagemDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IEtapaService EtapaService => Create<IEtapaService>();

        #endregion [ Services ]

        /// <summary>
        /// Ao salvar (incluir, alterar ou importar) um material (Caso o tipo de origem do material seja "Divisão de turma"),
        /// apenas para material do tipo arquivo/link, gravar uma mensagem e associá-la a todos os alunos da divisão da turma em questão.
        /// Os seguintes dados deverão ser gravados para cada mensagem:
        /// - Tipo de mensagem: usar tipo de mensagem da categoria "Linha do Tempo" disponível para o tipo de atuação "Aluno" com token "MATERIAL_DIDATICO_INCLUSAO” em caso de inclusão de novo material e "MATERIAL_DIDATICO_ALTERACAO” em caso de alteração de material.
        /// - Descrição da mensagem: mensagem padrão parametrizada por instituição e nível para o tipo de mensagem em questão.
        /// - Data início igual a data da postagem e data fim vazia
        /// - A data e o usuário de inclusão.
        /// - TAGs possíveis:
        /// {{TURMA}} - utilizar a RN_TUR_025 - Exibição Descrição Turma
        /// {{MATERIAL}} - utilizar a descrição do material
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma para associar a mensagem aos alunos desta divisão.</param>
        /// <param name="tokenMensagem">Token da mensagem.</param>
        public void EnviarMensagemMaterialDidatico(long seqDivisaoTurma, string descricaoMaterial, bool novaMensagem)
        {
            var tokenMensagem = (novaMensagem) ?
                                    TOKEN_TIPO_MENSAGEM.MATERIAL_DIDATICO_INCLUSAO :
                                    TOKEN_TIPO_MENSAGEM.MATERIAL_DIDATICO_ALTERACAO;
            TipoMensagem tm = RecuperarTipoMensagem(tokenMensagem, CategoriaMensagem.LinhaDoTempo);
            if (tm == null || tm.Seq == 0)
            {
                return;
            }

            var divisaoTurma = DivisaoTurmaDomainService.BuscarDivisaoTurmaDetalhes(seqDivisaoTurma).FirstOrDefault(d => d.Seq == seqDivisaoTurma);
            InstituicaoNivelTipoMensagem intm = BuscarInstituicaoNivelTipoMensagem(divisaoTurma.SeqInstituicaoEnsino, divisaoTurma.SeqNivelEnsino, tm.Seq);
            if (intm == null || intm.Seq == 0)
            {
                return;
            }

            // FIX: Verificar regra de buscar descrição da turma
            var origemMaterial = DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), x => x.OrigemMaterial);

            //Criando a mensagem que será enviada aos alunos da divisão de turma.
            Mensagem mensagem = new Mensagem
            {
                SeqTipoMensagem = tm.Seq,
                Descricao = intm.MensagemPadrao.Replace(TOKEN_TAG_MENSAGEM.TURMA, origemMaterial.Descricao).Replace(TOKEN_TAG_MENSAGEM.MATERIAL, descricaoMaterial),
                DataInicioVigencia = DateTime.Now
            };

            //Salvando a mensagem
            SaveEntity(mensagem);

            //Associando a mensagem salva aos alunos da divisão de turma.
            var listaAlunos = DivisaoTurmaDomainService.ListarAlunosPorDivisaoTurma(seqDivisaoTurma);
            foreach (var aluno in listaAlunos)
            {
                MensagemPessoaAtuacao mpa = new MensagemPessoaAtuacao() { SeqMensagem = mensagem.Seq, SeqPessoaAtuacao = aluno.Seq };
                MensagemPessoaAtuacaoDomainService.SaveEntity(mpa);
            }
        }

        /// <summary>
        /// Cria e envia uma mensagem PADRÃO PARAMETRIZADA por instituição e nível às 'pessoas-atuação' informadas, com base em um tipo de mensagem e categoria.
        /// </summary>
        /// <param name="listaSeqsPessoaAtuacao">Lista de sequenciais de pessoa atuação que receberão a mensagem.</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino.</param>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino para auxiliar na busca do tipo da mensagem da instituição.</param>
        /// <param name="token">Token do tipo de mensagem.</param>
        /// <param name="categoria">Categoria do tipo de mensagem.</param>
        public void EnviarMensagemPessoasAtuacao(List<long> listaSeqsPessoaAtuacao, long seqInstituicaoEnsino, long seqNivelEnsino, string token, CategoriaMensagem categoria)
        {
            TipoMensagem tm = RecuperarTipoMensagem(token, categoria);
            if (tm == null || tm.Seq == 0)
            {
                return;
            }

            InstituicaoNivelTipoMensagem intm = BuscarInstituicaoNivelTipoMensagem(seqInstituicaoEnsino, seqNivelEnsino, tm.Seq);
            if (intm == null || intm.Seq == 0)
            {
                return;
            }

            //Criando a mensagem que será enviada.
            Mensagem m = new Mensagem();
            m.SeqTipoMensagem = tm.Seq;
            m.Descricao = intm.MensagemPadrao;
            m.DataInicioVigencia = DateTime.Now;

            //Salvando a mensagem
            SaveEntity(m);

            //Associando a mensagem salva às pessoas informadas.
            foreach (var pessoaAtuacao in listaSeqsPessoaAtuacao)
            {
                MensagemPessoaAtuacao mpa = new MensagemPessoaAtuacao() { SeqMensagem = m.Seq, SeqPessoaAtuacao = pessoaAtuacao };
                MensagemPessoaAtuacaoDomainService.SaveEntity(mpa);
            }
        }

        private TipoMensagem RecuperarTipoMensagem(string token, CategoriaMensagem categoria)
        {
            TipoMensagemFilterSpecification specTM = new TipoMensagemFilterSpecification();
            specTM.Token = token;
            specTM.CategoriaMensagem = categoria;
            List<TipoMensagem> listaTM = TipoMensagemDomainService.SearchBySpecification(specTM).ToList();
            return listaTM.FirstOrDefault();
        }

        private InstituicaoNivelTipoMensagem BuscarInstituicaoNivelTipoMensagem(long seqInstituicaoEnsino, long seqNivelEnsino, long seqTipoMensagem)
        {
            var spec = new InstituicaoNivelTipoMensagemFilterSpecification() { SeqNivelEnsino = seqNivelEnsino, SeqTipoMensagem = seqTipoMensagem, SeqInstituicaoEnsino = seqInstituicaoEnsino };
            return InstituicaoNivelTipoMensagemDomainService.SearchBySpecification(spec, a => a.TipoMensagem).FirstOrDefault();
        }

        /// <summary>
        /// Busca mensagens de um aluno.
        /// </summary>
        /// <param name="categoria">Categoria da mensagem.</param>
        /// <param name="tipoUso">Tipo de uso da mensagem.</param>
        /// <param name="seq">Sequencial do aluno (pessoa atuação).</param>
        /// <returns>Lista de mesagens do aluno.</returns>
        public List<Mensagem> BuscarMensagensAluno(CategoriaMensagem categoria, TipoUsoMensagem tipoUso, long seqPessoaAtuacao)
        {
            MensagemFilterSpecification spec = new MensagemFilterSpecification();
            spec.SeqPessoaAtuacao = seqPessoaAtuacao;
            spec.CategoriaMensagem = categoria;
            spec.TipoUsoMensagem = tipoUso;

            return SearchBySpecification(spec).ToList();
        }

        /// <summary>
        /// Cria e envia uma mensagem PADRÃO PARAMETRIZADA por instituição e nível à 'pessoa-atuação' informada, com base em um tipo de mensagem e categoria.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação que receberá a mensagem.</param>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino para auxiliar na busca do tipo da mensagem da instituição.</param>
        /// <param name="token">Token do tipo de mensagem.</param>
        /// <param name="categoria">Categoria do tipo de mensagem.</param>
        /// <param name="tags">dicionário com as tags e seus respectivos valores para substituir na mensagem padrão, se for o caso.</param>
        public void EnviarMensagemPessoaAtuacao(long seqPessoaAtuacao, long seqInstituicaoEnsino, long seqNivelEnsino, string token, CategoriaMensagem categoria, Dictionary<string, string> tags)
        {
            TipoMensagem tm = RecuperarTipoMensagem(token, categoria);
            if (tm == null || tm.Seq == 0)
            {
                return;
            }

            InstituicaoNivelTipoMensagem intm = BuscarInstituicaoNivelTipoMensagem(seqInstituicaoEnsino, seqNivelEnsino, tm.Seq);
            if (intm == null || intm.Seq == 0)
            {
                return;
            }

            // Substituindo as tags na mensagem padrão.
            string mensagem = intm.MensagemPadrao;
            if (tags != null && tags.Count > 0)
            {
                foreach (var tag in tags)
                {
                    mensagem = mensagem.Replace(tag.Key, tag.Value);
                }
            }

            // Criando a mensagem que será enviada.
            Mensagem m = new Mensagem();
            m.SeqTipoMensagem = tm.Seq;
            m.Descricao = mensagem;
            m.DataInicioVigencia = DateTime.Now;

            // Salvando a mensagem
            SaveEntity(m);

            // Associando a mensagem salva às pessoa informada.
            MensagemPessoaAtuacao mpa = new MensagemPessoaAtuacao() { SeqMensagem = m.Seq, SeqPessoaAtuacao = seqPessoaAtuacao };
            MensagemPessoaAtuacaoDomainService.SaveEntity(mpa);
        }

        /// <summary>
        /// Sequencial da solicitação de serviço que foi encerrada
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="tokenEncerramento">Token com tipo de encerramento da solicitação</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        public void EnviarMensagemLinhaDoTempoEncerramentoSolicitacao(long seqSolicitacaoServico, string tokenEncerramento, bool desabilitarFiltro = false)
        {
            if (desabilitarFiltro)
            {
                SolicitacaoServicoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            // Busca a solicitação de serviço com os parâmetros para mensagem
            var solicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => new
            {
                x.SeqPessoaAtuacao,
                x.NumeroProtocolo,
                x.ConfiguracaoProcesso.Processo.Descricao,
                x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault().SituacaoAtual.SeqSituacaoEtapaSgf
            });

            // Busca a situação atual da solicitação apos a finalização da etapa
            var DescricaoSituacaoAtual = EtapaService.BuscarSituacaoEtapa(solicitacao.SeqSituacaoEtapaSgf).Situacao.Descricao;

            // Criar mensagem na linha do tempo do aluno (RN_SRC_067)
            Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROTOCOLO_SOLICITACAO, solicitacao.NumeroProtocolo);
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.PROCESSO_SOLICITACAO, solicitacao.Descricao);
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.SITUACAO_ATUAL_SOLICITACAO, DescricaoSituacaoAtual);

            var dadosPessoa = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacao.SeqPessoaAtuacao, desabilitarFiltro);

            this.EnviarMensagemPessoaAtuacao(solicitacao.SeqPessoaAtuacao,
                                                              dadosPessoa.SeqInstituicaoEnsino,
                                                              dadosPessoa.SeqNivelEnsino,
                                                              tokenEncerramento,
                                                              CategoriaMensagem.LinhaDoTempo,
                                                              dicTagsMsg);

            if (desabilitarFiltro)
            {
                SolicitacaoServicoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }
        }
    }
}