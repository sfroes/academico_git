using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Framework;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class InstituicaoNivelTipoProcessoSeletivoDomainService : AcademicoContextDomain<InstituicaoNivelTipoProcessoSeletivo>
    {
        public List<SMCDatasourceItem> BuscarTiposProcessoSeletivoPorNivelEnsino(List<long> seqsNivelEnsino)
        {
            if (seqsNivelEnsino == null)
                return new List<SMCDatasourceItem>();

            var spec = new TipoProcessoSeletivoPorNivelEnsinoSpecification(seqsNivelEnsino);
            return SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.SeqTipoProcessoSeletivo,
                Descricao = x.TipoProcessoSeletivo.Descricao,
                DataAttributes = new List<SMCKeyValuePair>()
                {
                    new SMCKeyValuePair() { Key = "tipo", Value = x.TipoProcessoSeletivo.Token },
                    new SMCKeyValuePair() { Key = "ingresso-direto", Value = x.TipoProcessoSeletivo.IngressoDireto ? "true" : "false" }
                }
            }).SMCDistinct(f => f.Seq).OrderBy(o => o.Descricao).ToList();
        }
    }
}