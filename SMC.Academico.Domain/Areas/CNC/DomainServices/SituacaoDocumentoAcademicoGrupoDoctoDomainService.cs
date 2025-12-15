using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Framework;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class SituacaoDocumentoAcademicoGrupoDoctoDomainService : AcademicoContextDomain<SituacaoDocumentoAcademicoGrupoDocto>
    {
        public List<SMCDatasourceItem> BuscarGruposDocumentoPorSituacaoDocumentoAcademicoSelect(long seqSituacaoDoc)
        {
            var spec = new SituacaoDocumentoAcademicoGrupoDoctoFilterSpecification() { SeqSituacaoDocumento = seqSituacaoDoc };

            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.Seq,
                Descricao = x.GrupoDocumentoAcademico.SMCGetDescription()
            }).ToList();

            return lista;
        }
    }
}
