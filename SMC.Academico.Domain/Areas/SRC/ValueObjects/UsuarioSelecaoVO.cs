using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class UsuarioSelecaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public int? CodigoPessoa { get; set; }
    }
}