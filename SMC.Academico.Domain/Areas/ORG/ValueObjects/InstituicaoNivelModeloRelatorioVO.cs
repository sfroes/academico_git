using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoNivelModeloRelatorioVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivel { get; set; }

        public string DescricaoInstituicaoNivel { get; set; }

        public ModeloRelatorio ModeloRelatorio { get; set; }

        public SMCUploadFile ArquivoModelo { get; set; }

        public long SeqArquivoModelo { get; set; }
    }
}
