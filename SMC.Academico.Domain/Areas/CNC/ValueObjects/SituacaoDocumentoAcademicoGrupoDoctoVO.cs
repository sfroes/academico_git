using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoDocumentoAcademicoGrupoDoctoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }
        
    }
}
