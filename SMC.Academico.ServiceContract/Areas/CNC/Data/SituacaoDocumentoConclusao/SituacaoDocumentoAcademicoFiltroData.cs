using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class SituacaoDocumentoAcademicoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public string Descricao { get; set; }
        public GrupoDocumentoAcademico? GrupoDocumentoAcademico { get; set; }
    }
}
