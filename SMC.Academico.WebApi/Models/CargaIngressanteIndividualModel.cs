using SMC.Framework.Jobs;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;

namespace SMC.Academico.WebApi.Models
{
    public class CargaIngressanteIndividualModel: ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqChamada { get; set; }

        public long SeqEntidadeInstituicao { get; set; }

        public string SeqSolicitante { get; set; }

        public string NomeSolicitante { get; set; }

        public PessoaIntegracaoData Inscrito { get; set; }
    }
}