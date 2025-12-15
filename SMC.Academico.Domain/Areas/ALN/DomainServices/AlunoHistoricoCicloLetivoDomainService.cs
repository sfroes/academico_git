using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoHistoricoCicloLetivoDomainService : AcademicoContextDomain<AlunoHistoricoCicloLetivo>
    {

        #region [DomainService]
        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        #endregion [DomainService]

        #region [ Dados Relatório Posição Consolidada Situação por Tipo de Atuação ] 

        public List<RelatorioConsolidadoSituacaoVO> BuscarDadosRelatorioConsolidadoSituacao(RelatorioConsolidadoSituacaoFiltroVO filtro)
        {
            /*
            •	@SEQ_CICLO_LETIVO = sequencial do ciclo letivo informado na tela de filtro (obrigatório)
            •	@LISTA_ENTIDADES_RESPONSAVEL = string com os sequenciais das entidades responsáveis selecionadas na tela de filtro separados por virgula (opcional – se não informado passar null)
            •	@LISTA_TIPO_ATUACAO = string com os sequencias dos tipos de atuação selecionados na tela de filtro separados por virgula (obrigatório) se não tiver selecionado o check de ingressante e/ou de aluno não deve emitir o relatório
            */
            const string PROCEDURE_RELATORIO_POSICAO_CONSOLIDADA_SITUACAO_ALUNO = "ACADEMICO.MAT.st_rel_posicao_consolidada_situacao_aluno";

            var listaEntidades = filtro.SeqsEntidadeResponsavel != null && filtro.SeqsEntidadeResponsavel.Any() ? $"'{string.Join(",", filtro.SeqsEntidadeResponsavel)}'" : "null";
            var listaTipoAtuacao = $"'{string.Join(",", filtro.TipoAtuacoes.Select(x => (short)x))}'";

            string chamadaProcedure = $"exec {PROCEDURE_RELATORIO_POSICAO_CONSOLIDADA_SITUACAO_ALUNO} {filtro.SeqCicloLetivo}, {listaEntidades}, {listaTipoAtuacao}";

            var result = RawQuery<RelatorioConsolidadoSituacaoVO>(chamadaProcedure);

            return result;
        }

        #endregion [ Dados Relatório Posição Consolidada Situação por Tipo de Atuação ]

        /// <summary>
        /// Busca o aluno histórico ciclo letivo, com campos chave para um registro e include de situação de matrícula
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno histórico</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Registro Aluno Histórico Ciclo Letivo</returns>
        public AlunoHistoricoCicloLetivo BuscarAlunoHistoricoCicloLetivoPorHistoricoCiclo(long seqAlunoHistorico, long seqCicloLetivo)
        {
            var specHistoricoCiclo = new AlunoHistoricoCicloLetivoFilterSpecification()
            {
                SeqAlunoHistorico = seqAlunoHistorico,
                SeqCicloLetivo = seqCicloLetivo
            };

            var registro = this.SearchByKey(specHistoricoCiclo, IncludesAlunoHistoricoCicloLetivo.AlunoHistoricoSituacao);

            return registro;
        }

        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do plano de estudo
        /// </summary>
        /// <param name="seq">Sequencial do aluno histórico ciclo letivo</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        public (long SeqCicloLetivo, TipoAluno TipoAluno) BuscarCicloLetivoLocalidadeTurnoPorAlunoHistoricoCicloLetivo(long seq)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<AlunoHistoricoCicloLetivo>(seq), p => new
            {
                SeqCicloLetivo = p.SeqCicloLetivo,
                TipoAluno = p.TipoAluno
            });

            return (registro.SeqCicloLetivo, registro.TipoAluno);
        }

        /// <summary>
        /// Buscar aluno historico do ciclo letivo por aluno do ciclo atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno.</param>
        public AlunoHistoricoCicloLetivo BuscarAlunoHistoricoCicloLetivoCicloAtual(long seqAluno)
        {
            var seqAlunoCicloLetivo = AlunoDomainService.BuscarCicloLetivoAtual(seqAluno);
            var alunoSpec = new SMCSeqSpecification<Aluno>(seqAluno);
            var alunoHistoricoCicloLetivo = AlunoDomainService.SearchProjectionByKey(alunoSpec, x =>
                                                x.Historicos.FirstOrDefault(f => f.Atual)
                                                .HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqAlunoCicloLetivo));

            return alunoHistoricoCicloLetivo;
        }

    }
}
