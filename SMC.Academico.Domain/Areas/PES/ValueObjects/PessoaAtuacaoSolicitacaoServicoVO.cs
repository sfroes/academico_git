using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoSolicitacaoServicoVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoSolicitacaoServico { get; set; }

        public List<PessoaAtuacaoAnexoVO> Anexos { get; set; }
    }
}