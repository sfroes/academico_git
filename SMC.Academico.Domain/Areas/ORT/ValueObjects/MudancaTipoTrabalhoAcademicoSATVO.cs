using SMC.Framework.Jobs;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class MudancaTipoTrabalhoAcademicoSATVO : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string DataProcessamento { get; set; }
    }
}
