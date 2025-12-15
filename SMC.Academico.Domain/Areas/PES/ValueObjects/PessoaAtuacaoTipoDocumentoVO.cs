using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoTipoDocumentoVO : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public List<PessoaAtuacaoSolicitacaoServicoVO> Solicitacoes { get; set; }
    }
}