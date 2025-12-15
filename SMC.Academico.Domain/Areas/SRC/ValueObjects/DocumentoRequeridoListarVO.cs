using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DocumentoRequeridoListarVO : ISMCMappable
    {       
        public long Seq { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public bool Obrigatorio { get; set; }

        public bool PermiteUploadArquivo { get; set; }

        public bool ObrigatorioUpload { get; set; }

        public VersaoDocumento VersaoDocumento { get; set; }

        public SituacaoEtapa SituacaoEtapa { get; set; }
    }
}
