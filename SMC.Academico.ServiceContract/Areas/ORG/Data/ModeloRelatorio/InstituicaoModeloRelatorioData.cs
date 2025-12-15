using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoModeloRelatorioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public ModeloRelatorio ModeloRelatorio { get; set; }

        public Idioma Idioma { get; set; }

        public SMCUploadFile ArquivoModelo { get; set; }

        public long SeqArquivoModelo { get; set; }
    }
}
