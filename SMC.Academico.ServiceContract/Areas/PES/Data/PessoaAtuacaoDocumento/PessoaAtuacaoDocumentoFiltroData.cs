using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoDocumentoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqPessoaAtuacao { get; set; }
        public long? SeqTipoDocumento { get; set; }
        public bool? EntreguePorSolicitacao { get; set; }
        public long? SeqServico { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public string DescricaoSolicitacaoServico { get; set; }
    }
}
