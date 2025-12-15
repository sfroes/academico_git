using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoTipoDocumentoData : ISMCMappable
    {
        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoTipoDocumento { get; set; }

        public List<PessoaAtuacaoSolicitacaoServicoData> Solicitacoes { get; set; }
    }
}