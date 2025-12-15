using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoVisualizacaoDocumentoVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public List<PessoaAtuacaoTipoDocumentoVO> TiposDocumento { get; set; }
    }
}