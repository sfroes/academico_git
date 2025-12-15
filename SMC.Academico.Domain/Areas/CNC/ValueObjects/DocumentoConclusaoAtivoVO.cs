using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DocumentoConclusaoAtivoVO : ISMCMappable
    {
        public GrupoDocumentoAcademico? GrupoDocumentoAcademico { get; set; }
        public long SeqPessoaAtuacao { get; set; }
        public List<DocumentosConclusaoVO> DocumentosConclusao { get; set; }
    }

    public class DocumentosConclusaoVO : ISMCMappable
    {
        public List<DocumentoConclusaoFormacaoVO> FormacoesEspecificas { get; set; }
    }

    public class DocumentoConclusaoFormacaoVO : ISMCMappable
    {
        public string DescricaoDocumentoConclusao { get; set; }
        public long? SeqGrauAcademico { get; set; }
        public long? SeqTitulacao { get; set; }
    }
}
