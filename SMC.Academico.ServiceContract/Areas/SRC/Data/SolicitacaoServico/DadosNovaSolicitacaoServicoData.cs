using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class DadosNovaSolicitacaoServicoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }
    }
}