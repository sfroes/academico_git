using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Repositories
{
    public interface IAcademicoRepository
    {
        /// <summary>
        /// Chama a procedure ORT.st_envia_trabalho_academico_pergamum para integrar o trabalho com o 
        /// banco do pergamum
        /// </summary>
        /// <param name="seqTrabalhoAcademico">Sequencial do trabalho a ser integrado</param>
        /// <returns>Sequencial do acervo criado</returns>
        int CarregarTrabalhoPergamun(long seqTrabalhoAcademico);

        /// <summary>
        /// Chama a procedure CNC.st_calcular_conclusao_curso para calcular o percentual de conclusão de 
        /// um curso por um aluno
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno-histórico a ser validado</param>
        /// <returns>Objeto com o calculo geral do percentual de conclusão do curso e do percentual de conclusão por grupo</returns>
        CalculoConclusaoCursoAlunoVO CalcularPercentualConclusaoCursoAluno(long seqAlunoHistorico);

        /// <summary>
        /// Chama a procedure ALN.st_carga_sincronismo_aluno_graduacao para sincronizar os dados do aluno
        /// </summary>
        /// <param name="codigoAlunoMigracao">Código do aluno migração a ser sincronizado</param>
        /// <returns>Sequencial da pessoa atuação que foi sincronizada</returns>
        long SincronizarDadosAluno(int codigoAlunoMigracao, string usuarioOperacao);

        /// <summary>
        /// Chama a procedure PES.st_carga_avaliacao_semestral_por_turma_aluno_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        void CargaAvaliacaoSemestralPorTurmaPPA(long seqConfiguracao, string usuarioOperacao);

        /// <summary>
        /// Chama a procedure PES.st_carga_autoavaliacao_aluno_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        void CargaAutoavaliacaoAlunoPPA(long seqConfiguracao, string usuarioOperacao);

        /// <summary>
        /// Chama a procedure PES.st_carga_autoavaliacao_professor_ppa para carga dos dados no PPA
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuração de avaliação a ser carregada</param>
        /// <param name="usuarioOperacao">Usuário que realiza a carga</param>
        void CargaAutoavaliacaoProfessorPPA(long seqConfiguracao, string usuarioOperacao);

        /// <summary>
        /// Chama a procedure PES.st_excluir_configuracao_avaliacao_ppa para excluir o registro da configuração informada.
        /// </summary>
        /// <param name="seqConfiguracao">Sequencial da configuação de avaliação a ser excluída</param>
        /// <param name="usuarioOperacao">Usuário que realiza a exclusão</param>
        void ExcluirConfiguracaoAvaliacaoPpa(long seqConfiguracao, string usuarioOperacao);
    }
}
