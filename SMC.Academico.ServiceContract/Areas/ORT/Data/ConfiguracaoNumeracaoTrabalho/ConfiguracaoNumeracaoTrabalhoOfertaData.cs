using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class ConfiguracaoNumeracaoTrabalhoOfertaData : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqConfiguracaoNumeracaoTrabalho { get; set; }
        public long SeqCursoOfertaLocalidade { get; set; }
        public long SeqEntidadeResponsavel { get; set; }
    }
}
