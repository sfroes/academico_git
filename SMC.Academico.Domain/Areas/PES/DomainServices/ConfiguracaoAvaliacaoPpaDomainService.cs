using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Repositories;
using SMC.AvaliacaoPermanente.ServiceContract.Areas.QST.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class ConfiguracaoAvaliacaoPpaDomainService : AcademicoContextDomain<ConfiguracaoAvaliacaoPpa>
    {
        #region [Services]

        private IAmostraService AmostraService => Create<IAmostraService>();

        #endregion [Services]

        #region [ DomainServices ]

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        #endregion [ DomainServices ]

        #region [ Repositories ]

        private IAcademicoRepository AcademicoRepository => this.Create<IAcademicoRepository>();

        #endregion


        /// <summary>
        /// Encontrar na tabela de configuracao_avaliacao_PPA a configuração de avaliação que:
        /// - a unidade resposnsável seja a unidade responsável informada por parâmetro;
        /// - o tipo de avaliação seja igual ao tipo informada por parâmetro;
        /// - esteja válida (data de hoje entre inicio e fim de validade);
        /// - a data limite de resposta seja maior que a data do dia;
        /// - o nível de ensino seja igual ao informado no parâmetro;
        /// Obs.: Se não encontrou nenhuma configuração com todas os filtros acima, refazer a pesquisa sem
        /// considerar o nível de ensino.
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencial da entidade responsável</param>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <param name="tipoAvaliacao">Tipo de avaliação</param>
        /// <returns></returns>
        public ConfiguracaoAvaliacaoPpa BuscarConfiguracaoValida(long seqEntidadeResponsavel, long seqNivelEnsino, TipoAvaliacaoPpa tipoAvaliacao)
        {
            // Lista de avaliações a serem filtradas nenhuma, 1 ou mais
            var listaAvaliacao = new List<TipoAvaliacaoPpa>();
            listaAvaliacao.Add(tipoAvaliacao);

            // Pesquisa a configuração com o nível de ensino
            var specConfig = new ConfiguracaoAvaliacaoPpaFilterSpecification()
            {
                SeqEntidadeResponsavel = seqEntidadeResponsavel,
                ListaTipoAvaliacaoPpa = listaAvaliacao,
                SeqNivelEnsino = seqNivelEnsino,
                DataLimiteRespostasFutura = true,
                ConfiguracaoVigente = true
            };
            var configComNivel = this.SearchByKey(specConfig);

            // Se encontrou a configuração, retorna
            if (configComNivel != null)
                return configComNivel;

            // Se não encontrou configuração, pesquisa sem o nível de ensino
            specConfig.SeqNivelEnsino = null;
            var configSemNivel = this.SearchByKey(specConfig);

            // Se encontrou a configuração, retorna. Senão retorna null
            if (configSemNivel != null)
                return configSemNivel;
            else
                return null;
        }

        /// <summary>
        /// Metodo de listagem dos filtros da tela inicial de configuração de avaliaçao.
        /// </summary>
        /// <param name="spec">Filtros informados na listagem</param>
        /// <returns>Retorna as informações filtradas pela especificação.</returns>
        public SMCPagerData<ConfiguracaoAvaliacaoPpaVO> BuscarAvaliacoes(ConfiguracaoAvaliacaoPpaFilterSpecification spec)
        {
            // Faz a pesquisa de acordo com os filtros informados
            int total = 0;
            spec.SetOrderBy(x => x.SeqCicloLetivo);
            spec.SetOrderBy(x => x.Descricao);

            var lista = this.SearchProjectionBySpecification(spec, x =>
            new ConfiguracaoAvaliacaoPpaVO
            {
                Seq = x.Seq,
                DescricaoCicloLetivo = x.CicloLetivo.Descricao,
                NomeEntidadeResponsavel = x.EntidadeResponsavel.Nome,
                SeqNivelEnsino = x.SeqNivelEnsino,
                DescricaoNivelEnsino = x.NivelEnsino.Descricao,
                Descricao = x.Descricao,
                TipoAvaliacaoPpa = x.TipoAvaliacaoPpa,
                DataInicioVigencia = x.DataInicioVigencia,
                DataFimVigencia = x.DataFimVigencia,
                DataLimiteRespostas = x.DataLimiteRespostas,
                CodigoAvaliacaoPpa = x.CodigoAvaliacaoPpa,
                CodigoOrigemPpa = x.CodigoOrigemPpa,
                SeqTipoInstrumentoPpa = x.SeqTipoInstrumentoPpa,
                CodigoInstrumentoPpa = x.CodigoInstrumentoPpa,
                CodigoAplicacaoQuestionarioSgq = x.CodigoAplicacaoQuestionarioSgq,
                SeqEspecieAvaliadorPpa = x.SeqEspecieAvaliadorPpa,
                CargaRealizada = x.CargaRealizada,
                FiltroConfiguracaoAvaliacaoPpaTurma = new ConfiguracaoAvaliacaoPpaTurmaFiltroVO { SeqConfiguracaoAvaliacaoPpa = x.Seq }
            },
            out total)
                .ToList();

            PreencherIdentificadores(lista);

            // Retorna o pager
            return new SMCPagerData<ConfiguracaoAvaliacaoPpaVO>(lista, total);
        }

        /// <summary>
        /// Metodo de consulta do serviço do PPA, que preenche a lista dos identificadores informados.
        /// </summary>
        /// <param name="lista">Lista com os codigos identificadores das entidades a serem consultadas no PPA</param>
        private void PreencherIdentificadores(List<ConfiguracaoAvaliacaoPpaVO> lista)
        {
            var codigosOrigem = lista.Select(x => x.CodigoOrigemPpa)
                            .OfType<int>() // ignora nulos
                            .Distinct()
                            .ToList();

            var codigosInstrumentos = lista.Select(x => x.SeqTipoInstrumentoPpa)
                           .OfType<int>()
                           .Distinct()
                           .ToList();

            var codigosAvaliacoes = lista.Select(x => x.CodigoAvaliacaoPpa)
                           .OfType<int>()
                           .Distinct()
                           .ToList();

            // Busca as informações complementares no PPA
            var listaDescricaoOrigem = AmostraService.BuscarOrigensAmostras(codigosOrigem, null);
            var listaDescricaoTipoInstrumento = AmostraService.BuscarTiposInstrumentos(codigosInstrumentos);
            var listaDescricaoAvaliacao = AmostraService.BuscarDescricaoAvaliacoes(codigosAvaliacoes);

            // Preencher as descrições
            foreach (var item in lista)
            {
                item.DescricaoOrigemPpa = listaDescricaoOrigem
                    .Where(x => x.Seq == item.CodigoOrigemPpa)
                    .Select(p => p.Descricao)
                    .FirstOrDefault();

                item.DescricaoTipoInstrumentoPpa = listaDescricaoTipoInstrumento
                    .Where(x => x.Seq == item.SeqTipoInstrumentoPpa)
                    .Select(p => p.Descricao)
                    .FirstOrDefault();

                item.DescricaoAvaliacaoPpa = listaDescricaoAvaliacao
                    .Where(x => x.CodigoAvaliacao == item.CodigoAvaliacaoPpa)
                    .Select(p => p.Nome)
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Salvar configuração para cada entidade responsavel informada no cadastro
        /// </summary>
        /// <param name="model">Modelo de registro de configuração de avaliação</param>
        /// <returns>Retorna Id do novo registro</returns>
        public long SalvarConfiguracaoAvaliacao(ConfiguracaoAvaliacaoPpaVO model)
        {
            ValidarConfiguracaoAvaliacao(model);

            string cicloLetivoDescricao = string.Empty;
            long seqUltimaEntidade = 0;

            // Buscar descrição do ciclo letivo
            if (model.SeqCicloLetivo.HasValue)
            {
                cicloLetivoDescricao = CicloLetivoDomainService.SearchProjectionByKey(
                    new SMCSeqSpecification<CicloLetivo>(model.SeqCicloLetivo.Value),
                    p => p.Descricao);
            }

            List<ConfiguracaoAvaliacaoPpa> listaConfiguracao = new List<ConfiguracaoAvaliacaoPpa>();

            // Loop param modelar as configurações a serem salvas
            foreach (var seqEntidade in model.SeqsEntidadesResponsaveis)
            {
                var configuracaoAvaliacao = model.Transform<ConfiguracaoAvaliacaoPpa>();
                
                configuracaoAvaliacao.SeqEntidadeResponsavel = seqEntidade;

                string entidadeNomeReduzido = string.Empty;

                //Se houver entidade responsavel, buscar a descrição
                if (configuracaoAvaliacao.SeqEntidadeResponsavel != 0)
                {
                    entidadeNomeReduzido = EntidadeDomainService.SearchProjectionByKey(
                        new SMCSeqSpecification<Entidade>(configuracaoAvaliacao.SeqEntidadeResponsavel),
                        p => p.NomeReduzido);
                }

                // RN_PES_042 - Adicionar descrição da configuração
                switch (configuracaoAvaliacao.TipoAvaliacaoPpa)
                {
                    case TipoAvaliacaoPpa.SemestralDisciplina:
                    case TipoAvaliacaoPpa.AutoavaliacaoProfessor:
                    case TipoAvaliacaoPpa.AutoavaliacaoAluno:
                        configuracaoAvaliacao.Descricao = $"{model.ParteFixaNomeAvaliacao} {entidadeNomeReduzido} - { cicloLetivoDescricao }";
                        break;

                    case TipoAvaliacaoPpa.Concluinte:
                        configuracaoAvaliacao.Descricao = $"{model.ParteFixaNomeAvaliacao} {entidadeNomeReduzido} - { configuracaoAvaliacao.DataInicioVigencia.Year }";
                        break;

                    default:
                        configuracaoAvaliacao.Descricao = $"Avaliação {configuracaoAvaliacao.TipoAvaliacaoPpa.SMCGetDescription()} - {entidadeNomeReduzido}";
                        break;
                }

                listaConfiguracao.Add(configuracaoAvaliacao);
            }

            using (var transaction = SMCUnitOfWork.Begin())
            {
                try
                {
                    // Para cada entidade responsavel dentro da configuracao, sera registrada uma nova configuração
                    foreach (var configuracao in listaConfiguracao)
                    {

                        SaveEntity(configuracao);

                        CargaInstrumentosAmostras(configuracao);

                        seqUltimaEntidade = configuracao.Seq;
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            // RN_PES_042 - item 3 - Realizar a carga dos docentes que terão acesso aos relatórios
            // finais da avaliação no PPA. Para esta carga executar a procedure
            // st_carga_avaliacao_professor_stricto_sensu que se encontra no banco PPA.
            // Para a execução desta procedure passar como parâmetro o código da avaliação.
            if (model.CodigoAvaliacaoPpa.HasValue)
            {
                AmostraService.CargaAvaliacaoProfessorStrictoSensu(model.CodigoAvaliacaoPpa.Value);
            }

            return seqUltimaEntidade;
        }

        private void ValidarConfiguracaoAvaliacao(ConfiguracaoAvaliacaoPpaVO model)
        {
            if (!model.SeqEspecieAvaliadorPpa.HasValue)
            {
                return;
            }

            /// RN_PES_042
            /// Verificar se a espécie avaliador esta associada a avaliação informada. 
            /// Caso não esteja enviar a mensagem de erro :
            /// 'O sequencial da espécie avaliador não esta associada a avaliação informada na configuração.'

            var associacaoEspecieAvaliador = AmostraService.BuscarEspecieAvaliador(model.CodigoAvaliacaoPpa);

            if (!associacaoEspecieAvaliador.Any(x => x.Seq == model.SeqEspecieAvaliadorPpa))
            {
                throw new ConfiguracaoAvaliacaoPpaAssociacaoEspecieAvaliadorException();
            }

        }

        private void CargaInstrumentosAmostras(ConfiguracaoAvaliacaoPpa configuracao)
        {
            switch (configuracao.TipoAvaliacaoPpa)
            {
                //Se o tipo de avaliação for Semestral(Disciplina), chamar a rotina
                //PES.st_carga_avaliacao_semestral_por_turma_aluno_ppa passando como parametro o sequencial da configuração que foi criada.
                case TipoAvaliacaoPpa.SemestralDisciplina:
                    AcademicoRepository.CargaAvaliacaoSemestralPorTurmaPPA(configuracao.Seq, SMCContext.User.Identity.Name);
                    break;

                //Se o tipo de avaliação for Autoavaliação(Aluno), chamar a rotina
                //PES.st_carga_autoavaliacao_aluno_ppa passando como parâmetro o sequencial da configuração que foi criada.
                case TipoAvaliacaoPpa.AutoavaliacaoAluno:
                    AcademicoRepository.CargaAutoavaliacaoAlunoPPA(configuracao.Seq, SMCContext.User.Identity.Name);
                    break;

                //Se o tipo de avaliação for Autoavaliação(Professor), chamar a rotina
                //PES.st_carga_autoavaliacao_professor_ppa passando como parâmetro o sequencial da configuração que foi criada.
                case TipoAvaliacaoPpa.AutoavaliacaoProfessor:
                    AcademicoRepository.CargaAutoavaliacaoProfessorPPA(configuracao.Seq, SMCContext.User.Identity.Name);
                    break;
            }
        }

        /// <summary>
        /// Alterar a data limite resposta da Configuração de avaliacao
        /// </summary>
        /// <param name="seq">Seq da configuracao de avaliação</param>
        /// <param name="novaDataLimiteResposta">Nova data para a configuração</param>
        public void AlterarDataLimiteResposta(long seq, DateTime novaDataLimiteResposta)
        {
            var configuracaoAvaliacao = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoAvaliacaoPpa>(seq));

            if (!(novaDataLimiteResposta > configuracaoAvaliacao.DataLimiteRespostas))
            {
                throw new ConfiguracaoAvaliacaoPpaAlteracaoDataLimiteException();
            }

            configuracaoAvaliacao.DataLimiteRespostas = novaDataLimiteResposta;

            UpdateFields(configuracaoAvaliacao, f => f.DataLimiteRespostas);
        }

        public void ExcluirConfiguracaoAvaliacaoPpa(long seq)
        {
            // NV09
            // Caso a data de inicio da configuração seja menor ou igual a data de hoje apresentar a mensagem informativa:
            // 'Avaliação já esta em andamento não sendo permitido a exclusão da configuração'
            var configuracao = this.SearchByKey(new SMCSeqSpecification<ConfiguracaoAvaliacaoPpa>(seq));

            if (configuracao.DataInicioVigencia <= DateTime.Now)
            {
                throw new ConfiguracaoAvaliacaoPpaEmAndamentoException();
            }

            //chamar proc PES.st_excluir_configuracao_avaliacao_ppa
            AcademicoRepository.ExcluirConfiguracaoAvaliacaoPpa(configuracao.Seq, SMCContext.User.Identity.Name);

        }

    }
}