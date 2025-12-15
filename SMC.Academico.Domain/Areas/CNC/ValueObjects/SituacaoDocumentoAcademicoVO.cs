using iTextSharp.text;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoDocumentoAcademicoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public ClasseSituacaoDocumento ClasseSituacaoDocumento { get; set; }

        public short Ordem { get; set; }
        public List<SituacaoDocumentoAcademicoGrupoDoctoVO> GruposDocumento { get; set; }
    }
}
