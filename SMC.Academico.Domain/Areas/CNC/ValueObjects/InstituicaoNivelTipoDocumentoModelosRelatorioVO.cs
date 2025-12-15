using SMC.Academico.Common.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InstituicaoNivelTipoDocumentoModelosRelatorioVO : ISMCMappable, ISMCSeq
    {

        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        public long SeqArquivoModelo { get; set; }

        public Linguagem Idioma { get; set; }

        public SMCUploadFile ArquivoModelo { get; set; }

    }
}
