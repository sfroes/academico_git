using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class CriarSolicitacaoData : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public OrigemSolicitacaoServico Origem { get; set; }

        public bool SalvarMensagemLinhaTempo { get; set; }
    }
}