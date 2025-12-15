using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Framework.Model;
using SMC.Academico.Common.Areas.CNC.Constants;
using System.Collections.Generic;
using System.Linq;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class TipoApostilamentoDomainService : AcademicoContextDomain<TipoApostilamento>
    {
        public TipoApostilamentoVO BuscarTipoApostilamento(long seq)
        {
            return this.SearchProjectionByKey(new SMCSeqSpecification<TipoApostilamento>(seq), x => new TipoApostilamentoVO
            {
                Seq = x.Seq,
                SeqInstituicaoEnsino = x.SeqInstituicaoEnsino,
                Descricao = x.Descricao,
                Token = x.Token,
                Ativo = x.Ativo
            });
        }

        public List<SMCDatasourceItem> BuscarTiposApostilamentoSelect()
        {
            var spec = new TipoApostilamentoFilterSpecification()
            {
                Ativo = true
            };

            var result = this.SearchBySpecification(spec);

            return result.OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }

        public List<SMCDatasourceItem> BuscarTiposApostilamentoSemTokenFormacaoSelect()
        {
            var spec = new TipoApostilamentoFilterSpecification()
            {
                TokenDiferente = TOKEN_TIPO_APOSTILAMENTO.NOVA_FORMACAO_ALUNO,
                Ativo = true
            };

            var result = this.SearchBySpecification(spec);

            return result.OrderBy(o => o.Descricao).TransformList<SMCDatasourceItem>();
        }
    }
}
