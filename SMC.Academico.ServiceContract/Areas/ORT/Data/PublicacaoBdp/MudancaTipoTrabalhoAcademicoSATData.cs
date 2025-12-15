using SMC.Framework.Jobs;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class MudancaTipoTrabalhoAcademicoSATData : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string DataProcessamento { get; set; }
    }
}
