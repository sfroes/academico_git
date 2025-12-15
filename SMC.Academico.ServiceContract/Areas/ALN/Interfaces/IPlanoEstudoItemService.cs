using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IPlanoEstudoItemService : ISMCService
    {
        /// <summary>
        /// Busca as atividades acadêmicas de um aluno em um ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno para recuperar as atividades acadêmicas</param>
        /// <param name="seqCicloLetivo">Ciclo letivo para recuperar as atividades acadêmicas</param>
        /// <returns>Lista de atividades acadêmicas de um aluno / ciclo letivo</returns>
        [OperationContract]
        List<PlanoEstudoItemData> BuscarAtividadesAcademicasAluno(long seqAluno, long? seqCicloLetivo);

        /// <summary>
        /// Buscar itens do plano de estudo atual por aluno
        /// </summary>
        /// <param name="seqAluno"></param>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Retorna SeqDivisaoTurma e SeqConfiguracaoComponente dos itens do plano</returns>
        List<PlanoEstudoItemData> BuscarSeqsItensPlanoEstudoAtualAluno(long seqAluno, long? seqCicloLetivo);

        /// <summary>
        /// Buscar turmas dos componentes currículares do plano de estudo item para listar na integralização.
        /// </summary>
        /// <param name="seqsAlunoHistorico">Lista de sequenciais do aluno historico atual</param>
        /// <param name="seqsCicloLetivo">Lista de sequencial do ciclo letivo do historico escolar</param>
        /// <returns>Lista dos itens com códigos da turma</returns>
        List<HistoricoEscolarTurmaData> BuscarIntegralizacaoComponentePlanoEstudoTurma(List<long> seqsAlunoHistorico, List<long> seqsCicloLetivo);

        /// <summary>
        /// Busca o plano de estudo item da configuração em curso para montar a modal de histórico escolar
        /// </summary>
        /// <param name="seqPlanoEstudo">Sequencial do plano de estudo em curso</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração de componente</param>
        /// <returns>Dados do plano de estudo item do aluno</returns>
        List<HistoricoEscolarListaData> BuscarPlanoEstudoItemIntegralizacaoConfiguracao(long seqPlanoEstudo, long? seqComponenteCurricular, long? seqConfiguracaoComponente);
    }
}