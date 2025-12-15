using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class EfetivacaoMatriculaVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public string TokenServico { get; set; }
    }
}