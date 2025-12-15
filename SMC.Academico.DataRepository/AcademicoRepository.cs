using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Repositories;
using SMC.Framework;
using SMC.Framework.Data;
using SMC.Framework.Exceptions;
using System;
using System.Data;
using System.Linq;

namespace SMC.Academico.DataRepository
{
    public class AcademicoRepository : SMCSqlDbRepositoryProvider, IAcademicoRepository
    {
        public AcademicoRepository()
            : base("AcademicoContext")
        { }

        /// <summary>
        /// Chama a procedure ORT.st_envia_trabalho_academico_pergamum para integrar o trabalho com o 
        /// banco do pergamum
        /// </summary>
        /// <param name="seqTrabalhoAcademico">Sequencial do trabalho a ser integrado</param>
        /// <returns>Sequencial do acervo criado</returns>
        public int CarregarTrabalhoPergamun(long seqTrabalhoAcademico)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("ORT.st_envia_trabalho_academico_pergamum", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@seq_trabalho_academico", seqTrabalhoAcademico, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@cod_acervo", string.Empty, SqlDbType.Int, ParameterDirection.Output);
            AddParameter(command, "@dsc_erro", string.Empty, SqlDbType.VarChar, 8000, ParameterDirection.Output);

            try
            {
                // Executa a procedure
                this.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!string.IsNullOrWhiteSpace(command.Parameters["@dsc_erro"].SqlValue?.ToString()) &&
                    !string.IsNullOrEmpty(command.Parameters["@dsc_erro"].SqlValue?.ToString()))
                    throw new SMCApplicationException(command.Parameters["@dsc_erro"].SqlValue.ToString());
            }

            // Retorna o código do acervo
            return GetParameter<int>(command, "@cod_acervo");
        }

        /// <summary>
        /// Chama a procedure CNC.st_calcular_conclusao_curso para calcular o percentual de conclusão de 
        /// um curso por um aluno
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno-histórico a ser validado</param>
        /// <returns>Objeto com o calculo geral do percentual de conclusão do curso e do percentual de conclusão por grupo</returns>
        public CalculoConclusaoCursoAlunoVO CalcularPercentualConclusaoCursoAluno(long seqAlunoHistorico)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("CNC.st_calcular_conclusao_curso", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@SEQ_ALUNO_HISTORICO", seqAlunoHistorico, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@NUM_PERCENTUAL_GERAL", string.Empty, SqlDbType.Decimal, ParameterDirection.Output);

            // Executa a procedure e recupera os dados retornados
            var result = this.ExecuteReader<PercentualConclusaoGrupoVO>(command, reader =>
                new PercentualConclusaoGrupoVO()
                {
                    SeqGrupoCurricular = reader.SMCGetValue<long>("seq_grupo_curricular"),
                    SeqGrupoCurricularSuperior = reader.SMCGetValue<long?>("seq_grupo_curricular_superior"),
                    PercentualConclusaoGrupo = reader.SMCGetValue<decimal?>("num_percentual_conclusao")
                });

            // Recupera o parametro de output com o percentual geral
            var percentualGeral = GetParameter<decimal>(command, "@NUM_PERCENTUAL_GERAL");

            // Monta o retorno
            return new CalculoConclusaoCursoAlunoVO()
            {
                PercentualConclusaoGeral = percentualGeral,
                PercentualGrupo = result.ToList()
            };
        }

        /// <summary>
        /// Chama a procedure ALN.st_carga_sincronismo_aluno_graduacao para sincronizar os dados do aluno
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno migração a ser sincronizado</param>
        /// <returns>Sequencial da pessoa atuação que foi sincronizada</returns>
        public long SincronizarDadosAluno(int codigoAlunoMigracao, string usuarioOperacao)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("ALN.st_carga_sincronismo_aluno_graduacao", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@cod_aluno_migracao", codigoAlunoMigracao, SqlDbType.Int, ParameterDirection.Input);
            AddParameter(command, "@usu_operacao", usuarioOperacao, SqlDbType.VarChar, ParameterDirection.Input);
            AddParameter(command, "@seq_pessoa_atuacao_aluno", 0, SqlDbType.BigInt, ParameterDirection.Output);

            // Executa a procedure
            this.ExecuteNonQuery(command);

            // Retorna o código da pessoa atuação
            return GetParameter<long>(command, "@seq_pessoa_atuacao_aluno");
        }

        /// <summary>
        /// Chama a procedure PES.st_carga_avaliacao_semestral_por_turma_aluno_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        public void CargaAvaliacaoSemestralPorTurmaPPA(long seqConfiguracao, string usuarioOperacao)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("PES.st_carga_avaliacao_semestral_por_turma_aluno_ppa", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@SEQ_CONFIGURACAO_AVALIACAO_PPA", seqConfiguracao, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@USU_OPERACAO", usuarioOperacao, SqlDbType.VarChar, ParameterDirection.Input);

            // Executa a procedure
            this.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Chama a procedure PES.st_carga_autoavaliacao_aluno_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        public void CargaAutoavaliacaoAlunoPPA(long seqConfiguracao, string usuarioOperacao)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("PES.st_carga_autoavaliacao_aluno_ppa", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@SEQ_CONFIGURACAO_AVALIACAO_PPA", seqConfiguracao, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@USU_OPERACAO", usuarioOperacao, SqlDbType.VarChar, ParameterDirection.Input);

            // Executa a procedure
            this.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Chama a procedure PES.st_carga_autoavaliacao_professor_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        public void CargaAutoavaliacaoProfessorPPA(long seqConfiguracao, string usuarioOperacao)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("PES.st_carga_autoavaliacao_professor_ppa", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@SEQ_CONFIGURACAO_AVALIACAO_PPA", seqConfiguracao, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@USU_OPERACAO", usuarioOperacao, SqlDbType.VarChar, ParameterDirection.Input);

            // Executa a procedure
            this.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Chama a procedure PES.st_excluir_configuracao_avaliacao_ppa para excluir o registro da configuração informada.
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuação de avaliação a ser excluída</param>
        /// <param name="usuarioOperacao">Usuário que realiza a exclusão</param>
        public void ExcluirConfiguracaoAvaliacaoPpa(long seqConfiguracao, string usuarioOperacao)
        {
            // Cria o command apontando para procedure
            var command = CreateCommand("PES.st_excluir_configuracao_avaliacao_ppa", CommandType.StoredProcedure);

            // Adiciona os parâmetros da procedure
            AddParameter(command, "@SEQ_CONFIGURACAO_AVALIACAO_PPA", seqConfiguracao, SqlDbType.BigInt, ParameterDirection.Input);
            AddParameter(command, "@USU_OPERACAO", usuarioOperacao, SqlDbType.VarChar, ParameterDirection.Input);

            // Executa a procedure
            this.ExecuteNonQuery(command);
        }


    }
}