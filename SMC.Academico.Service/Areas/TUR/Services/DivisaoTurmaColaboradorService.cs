using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.TUR.Services
{
    public class DivisaoTurmaColaboradorService : SMCServiceBase, IDivisaoTurmaColaboradorService
    {
        #region [ DomainService ]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService
        {
            get { return this.Create<DivisaoTurmaDomainService>(); }
        }

        private DivisaoTurmaColaboradorDomainService DivisaoTurmaColaboradorDomainService
        {
            get { return this.Create<DivisaoTurmaColaboradorDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar o tipo de organização da divisão turma para associar professor
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisao turma</param>
        /// <returns>Modelo com o tipo organização definido</returns>
        public string BuscarTipoComponenteDivisaoTurma(long seqDivisaoTurma)
        {
            return DivisaoTurmaDomainService.BuscarTipoComponenteDivisaoTurma(seqDivisaoTurma).DescricaoTipoOrganizacao;
        }

        /// <summary>
        /// Buscar o tipo de organização e dados iniciais da divisão de turma para associar professor
        /// </summary>
        /// <param name="seqDivisao">Sequencial da divisão turma</param>
        /// <returns>Objeto com dados iniciais da divisão da turma</returns>
        public DivisaoTurmaColaboradorData BuscarConfiguracaoDivisaoTurmaColaborador(long seqDivisao)
        {
            var registro = DivisaoTurmaColaboradorDomainService.BuscarConfiguracaoDivisaoTurmaColaborador(seqDivisao).Transform<DivisaoTurmaColaboradorData>();
            return registro;
        }

        /// <summary>
        /// Buscar a associação de colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        /// <returns>Objeto que associa o colaborador a divisão com suas organizações</returns>
        public DivisaoTurmaColaboradorData BuscarDivisaoTurmaColaborador(long seq)
        {
            var registro = DivisaoTurmaColaboradorDomainService.BuscarDivisaoTurmaColaborador(seq).Transform<DivisaoTurmaColaboradorData>();
            return registro;
        }

        /// <summary>
        /// Remover a associação do colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        public void ExcluirDivisaoTurmaColaborador(long seq)
        {
            DivisaoTurmaColaboradorDomainService.ExcluirDivisaoTurmaColaborador(seq);
        }

        /// <summary>
        /// Grava uma colaborador associado a divisão com suas respectivas organizações
        /// </summary>
        /// <param name="divisaoColaborador">Dados da divisão turma colaboradores</param>
        public void SalvarDivisaoTurmaColaborador(DivisaoTurmaColaboradorData divisaoColaborador)
        {            
            DivisaoTurmaColaboradorDomainService.SalvarDivisaoTurmaColaborador(divisaoColaborador.Transform<DivisaoTurmaColaboradorVO>());
        }

        /// <summary>
        /// Buscar a lista de colaboradores de todas as divisões de turma do relatório
        /// </summary>
        /// <param name="seqsDivisaoTurma">Sequenciais de divisões de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as divisões de turmas</returns>
        public List<DivisaoTurmaRelatorioColaboradorData> BuscarColaboradoresDivisaoRelatorioTurma(List<long> seqsDivisaoTurma)
        {
            var registro = DivisaoTurmaColaboradorDomainService.BuscarColaboradoresDivisaoRelatorioTurma(seqsDivisaoTurma).TransformList<DivisaoTurmaRelatorioColaboradorData>();

            return registro;
        }
    }
}
