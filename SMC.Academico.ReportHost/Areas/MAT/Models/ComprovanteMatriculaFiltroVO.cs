using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class ComprovanteMatriculaFiltroVO : ISMCMappable
    {
        public long SeqSolicitacaoMatricula { get; set; }

        public long SeqIngressante { get; set; }

        public long? SeqProcessoEtapa { get; set; }
    }
}