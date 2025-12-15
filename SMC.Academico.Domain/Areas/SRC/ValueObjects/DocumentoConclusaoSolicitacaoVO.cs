using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DocumentoConclusaoSolicitacaoVO : ISMCMappable
    {
        public long SeqDocumentoAcademico { get; set; }

        public string DescricaoCurso { get; set; }

        public List<string> DescricoesFormacaoEspecifica { get; set; }

        public string DescricaoGrauAcademico { get; set; }

        public string DescricaoTitulacao { get; set; }

        public string DescricaoTipoDocumentoAcademico { get; set; }

        public string DescricaoSituacaoDocumentoAcademicoAtual { get; set; }

        public short NumeroViaDocumento { get; set; }

        public long? SeqDocumentoGAD { get; set; }

        public string UrlDocumentoGAD { get; set; }

        public string TokenTipoDocumentoAcademico { get; set; }
    }
}
