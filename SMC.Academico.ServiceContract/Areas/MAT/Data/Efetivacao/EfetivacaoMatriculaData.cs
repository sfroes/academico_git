using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data.Efetivacao
{
    public class EfetivacaoMatriculaData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }
        
        public string TokenServico { get; set; }
    }
}