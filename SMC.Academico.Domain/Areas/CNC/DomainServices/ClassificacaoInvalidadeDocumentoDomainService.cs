using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CNC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CNC.DomainServices
{
    public class ClassificacaoInvalidadeDocumentoDomainService : AcademicoContextDomain<ClassificacaoInvalidadeDocumento>
    {
        public List<SMCDatasourceItem> BuscarDadosSelectClassificacaoInvalidadeDocumento(TipoInvalidade? tipoInvalidade)
        {
            var seqInstituicaoEnsinoLogada = GetDataFilter(FILTER.INSTITUICAO_ENSINO).FirstOrDefault();

            var spec = new ClassificacaoInvalidadeDocumentoFilterSpecification()
            {
                SeqInstituicaoEnsino = seqInstituicaoEnsinoLogada,
                Ativo = true,
                TipoInvalidade = tipoInvalidade
            };
            var lista = this.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).OrderBy(o => o.Descricao).ToList();
            return lista;
        }

        public string BuscarClassificacaoInvalidadeDocumentoXSD(long seq)
        {
            var spec = new ClassificacaoInvalidadeDocumentoFilterSpecification() { Seq = seq };
            var retorno = this.SearchProjectionByKey(spec, x => new { x.DescricaoXSD });

            if (retorno == null)
                throw new ClassificacaoInvalidadeDocumentoNaoLocalizadoException();

            return retorno.DescricaoXSD;
        }

        public long BuscarSeqClassificacaoInvalidadeDocumentoPorToken(string token)
        {
            var spec = new ClassificacaoInvalidadeDocumentoFilterSpecification() { Token = token };
            var retorno = this.SearchProjectionByKey(spec, x => new { x.Seq });

            if (retorno == null)
                throw new ClassificacaoInvalidadeDocumentoNaoLocalizadoException();

            return retorno.Seq;
        }
    }
}
