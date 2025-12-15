namespace SMC.Academico.Common.Constants
{
    public class TOKEN_INGRESSANTE_DESISTENTE
    {
        /// <summary>
        /// Token do job para verificar se uma pessoa-atuação é desistente ou não prosseguiu no processo de matrícula, pois possuia algum bloqueio.
        /// Este Job irá finalizar a solicitação de acordo com as situações parametrizadas na etapa.
        /// Deverá rodar diariamente, entre 00:00 e 06:00, enquanto existirem processos de matrícula vigentes.
        /// 
        /// Nome do serviço no SAT: Ingressante desistente
        /// Tipo de agendamento: Privado
        /// Aplicação: SGA.Administrativo
        /// Token: INGRESSANTE_DESISTENTE
        /// URL do Serviço: /Academico.WebApi/api/IngressanteDesistente
        /// </summary>
        public const string INGRESSANTE_DESISTENTE = "INGRESSANTE_DESISTENTE";
    }
}
