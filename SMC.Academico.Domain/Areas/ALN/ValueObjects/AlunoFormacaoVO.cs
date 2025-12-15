using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoFormacaoVO : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public string TokenTipoFormacaoEspecifica { get; set; }
    }
}
