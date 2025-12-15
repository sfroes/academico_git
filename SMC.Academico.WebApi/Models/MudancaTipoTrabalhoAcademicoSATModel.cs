using SMC.Framework.Jobs;

namespace SMC.Academico.WebApi.Models
{
    public class MudancaTipoTrabalhoAcademicoSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string DataProcessamento { get; set; }
    }
}