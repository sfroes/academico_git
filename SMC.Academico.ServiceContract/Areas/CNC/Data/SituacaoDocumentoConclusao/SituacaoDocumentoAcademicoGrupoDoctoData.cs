using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class SituacaoDocumentoAcademicoGrupoDoctoData : ISMCMappable
    {
        public long Seq { get; set; }

        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }

    }
}
