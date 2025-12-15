using SMC.Academico.Common.Areas.CUR.Exceptions.MatrizCurricularOferta;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class DivisaoTurmaService : SMCServiceBase, IDivisaoTurmaService
    {
        #region [ DomainService ]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService
        {
            get { return this.Create<DivisaoTurmaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Buscar a divisão turma com detalhes para cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="telaDetalhes">Quando true, quer dizer que é da tela de detalhes da divisão de turma, e o cabeçalho é montado de maneira diferente</param>
        /// <returns>Objeto divisão turma com detalhes para o cabeçalho</returns>
        public DivisaoTurmaDetalhesData BuscarDivisaoTurmaCabecalho(long seq, bool telaDetalhes = false)
        {
            var registro = DivisaoTurmaDomainService.BuscarDivisaoTurmaCabecalho(seq, telaDetalhes);

            return registro.Transform<DivisaoTurmaDetalhesData>();
        }

        /// <summary>
        /// Buscar a divisão turma com detalhes para tela de visualização
        /// </summary>
        /// <param name="seq">Sequencial da divisão turma</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="preservarColaboradores">Quando true, mesmo que não seja infromada uma pessoa atuação e a turma seja de orientação os colaboradores serão retornados</param>
        /// <param name="telaDetalhes">Quando true, quer dizer que é da tela de detalhes da divisão de turma, e o cabeçalho é montado de maneira diferente</param>
        /// <returns>Objeto divisão turma com detalhes e colaboradores</returns>
        public List<DivisaoTurmaDetalhesData> BuscarDivisaoTurmaDetalhes(long seq, long? seqPessoaAtuacao = null, bool preservarColaboradores = false, bool telaDetalhes = false)
        {
            var registro = DivisaoTurmaDomainService.BuscarDivisaoTurmaDetalhes(seq, seqPessoaAtuacao, preservarColaboradores, telaDetalhes);

            return registro.TransformList<DivisaoTurmaDetalhesData>();
        }

        /// <summary>
        /// Buscar a divisão turma com detalhes e colaboradores para tela de visualização
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Lista divisão turma com detalhes e colaboradores</returns>
        public List<DivisaoTurmaDetalhesData> BuscarDivisaoTurmaLista(long seqTurma)
        {
            var registro = DivisaoTurmaDomainService.BuscarDivisaoTurmaLista(seqTurma);

            return registro.TransformList<DivisaoTurmaDetalhesData>();
        }

        /// <summary>
        /// Lista de alunos por uma divisão de turma. Modelo de dados para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns></returns>
        public List<DivisaoTurmaRelatorioAlunoData> BuscarAlunosDivisaoTurma(long seqDivisaoTurma)
        {
            var registro = DivisaoTurmaDomainService.BuscarAlunosDivisaoTurma(seqDivisaoTurma);

            return registro.TransformList<DivisaoTurmaRelatorioAlunoData>();
        }

        /// <summary>
        /// Lista de cabecalho por uma divisão de turma. Informações da turma. Modelo de dados para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma"></param>
        /// <returns></returns>
        public List<DivisaoTurmaRelatorioCabecalhoData> BuscarDivisaoTurmaRelatorioCabecalho(long seqDivisaoTurma)
        {
            var registro = DivisaoTurmaDomainService.BuscarDivisaoTurmaRelatorioCabecalho(seqDivisaoTurma);

            return registro.TransformList<DivisaoTurmaRelatorioCabecalhoData>();
        }

        /// <summary>
        /// Lista de colaboradores por uma divisão de turma.
        /// Modelo de VO para relatório de Emissão de Lista de Frequencia
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão de turma</param>
        /// <param name="colaboradores">Lista de colaboradores selecionados</param>
        /// <returns>Lista de colaboradores da divisão de turma</returns>
        public List<DivisaoTurmaRelatorioColaboradorData> BuscarDivisaoTurmaRelatorioColaborador(long seqDivisaoTurma, List<long> colaboradores, DateTime? dataVinculo)
        {
            var registro = DivisaoTurmaDomainService.BuscarDivisaoTurmaRelatorioColaborador(seqDivisaoTurma, colaboradores, dataVinculo);

            return registro.TransformList<DivisaoTurmaRelatorioColaboradorData>();
        }

        /// <summary>
        /// Busca a data de inicio e fim de um evento letivo de uma turma, baseado na divisão da turma.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma</param>
        /// <returns>Datas de início e fim do evento letivo.</returns>
        public (DateTime dataInicio, DateTime dataFim) BuscarDatasEventoLetivoTurma(long seqDivisaoTurma)
        {
            return DivisaoTurmaDomainService.BuscarDatasEventoLetivoTurma(seqDivisaoTurma);
        }

        /// <summary>
        /// Montar descrição divisao de turma - RN_TUR_026 - Exibição Descrição - Divisão Turma
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial divisão de turma</param>
        /// <returns>Descrição divisão de turma</returns>
        public string ObterDescricaoDivisaoTurma(long seqDivisaoTurma, long seqAluno = 0)
        {
            return DivisaoTurmaDomainService.ObterDescricaoDivisaoTurma(seqDivisaoTurma, seqAluno);
        }

        public void ValidarOfertaMatrizExcluidas(long seqTurma, List<long> seqsMatrizesCurricularesOfertasExcluidas)
        {
            var matrizesProblema = DivisaoTurmaDomainService.ValidarOfertaMatrizExcluidas(seqTurma, seqsMatrizesCurricularesOfertasExcluidas);
            if (matrizesProblema != null)
                throw new MatrizCurricularOfertaAlunoAssociadoExcluirException(matrizesProblema);
        }

        public DetalheDivisaoTurmaGradeData BuscarDetalhesDivisaoTurmaGrade(long seqDivisaoTurma)
        {
            return this.DivisaoTurmaDomainService.BuscarDetalhesDivisaoTurmaGrade(seqDivisaoTurma).Transform<DetalheDivisaoTurmaGradeData>();
        }

        /// <summary>
        ///  Buscar a divisões de turma para a grade horaria compartilhada
        /// </summary>
        /// <param name="seqTurma">Sequencial de turma</param>
        /// <returns>Divisões filtradas para turma</returns>
        public List<SMCDatasourceItem> BuscarDivisõesPorTurmaParaGradeCompartilhadaSelect(long seqTurma)
        {
            return DivisaoTurmaDomainService.BuscarDivisoesPorTurmaParaGradeCompartilhadaSelect(seqTurma);
        }

        /// <summary>
        /// Buscar se pelo menos uma divisão tem obrigatoriedade de matéria lecionada obrigatória
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns></returns>
        public bool MateriaLecionadaObrigatoria(long seqTurma)
        {
            return DivisaoTurmaDomainService.MateriaLecionadaObrigatoria(seqTurma);
        }
    }
}