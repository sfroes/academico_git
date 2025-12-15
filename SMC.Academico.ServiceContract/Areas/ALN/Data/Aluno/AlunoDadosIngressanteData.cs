using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AlunoDadosIngressanteData : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqSolicitacaoMatricula { get; set; }
    }
}