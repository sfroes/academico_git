using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class HistoricoEscolarService : SMCServiceBase, IHistoricoEscolarService
    {
        #region [ DomainService ]

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();
        private ConfiguracaoComponenteDomainService ConfiguracaoComponenteDomainService => Create<ConfiguracaoComponenteDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os dados do cabeçalho do histórico escolar de um aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados do Cabeçalho</returns>
        public HistoricoEscolarCabecalhoData BuscarHistoricoEscolarCabecalho(long seqAluno)
        {
            return HistoricoEscolarDomainService.BuscarHistoricoEscolarCabecalho(seqAluno).Transform<HistoricoEscolarCabecalhoData>();
        }

        /// <summary>
        /// Busca o histórico escolar de um aluno
        /// </summary>
        /// <param name="filtros">Sequencial do aluno e dados de paginação</param>
        /// <returns>Dados do histórico escolar do aluno</returns>
        public SMCPagerData<HistoricoEscolarListaData> BuscarHistoricosEscolares(HistoricoEscolarFiltroData filtros)
        {
            return HistoricoEscolarDomainService.BuscarHistoricosEscolares(filtros.Transform<HistoricoEscolarFilterSpecification>()).Transform<SMCPagerData<HistoricoEscolarListaData>>();
        }

        /// <summary>
        /// Busca um registro de histórico escolar que representa a associação de um componente
        /// </summary>
        /// <param name="seq">Sequencial do item de histórico escolar</param>
        /// <exception cref="SMCInvalidOperationException">Caso o histórico informado tenha origem de avaliação (significa que não foi criado nesta tela)</exception>
        /// <returns>Dados do histórico escolar</returns>
        public HistoricoEscolarData BuscarHistoricoEscolarComponente(long seq)
        {
            return HistoricoEscolarDomainService.BuscarHistoricoEscolarComponente(seq).Transform<HistoricoEscolarData>();
        }

        /// <summary>
        /// Grava uma associação de histórico escolar com componente
        /// </summary>
        /// <param name="historicoEscolar">Dados do histórico escolar</param>
        /// <returns>Sequencial do histórico escolar gravado</returns>
        public long SalvarHistoricoEscolarComponente(HistoricoEscolarData historicoEscolar)
        {
            return HistoricoEscolarDomainService.SalvarHistoricoEscolarComponente(historicoEscolar.Transform<HistoricoEscolarVO>());
        }

        public HistoricoEscolarCompletoData BuscarHistoricoEscolar(HistoricoEscolarFiltroData filtro)
        {
            return HistoricoEscolarDomainService.BuscarHistoricoEscolar(filtro.Transform<HistoricoEscolarFilterSpecification>()).Transform<HistoricoEscolarCompletoData>();
        }

        public HistoricoEscolarCompletoData BuscarHistoricoEscolarTurma(long seqTurma, long seqAluno)
        {
            return HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(seqTurma, seqAluno).Transform<HistoricoEscolarCompletoData>();
        }

        /// <summary>
        /// Grava o lançamento de notas e frequências finais de todos os alunos de uma turma.
        /// </summary>
        /// <param name="dados">Informações de notas, faltas e conteúdo lecionado de uma turma.</param>
        /// <returns></returns>
        public void SalvarLancamentoNotasFrequenciaFinal(LancamentoHistoricoEscolarData dados)
        {
            HistoricoEscolarDomainService.SalvarLancamentoNotasFrequenciaFinal(dados.Transform<LancamentoHistoricoEscolarVO>());
        }

        /// <summary>
        /// Inclui o lançamento de notas e frequências finais de todos os alunos da turma.
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqsOrientadores">Sequenciais dos orientadores para filtro dos alunos</param>
        /// <returns></returns>
        public LancamentoHistoricoEscolarData LancarNotasFrequenciasFinais(long seqTurma, List<long> seqsOrientadores)
        {
            return HistoricoEscolarDomainService.LancarNotasFrequenciasFinais(seqTurma, seqsOrientadores).Transform<LancamentoHistoricoEscolarData>();
        }

        /// <summary>
        /// Calcular situação final do aluno no lançamento de nota parcial
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="nota">Nota final aluno</param>
        /// <param name="seqsProfessores">Lista sequencial dos professores</param>
        /// <returns>Situação final do aluno</returns>
        public SituacaoHistoricoEscolar CalcularSituacaofinalLancamentoNotaParcial(long seqOrigemAvaliacao, long seqAlunoHistorico, short? nota, List<long> seqsProfessores)
        {
            return HistoricoEscolarDomainService.CalcularSituacaofinalLancamentoNotaParcial(seqOrigemAvaliacao, seqAlunoHistorico, nota, seqsProfessores);
        }

        /// <summary>
        /// Calcula a situação final do aluno segundo a regra RN_APR_007 - Cálculo da situação final de aluno
        /// </summary>
        /// <param name="historicoEscolarSituacaoData">Dados para o cálculo da situação final do aluno</param>
        /// <returns>Situação final do aluno</returns>
        public SituacaoHistoricoEscolar CalcularSituacaoFinal(HistoricoEscolarSituacaoFinalData historicoEscolarSituacaoData)
        {
            return HistoricoEscolarDomainService.CalcularSituacaoFinal(historicoEscolarSituacaoData.Transform<HistoricoEscolarSituacaoFinalVO>());
        }

        /// <summary>
        /// Apaga o registro de histórico escolar
        /// </summary>
        /// <param name="seq">Sequencial do registro</param>
        /// <exception cref="SMCInvalidOperationException">Caso o registro tenha uma origem de avaliação (Significa que não foi criado na tela de histórico escolar)</exception>
        public void ExcluirHistoricoEscolarComponente(long seq)
        {
            HistoricoEscolarDomainService.ExcluirHistoricoEscolarComponente(seq);
        }

        /// <summary>
        /// Excluir historico esclar por aluno historico
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        public void ExcluirHistoricoEscolarPorAlunoHistorico(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            HistoricoEscolarDomainService.ExcluirHistoricoEscolarPorAlunoHistorico(seqAlunoHistorico, seqOrigemAvaliacao);
        }

        /// <summary>
        /// Recupera a obrigatoriedade de matriz conforme o vínculo do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Obrigatoriedade da matriz conforme o vínculo do aluno</returns>
        public HistoricoEscolarData BuscarConfiguracaoHistoricoEscolarComponente(long seqAluno)
        {
            return HistoricoEscolarDomainService.BuscarConfiguracaoHistoricoEscolarComponente(seqAluno).Transform<HistoricoEscolarData>();
        }

        /// <summary>
        /// Dados do cabeçalho da consulda de integralização, separado quando colocado o filtro para evitar duas buscas completas
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="visaoAluno">Verifica o sistema que chamou a modal</param>
        /// <returns>Dados do cabeçalho da integralização</returns>
        public IntegralizacaoMatrizCurricularOfertaCabecalhoData CabecalhoTelaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno)
        {
            return HistoricoEscolarDomainService.CabecalhoTelaIntegralizacaoCurricularHistorico(seqPessoaAtuacao, visaoAluno).Transform<IntegralizacaoMatrizCurricularOfertaCabecalhoData>();
        }

        /// <summary>
        /// Consulta histórico escolar, matriz curricular e plano de estudo para exibir os dados da integralização curricular do aluno/ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="visaoAluno">Defini o projeto, se for SGA.Aluno exibe apenas nome social</param>
        /// <param name="filtro">Filtro de situação e descrição da configuração de componente</param>
        /// <returns>Objeto de retorno da consulta estruturado com matriz e componentes</returns>
        public IntegralizacaoConsultaHistoricoData ConsultaIntegralizacaoCurricularHistorico(long seqPessoaAtuacao, bool visaoAluno, IntegralizacaoConsultaHistoricoFiltroData filtro)
        {
            return HistoricoEscolarDomainService.ConsultaIntegralizacaoCurricularHistorico(seqPessoaAtuacao, visaoAluno, filtro.Transform<IntegralizacaoConsultaHistoricoFiltroVO>()).Transform<IntegralizacaoConsultaHistoricoData>();
        }

        /// <summary>
        /// Busca os históricos escolares para modal de integralização curricular
        /// </summary>
        /// <param name="seqsHistoricoEscolar">Sequenciais concatenados por ,</param>
        /// <returns>Dados do histórico escolar do aluno</returns>
        public List<HistoricoEscolarListaData> BuscarHistoricosEscolaresIntegralizacao(string seqsHistoricoEscolar)
        {
            return HistoricoEscolarDomainService.BuscarHistoricosEscolaresIntegralizacao(seqsHistoricoEscolar).TransformList<HistoricoEscolarListaData>();
        }

        /// <summary>
        /// Flag para visualizar consulta de integralização, sempre para aluno e quando for ingressante somente se conceder formação.
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Retorna o valor da flag de controle da visualização</returns>
        public bool VisualizarConsultaConcederFormacaoIntegralizacao(long seqPessoaAtuacao)
        {
            return HistoricoEscolarDomainService.VisualizarConsultaConcederFormacaoIntegralizacao(seqPessoaAtuacao);
        }

        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do historico escolar
        /// </summary>
        /// <param name="seq">Sequencial do historico escolar</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        public (long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoHistoricoEscolar(long seq)
        {
            return HistoricoEscolarDomainService.BuscarCicloLetivoLocalidadeTurnoHistoricoEscolar(seq);
        }

        /// <summary>
        /// Método que verifica se existe componentes ja aprovados ou dispensados na lista
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dadosComponentesSelecionados">Dados dos componentes selecionados</param>
        /// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
        public string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<HistoricoEscolarAprovadoFiltroData> componentesComAssunto)
        {
            return HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(seqPessoaAtuacao, componentesComAssunto.TransformList<HistoricoEscolarAprovadoFiltroVO>(), null);
        }

        /// <summary>
        /// Método que verifica se existe componentes ja aprovados ou dispensados na lista
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="dadosComponentesSelecionados">Dados dos componentes selecionados</param>
        /// <param name="seqCicloLetivoProcesso">Sequencial do ciclo letivo processo</param>
        /// <returns>Retorna lista de componentes separado com , para os componentes já aprovados ou dispensados</returns>
        public string VerificarHistoricoComponentesAprovadosDispensados(long seqPessoaAtuacao, List<(long? SeqConfiguracaoComponente, long? SeqDivisaoTurma)> dadosComponentesSelecionados, long? seqCicloLetivoProcesso)
        {
            return HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(seqPessoaAtuacao, dadosComponentesSelecionados, seqCicloLetivoProcesso);
        }

        /// <summary>
        /// Retornar dados historico escolar baseado na procedure ACADEMICO.APR.st_rel_componentes_historico_escolar
        /// </summary>
        /// <param name="seqAluno">Sequencial Aluno</param>
        /// <param name="exibirReprovados">Indicador para exibir ou não componentes reprovados</param>
        /// <param name="exibeCreditoZero">Indicador para exibir ou não componentes com crédito zero</param>
        /// <param name="exibeComponenteCursadoSemHistorico">Indicador para exibir ou não componentes cursados que ainda não tem lançamento de nota no histórico</param>
        /// <param name="exibeExame">Indicador para exibir ou não componentes de exame cursados</param>
        /// <returns>Lista de componentes cursados</returns>
        public List<ComponentesCreditosData> ConsultarNotasFrequencias(long seqAluno, bool exibirReprovados, bool exibeCreditoZero, bool exibeComponenteCursadoSemHistorico, bool exibeExame)
        {
            return HistoricoEscolarDomainService.ConsultarNotasFrequencias(seqAluno, exibirReprovados, exibeCreditoZero, exibeComponenteCursadoSemHistorico, exibeExame).TransformList<ComponentesCreditosData>();
        }
    }
}