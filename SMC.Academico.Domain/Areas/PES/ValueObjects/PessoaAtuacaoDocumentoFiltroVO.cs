using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoDocumentoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }
        public long? SeqTipoDocumento { get; set; }
        public long[] ListaSeqsTipoDocumento { get; set; }
        public bool? EntreguePorSolicitacao { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public string DescricaoSolicitacaoServico { get; set; }

    }
}
