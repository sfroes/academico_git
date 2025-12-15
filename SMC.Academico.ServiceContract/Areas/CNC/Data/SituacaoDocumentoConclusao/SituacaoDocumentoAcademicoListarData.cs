using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class SituacaoDocumentoAcademicoListarData : ISMCMappable
    {
        public long Seq { get; set; }
        public string Descricao { get; set; }
        public List<string> ListaGruposDocumento { get; set; }
    }
}
