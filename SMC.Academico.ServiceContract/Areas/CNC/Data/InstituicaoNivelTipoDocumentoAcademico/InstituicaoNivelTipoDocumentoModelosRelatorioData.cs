using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class InstituicaoNivelTipoDocumentoModelosRelatorioData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        public long SeqArquivoModelo { get; set; }

        public Linguagem Idioma { get; set; }

        public SMCUploadFile ArquivoModelo { get; set; }
    }
}
