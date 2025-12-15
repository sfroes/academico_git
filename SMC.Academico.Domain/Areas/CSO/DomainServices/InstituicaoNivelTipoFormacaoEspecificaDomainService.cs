using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.DomainServices
{
    public class InstituicaoNivelTipoFormacaoEspecificaDomainService : AcademicoContextDomain<InstituicaoNivelTipoFormacaoEspecifica>
    {
        public List<SMCDatasourceItem> BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(long seqInstituicaoNivel)
        {
            var spec = new InstituicaoNivelTipoFormacaoEspecificaFilterSpecification()
            {
                SeqInstituicaoNivel = seqInstituicaoNivel,
                PermiteEmitirDocumentoConclusao = true,
                Ativo = true
            };

            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem()
            {
                Seq = x.TipoFormacaoEspecifica.Seq,
                Descricao = x.TipoFormacaoEspecifica.Descricao

            }).OrderBy(o => o.Descricao).ToList();

            return lista;
        }
    }
}