using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class SituacaoDocumentoAcademicoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public string Token { get; set; }

        public ClasseSituacaoDocumento ClasseSituacaoDocumento { get; set; }

        public short Ordem { get; set; }
        public List<SituacaoDocumentoAcademicoGrupoDoctoData> GruposDocumento { get; set; }
    }
}
