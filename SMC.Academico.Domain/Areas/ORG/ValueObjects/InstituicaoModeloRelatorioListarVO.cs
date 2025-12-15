using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoModeloRelatorioListarVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoInstituicaoEnsino { get; set; }

        public ModeloRelatorio ModeloRelatorio { get; set; }

        public Idioma? Idioma { get; set; }

        public SMCUploadFile ArquivoModelo { get; set; }
    }
}
