using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PlanoEstudoAlterarVO : ISMCMappable
    {
        public long SeqPlanoEstudo { get; set; }

        public long SeqPlanoEstudoItemRemover { get; set; }
    }
}