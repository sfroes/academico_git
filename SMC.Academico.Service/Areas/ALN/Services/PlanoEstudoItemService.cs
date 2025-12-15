using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ALN.Services
{
    public class PlanoEstudoItemService : SMCServiceBase, IPlanoEstudoItemService
    {
        #region [ DomainService ]

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as atividades acadêmicas de um aluno em um ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno para recuperar as atividades acadêmicas</param>
        /// <param name="seqCicloLetivo">Ciclo letivo para recuperar as atividades acadêmicas</param>
        /// <returns>Lista de atividades acadêmicas de um aluno / ciclo letivo</returns>
        public List<PlanoEstudoItemData> BuscarAtividadesAcademicasAluno(long seqAluno, long? seqCicloLetivo)
        {
            return PlanoEstudoItemDomainService.BuscarSolicitacaoMatriculaAtividadesItens(seqAluno, seqCicloLetivo).TransformList<PlanoEstudoItemData>();
        }

        /// <summary>
        /// Buscar itens do plano de estudo atual por aluno
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Retorna SeqDivisaoTurma e SeqConfiguracaoComponente dos itens do plano</returns>
        public List<PlanoEstudoItemData> BuscarSeqsItensPlanoEstudoAtualAluno(long seqAluno, long? seqCicloLetivo)
        {
            return PlanoEstudoItemDomainService.BuscarSeqsItensPlanoEstudoAtualAluno(seqAluno, seqCicloLetivo).TransformList<PlanoEstudoItemData>();
        }

        /// <summary>
        /// Buscar turmas dos componentes currículares do plano de estudo item para listar na integralização.
        /// </summary>
        /// <param name="seqsAlunoHistorico">Lista de sequenciais do aluno historico atual</param>
        /// <param name="seqsCicloLetivo">Lista de sequencial do ciclo letivo do historico escolar</param>
        /// <returns>Lista dos itens com códigos da turma</returns>
        public List<HistoricoEscolarTurmaData> BuscarIntegralizacaoComponentePlanoEstudoTurma(List<long> seqsAlunoHistorico, List<long> seqsCicloLetivo)
        {
            return PlanoEstudoItemDomainService.BuscarIntegralizacaoComponentePlanoEstudoTurma(seqsAlunoHistorico, seqsCicloLetivo).TransformList<HistoricoEscolarTurmaData>();
        }

        /// <summary>
        /// Busca o plano de estudo item da configuração em curso para montar a modal de histórico escolar
        /// </summary>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo em curso</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>Dados do plano de estudo item do aluno</returns>
        public List<HistoricoEscolarListaData> BuscarPlanoEstudoItemIntegralizacaoConfiguracao(long seqPlanoEstudo, long? seqComponenteCurricular, long? seqConfiguracaoComponente)
        {
            return PlanoEstudoItemDomainService.BuscarPlanoEstudoItemIntegralizacaoConfiguracao(seqPlanoEstudo, seqComponenteCurricular, seqConfiguracaoComponente).TransformList<HistoricoEscolarListaData>();
        }
    }
}