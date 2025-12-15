using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class TurmaColaboradorService : SMCServiceBase, ITurmaColaboradorService
    {
        #region [ DomainService ]

        private TurmaColaboradorDomainService TurmaColaboradorDomainService
        {
            get { return this.Create<TurmaColaboradorDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar a lista turma colaborador
        /// </summary>
        /// <param name="seq">Sequencial da turma</param>
        /// <returns>Objeto turma colaborador</returns>
        public TurmaColaboradorData BuscarTurmaColaborador(long seq)
        {
            var registro = TurmaColaboradorDomainService.BuscarTurmaColaborador(seq).Transform<TurmaColaboradorData>();
            return registro;
        }

        /// <summary>
        /// Grava uma lista de turma colaboradores
        /// </summary>
        /// <param name="turmaColaborador">Dados da turma colaboradores</param>
        public void SalvarTurmaColaborador(TurmaColaboradorData turmaColaborador)
        {
            TurmaColaboradorDomainService.SalvarTurmaColaborador(turmaColaborador.Transform<TurmaColaboradorVO>());
        }

        /// <summary>
        /// Buscar a lista de colaboradores de todas as turma do relatório
        /// </summary>
        /// <param name="seqsTurma">Sequenciais de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as turmas</returns>
        public List<TurmaColaboradorRelatorioData> BuscarColaboradoresRelatorioTurma(List<long> seqsTurma)
        {
            var registro = TurmaColaboradorDomainService.BuscarColaboradoresRelatorioTurma(seqsTurma).TransformList<TurmaColaboradorRelatorioData>();
            return registro;
        }
    }
}
