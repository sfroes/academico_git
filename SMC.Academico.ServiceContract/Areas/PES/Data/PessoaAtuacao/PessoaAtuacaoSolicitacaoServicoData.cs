using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoSolicitacaoServicoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqTipoDocumento { get; set; }

        public string DescricaoSolicitacaoServico { get; set; }

        public List<PessoaAtuacaoAnexoData> Anexos { get; set; }
    }
}