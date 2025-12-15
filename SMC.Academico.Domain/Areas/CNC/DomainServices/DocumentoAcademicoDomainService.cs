using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class DocumentoAcademicoDomainService : AcademicoContextDomain<DocumentoAcademico>
    {
        public GrupoDocumentoAcademico? BuscarGrupoDocumentoAcademico(long seqDocumentoAcademicoGAD)
        {
            var spec = new DocumentoAcademicoFilterSpecification { SeqDocumentoGAD = seqDocumentoAcademicoGAD };
            var documentoAcademico = this.SearchByKey(spec, s => s.TipoDocumentoAcademico);

            if (documentoAcademico == null)
                return null;

            return documentoAcademico.TipoDocumentoAcademico.GrupoDocumentoAcademico;
        }
    }
}
