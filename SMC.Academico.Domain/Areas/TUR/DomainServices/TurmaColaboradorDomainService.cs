using SMC.Academico.Common.Areas.TUR.Includes;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class TurmaColaboradorDomainService : AcademicoContextDomain<TurmaColaborador>
    {
        #region [ Queries ]

        #region [ _buscarColaboradoresRelatorioTurma ]

        private string _buscarColaboradoresRelatorioTurma =
                        @"  SELECT  T.seq_turma AS SeqTurma, 
                                    TC.seq_atuacao_colaborador AS SeqPessoaColaborador,
		                            PDPC.nom_pessoa AS NomeColaborador,
		                            PDPC.nom_social AS NomeSocialColaborador
                              FROM TUR.turma T
                             INNER JOIN TUR.turma_colaborador TC ON T.seq_turma = TC.seq_turma 
                              LEFT JOIN PES.pessoa_atuacao PAC ON TC.seq_atuacao_colaborador = PAC.seq_pessoa_atuacao 
                              LEFT JOIN PES.pessoa_dados_pessoais PDPC ON PAC.seq_pessoa_dados_pessoais = PDPC.seq_pessoa_dados_pessoais
                             WHERE T.seq_turma IN ({0})";

        #endregion [ _buscarColaboradoresRelatorioTurma ]

        #endregion [ Queries ]

        /// <summary>
        /// Buscar a lista turma colaborador
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Objeto turma colaborador</returns>
        public TurmaColaboradorVO BuscarTurmaColaborador(long seqTurma)
        {
            var filterTurma = new TurmaColaboradorFilterSpecification() { SeqTurma = seqTurma };
            var colaboradores = this.SearchBySpecification(filterTurma, IncludesTurmaColaborador.Colaborador_DadosPessoais).ToList();

            TurmaColaboradorVO retorno = new TurmaColaboradorVO();
            retorno.SeqTurma = seqTurma;
            retorno.Colaborador = new List<ColaboradorVO>();

            if (colaboradores != null && colaboradores.Count > 0)
                retorno.Colaborador.AddRange(colaboradores.Select(s => s.Colaborador).TransformList<ColaboradorVO>());

            return retorno;
        }

        /// <summary>
        /// Grava uma lista de turma colaboradores
        /// </summary>
        /// <param name="turmaColaborador">Dados da turma colaboradores</param>
        public void SalvarTurmaColaborador(TurmaColaboradorVO turmaColaborador)
        {
            var filterTurma = new TurmaColaboradorFilterSpecification() { SeqTurma = turmaColaborador.SeqTurma };
            var colaboradoresBanco = this.SearchBySpecification(filterTurma).ToList();

            if (turmaColaborador.Colaborador != null)
            {
                foreach (var item in turmaColaborador.Colaborador)
                {
                    if (colaboradoresBanco.Any(c => c.SeqColaborador == item.Seq))
                        colaboradoresBanco.RemoveAll(r => r.SeqColaborador == item.Seq);
                    else
                    {
                        var registro = new TurmaColaborador();
                        registro.SeqTurma = turmaColaborador.SeqTurma;
                        registro.SeqColaborador = item.Seq;
                        this.InsertEntity(registro);
                    }
                }
            }

            if (colaboradoresBanco != null && colaboradoresBanco.Count > 0)
                colaboradoresBanco.ForEach(f => this.DeleteEntity(f));
        }

        /// <summary>
        /// Salvar turma colaborador
        /// </summary>
        /// <param name="turmaColaborador">Dados do turma colaborador</param>
        public void SalvarTurmaColaboradorEventoAula(TurmaColaboradorVO turmaColaborador)
        {
            var modelo = turmaColaborador.Transform<TurmaColaborador>();
            SaveEntity(modelo);
        }

        /// <summary>
        /// Buscar a lista de colaboradores de todas as turma do relatório
        /// </summary>
        /// <param name="seqsTurma">Sequenciais de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as turmas</returns>
        public List<TurmaColaboradorRelatorioVO> BuscarColaboradoresRelatorioTurma(List<long> seqsTurma)
        {
            var registros = RawQuery<TurmaColaboradorRelatorioVO>(string.Format(_buscarColaboradoresRelatorioTurma, string.Join(" , ", seqsTurma)));

            return registros;
        }

        /// <summary>
        /// Remover a associação do colaborador com a turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de turma com colaborador</param>
        public void ExcluirTurmaColaborador(long seq)
        {
            this.DeleteEntity(seq);
        }
    }
}
