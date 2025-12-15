using SMC.Framework.Mapper;

namespace SMC.Academico.WebApi.Models
{
    public class ProcessamentoPlanoEstudoSATModel : ISMCMappable
    {        
        /// <summary>
        /// Sequencial do histórico do agendamento do SAT.
        /// </summary>
        public long SeqHistoricoAgendamento { get; set; }

        /// <summary>
        /// Lista de sequenciais do processos de matriculas de cada ingressante separado por ;
        /// </summary>
        public string SeqsSolicitacoesServicos { get; set; }

        /// <summary>
        /// Sequencial do processo etapa
        /// </summary>
        public long SeqProcessoEtapa { get; set; }
    }
}