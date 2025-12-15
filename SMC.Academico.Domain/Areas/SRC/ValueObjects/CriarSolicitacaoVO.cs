using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class CriarSolicitacaoVO : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public OrigemSolicitacaoServico Origem { get; set; }

        public bool SalvarMensagemLinhaTempo { get; set; }
    }
}